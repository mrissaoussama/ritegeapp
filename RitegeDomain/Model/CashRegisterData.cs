namespace RitegeDomain.Model
{
    public class CashRegisterData
    {
        public int CashRegisterId { get; set; }
        public string CashRegisterName { get; set; }
        public CashRegisterData() { }
        public CashRegisterData(string CashRegistername) { CashRegisterName = CashRegistername; }

        public CashRegisterData(int cashRegisterId, string cashRegisterName)
        {
            CashRegisterId = cashRegisterId;
            CashRegisterName = cashRegisterName;
        }
    }
}
