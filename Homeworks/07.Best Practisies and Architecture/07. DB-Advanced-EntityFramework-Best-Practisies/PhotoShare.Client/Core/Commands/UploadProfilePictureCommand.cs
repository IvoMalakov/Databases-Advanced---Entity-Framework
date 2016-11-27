namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.IO;
    using Models;
    public class UploadProfilePictureCommand : Command
    {      
        public UploadProfilePictureCommand(string[] data) : base(data)
        {
        }

        //UploadProfilePicture <username> <pictureFilePath>
        public override string Execute()
        {
            string userName = Data[1];
            string pictureFilePath = Data[2];

            User user = this.unit.Users.FirstOrDefaultWhere(u => u.Username.ToLower() == userName.ToLower());

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User with such userName dosn`t exist in database");
            }

            byte[] bytes;

            using (StreamReader reader = new StreamReader(pictureFilePath))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    reader.BaseStream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }
            }

            user.ProfilePicture = bytes;
            File.WriteAllBytes("testResults.txt", bytes);

            this.unit.Commit();
            return $"Profile picture of {userName} was added to the database";
        }
    }
}
