using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System.Net;

namespace CoachConnect.IntegrationTests.Services;

public class UserServiceTests : BaseIntegrationTests
{

    public UserServiceTests(CoachConnectWebAppFactory factory) 
        : base(factory)
    {        
    }

    [Fact]
    public async Task GetUsersAsync_DefaultSize_10_Return_10_Users() 
    {
        // arrange
        // test data som vi skal sjekke resultatet mot!
        var userQuery = new UserQuery();

        // act
        var userDtos = await UserService!.GetAllAsync(userQuery);

        // assert
        Assert.NotNull(userDtos);
        Assert.Equal(10, userDtos.ToList().Count);
    }

    [Fact]
    public async Task GetUserByIdAsync_Return_User()
    {
        // arrange

        var userId = new UserId(new Guid("22222222-2222-2222-2222-222222222222"));

        User user = new()
        {
            Id = userId,
            FirstName = "Maria",
            LastName = "Mariah",
            PhoneNumber = "99999999",
            Email = "mariah@yahoo.com",
            HashedPassword = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu7/Qw89vF7aLl0yWQ/dnMZ8M2N/cDBhK",
            Salt = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu",
            Created = new DateTime(2024, 02, 22, 19, 54, 51),
            Updated = new DateTime(2024, 02, 22, 19, 54, 51),
            
        };
        // testdata som vi skal sjekke resultatet mot
        var guid = new Guid("22222222-2222-2222-2222-222222222222");
        // act
        var userDto = await UserService!.GetByIdAsync(guid);

        // assert
        Assert.NotNull(userDto);
        Assert.Equal(user.Id, userDto.Id);
        Assert.Equal(user.FirstName, userDto.FirstName);
        Assert.Equal(user.LastName, userDto.LastName);
        Assert.Equal(user.PhoneNumber, userDto.PhoneNumber);
        Assert.Equal(user.Email, userDto.Email);
        
    }
}