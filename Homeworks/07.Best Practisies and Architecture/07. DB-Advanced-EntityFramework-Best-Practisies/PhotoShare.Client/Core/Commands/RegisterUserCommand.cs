﻿namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Models;
    public class RegisterUserCommand : Command
    {
      
        public RegisterUserCommand(string[] data) : base(data)
        {
        }

        //RegisterUser <username> <password> <repeat-password> <email>
        public override string Execute()
        {
            string username = Data[1];
            string password = Data[2];
            string repeatPassword = Data[3];
            string email = Data[4];
            if(password == repeatPassword)
            {
                throw new InvalidOperationException("Passwords does not match");
            }

            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            this.unit.Users.Add(user);
            this.unit.Commit();
            return "User "+user.Username+" was registered sucesfully";
        }
    }
}
