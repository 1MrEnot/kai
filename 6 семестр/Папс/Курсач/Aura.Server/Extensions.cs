namespace Aura.Server
{
    public static class Extensions
    {
        public static string ToMinSec(this int sec)
        {
            var mins = sec / 60;
            var secs = (sec % 60).ToString().PadLeft(2, '0');
            return $"{mins}:{secs}";
        }
    }
}