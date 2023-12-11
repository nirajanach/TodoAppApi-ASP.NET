using AutoMapper;
using TodoApps.Models;
using TodoApps.ViewModels;

namespace TodoApps.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoViewModel>();
    }
}
