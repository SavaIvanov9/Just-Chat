namespace JustChat.Api.Models.Messages
{
    public class CreateMessageRequest
    {
        public long UserId { get; set; }

        public long RoomId { get; set; }

        public string Content { get; set; }
    }
}
