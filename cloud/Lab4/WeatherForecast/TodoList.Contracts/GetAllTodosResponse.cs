namespace TodoList.Contracts;
public class GetAllTodosResponse
{
    public ICollection<ItemModelBase> Items { get; init; } = null!;
}
