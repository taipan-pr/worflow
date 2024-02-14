using AutoMapper;
using Workflow.Domain.DataTransferObjects.Identity;
using Workflow.Domain.RequestResponse.Identity;

namespace Workflow.Application.Commands.Identity.RegisterCommand;

internal class RegisterCommandMappingProfile : Profile
{
    public RegisterCommandMappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommandRequest>();
        CreateMap<RegisterCommandRequest, CreateIdentity>();
        CreateMap<Domain.DataTransferObjects.Identity.Identity, RegisterCommandResponse>();
        CreateMap<RegisterCommandResponse, RegisterResponse>();
    }
}
