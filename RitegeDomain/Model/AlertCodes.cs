namespace RitegeDomain.Model
{

    public static class EventCodes
    {
        public static string[] AlertCodes = new[] { "21", "17" };
        public static int EgressCode = 15;
        public static string GetAlertSqlString()
        {
        string str = "in(";
            foreach (var alertcode in AlertCodes)
            {
                str += "'" + alertcode + "',";
            }
            str = str.Remove(str.Length - 1, 1);
            str += ")";
            return str;
        }
    }
}
