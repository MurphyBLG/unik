namespace TodoList.Contracts;
public class ItemModelBase
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
}
