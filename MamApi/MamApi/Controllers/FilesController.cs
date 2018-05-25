using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Data.Repositories;
using MamApi.Models;
using MamApi.Models.Resources;
using MamApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/files")]
    public class FilesController : Controller
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

            // สร้าง object สำหรับ return กลับไป ที่ client เพื่อใช้ส่งกลับมาอีกครั้งเพื่อ update ข้อมูล Attachment ลง Database
            string displayFilePath = _fileAttachmentService.GetDisplayCreditCheckingFilePath
                (
                    attachmentPath,
                    attachmentInfo.AppId,
                    fileName
                );

            AttachmentUploadResource uploadAttach = new AttachmentUploadResource
            {
                AppId = attachmentInfo.AppId,
                CustomerId = attachmentInfo.CustomerId,
                AttachmentType = attachmentInfo.AttachmentType,
                AttachmentTypeName = attachmentInfo.AttachmentTypeName,
                Name = fileName,
                DisplayFilePath = displayFilePath
            };

            return Ok(uploadAttach);
        }
    }
}