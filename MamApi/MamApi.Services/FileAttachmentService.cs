using MamApi.Data.Repositories;
using MamApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public class FileAttachmentService : IFileAttachmentService
    {
        private readonly IConfiguration _config;
        private readonly IMasterService _masterService;
        private readonly IParameterRepository _paramRepo;

        public FileAttachmentService(IConfiguration config, 
            IMasterService masterService, 
            IParameterRepository paramRepo)
        {
            _config = config;
            _masterService = masterService;
            _paramRepo = paramRepo;
        }

        public string GetFullFilePathToViewFromConfigurationFile()
        {
            return _config["HPCS:Environment:FilePath"];
        }

        public string GetAttachmentTypeName(string attachmentTypeId)
        {
            string attachmentTypeName =
                _masterService.GetMasterInfosByIdAndType(attachmentTypeId,
                    "AttachmentType", BusinessConstant.StatusActive)
                    .Select(a => a.ThaiName)
                    .SingleOrDefault();

            return attachmentTypeName;
        }

        public string GetCreditCheckingFilePath()
        {
            string creditCheckingFilePath = _paramRepo.GetParameterValue(p => p.ParameterId == "PathCredit");

            return creditCheckingFilePath;
        }

        public long GetMaxUploadFileSizeBytes()
        {
            string maxFileSizeFromDB = _paramRepo.GetParameterValue(p => p.ParameterId == "FileSize");

            Int64.TryParse(maxFileSizeFromDB, out long maxUploadFileSizeBytes);

            return maxUploadFileSizeBytes;
        }

        public string GetDisplayCreditCheckingFilePath(string attachmentPath, string appId, string fileName)
        {
            string CreditCheckingDir = Path.GetDirectoryName(attachmentPath).Split('\\').Last();

            string displayCreditCheckingFilePath = Path.Combine(
                GetFullFilePathToViewFromConfigurationFile(),
                CreditCheckingDir,
                appId,
                fileName);

            displayCreditCheckingFilePath = displayCreditCheckingFilePath.Replace('\\', '/');

            return displayCreditCheckingFilePath;
        }
    }
}
