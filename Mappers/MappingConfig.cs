using AutoMapper;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using System.Net.NetworkInformation;

namespace MagicVilla_DB.Mappers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => { 
                config.CreateMap<TownRequest, Town>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

                config.CreateMap<VillaRequest, Villa>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Town, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                ;

            });

            return mappingConfig;
        }
    }
}
