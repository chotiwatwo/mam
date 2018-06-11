using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamApi.Common
{
    public class Utility
    {
        public void CheckStringIsEmpty(ErrorMessage errMsg, string msg, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                errMsg.Add(msg);
            }
        }
    }
}
