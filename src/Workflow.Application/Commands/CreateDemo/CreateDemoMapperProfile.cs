using AutoMapper;

namespace Workflow.Application.Commands.CreateDemo;

internal class CreateDemoMapperProfile : Profile
{
    public CreateDemoMapperProfile()
    {
        CreateMap<Domain.DataTransferObjects.DemoRequest, CreateDemoRequest>();
        CreateMap<CreateDemoResponse, Domain.DataTransferObjects.DemoResponse>();
    }
}
