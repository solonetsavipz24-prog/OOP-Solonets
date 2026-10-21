using Lab12v2;
using Xunit;

namespace Lab12v2.Tests;

public class InMemoryRepositoryTests
{
    [Fact]
    public void Add_AddsNewUser()
    {
        var repository = new InMemoryRepository<User>();
        var user = CreateUser(1, "artem");

        repository.Add(user);

        User result = repository.GetById(1);
        Assert.Equal("artem", result.Username);
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void GetById_ReturnsUserWithCorrectId()
    {
        var repository = new InMemoryRepository<User>();
        repository.Add(CreateUser(1, "one"));
        repository.Add(CreateUser(2, "two"));

        User result = repository.GetById(2);

        Assert.Equal(2, result.Id);
        Assert.Equal("two", result.Username);
    }

    [Fact]
    public void GetById_NonExistingId_ThrowsKeyNotFoundException()
    {
        var repository = new InMemoryRepository<User>();

        Assert.Throws<KeyNotFoundException>(() => repository.GetById(99));
    }

    [Fact]
    public void GetAll_ReturnsAllUsers()
    {
        var repository = new InMemoryRepository<User>();
        repository.Add(CreateUser(1, "one"));
        repository.Add(CreateUser(2, "two"));
        repository.Add(CreateUser(3, "three"));

        List<User> result = repository.GetAll().ToList();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Update_ChangesExistingUser()
    {
        var repository = new InMemoryRepository<User>();
        repository.Add(CreateUser(1, "old_name"));

        var updatedUser = new User
        {
            Id = 1,
            Username = "new_name",
            Email = "new@example.com",
            IsActive = false
        };

        repository.Update(updatedUser);

        User result = repository.GetById(1);
        Assert.Equal("new_name", result.Username);
        Assert.Equal("new@example.com", result.Email);
        Assert.False(result.IsActive);
    }

    [Fact]
    public void Update_NonExistingUser_ThrowsKeyNotFoundException()
    {
        var repository = new InMemoryRepository<User>();

        Assert.Throws<KeyNotFoundException>(() => repository.Update(CreateUser(99, "missing")));
    }

    [Fact]
    public void Delete_RemovesExistingUser()
    {
        var repository = new InMemoryRepository<User>();
        repository.Add(CreateUser(1, "one"));
        repository.Add(CreateUser(2, "two"));

        repository.Delete(1);

        Assert.Single(repository.GetAll());
        Assert.Throws<KeyNotFoundException>(() => repository.GetById(1));
    }

    [Fact]
    public void Delete_NonExistingId_ThrowsKeyNotFoundException()
    {
        var repository = new InMemoryRepository<User>();

        Assert.Throws<KeyNotFoundException>(() => repository.Delete(99));
    }

    private static User CreateUser(int id, string username)
    {
        return new User
        {
            Id = id,
            Username = username,
            Email = $"{username}@example.com",
            IsActive = true
        };
    }
}
