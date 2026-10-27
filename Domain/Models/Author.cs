namespace Domain.Models;

public class Author
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int BirthYear { get; set; }
}
