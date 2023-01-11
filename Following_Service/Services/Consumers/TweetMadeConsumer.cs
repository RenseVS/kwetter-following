using System;
using MassTransit;
using MessageContracts;

namespace Following_Service.Services.Consumers
{
	public class TweetMadeConsumer : IConsumer<TweetMade>
    {
        private readonly FollowerService _followerService;
        private readonly IPublishEndpoint _endpoint;
        public TweetMadeConsumer(FollowerService followerService, IPublishEndpoint endpoint)
        {
            _followerService = followerService;
            _endpoint = endpoint;
        }

        public async Task Consume(ConsumeContext<TweetMade> context)
        {
            IEnumerable<string> followers = await _followerService.GetUserFollowers(context.Message.UserID);
            foreach (string follower in followers) {
                await _endpoint.Publish(new TweetMadeWithFollower()
                {
                    TweetID = context.Message.TweetID,
                    UserName = context.Message.UserName,
                    Message = context.Message.Message,
                    TweetDate = context.Message.TweetDate,
                    FollowingUserID = follower
                });
            }
        }
    }
}

