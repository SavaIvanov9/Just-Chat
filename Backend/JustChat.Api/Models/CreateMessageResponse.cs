using System;

namespace JustChat.Api.Models
{
    public class CreateMessageResponse
    {
        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
