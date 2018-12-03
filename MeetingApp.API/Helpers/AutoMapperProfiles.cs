using System.Linq;
using AutoMapper;
using MeetingApp.API.Dtos;
using MeetingApp.API.Models;

namespace MeetingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //ForMemeber podes especificar que campo se puede modificar si no concuerda con el modelo
        //
        public AutoMapperProfiles()
        {
            CreateMap<Usuario, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(dest=>dest.Age,opt=>{
                    opt.ResolveUsing(d=>d.DateOfBirth.CalculateAge());
                });
            CreateMap<Usuario, UserForDetailedDto>().ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                    
                })
                .ForMember(dest=>dest.Age,opt=>{
                    opt.ResolveUsing(d=>d.DateOfBirth.CalculateAge());
                });
            //automappear desde el controlador    
            CreateMap<Photos, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, Usuario>();
            CreateMap<Photos, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto,Photos>();

        }
    }
}