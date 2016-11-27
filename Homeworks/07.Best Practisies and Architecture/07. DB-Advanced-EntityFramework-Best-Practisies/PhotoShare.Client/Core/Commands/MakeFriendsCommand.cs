namespace PhotoShare.Client.Core.Commands
{
    using Models;
    public class MakeFriendsCommand : Command
    {
        public MakeFriendsCommand(string[] data) : base(data)
        {
        }

        public override string Execute()
        {
            //bidirectional adding friends
            //MakeFriends <username1> <username2>
            string userName1 = Data[1];
            string userName2 = Data[2];

            User user1 = this.unit
                .Users
                .FirstOrDefaultWhere(u => u.Username.ToLower() == userName1.ToLower());

            User user2 = this.unit
                .Users
                .FirstOrDefaultWhere(u => u.Username.ToLower() == userName2.ToLower());

            user1.Friends.Add(user2);
            user2.Friends.Add(user1);

            this.unit.Commit();

            return $"{userName1} and {userName2} are already friends";
        }
    }
}
