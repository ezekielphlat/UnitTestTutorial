using CloudCustomer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomer.UnitTest.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {

                Id = 1,
                Name = "Jame",
                Email = "jame@gmail.com",
                Address = new Address()
                {
                    Street = "4, simbi street",
                    City = "Lagos",
                    ZipCode = "Test",
                }
            }, 
            new User
            {

                Id = 2,
                Name = "John",
                Email = "john@gmail.com",
                Address = new Address()
                {
                    Street = "4, olaiya street",
                    City = "Lagos",
                    ZipCode = "Test",
                }
            }, 
            new User
            {

                Id = 3,
                Name = "James",
                Email = "james@gmail.com",
                Address = new Address()
                {
                    Street = "4, Jameson street",
                    City = "Lagos",
                    ZipCode = "Test",
                }
            },
        };
    }
}
