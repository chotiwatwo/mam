namespace MamApi.Models
{
    /*
        ddlValue	ddlText	ddlKey	ddlsort	type	ddlType	DataValue
        0	--Select--			S	MLoanType	Y
        B	รถซื้อขายกันเอง		B	S	MLoanType	NULL
        J	สินเชื่อเช่าซื้อ(นิติบุคคล)		J	S	MLoanType	NULL
        L	Low Ticket Size		L	S	MLoanType	NULL
        P	สินเชื่อเช่าซื้อ		P	S	MLoanType	NULL
        R	Top-Up Loan		R	S	MLoanType	NULL
        S	จำนำ(SLB)		S	S	MLoanType	NULL
    */
    public class DropDownMasterData
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
