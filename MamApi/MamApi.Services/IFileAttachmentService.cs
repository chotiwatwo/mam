using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public interface IFileAttachmentService
    {
        string GetCreditCheckingFilePath();

        string GetAttachmentTypeName(string attachmentTypeId);

        string GetFullFilePathToViewFromConfigurationFile();

        long GetMaxUploadFileSizeBytes();

        string GetDisplayCreditCheckingFilePath(string attachmentPath, string appId, string fileName);
    }
}
