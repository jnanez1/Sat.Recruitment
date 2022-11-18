using System;
using System.Dynamic;
using Entities.Class;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void CreateUser_Validation_False()
        {
            UserDataInput user = new UserDataInput();

            user.Name = "Mike";
            user.Email = null;
            user.Address = "Av. Juan G";
            user.Phone = null;
            user.UserType = "Normal";
            user.Money = "124";

            var userController = new UsersController(new UserProcess());
            var result = userController.CreateUser(user).Result;
            Assert.False(result.IsSuccess);
            Assert.NotEqual("User Created", result.Information);

        }

        [Fact]
        public void CreateUser_DuplicateUser_true()
        {
            UserDataInput user = new UserDataInput();

            user.Name = "Agustina";
            user.Email = "test@test";
            user.Address = "Av. Juan G";
            user.Phone = "+8654372";
            user.UserType = "Normal";
            user.Money = "124";

            var userController = new UsersController(new UserProcess());
            var result = userController.CreateUser(user).Result;
            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Information);

        }

        [Fact]
        public void CreateUser_CreateUser_true()
        {
            UserDataInput user = new UserDataInput();

            user.Name = "Jose";
            user.Email = "test@test";
            user.Address = "Av. Juan G";
            user.Phone = "+8654372";
            user.UserType = "Normal";
            user.Money = "124";

            var userController = new UsersController(new UserProcess());
            var result = userController.CreateUser(user).Result;
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Information);

        }
    }
}
