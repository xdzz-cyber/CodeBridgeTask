using Application.Common.Mappings;
using AutoMapper;
using Domain;

namespace Application.Common.Dtos;

public class DogDto : IMapWith<Domain.Dog>
{
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
    public int TailLength { get; set; }
    public int Weight { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Dog, DogDto>()
            .ForMember(m => m.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(m => m.Color, opt => opt.MapFrom(s => s.Color))
            .ForMember(m => m.TailLength, opt => opt.MapFrom(s => s.TailLength))
            .ForMember(m => m.Weight, opt => opt.MapFrom(s => s.Weight));
    }
}