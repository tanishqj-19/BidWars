using AutoMapper;
using Azure;
using server.Dto;
using server.Models;

namespace server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PerformanceReport, ReportDto>().ReverseMap();
            CreateMap<Auction, AuctionDto>().ReverseMap();
        }

        
    }
}
