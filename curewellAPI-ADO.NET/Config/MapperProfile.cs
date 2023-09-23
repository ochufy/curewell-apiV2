using AutoMapper;
using curewell.Entity.Models;
using curewellAPI_ADO.NET.DTOs;

namespace curewellAPI_ADO.NET.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SurgeryDto, Surgery>();
        }
    }
}
