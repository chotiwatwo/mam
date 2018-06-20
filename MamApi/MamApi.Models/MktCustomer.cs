using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MktCustomer
    {
        [Key]
        [Column("Customer_ID")]  // รหัสลูกค้า
        public int Id { get; set; }

        [Required]
        [Column("Customer_AppID")]  // เลขที่ใบคำขอ
        public string AppId { get; set; }

        [Column("Customer_AppCustType")] // ประเภทบุคคล (P : บุคคลธรรมดา, J : นิติบุคคล) (ถ้าจาก MAM/HPCS จะเป็น 'P' เสมอ เพราะนิติบุคคลไปอยู่ในระบบ HPPRO)
        public string AppCustomerType { get; set; }

        [Column("Customer_NewOldType")]  // ประเภทลูกค้า (N : ลูกค้าใหม่, O : ลูกค้าเก่า)
        public string NewOrOld { get; set; }

        [Column("Customer_Type")]   // สถานะลูกค้า สำหรับใบคำขอนี้ (P : ผู้เช่าซื้่อ , G : ผู้ค้ำประกัน , S : คู่สมรส , X : เป็น record ที่ถูก mark ว่าลบทิ้งแล้ว ไม่เอาข้อมูลนี้)
        public string CustomerType { get; set; }

        [Column("Customer_SpouseID")]  // รหัสลูกค้า ของคู่สมรส
        public int? SpouseId { get; set; }

        [Column("Customer_Prefix")]   // คำนำหน้า  (Master table => MasterInfo.MasterInfo_MasterInfoType = 'title')
        public string TitleId { get; set; }

        [Column("Customer_FNameTH")]  // ชื่อไทย
        public string FirstNameThai { get; set; }

        [Column("Customer_MNameTH")]  // ชื่อกลางไทย
        public string MiddleNameThai { get; set; }

        [Column("Customer_LNameTH")] // นามสกุลไทย
        public string LastNameThai { get; set; }

        [Column("Customer_FNameEN")]
        public string FirstNameEng { get; set; }

        [Column("Customer_MNameEN")]
        public string MiddleNameEng { get; set; }

        [Column("Customer_LNameEN")]
        public string LastNameEng { get; set; }

        [Column("Customer_BirthDate")]  // วัน เดือน ปี เกิด
        public DateTime? BirthDate { get; set; }

        [Column("Customer_Age")]  // อายุ 
        public byte? Age { get; set; }

        [Column("Customer_Sex")]  // เพศ (Master table => MasterInfo.MasterInfo_MasterInfoType = 'sex')
        public string SexId { get; set; }

        [Column("Customer_Race")]  // เชื้อชาติ (Master table => MasterInfo.MasterInfo_MasterInfoType = 'race')
        public string RaceId { get; set; }
        //public Race Race { get; set; }

        [Column("Customer_StayCountry")] // ประเทศถิ่นพำนัก (Master table => MasterInfo.MasterInfo_MasterInfoType = 'Nationality')
        public string StayCountryId { get; set; }

        [Column("Customer_IncomeCountry")] //ประเทศแหล่งเงินได้ (เหมือนกับประเทศถิ่นพำนัก)
        public string IncomeCountryId { get; set; }

        [Column("Customer_CardType")] // ประเภทบัตรแสดงตน (Master table => MasterInfo.MasterInfo_MasterInfoType = 'IDType')
        public string CardTypeId { get; set; }

        [Required]
        [Column("Customer_IDCardNo")] // เลขบัตรแสดงตน
        public string IDCardNo { get; set; }

        [Column("Customer_Education")]  // วุฒิการศึกษา (Master table => MasterInfo.MasterInfo_MasterInfoType = 'EducationType')
        public string Education { get; set; }

        [Column("Customer_MarriedStatus")]  // สถานภาพการสมรส (Master table => MasterInfo.MasterInfo_MasterInfoType = 'MarriedStatus')
        public string MarriedStatus { get; set; }

        // MKT_Income.Income_Reponsibility  // ภาระรับผิดชอบ (Master table => MasterInfo.MasterInfo_MasterInfoType = 'Responsibility')
        
        public ICollection<MktAddress> Addresses { get; set; }  // ที่อยู่ลูกค้า

        // MKT_Occupation  // อาชีพ

        //[Column("")]
        //[Column("")]
        //[Column("")]
        //[Column("")]
        //[Column("")]

        [Required]
        [Column("Customer_Status")]  // สถานะ record  (A : Active)
        public string Status { get; set; }


        [Column("Customer_CreateBy")]
        public string CreatedBy { get; set; }

        [Column("Customer_CreateDate")]
        public DateTime? CreatedDate { get; set; }

        public MktAsset Asset { get; set; }

        [Column("Customer_OpenInfo")] // ยินยอมให้เปิดเผยข้อมูล หรือไม่? (Y, N)
        public string OpenInfo { get; set; }



        
    }
}
