using System.Linq;

namespace WhatsAppParser.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var parser = new Parser("chat.txt");
            var messages = parser.Messages().ToList();
        }
    }
}
