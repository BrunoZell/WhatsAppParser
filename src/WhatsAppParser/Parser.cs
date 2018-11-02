using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WhatsAppParser
{
    public class Parser
    {
        private readonly Regex _messageRegex = new Regex(@"(\d{2}/\d{2}/\d\{4}, \d{2}:\d{2}) - ([^:]+): (.*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private readonly string _filePath;

        public Parser(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public IEnumerable<Message> Messages()
        {
            using (var reader = File.OpenText(_filePath)) {

                Message message = null;
                StringBuilder messageBuilder = null;

                while (reader.NextLine(out string line)) {
                    var match = _messageRegex.Match(line);
                    if (match.Success) {
                        // Line starts a new message
                        if (message != null && messageBuilder != null) {
                            // Complete current message and return
                            message.Content = messageBuilder.ToString();
                            yield return message;
                        }

                        // Prepare for next message
                        message = new Message {
                            Timestamp = DateTime.ParseExact(match.Groups[1].Value, @"dd/MM/yyyy, HH:mm", CultureInfo.InvariantCulture),
                            Sender = match.Groups[2].Value
                        };

                        messageBuilder = new StringBuilder();
                        messageBuilder.Append(match.Groups[3].Value);
                    } else {
                        // Line appends to the existing message
                        messageBuilder.Append(line);
                    }
                }
            }
        }
    }
}
