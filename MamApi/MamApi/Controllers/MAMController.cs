using MamApi.Common;
using MamApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MamApi.Controllers
{
    public class MAMController : Controller
    {
        private Utility _utility;
        private FileContentType _fileContentType;
        private ErrorMessage _errMsg;
        private IConfigurationRoot _configuration;

        public MAMController() : base()
        {
            this._utility = new Utility();
            this._fileContentType = new FileContentType();
            this._errMsg = new ErrorMessage();
        }

        public IConfiguration AppSettingJsonConfiguration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
                this._configuration = builder.Build();
                return this._configuration;
            }
        }

        public Utility Utility { get { return this._utility; } }
        public FileContentType FileContentType { get { return this._fileContentType; } }

        public ErrorMessage ErrorMessage { get { return this._errMsg; } }
    }
}
