namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Models;

    public class UploadPictureCommand : Command
    {
        public UploadPictureCommand(string[] data) : base(data)
        {
        }

        //UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public override string Execute()
        {
            string albumName = Data[1];
            string pictureTitle = Data[2];
            string pictureFilePath = Data[3];

            Album album = this.unit.Albums.FirstOrDefaultWhere(a => a.Name.ToLower() == albumName.ToLower());

            if (album == null)
            {
                throw new ArgumentNullException(nameof(album), "Album with such id dosn`t exist in database");
            }

            Picture picture = new Picture()
            {
                Title = pictureTitle,
                Path = pictureFilePath
            };

            picture.Albums.Add(album);
            album.Pictures.Add(picture);

            this.unit.Commit();
            return $"Picture with name {pictureTitle} was added to the database in album {albumName}";
        }
    }
}
