namespace PR.OpenGym.Utilities.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static DateTime ConvertDateToMexicoCentralLocalZone(this DateTime localZone)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTime(localZone, timeZoneInfo);
        }
    }
}
