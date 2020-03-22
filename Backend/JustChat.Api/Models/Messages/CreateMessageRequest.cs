namespace JustChat.Api.Models.Messages
{
    public class CreateMessageRequest
    {
        public string UserId { get; set; }

        public string RoomId { get; set; }

        public string Content { get; set; }
    }
}
