using Entities.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
{
    public class DataProcess
    {
        private string   path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        protected  StreamReader ReadUsersFromFile()
        {
            

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public  string NormalizeEmail(string email) {

            //Normalize email
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return  string.Join("@", new string[] { aux[0], aux[1] });

        }

        public decimal CalcuteMoney(string userType, decimal money) {

            decimal result;
            var gif = Convert.ToDecimal(0.0);

            switch (userType)
            {
                case "Normal":
                    if (money > 100) {
                        gif = money * Convert.ToDecimal(0.12);
                    }
                    else
                    {
                        if (money > 10){
                            gif = money * Convert.ToDecimal(0.8);
                        }
                    }
                    break;

                case "SuperUser":
                    if (money > 100)
                    {
                        gif = money * Convert.ToDecimal(0.20);
                    }
                    break;
                case "Premium":
                    if (money > 100)
                    {
                        gif = money * 2;
                    }
                    break;

                default:
                    break;
            }

            result = money + gif;
            return result;
        
        }

        public Result RegisterUser(IUser user)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(user.ToString());
                }
                
            }
            catch (Exception e)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Information = e.Message
                };
            }
            return new Result()
            {
                IsSuccess = true,
                Information = "User Created"
            };
        }
    }
}
