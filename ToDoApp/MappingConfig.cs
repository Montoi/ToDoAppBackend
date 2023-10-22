using AutoMapper;
using ToDoApp.Models;
using ToDoApp.Models.Dto;

namespace ToDoApp
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteCreateDto>().ReverseMap();
            CreateMap<Note, NoteUpdateDto>().ReverseMap();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<Color, ColorCreateDto>().ReverseMap();
            CreateMap<Color, ColorUpdateDto>().ReverseMap();

            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

        }
    }
}
