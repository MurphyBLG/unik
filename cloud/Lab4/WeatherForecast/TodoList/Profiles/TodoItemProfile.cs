using AutoMapper;
using TodoList.Contracts;
using TodoList.Core;

namespace TodoList.Profiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItem, TodoItem>();
    }
}
