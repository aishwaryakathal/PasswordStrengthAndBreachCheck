using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePasswordCheck
{
    class Program
    {
        /// <summary>
        /// Console Application connecting to Password Check API
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using (var client = new System.Net.WebClient()) 
            {
                String userName, password;

                //Password as Input from user
                Console.Write("Enter User name: ");
                userName = Console.ReadLine();

                Console.Write("Enter Password: ");
                password = Console.ReadLine();

                //Sending user Password to Password Strength controller
                client.Headers.Add("Content-Type:application/json");  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://localhost:2376/api/PasswordStrength/"+ password);
                Console.WriteLine(Environment.NewLine + result);
                Console.ReadLine();
            }
        }
    }
}
