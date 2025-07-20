using Castle.Core.Configuration;
using HealthInsuranceAPI.Controllers;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HealthInsuranceUnitTestCase
{
    public class UserDetailMock
    {
        HealthInsuranceContext DbContext;
        UserDetailDB dbObject { get; set; }
        UserDetailController controller { get; }

        public UserDetailMock()
        {
            DbContext = InMemoryContext.CreateContext;
            dbObject = new UserDetailDB(DbContext);
            controller = new UserDetailController(dbObject, It.IsAny<Microsoft.Extensions.Configuration.IConfiguration>());

            DbContext.UserDetails.Add(new UserDetail()
            {
                UserDetailId = 1,
                UserName = "admin",
                Password = "admin",
                PhoneNumber = 123
            });
            DbContext.SaveChanges();
        }

        [Fact]
        public void Should_Be_ValidUser()
        {
            PageData<UserDetail> pagedata  = controller.ValidateUser("admin", "admin");
            Assert.Equal(pagedata.Data.UserDetailId, 1);
        }

        [Fact]
        public void Should_Be_InValidUser()
        {
            PageData<UserDetail> pagedata = controller.ValidateUser("admin1", "admin1");
            Assert.True(pagedata.Data == null);
        }

        [Fact]
        public void Should_Get_AllAcquirer()
        {
            PaginationData<UserDetail> pagedata = controller.GetAllAcquirer(1, 1);
            Assert.Equal(pagedata.Data.Count(), 1);
            Assert.Equal(pagedata.TotalCount, 1);
        }

        [Fact]
        public void Should_Get_UserDetail()
        {
            PageData<UserDetail> pagedata = controller.GetUserData(1);
            Assert.Equal(pagedata.Data.UserDetailId, 1);
        }

        [Fact]
        public void Should_Add_User()
        {
            UserDetail pagedata = controller.Add(new UserDetail()
            {
                UserName = "admin",
                Password = "admin",
                PhoneNumber = 1235678
            });
            Assert.Equal(pagedata.UserDetailId, 2);
        }

        [Fact]
        public void Should_Update_User()
        {
            var userDetail = DbContext.UserDetails.Where(X => X.UserDetailId == 1).First();
            userDetail.UserName = "admin123";
            UserDetail pagedata = controller.Update(userDetail);
            Assert.Equal(pagedata.UserName, "admin123");
        }

        [Fact]
        public void Should_Remove_User()
        {
            UserDetail UserDetail = controller.Add(new UserDetail()
            {
                UserName = "admin",
                Password = "admin",
                PhoneNumber = 123
            });
            UserDetail pagedata = controller.Delete(UserDetail);
            Assert.Equal(pagedata.UserDetailId, 2);
        }

    }
}