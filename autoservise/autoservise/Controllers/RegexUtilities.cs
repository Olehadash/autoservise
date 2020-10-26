using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace autoservise.Controllers
{
    class RoolsFormValidate
    {

        private static RoolsFormValidate _instance = null;

        public string[] paswords = new string[2]; 

        public static RoolsFormValidate Instance()
        {
            if(_instance == null)
            {
                _instance = new RoolsFormValidate();
            }
            return _instance;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsNullValidate(string str)
        {
            return String.IsNullOrEmpty(str);
        }

        public bool SetPasLink(string line)
        {
            bool result = false;
            if (string.IsNullOrEmpty(paswords[0]))
                paswords[0] = line;
            else
            {
                paswords[1] = line;
                result = true;
            }
            return result;
        }

        public bool ComparePassword()
        {
            if (string.IsNullOrEmpty(paswords[0])) return false;
            return paswords[0].Equals(paswords[1]);
        }

        public bool CompareLineValidate(string str1, string str2)
        {
            return str1.Equals(str2);
        }
    }
}
