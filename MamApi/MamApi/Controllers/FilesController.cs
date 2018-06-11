//2018.06.08 NW Modify add "CreditCheckDownloadFile", "ShareDownloadFile" support request download file.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Common;
using MamApi.Data.Repositories;
using MamApi.Models;
using MamApi.Models.Resources;
using MamApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Serilog;

namespace MamApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/files")]
    public class FilesController : MAMController
    {
        private readonly IFileAttachmentService _fileAttachmentService;

        public FilesController(IFileAttachmentService fileAttachmentService)
        {
            _fileAttachmentService = fileAttachmentService;
        }

        #region Private Method
        private bool IsAttachmentInfoValid(string appId, int customerId, string attachmentType,
            out string errorText)
        {
            errorText = string.Empty;

            if (String.IsNullOrEmpty(appId))
            {
                errorText = $"เลขที่ใบคำขอ เป็นค่าว่าง (appId : {appId} )";

                return false;
            }

            if (customerId <= 0)
            {
                errorText = $"รหัสลูกค้า ไม่ถูกต้อง (customerId : {customerId} )";

                return false;
            }

            if (String.IsNullOrEmpty(attachmentType))
            {
                errorText = $"ประเภทเอกสารที่แนบ เป็นค่าว่าง (attachmentType : {attachmentType} )";

                return false;
            }

            return true;
        }

        private bool IsUploadedFileValid(IFormFile file, out string errorText)
        {
            errorText = string.Empty;

            // Check if file is uploaded or not
            if (file == null)
            {
                errorText = "ไม่พบไฟล์ที่ทำการ Upload";

                return false;
            }

            long maxUploadFileSizeBytes = _fileAttachmentService.GetMaxUploadFileSizeBytes();

            if (file.Length > maxUploadFileSizeBytes)
            {
                errorText = $"ไฟล์ที่ upload มีขนาดใหญ่เกินกำหนด (ต้องไม่เกิน { maxUploadFileSizeBytes } bytes)";

                return false;
            }

            return true;
        }

        private string GenerateFormattedFileName(string fileName, AttachmentInfo attachmentInfo)
        {
            string FileNameFormat = "{0}_{1}_{2}.{3}";//ex. App-000001_1_Consent.jpg

            string AttachmentTypeName = _fileAttachmentService.GetAttachmentTypeName(attachmentInfo.AttachmentType);
            attachmentInfo.AttachmentTypeName = AttachmentTypeName;

            string[] ArrFilename = fileName.Split('.');
            string FileExtension = ArrFilename[ArrFilename.Length - 1];

            string FormattedFileName = string.Format(FileNameFormat, attachmentInfo.AppId,
                attachmentInfo.CustomerId, AttachmentTypeName, FileExtension);

            return FormattedFileName;
        }


        #endregion

        [HttpPost("creditcheck/upload")]
        public async Task<IActionResult> CreditCheckUploadFile(string appId, int customerId, string attachmentType,
            IFormFile file)
        {
            // Validate Uploaded File
            if (!IsAttachmentInfoValid(appId, customerId, attachmentType, out string errorText))
            {
                return BadRequest(new ErrorMessage { ErrorText = errorText });
            }

            if (!IsUploadedFileValid(file, out errorText))
            {
                return BadRequest(new ErrorMessage { ErrorText = errorText });
            }

            /* Prepare to generate Format Filename to be uploaded
               Pattern ==> 
                 Path (จาก table [Parameter]) + 
                 AppId + 
                 Filename (AppId_CustId_AttachmentTypeName) + ".xxx"
               ex.  CreditChecking\9961000101\9961000101_383794_บัตรประชาชน.jpg
             */
            AttachmentInfo attachmentInfo = new AttachmentInfo
            {
                AppId = appId,
                CustomerId = customerId,
                AttachmentType = attachmentType,
                AttachmentTypeName = string.Empty // จะถูก Assign ค่ามาจาก GenerateFormattedFileName() อีกทีบรรทัดข้างล่าง
            };

            // Path (จาก table [HPCS].[Parameter])
            string attachmentPath = _fileAttachmentService.GetCreditCheckingFilePath();

            // Filename (AppId_CustId_AttachmentTypeName) + ".xxx"
            string fileName = GenerateFormattedFileName(file.FileName, attachmentInfo);

            // ถ้ามี File เดิมอยู่ ให้ลบทิ้งก่อน
            string fullPathFilename = Path.Combine(attachmentPath, appId, fileName);
            if (System.IO.File.Exists(fullPathFilename))
                System.IO.File.Delete(fullPathFilename);

            // สร้าง Directory ที่เป็น App. Id เพื่อเก็บไฟล์ upload ตามเลข App.
            FileInfo FileInfo = new FileInfo(fullPathFilename);
            if (!FileInfo.Directory.Exists)
                FileInfo.Directory.Create();

            // Save file ที่ upload มาเก็บลง Server Disk
            using (var stream = new FileStream(fullPathFilename, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Log.Information("GetDisplayCreditCheckingFilePath attachmentPath : {@attachmentPath} , " +
                "AppId : {@attachmentInfo.AppId} , " +
                "fileName : {@fileName} ", attachmentPath, attachmentInfo.AppId, fileName);

            string displayFilePath = string.Empty;
            try
            {
                // สร้าง object สำหรับ return กลับไป ที่ client เพื่อใช้ส่งกลับมาอีกครั้งเพื่อ update ข้อมูล Attachment ลง Database
                displayFilePath = _fileAttachmentService.GetDisplayCreditCheckingFilePath
                    (
                        attachmentPath,
                        attachmentInfo.AppId,
                        fileName
                    );
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetDisplayCreditCheckingFilePath : {@ex} ", ex);
            }

            AttachmentUploadResource uploadAttach = new AttachmentUploadResource
            {
                AppId = attachmentInfo.AppId,
                CustomerId = attachmentInfo.CustomerId,
                AttachmentType = attachmentInfo.AttachmentType,
                AttachmentTypeName = attachmentInfo.AttachmentTypeName,
                Name = fileName,
                DisplayFilePath = displayFilePath  // !!! Error but upload completed 
            };

            return Ok(uploadAttach);
        }

        /**
         * Download file from share file server and send to client request.
         * */
        [HttpPost("creditcheck/download")]
        public IActionResult CreditCheckDownloadFile([FromBody] AttachmentDownloadResource attchDownload)
        {
            this.Utility.CheckStringIsEmpty(this.ErrorMessage, string.Format("Category invalid.: {0}", attchDownload.Category), attchDownload.Category);
            this.Utility.CheckStringIsEmpty(this.ErrorMessage, string.Format("Application No invalid.: {0}", attchDownload.AppId), attchDownload.AppId);
            this.Utility.CheckStringIsEmpty(this.ErrorMessage, string.Format("Display name invalid.: {0}", attchDownload.DisplayFilePath), attchDownload.DisplayFilePath);

            if (!this.ErrorMessage.IsError)
            {
                string url = $"{AppSettingJsonConfiguration["Server:FileServer"]}";
                string urlMethod = $"{AppSettingJsonConfiguration["Server:DownloadMethod"]}";

                var client = new RestClient(new Uri(url));

                var request = new RestRequest(urlMethod, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new
                {
                    AppId = "testAc012",
                    ategory = "cat1",
                    CustomerId = "00001",
                    DisplayFilePath = "xxxxx"

                });

                client.AddDefaultHeader("Content-Type", "application/json");
                string token = ((FrameRequestHeaders)this.Request.Headers).HeaderAuthorization;
                client.AddDefaultHeader("Authorization", token);

                var restRep = client.Execute(request);

                var resp = restRep.Content;

                return Content(resp);
            }
            else
            {
                return Content(this.ErrorMessage.ToString());
            }
        }

        /**
         * Download file from local server and send to other request
         * */
        [HttpPost("server/download")]
        public async Task<IActionResult> ShareDownloadFile([FromBody] AttachmentDownloadResource attchDownload)
        {
            string fileName;
            string fileLoc = string.Empty;
            var msmstr = new MemoryStream();
            try
            {
                fileName = "Fiesta_1.6L_PowerShift.pdf";

                if (String.IsNullOrEmpty(fileName))
                {
                    this.ErrorMessage.Add("File name invalid.");
                }

                var path = Path.Combine(@"D:\Doument\Other\");
                fileLoc = string.Format("{0}{1}", path, fileName);

                using (var stream = new FileStream(fileLoc, FileMode.Open))
                {
                    await stream.CopyToAsync(msmstr);
                }

            }
            catch (Exception e)
            {
                this.ErrorMessage.Add(e.Message);
            }


            if (this.ErrorMessage.IsError)
            {
                return Content(this.ErrorMessage.ToString());
            }
            else
            {
                msmstr.Position = 0;
                return File(msmstr, this.FileContentType.GetContentType(fileLoc), Path.GetFileName(fileLoc));
            }
        }




    }
}