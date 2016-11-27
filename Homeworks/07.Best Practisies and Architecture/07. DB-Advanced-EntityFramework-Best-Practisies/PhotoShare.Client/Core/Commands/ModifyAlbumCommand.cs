namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Text;
    using Models;
    public class ModifyAlbumCommand : Command
    {
                
        public ModifyAlbumCommand(string[] data) : base(data)
        {
        }

        //ModifyAlbum <albumId> <property> <new value>
        //For example
        //ModifyAlbum 4 Name <new name>
        //ModifyAlbum 4 BackgroundColor <new color>
        //ModifyAlbum 4 IsPublic <True/False>
        public override string Execute()
        {
            int albumId = int.Parse(Data[1]);
            string property = Data[2];
            string newValue = Data[3];

            Album album = this.unit
                .Albums
                .FirstOrDefaultWhere(u => u.Id == albumId);

            StringBuilder sb = new StringBuilder();

            switch (property.ToLower())
            {
                case "name":
                    album.Name = newValue;
                    sb.AppendFormat("The name of the album was updated to {0}", newValue);
                    break;

                case "backgroundcolor":
                    album.BackgroundColor = (Color?) Enum.Parse(typeof(Color), newValue);
                    sb.AppendFormat("The color of the album was updated in the database to {0}", newValue);
                    break;

                case "ispublic":
                    album.IsPublic = bool.Parse(newValue);
                    sb.AppendFormat("Album's status \"IsPublic\" was updated to {0}", newValue);
                    break;

                default:
                    throw new ArgumentException("Invalid property name");
            }

            this.unit.Commit();
            return sb.ToString();
        }
    }
}
