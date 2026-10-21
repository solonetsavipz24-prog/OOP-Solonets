namespace Lab12v2;

public class User : IEntity
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Username: {Username}, Email: {Email}, Active: {IsActive}";
    }
}
