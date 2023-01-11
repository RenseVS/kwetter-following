using System;
namespace Following_Service.Services
{
	public class FollowerService
	{
		public FollowerService()
		{
		}

		public async Task<IEnumerable<string>> GetUserFollowers(string userid) {
			//if user is famous only return other famous followings
			//als geimplementeerd kan dit met een enkele query gedaan worden
			return new List<string> { "8776", "323", "418" };
		}
	}
}

