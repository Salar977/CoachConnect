using CoachConnect.Shared.Helpers;

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
        // testdata som vi skal sjekke resultatet mot
        var guid = new Guid("22222222-2222-2222-2222-222222222222");
        // act
        var userDto = await UserService!.GetByIdAsync(guid);

        // assert
        Assert.NotNull(userDto);
    }
}