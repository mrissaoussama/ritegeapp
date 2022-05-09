namespace RitegeDomain.Model
{
    public enum AlertCodes
    {
        CodeEvent21, CodeEvent17, ForceOpenAlarm,
    }
    public static class AlertString
    {
        public static string GetAlertSqlString()
        {
            string str = "in(";
            foreach (AlertCodes alertcode in (AlertCodes[])Enum.GetValues(typeof(AlertCodes)))
            {
                str += "'" + alertcode + "',";
            }
            str = str.Remove(str.Length - 1, 1);
            str += ")";
            return str;
        }
    }
}
