using AutoMapper;
using Workflow.Domain.DataTransferObjects.Identity;
using Workflow.Domain.RequestResponse.Identity;

namespace Workflow.Application.Commands.RegisterCommand;

internal class RegisterCommandMappingProfile : Profile
{
    public RegisterCommandMappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommandRequest>();
        CreateMap<RegisterCommandRequest, CreateIdentity>();
        CreateMap<CreateIdentityResult, RegisterCommandResponse>();
        CreateMap<RegisterCommandResponse, RegisterResponse>();
    }
}
