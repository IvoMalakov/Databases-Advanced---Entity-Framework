namespace StudentSystem.Models.ModelValidations
{
    using System;
    using System.Text.RegularExpressions;

    public static class ValidationClass
    {
        public static bool CheckIfNameIsValid(string name)
        {
            if (name.Length < 3 || name.Length > 50)
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfPhoneNumberIsValid(string phoneNumber)
        {
            if (phoneNumber.Length < 8 || phoneNumber.Length > 14)
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfRegistrationDateIsVaild(DateTime registrationDate)
        {
            if (registrationDate > DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfBirthDateIsValid(DateTime? birthDate)
        {
            if ((birthDate != null) && (birthDate.Value > DateTime.Now))
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfPriceIsValid(decimal price)
        {
            if (price < 0m)
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfUrlIsValid(string url)
        {
            string regexString = @"^(((ht|f)tp(s?)\:\/\/)|(www))?[^.](.)[^.](([-.\w]*[0-9a-zA-Z])+(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*))[^.](.)[^.]([a-zA-Z0-9]+(\/)*)$";

            Regex regex = new Regex(regexString);

            if (!regex.IsMatch(url))
            {
                return false;
            }

            return true;
        }

        public static bool CheckIfContentIsValid(string content)
        {
            if ((content.Length < 3) || (content.Length > 10000))
            {
                return false;
            }

            return true;
        }
    }
}