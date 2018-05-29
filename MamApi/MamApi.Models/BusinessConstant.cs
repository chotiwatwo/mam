using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public static class BusinessConstant
    {
        // General
        public const string StatusActive = "A";
        public const string FlagYes = "Y";
        public const string NonSelectedDropDownIndex = "0";


        // MKT_Customer
        public const string CustomerNewType = "N";  // ลูกค้าใหม่
        public const string CustomerOldType = "O";  // ลูกค้าเก่า

        public const string CustomerAppCusTypePersonal = "P";  // บุคคลธรรมดา

        public const string CustomerTypePurchase = "P";  // ผู้เช่าซื้อ
        public const string CustomerTypeGuarantor = "G";  // ผู้ค้ำประกัน
        public const string CustomerTypeSpouse = "S";  // คู่สมรส
        public const string CustomerTypeGuarantorExclude = "X";  // ผู้ค้ำประกัน ที่ไม่รวมรายได้

        // MKT_Application
        public const string AppStatusMarketingInitial = "P";   // MKT.บันทึกคำขอสินเชื่อเบื้องต้น

        // Attachment Type
        public const string AttachmentTypeConsent = "C";               // Consent
        public const string AttachmentTypeFrontApplicationForm = "F";  // คำเสนอขอเช่าซื้อ ด้านหน้า
        public const string AttachmentTypeIndividual = "I";            // บัตรประชาชน
        public const string AttachmentTypeMemoInternal = "O";          // Memo Internal
        public const string AttachmentTypeConsentModel = "T";          // Consent Model

        public enum AddressType
        {
            // เวลา Save ลง MKT_Address.MKT_Address_ID จะเป็นตามนี้
            //1	Address
            //2	Current
            //3	Debt
            //4	Document
            //5	Office
            //6	Other
            //7	Person

            Address,
            Current,
            Debt,
            Document,
            Office,
            Other,
            Person
        }
    }
}
