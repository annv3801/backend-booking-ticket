using Application.Commands.Account;
using Application.Common.Models.Account;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using AutoMapper;
using Infrastructure.Profiles.Account.Resolvers;
using Infrastructure.Profiles.Resolvers;

namespace Infrastructure.Profiles.Account;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        // Map from create command to user entity
        CreateMap<CreateAccountCommand, Domain.Entities.Identity.Account>()
            // generate new id
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom<IdGeneratorResolver>())
            // set default gender as true
            .ForMember(d => d.Gender, opt => opt.MapFrom(src =>
                src.Gender ?? true))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src =>
                src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
            
            .ForMember(d => d.Status, opt => opt.MapFrom<AccountDefaultStatusResolver>())
            .ForMember(d => d.SecurityStamp,
                opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.PasswordChangeRequired, opt => opt.MapFrom(src => false))
            // .ForMember(d => d.Otp, opt => opt.MapFrom<SmsOtpResolver>())
            // .ForMember(d => d.OtpValidEnd, opt => opt.MapFrom<SmsOtpValidEndResolver>())
            ;

        // Map from create request to create command
        CreateMap<CreateAccountRequest, CreateAccountCommand>();
        CreateMap<Domain.Entities.Identity.Account, ViewAccountResponse>();

        // Map last modified by and created by
        CreateMap<Domain.Entities.Identity.Account, CreatedByView>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AvatarPhoto, o => o.MapFrom(s => s.AvatarPhoto))
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName));
        CreateMap<Domain.Entities.Identity.Account, LastModifiedByView>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AvatarPhoto, o => o.MapFrom(s => s.AvatarPhoto))
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName));

        CreateMap<Domain.Entities.Identity.Account, ViewAccountResponse>();

        // Map 2 account for update case, some field will be ignored
        CreateMap<Domain.Entities.Identity.Account, Domain.Entities.Identity.Account>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.AvatarPhoto, opt => opt.MapFrom(src => src.AvatarPhoto))
            .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.Gender))
            ;

        CreateMap<UpdateAccountRequest, UpdateAccountCommand>();
        CreateMap<UpdateAccountCommand, Domain.Entities.Identity.Account>()
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            ;

        CreateMap<ChangePasswordRequest, ChangePasswordCommand>().ReverseMap();

        CreateMap<SignInWithPhoneNumberCommand, SignInWithPhoneNumberRequest>().ReverseMap();
    }
}
