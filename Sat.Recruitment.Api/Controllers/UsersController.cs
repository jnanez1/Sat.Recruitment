using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Entities.Class;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserProcess _userProcess;

        public UsersController(IUserProcess userProcess)
        {
            _userProcess = userProcess;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser([FromBody] UserDataInput _userDataInput)
        {
            var resultValidation = _userProcess.Validation(_userDataInput);

            if (!string.IsNullOrEmpty(resultValidation))
            {
                return new Result()
                {
                    IsSuccess = false,
                    Information = resultValidation
                };
            }
            var result = _userProcess.CreateUser(_userDataInput);

            return result;

        }
    }
}
