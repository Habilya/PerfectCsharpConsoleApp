using FluentAssertions;
using Microsoft.Data.Sqlite;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;
using Users.Api.Services;

namespace Users.Api.Tests;

public class UserServiceTests
{
	private readonly UserService _sut;
	private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
	// ILogger is usually build on top of extension methods (This is super hard to write tests to(? to be confirmed)
	// private readonly ILogger<UserService> _logger = Substitute.For<ILogger<UserService>>();
	// Using an adapter to make this testable
	private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();

	public UserServiceTests()
	{
		_sut = new UserService(_userRepository, _logger);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
	{
		// Arrange
		/// .Returns(Enumerable.Empty<User>())  - Mocks the return of the db call to fetch users
		/// In this case with an empty collection
		_userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

		// Act
		var result = await _sut.GetAllAsync();

		// Assert
		result.Should().BeEmpty();
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
	{
		// Arrange
		var expected = new[]
		{
			new User
			{
				Id = Guid.NewGuid(),
				FullName = "Test"
			}
		};
		_userRepository.GetAllAsync().Returns(expected);

		// Act
		var result = await _sut.GetAllAsync();

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnLogMessages_WhenInvoked()
	{
		// Arrange
		/// The result doen't matter in this test, the purpose is to test logging
		_userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

		// Act
		await _sut.GetAllAsync();

		// Assert
		/// Checks if  logger was called exactly 1 times with argument "Retrieving all users"
		_logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));

		/// Alternatively, partial pattern assertions are also possible using lambdas
		///_logger.Received(1).LogInformation(Arg.Is<string?>(x => x!.StartsWith("Retrieving")));

		/// Also it is possible to use parametrized matching with any long (number)
		_logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
	}

	[Fact]
	public async Task GetAllAsync_ShouldThrowException_WhenExceptionIsThrown()
	{
		// Arrange
		var sqliteException = new SqliteException("Somtehing went wrong", 500);
		/// An exception thrown can be mocked in a dependency method
		_userRepository.GetAllAsync().Throws(sqliteException);

		// Act
		var requestAction = async () => await _sut.GetAllAsync();

		// Assert
		await requestAction.Should()
			.ThrowAsync<SqliteException>()
			.WithMessage("Somtehing went wrong");

		_logger.Received(1)
			.LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving all users"));
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnNull_WhenNoUsersExist()
	{
		// Arrange
		_userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

		// Act
		/// A new random Guid will be generated here EVERYTIME
		/// It actually doesn't matter as the mockup is configured to return null on ANY guid in Arrange
		var result = await _sut.GetByIdAsync(Guid.NewGuid());

		// Assert
		result.Should().BeNull();
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnUser_WhenUserExist()
	{
		// Arrange
		var existingUser = new User
		{
			Id = Guid.NewGuid(),
			FullName = "Test",
		};
		_userRepository.GetByIdAsync(existingUser.Id).Returns(existingUser);

		// Act
		var result = await _sut.GetByIdAsync(existingUser.Id);

		// Assert
		result.Should().BeEquivalentTo(existingUser);
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnLogMessages_WhenInvoked()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepository.GetByIdAsync(userId).ReturnsNull();

		// Act
		await _sut.GetByIdAsync(userId);

		// Assert
		_logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"), Arg.Is(userId));
		_logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms"), Arg.Is(userId), Arg.Any<long>());
	}

	[Fact]
	public async Task GetByIdAsync_ShouldThrowException_WhenExceptionIsThrown()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var sqliteException = new SqliteException("Somtehing went wrong", 500);
		/// An exception thrown can be mocked in a dependency method
		_userRepository.GetByIdAsync(Arg.Any<Guid>()).Throws(sqliteException);

		// Act
		var requestAction = async () => await _sut.GetByIdAsync(userId);

		// Assert
		await requestAction.Should()
			.ThrowAsync<SqliteException>()
			.WithMessage("Somtehing went wrong");

		_logger.Received(1)
			.LogError(
				Arg.Is(sqliteException),
				Arg.Is("Something went wrong while retrieving user with id {0}"),
				Arg.Is(userId)
			);
	}

	[Fact]
	public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValid()
	{
		// Arrange
		var user = new User
		{
			Id = Guid.NewGuid(),
			FullName = "Test",
		};
		_userRepository.CreateAsync(user).Returns(true);

		// Act
		var result = await _sut.CreateAsync(user);

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public async Task CreateAsync_ShouldReturnLogMessages_WhenInvoked()
	{
		// Arrange
		var user = new User
		{
			Id = Guid.NewGuid(),
			FullName = "Test",
		};
		_userRepository.CreateAsync(user).Returns(true);

		// Act
		await _sut.CreateAsync(user);

		// Assert
		_logger.Received(1).LogInformation(Arg.Is("Creating user with id {0} and name: {1}"), Arg.Is(user.Id), Arg.Is(user.FullName));
		_logger.Received(1).LogInformation(Arg.Is("User with id {0} created in {1}ms"), Arg.Is(user.Id), Arg.Any<long>());
	}

	[Fact]
	public async Task CreateAsync_ShouldThrowException_WhenExceptionIsThrown()
	{
		// Arrange
		var sqliteException = new SqliteException("Somtehing went wrong", 500);
		var user = new User
		{
			Id = Guid.NewGuid(),
			FullName = "Test",
		};
		_userRepository.CreateAsync(user).Throws(sqliteException);

		// Act
		var requestAction = async () => await _sut.CreateAsync(user);

		// Assert
		await requestAction.Should()
			.ThrowAsync<SqliteException>()
			.WithMessage("Somtehing went wrong");

		_logger.Received(1)
			.LogError(
				Arg.Is(sqliteException),
				Arg.Is("Something went wrong while creating a user")
			);
	}

	[Fact]
	public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExist()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepository.DeleteByIdAsync(userId).Returns(true);

		// Act
		var result = await _sut.DeleteByIdAsync(userId);

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUserDoesntExist()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepository.DeleteByIdAsync(userId).Returns(false);

		// Act
		var result = await _sut.DeleteByIdAsync(userId);

		// Assert
		result.Should().BeFalse();
	}

	[Fact]
	public async Task DeleteByIdAsync_ShouldReturnLogMessages_WhenInvoked()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepository.DeleteByIdAsync(userId).Returns(false);

		// Act
		await _sut.DeleteByIdAsync(userId);

		// Assert
		_logger.Received(1).LogInformation(Arg.Is("Deleting user with id: {0}"), Arg.Is(userId));
		_logger.Received(1).LogInformation(Arg.Is("User with id {0} deleted in {1}ms"), Arg.Is(userId), Arg.Any<long>());
	}

	[Fact]
	public async Task DeleteByIdAsync_ShouldThrowException_WhenExceptionIsThrown()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var sqliteException = new SqliteException("Somtehing went wrong", 500);
		/// An exception thrown can be mocked in a dependency method
		_userRepository.DeleteByIdAsync(Arg.Any<Guid>()).Throws(sqliteException);

		// Act
		var requestAction = async () => await _sut.DeleteByIdAsync(userId);

		// Assert
		await requestAction.Should()
			.ThrowAsync<SqliteException>()
			.WithMessage("Somtehing went wrong");

		_logger.Received(1)
			.LogError(
				Arg.Is(sqliteException),
				Arg.Is("Something went wrong while deleting user with id {0}"),
				Arg.Is(userId)
			);
	}
}
