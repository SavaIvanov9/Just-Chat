using System;

namespace JustChat.Api.Models.Messages
{
    public class CreateMessageResponse
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
