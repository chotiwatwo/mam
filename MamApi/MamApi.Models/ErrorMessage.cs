using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    
    public class ErrorMessage
    {
        private List<string> _errMsgs;

        public ErrorMessage()
        {
            this._errMsgs = new List<string>();
        }
        
        public List<string> ErrorMessages { get { return this._errMsgs; } set { this._errMsgs = value; } }
        public void Add(string message)
        {
            this._errMsgs = this._errMsgs == null ? new List<string>() : this._errMsgs;
            this._errMsgs.Add(message);
        }
        public bool IsError
        {
            get {

                return !string.IsNullOrEmpty(this.ErrorText) || this._errMsgs.Count != 0 ? true : false;
            }
        }
        public string ErrorText { get; set; }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            if (this._errMsgs.Count != 0)
            {
                this._errMsgs.ForEach(c =>
                {
                    res.AppendLine(c);
                });
            }
            if (!string.IsNullOrEmpty(this.ErrorText))
            {
                res.Append(this.ErrorText);
            }
            return res.ToString();
        }
        //public object ErrorObject { get; set; }
    }
}
