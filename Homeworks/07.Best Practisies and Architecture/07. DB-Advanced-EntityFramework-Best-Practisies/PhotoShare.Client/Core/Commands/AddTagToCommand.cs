namespace PhotoShare.Client.Core.Commands
{
    using System.Collections.Generic;
    using Models;
    public class AddTagToCommand : Command
    {
        public AddTagToCommand(string[] data) : base(data)
        {
        }

        //AddTagTo <albumName> <tag>
        public override string Execute()
        {
            string albumName = Data[1];
            string tagName = Data[2].ValidateOrTransform();

            var album = this.unit
                .Albums
                .FirstOrDefaultWhere(al => al.Name == albumName);

            Tag tag = new Tag()
            {
                Name = tagName
            };

            if (album == null)
            {
                this.unit.Albums.Add(new Album()
                {
                    Name = albumName,
                    Tags = new List<Tag>()
                    {
                        tag
                    }
                });
            }
            else
            {
                album.Tags.Add(tag);
            }

            return $"{albumName} was successful added to database and {tagName} was also successful added to database";
        }
    }
}
