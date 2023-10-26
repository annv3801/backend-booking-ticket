using Application.Interface;
using AutoMapper;

namespace Infrastructure.Profiles.Account.Resolvers;

public class IdGeneratorResolver : IValueResolver<object, object, long>
{
    private readonly ISnowflakeIdService _snowflakeIdService;

    public IdGeneratorResolver(ISnowflakeIdService snowflakeIdService)
    {
        _snowflakeIdService = snowflakeIdService;
    }


    public long Resolve(object source, object destination, long destMember, ResolutionContext context)
    {
        return _snowflakeIdService.GenerateId().Result;
    }
}