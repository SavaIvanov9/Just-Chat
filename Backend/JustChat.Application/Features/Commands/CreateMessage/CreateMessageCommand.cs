﻿using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Features.Commands.CreateMessage
{
    public class CreateMessageCommand : ICommand<Message>
    {
        public string UserId { get; set; }

        public string RoomId { get; set; }

        public string Content { get; set; }
    }
}