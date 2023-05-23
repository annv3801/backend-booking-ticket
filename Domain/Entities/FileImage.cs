using Domain.Entities.Identity;

namespace Domain.Entities;

public class FileImage
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
    public string? Path { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}