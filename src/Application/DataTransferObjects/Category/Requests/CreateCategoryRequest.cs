namespace Application.DataTransferObjects.Category.Requests;

public class CreateCategoryRequest
{
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public int Status { get; set; }
}