using Entities.Class;
using Entities.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Sat.Recruitment.Api
{
    public class UserProcess : DataProcess, IUserProcess
    {
        private readonly static List<User> _users = new List<User>();


        public Result CreateUser(UserDataInput _userDataInput)
        {
            Result resultCreateUser = new Result();
            var money = CalcuteMoney(_userDataInput.UserType, Convert.ToDecimal(_userDataInput.Money));
            _userDataInput.Money = money.ToString();

            resultCreateUser = RegisterUser(_userDataInput);

            return resultCreateUser;
        }

        public string Validation(UserDataInput _userDataInput)
        {
            string errors = "";

            foreach (var prop in _userDataInput.GetType().GetProperties())
            {
                string value = (string)prop.GetValue(_userDataInput);
                if (string.IsNullOrEmpty(value))
                {
                    errors = errors + "The " + prop.Name + " is required ";
                }
            }

            if (string.IsNullOrEmpty(errors))
            {
                errors = ValidateDuplicateUser(_userDataInput);
            }

            return errors;
        }

        public string ValidateDuplicateUser(User _userDataInput)
        {
            string email = _userDataInput.Email;
            string result = "";
            var reader = ReadUsersFromFile();
            _userDataInput.Email = NormalizeEmail(email);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()).ToString(),
                };
                _users.Add(user);
            }
            reader.Close();
            try
            {

                foreach (var user in _users)
                {
                    if (user.Email == _userDataInput.Email || user.Phone == _userDataInput.Phone || user.Name == _userDataInput.Name || user.Address == _userDataInput.Address)
                    {
                        result = "User is duplicated";
                    }


                }
            }
            catch (Exception e)
            {
                // Debug.WriteLine("The user is duplicated"); ojooooooooooooooooooooooooooooo log               
            }

            return result;
        }
    }
}
