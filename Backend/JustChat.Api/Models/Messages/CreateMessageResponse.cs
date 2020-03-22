using System;

namespace JustChat.Api.Models.Messages
{
    public class CreateMessageResponse
    {
        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
