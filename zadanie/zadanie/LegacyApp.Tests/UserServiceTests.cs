namespace LegacyApp.Tests;

public class UserServiceTests
{
    private UserService _userService;
    public UserServiceTests()
    {
        //Arrange
        _userService = new UserService();
    }

    [Fact]
    public void AddUser_Return_False_When_First_Name_Is_Empty()
    {
        // Arrange
        //var userService = new UserService();
        
        // Act
        var result = _userService.AddUser(
            null,
            "Kowalski",
            "kowalski@pjwstk.edu.pl",
            DateTime.Parse("2000-01-01"),
            1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Return_False_When_Last_Name_Is_Empty()
    {
        // Arrange
        //var userService = new UserService();
        
        // Act
        var result = _userService.AddUser(
            "Jan",
            null,
            "kowalski@pjwstk.edu.pl",
            DateTime.Parse("2000-01-01"),
            1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Return_False_When_Email_Doesnt_Contains_At_And_Dot_Sign()
    {
        //Act
        var result = _userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalskipjwstkedupl",
            DateTime.Parse("2000-01-01"),
            1);
        
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Return_False_When_Age_Younger_Than_21()
    {
        //Act
        var result = _userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@pjwstk.edu.pl",
            DateTime.Now.AddYears(-10),
            1);
        
        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Return_False_When_Clients_CreditLimit_Is_Under_500()
    {
        //Act
        var result = _userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@pjwstk.edu.pl",
            DateTime.Parse("2000-01-01"),
            1);

        //Assert
        Assert.False(result);
    }
}