using AutoMapper;
using StudentMinimalAPI.Models;
using StudentMinimalAPI.Models.DTO;
using StudentMinimalAPI.Models.ViewModel;

namespace StudentMinimalAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentEntity>().ReverseMap();
            CreateMap<Student, StudentView>().ReverseMap();
        }
    }
}
