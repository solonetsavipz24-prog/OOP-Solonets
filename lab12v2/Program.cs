namespace Lab12v2;

class Program
{
    static void Main()
    {
        var repository = new InMemoryRepository<User>();

        repository.Add(new User
        {
            Id = 1,
            Username = "artem",
            Email = "artem@example.com",
            IsActive = true
        });

        repository.Add(new User
        {
            Id = 2,
            Username = "student",
            Email = "student@example.com",
            IsActive = false
        });

        Console.WriteLine("=== Усі користувачі ===");
        foreach (User user in repository.GetAll())
            Console.WriteLine(user);

        Console.WriteLine("\n=== GetById(1) ===");
        Console.WriteLine(repository.GetById(1));

        repository.Update(new User
        {
            Id = 2,
            Username = "student_updated",
            Email = "updated@example.com",
            IsActive = true
        });

        Console.WriteLine("\n=== Після Update ===");
        foreach (User user in repository.GetAll())
            Console.WriteLine(user);

        repository.Delete(1);

        Console.WriteLine("\n=== Після Delete(1) ===");
        foreach (User user in repository.GetAll())
            Console.WriteLine(user);
    }
}
