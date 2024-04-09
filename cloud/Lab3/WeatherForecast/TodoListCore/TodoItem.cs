namespace TodoList.Core;
public class TodoItem
{
    public Guid Id { get; init; }
    public string Title { get; set; } = null!;
}
