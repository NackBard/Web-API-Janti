using NodaTime.TimeZones;

namespace Web_API_Janti
{
    public class TimeManager : ITimeManager
    {
        public TimeManager()
        {
            CurrentTimeZone = CurrentTimeZone ?? TimeZoneInfo.Utc;
        }

        private static TimeZoneInfo CurrentTimeZone;

        public string GetTime()
        {
            var localDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, CurrentTimeZone);
            return localDateTime.ToString("dd.MM.yyyy HH:mm:ss zzz");
        }

        public bool SetTimeZone(string timeZone)
        {
            var mappings = TzdbDateTimeZoneSource.Default.WindowsMapping.MapZones;
            var map = mappings.FirstOrDefault(x =>
                x.TzdbIds.Any(z => z.Equals(timeZone, StringComparison.OrdinalIgnoreCase)));

            if (map == null)
                return false;
            else
                CurrentTimeZone = TimeZoneInfo.FindSystemTimeZoneById(map.WindowsId);

            return true;
        }

        public string ConvertDate(string timeStamp)
        {
            var currentTime = DateTime.Parse(timeStamp);
            var zoneTime = TimeZoneInfo.ConvertTime(currentTime, CurrentTimeZone);
            return zoneTime.ToString("dd.MM.yyyy HH:mm:ss zzz");
        }
    }
}
