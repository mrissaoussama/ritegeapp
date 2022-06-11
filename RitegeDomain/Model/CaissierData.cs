namespace RitegeDomain.Model
{
    public class CaissierData
    {
        public int IdCaisser;
        public string CaissierName { get; set; }
        public CaissierData() { }
        public CaissierData(string caissiername) { CaissierName = caissiername; }

        public CaissierData(int idCaisser, string caissierName)
        {
            IdCaisser = idCaisser;
            CaissierName = caissierName;
        }
    }
}
