using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserDB.Models
{
    partial class User
    {
        private bool CheckIfLowLetterIsContained(string value)
        {
            foreach (char symbol in value)
            {
                if (symbol.ToString() == symbol.ToString().ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfUpperLetterIsContained(string value)
        {
            foreach (char symbol in value)
            {
                if (symbol.ToString() == symbol.ToString().ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfSpecialSymbolsIsContained(string value)
        {
            char[] specialSymbols = {'!', '@', '#', '$', '%', '^', '&', '*', '(' , ')', '_', '+', '<', '>', '?'};

            foreach (var symbol in value)
            {
                if (specialSymbols.Contains(symbol))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfDigitIsContained(string value)
        {
            int number = 0;

            foreach (char symbol in value)
            {
                if (int.TryParse(symbol.ToString(), out number))
                {
                    return true;
                }
            }

            return false;
        }

        private bool EmailIsValid(string email)
        {
            string regularExpressionString = @"([a-zA-Z0-9][a-zA-Z_\-.]*[a-zA-Z0-9])@([a-zA-Z-]+\.[a-zA-Z-]+(\.[a-zA-Z-]+)*)\b";
            Regex regex = new Regex(regularExpressionString);

            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;
        }
    }
}
