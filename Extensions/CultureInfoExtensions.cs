using System.Globalization;

namespace P3D.Legacy.Shared.Extensions
{
    public static class CultureInfoExtensions
    {
        public static bool TryGetCultureInfo(string cultureCode, out CultureInfo culture)
        {
            try
            {
                culture = new CultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                culture = null;
                return false;
            }
        }
    }
}
