# WhatspApp chat history parser
A light-weight C# library that parses exported chat histories from WhatsApp.

``` cshrp
// Create a parser from an exported chat history
var parser = new Parser("chat.txt");

// Enumerate all messages in that history file
foreach (var message in parser.Messages()) {
    Console.WriteLine($"{message.Timestamp:yyyy-MM-dd HH:mm} {message.Sender}: {message.Content}");
}
```

`Parser.Messages` is an enumerator, that means it will only read that many messages as you really need by leveraging the power of Linq.

It will return `Message` objects. They have three properties:

- **Timestamp** The point in time the message was sent down to the minute.
- **Sender** String representation of the message author. This can be the telephone number or the contact name behind this number.
- **Content** The actual message sent by the sender. This might be multi-line.

Be aware that history entries like encryption information or group invitations are ignored when parsing.
