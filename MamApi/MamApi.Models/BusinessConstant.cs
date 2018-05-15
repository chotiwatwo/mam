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

    }
}
