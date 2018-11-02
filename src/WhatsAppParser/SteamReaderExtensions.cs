using System.IO;

namespace WhatsAppParser
{
    internal static class SteamReaderExtensions
    {
        public static bool NextLine(this StreamReader reader, out string line)
        {
            if (reader.EndOfStream) {
                line = null;
                return false;
            }

            line = reader.ReadLine();
            return true;
        }
    }
}
