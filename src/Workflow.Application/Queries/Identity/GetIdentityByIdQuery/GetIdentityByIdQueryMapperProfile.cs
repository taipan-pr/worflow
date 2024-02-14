using AutoMapper;
using Workflow.Domain.RequestResponse.Identity;

namespace Workflow.Application.Queries.Identity.GetIdentityByIdQuery;

internal class GetIdentityByIdQueryMapperProfile : Profile
{
    public GetIdentityByIdQueryMapperProfile()
    {
        CreateMap<Domain.DataTransferObjects.Identity.Identity, GetIdentityByIdQueryResponse>();
        CreateMap<GetIdentityByIdQueryResponse, IdentityResponse>();
    }
}
