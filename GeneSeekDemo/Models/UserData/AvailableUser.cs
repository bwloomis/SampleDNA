using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goofy.Models.UserData
{
    public class AvailableUser
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public List<AvailableUser> AllUsers()
        {
            List<AvailableUser> userList = new List<AvailableUser>();

            AvailableUser user1 = new AvailableUser
            {
                UserName = "Anupam Das",
                UserPassword = "lifeisbeautiful",
            };

            AvailableUser user2 = new AvailableUser
            {
                UserName = "Chinmoy Das",
                UserPassword = "GoodTime",
            };

            userList.Add(user1);
            userList.Add(user2);

            return userList;
        }
    }
}
