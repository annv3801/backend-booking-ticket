namespace Application.DataTransferObjects.Account.Requests;

public class CreateAndUpdateAccountCategoryRequest
{
    public ICollection<long> CategoryIds { get; set; }
}