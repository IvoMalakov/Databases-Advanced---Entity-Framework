namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Models;

    public class ShareAlbumCommand : Command
    {       
        public ShareAlbumCommand(string[] data) : base(data)
        {
        }

        //ShareAlbum <albumId> <username> <permission>
        //For example:
        //ShareAlbum 4 dragon321 Owner
        //ShareAlbum 4 dragon11 Viewer
        public override string Execute()
        {
            int albumId = int.Parse(Data[1]);
            string userName = Data[2];
            string permission = Data[3];

            Album album = this.unit.Albums.FirstOrDefaultWhere(a => a.Id == albumId);
            User user = this.unit.Users.FirstOrDefaultWhere(u => u.Username.ToLower() == userName.ToLower());
            Role role = (Role) Enum.Parse(typeof (Role), permission);

            if (album == null)
            {
                throw new ArgumentNullException(nameof(album), "Album with such id dosn`t exist in database");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User with such userName dosn`t exist in database");
            }

            AlbumRole albumRole = new AlbumRole()
            {
                Album = album,
                User = user,
                Role = role
            };

            this.unit.AlbumRoles.Add(albumRole);
            this.unit.Commit();

            return $"{userName} is {permission}  to album {album.Name}";
        }
    }
}
