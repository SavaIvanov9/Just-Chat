using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class RoomType : Enumeration<RoomType>
    {
        public static RoomType Default() => new RoomType(nameof(RoomType.Default), 1);
        public static RoomType Other() => new RoomType(nameof(RoomType.Other), 2);

        protected RoomType(string name, int value)
            : base(name, value)
        {
        }
    }
}
