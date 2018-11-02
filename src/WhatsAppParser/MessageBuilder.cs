using System;
using System.Text;

namespace WhatsAppParser
{
    internal class MessageBuilder
    {
        private readonly StringBuilder _contentBuilder;
        private readonly Message _message;

        public MessageBuilder(DateTime timestamp, string sender)
        {
            _contentBuilder = new StringBuilder();
            _message = new Message {
                Timestamp = timestamp,
                Sender = sender
            };
        }

        public void AppendContentLine(string line) =>
            _contentBuilder.AppendLine(line);

        public Message Build()
        {
            _message.Content = _contentBuilder
                .ToString()
                .TrimEnd('\r', '\n');

            return _message;
        }
    }
}
