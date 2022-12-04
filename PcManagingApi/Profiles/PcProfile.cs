using PcManagingApi.Data.Dtos;
using PcManagingApi.Models;
using AutoMapper;

namespace PcManagingApi.Profiles;

public class PcProfile : Profile
{
	public PcProfile()
	{
        CreateMap<CreatePcDto, Pc>();
        CreateMap<UpdatePcDto, Pc>();
        CreateMap<Pc, UpdatePcDto>();
        CreateMap<Pc, ReadPcDto>();
    }
}
