using AutoMapper;
using TodoList.Contracts;
using TodoList.Core;

namespace TodoList.Profiles;

public class ItemModelBaseProfile : Profile
{
    public ItemModelBaseProfile()
    {
        CreateMap<TodoItem, ItemModelBase>();
    }
}
