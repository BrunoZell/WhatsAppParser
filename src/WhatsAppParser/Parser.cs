using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace WhatsAppParser
{
    public class Parser
    {
        private readonly string _filePath;
        private readonly Regex _messageRegex;

        public Parser(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _messageRegex = new Regex(@"^(\d{2}/\d{2}/\d{4}, \d{2}:\d{2}) - ([^:]+)(: )?(.*)?$", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        }

        public IEnumerable<Message> Messages()
        {
            using (var reader = File.OpenText(_filePath)) {
                MessageBuilder messageBuilder = null;

                while (reader.NextLine(out string line)) {
                    var match = _messageRegex.Match(line);
                    if (match.Success) {
                        // Line starts a new message
                        if (messageBuilder != null) {
                            // Complete current message and return
                            yield return messageBuilder.Build();
                        }

                        if (!match.Groups[3].Success) {
                            // No ': ' after senders name.
                            // Probably some encryption message or similar. Just skip this one.
                            messageBuilder = null;
                            continue;
                        }

                        // Prepare for next message
                        var timestamp = DateTime.ParseExact(match.Groups[1].Value, "dd/MM/yyyy, HH:mm", CultureInfo.InvariantCulture);
                        messageBuilder = new MessageBuilder(timestamp, match.Groups[2].Value);
                        messageBuilder.AppendContentLine(match.Groups[4].Value);
                    } else {
                        // Line appends to the existing message
                        // There might be the case that this line is just invalid and there is no builder from previous lines
                        messageBuilder?.AppendContentLine(line);
                    }
                }

                if (messageBuilder != null) {
                    yield return messageBuilder.Build();
                }
            }
        }
    }
}
