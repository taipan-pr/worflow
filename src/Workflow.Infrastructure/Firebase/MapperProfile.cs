using AutoMapper;
using FirebaseAdmin.Auth;
using Workflow.Domain.DataTransferObjects.Identity;

namespace Workflow.Infrastructure.Firebase;

internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserRecord, Identity>()
            .AfterMap((src, dest) =>
            {
                dest.Id = src.Uid;
                dest.CreatedDateTime = src.UserMetaData.CreationTimestamp ?? null;
                dest.LastSignInDateTime = src.UserMetaData.LastSignInTimestamp ?? null;
            });
    }
}
