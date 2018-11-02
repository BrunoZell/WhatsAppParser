using System;

namespace WhatsAppParser.Terminal
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Create a parser from an exported chat history
            var parser = new Parser("chat.txt");

            // Enumerate all messages in that history file
            foreach (var message in parser.Messages()) {
                Console.WriteLine($"{message.Timestamp:yyyy-MM-dd HH:mm} {message.Sender}: {message.Content}");
            }

            Console.ReadKey();
        }
    }
}
