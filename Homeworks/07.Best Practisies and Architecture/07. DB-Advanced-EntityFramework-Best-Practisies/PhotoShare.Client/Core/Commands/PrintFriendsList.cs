namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;

    public class PrintFriendsListCommand : Command
    {      
        public PrintFriendsListCommand(string[] data) : base(data)
        {
        }

        //PrintFriendsList <username>
        public override string Execute()
        {
            string userName = Data[1];

            User user = this.unit
                .Users
                .FirstOrDefaultWhere(u => u.Username.ToLower() == userName.ToLower());

            string friends = String.Join(", ", user.Friends.Select(u => u.Username.ToLower()));
            return friends;
        }
    }
}
