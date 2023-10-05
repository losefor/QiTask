using AutoMapper;
using QiTask.Dtos;
using QiTask.Models;

namespace qi_task2.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, RegisterDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<NoteDto, Note>();
        CreateMap<Note, NoteDto>();
    }
}