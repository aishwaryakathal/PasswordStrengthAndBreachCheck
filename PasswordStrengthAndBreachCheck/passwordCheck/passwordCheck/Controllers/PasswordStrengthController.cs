using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;

namespace passwordCheck.Controllers
{
    // GET api/PasswordStrength/
    [Route("api/[controller]")]
    public class PasswordStrengthController : Controller
    {
        /// <summary>
        /// Variables
        /// </summary>
        string passwordStrength;
        string breachCheck;
        string passwordStrengthBreachStatus;
        string strengthShow;
        string breachShow;
        int score = 0;
        bool isShort;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly char[] _newLinesCheck = new[] { '\n', '\r' };
        private static readonly char[] _colonCheck = new[] { ':' };
        string[] strengthDesc = new string[] {
            "Password is Blank",
            "Password is Short",
            "Password is Very Weak",
            "Password is Weak",
            "Password is Medium",
            "Password is Strong"
        };


        // GET api/PasswordStrength/{password}
        /// <summary>
        /// Get function to call function to check Password Strength and data breach 
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Password Strength and Information about breach data</returns>
        [HttpGet("{password}")]
        public async Task<string> Get(string password)
        {
            //call function to check the data breach
            breachCheck = await PasswordBreachCheck(password);

            //call function to check the strength
            strengthShow = StrengthCheck(password);

            //check if there is no data breach
            if (breachCheck == null)
            {
                breachShow = "No Data Breach";
                passwordStrengthBreachStatus = strengthShow + ". \n" + breachShow;
                return passwordStrengthBreachStatus;
            }
            else
            {
                breachShow = "Your Password appeared in data breach " + breachCheck + " number of times.\nPlease consider another password.";
                passwordStrengthBreachStatus = strengthShow + ". \n" + breachShow;
                return passwordStrengthBreachStatus;
            }
        }


        /// <summary>
        /// Check the password strength based on score gained by password and returns the status of password strength
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Password strength</returns>
        public string StrengthCheck(string password)
        {
            //if conditions based on the length of the password
            if (password.Length < 1)
            {
                score = 0;               
            }                

            if (password.Length > 0 && password.Length < 8)
            {
                score = 1;
                isShort = true; //Check the length is shorter than 8
            }
                

            if (password.Length >= 8)
            {
                score = 2;
                isShort = false;
            }
                
            if (isShort==false)
            {
                //check if password have at least 1 numerical number
                if (Regex.IsMatch(password, @"[\d]", RegexOptions.ECMAScript))
                    score++;

                //check if password have at least 1 lower character and 1 upper character
                if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript) &&
                Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                    score++;

                //check if the password have at least one special character
                if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript))
                    score++;
            }
            
            //if contionions to display the strength of password based on the score password gained
            if (score == 0) { passwordStrength = strengthDesc[0]; }
            if (score == 1) { passwordStrength = strengthDesc[1]; }
            if (score == 2) { passwordStrength = strengthDesc[2]; }
            if (score == 3) { passwordStrength = strengthDesc[3]; }
            if (score == 4) { passwordStrength = strengthDesc[4]; }
            if (score == 5) { passwordStrength = strengthDesc[5]; }

            return passwordStrength;
        }

        /// <summary>
        /// Check the data breach of the password by API pwned passwords
        /// </summary>
        /// <param name="password"></param>
        /// <returns>The number of breach data count, if any</returns>
        public async Task<string> PasswordBreachCheck(string password)
        {
            byte[] byteStr = Encoding.UTF8.GetBytes(password);
            byte[] byteHash = null;
            var hashstring = string.Empty;
            string message;

            //convert the password into hash
            using (var sha1 = SHA1.Create())
            {
                byteHash = sha1.ComputeHash(byteStr);
            }

            var sb = new StringBuilder();

            //Join the bytes to make it a single string hash
            foreach (byte b in byteHash)
            {
                sb.Append(b.ToString("X2"));
            }

            hashstring = sb.ToString();

            //Break the prefix of hash string into 5 initial hash characters 
            string hashFirstFive = hashstring.Substring(0, 5);

            //Suffix is the rest of the characters ixcluting initial 5 characters
            string hashSuffix = hashstring.Substring(5, hashstring.Length - 5);

            //Send the prefix along with the api as parameter, it return the entire list of passwords starts with the prefix
            message = await _httpClient.GetStringAsync($"https://api.pwnedpasswords.com/range/{hashFirstFive}");

            //Check if the list returned by the api is not blank
            if (message != null)
            {
                //Split the string returned from api based on new line
                var apiPswardlines = message.Split(_newLinesCheck,StringSplitOptions.RemoveEmptyEntries);

                //Make the hash number stored in dictionary
                var pwnedPasswordRow = new Dictionary<string, int>(apiPswardlines.Length);

                //for each lines split the hash key and breach count by colon
                foreach (var line in apiPswardlines)
                {
                    var countSplit = line.Split(_colonCheck,StringSplitOptions.RemoveEmptyEntries);

                    //API sent invalid data
                    if (countSplit.Length != 2)
                    {
                        continue;
                    }

                    //API sent invalid breach count
                    if (!int.TryParse(countSplit[1], out int breachcount))
                    {
                        continue;
                    }

                    pwnedPasswordRow[countSplit[0]] = breachcount;
                }

                //fetch the breach count from hash key 
                if (pwnedPasswordRow.TryGetValue(hashSuffix, out int breachCount))
                {
                    return breachCount.ToString();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

    }
}
