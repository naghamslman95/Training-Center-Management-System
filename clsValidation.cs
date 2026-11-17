using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Training_Center_Management_System
{
    public class clsValidation
    {
        public static bool ValidateEmail(string EmailAddress)
        {
            string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(EmailAddress, Email))
                return true;
            else
                return false;
        }
    }
}
