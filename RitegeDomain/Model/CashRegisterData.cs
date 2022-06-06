namespace RitegeDomain.Model
{
    public class CashRegisterData
    {
        public string CashRegisterName { get; set; }
        public CashRegisterData() { }
        public CashRegisterData(string CashRegistername) { CashRegisterName = CashRegistername; }
    }
}
