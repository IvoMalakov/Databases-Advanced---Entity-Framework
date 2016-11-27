namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    public class CreateAlbumCommand : Command
    {    
        public CreateAlbumCommand(string[] data) : base(data)
        {
        }

        //CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public override string Execute()
        {
            string userName = Data[1];
            string albumTitle = Data[2];
            string bgColor = Data[3];
            IEnumerable<Tag> tags = Data.Skip(4).Select(tagName => new Tag()
            {
                Name = tagName
            });

            User user = this.unit
                .Users
                .FirstOrDefaultWhere(u => u.Username.ToLower() == userName.ToLower());

            Color backgroundColor = (Color) Enum.Parse(typeof (Color), bgColor);

            Album album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = backgroundColor,
                Tags = tags.ToArray()
            };

            AlbumRole role = new AlbumRole()
            {
                Album = album,
                User = user,
                Role = Role.Owner
            };

            user.AlbumRoles.Add(role);
            this.unit.Albums.Add(album);

            return $"{albumTitle} was added to database";
        }
    }
}
