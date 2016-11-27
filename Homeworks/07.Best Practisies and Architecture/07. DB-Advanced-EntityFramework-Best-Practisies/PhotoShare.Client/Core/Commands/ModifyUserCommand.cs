namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Text;
    using Models;

    public class ModifyUserCommand : Command
    {      
        public ModifyUserCommand(string[] data) : base(data)
        {
        }

        //ModifyUser <username> <property> <new value>
        //For example:
        //ModifyUser <username> Password <NewPassword>
        //ModifyUser <username> Email <NewEmail>
        //ModifyUser <username> FirstName <NewFirstName>
        //ModifyUser <username> LastName <newLastName>
        //ModifyUser <username> BornTown <newBornTownName>
        //ModifyUser <username> CurrentTown <newCurrentTownName>
        //!!! Cannot change username
        public override string Execute()
        {
            string userName = Data[1];
            string property = Data[2];
            string newValue = Data[3];

            User user = this.unit
                .Users
                .FirstOrDefaultWhere(u => u.Username.ToLower() == userName.ToLower());

            StringBuilder sb = new StringBuilder();

            switch (property.ToLower())
            {
                case "firstname":
                    user.FirstName = newValue;
                    sb.AppendFormat("User's first name was updated to {0}", newValue);
                    break;

                case "lastname":
                    user.LastName = newValue;
                    sb.AppendFormat("User's last name was updated to {0}", newValue);
                    break;

                case "borntown":
                    Town bornTown = this.unit
                        .Towns
                        .FirstOrDefaultWhere(t => t.Name.ToLower() == newValue.ToLower()) ?? new Town()
                        {
                            Name = newValue
                        };

                    user.BornTown = bornTown;
                    sb.AppendFormat("User's born town was updated in database to {0}", newValue);
                    break;

                case "currenttown":
                    Town currentTown = this.unit
                        .Towns
                        .FirstOrDefaultWhere(t => t.Name.ToLower() == newValue.ToLower()) ?? new Town()
                        {
                            Name = newValue
                        };

                    user.CurrentTown = currentTown;
                    sb.AppendFormat("User's current town was updated in database to {0}", newValue);
                    break;

                case "password":
                    user.Password = newValue;
                    sb.AppendFormat("User's password was updated in database to {0}", newValue);
                    break;

                case "email":
                    user.Email = newValue;
                    sb.AppendFormat("User's email was updated in database to {0}", newValue);
                    break;

                default:
                    throw new ArgumentException("Invalid property Name");
            }

            this.unit.Commit();
            return sb.ToString();
        }
    }
}
