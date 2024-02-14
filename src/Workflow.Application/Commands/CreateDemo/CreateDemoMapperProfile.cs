using AutoMapper;
using Workflow.Domain.RequestResponse.WeatherForecast;

namespace Workflow.Application.Commands.CreateDemo;

internal class CreateDemoMapperProfile : Profile
{
    public CreateDemoMapperProfile()
    {
        CreateMap<DemoRequest, CreateDemoRequest>();
        CreateMap<CreateDemoResponse, DemoResponse>();
    }
}
