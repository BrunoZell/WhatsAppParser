using System;

namespace WhatsAppParser
{
    public class Message
    {
        public DateTime Timestamp { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}
