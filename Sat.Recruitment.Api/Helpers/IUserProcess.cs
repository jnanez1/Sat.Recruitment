using Entities.Class;

namespace Sat.Recruitment.Api
{
    public interface IUserProcess
    {
        Result CreateUser(UserDataInput _userDataInput);
        string ValidateDuplicateUser(User _userDataInput);
        string Validation(UserDataInput _userDataInput);
    }
}