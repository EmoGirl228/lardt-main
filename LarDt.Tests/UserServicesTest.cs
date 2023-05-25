using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Wrapper;
using Domain.Models;

namespace LarDt.Tests
{
    public class UserServicesTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;

        public UserServicesTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();
            repositoryWrapperMoq.Setup(x => x.User)
                    .Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);


        }
        [Fact]
        public async Task CreateAsync_NullUser_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User(){Login1 = "", Password1 = "", Role1 = "", Userid = 1 } },
                new object[] { new User(){Login1 = "Plox", Password1 = "", Role1 = "", Userid = 2 } },
                new object[] { new User(){Login1 = "Hox", Password1 = "abc", Role1 = "da", Userid = 3 } },
            };
        }
        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(User user)
        {
            var newUser = user;
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newUser));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never());
            Assert.IsType<ArgumentException>(ex);

        }
        [Fact]
        public async Task CreateAsyncNewUserSholdCreateNewUser()
        {
            var newUser = new User()
            {
                Login1 = "pas",
                Password1 = "12",
                Role1 = "Admin",
                Userid = 1
            };

            await service.Create(newUser);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

    }

}
