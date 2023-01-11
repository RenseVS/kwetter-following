﻿using System;
namespace MessageContracts
{
    public record TweetMade
    {
        public string UserID { get; set; }
        public string TweetID { get; init; }
        public string UserName { get; init; }
        public string Message { get; init; }
        public DateTime TweetDate { get; set; }
    }
}

