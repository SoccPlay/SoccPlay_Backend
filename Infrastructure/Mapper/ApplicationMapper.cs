using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Application.Common.Security.Token;
using Application.Model.Request.RequestAccount;
using Application.Model.Request.RequestLand;
using Application.Model.Request.RequestPitch;
using Application.Model.Respone;
using Application.Model.Respone.ResponseAccount;
using Application.Model.Respone.ResponsePitch;
using Application.Model.ResponseLand;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Mapper;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {

        // LOGIN REQUEST 

        CreateMap<RequestLogin, Account>()
                .ForMember(p => p.UserName, act => act.MapFrom(src => src.UserName))
                .ForMember(p => p.Password, act => act.MapFrom(src => src.Password));

        CreateMap<AccessToken, ResponseLogin>()
                .ForMember(p => p.Token, act => act.MapFrom(src => src.Token))
                .ForMember(p => p.Expiration, act => act.MapFrom(src => src.ExpirationTicks))
                .ForMember(p => p.RefreshToken, act => act.MapFrom(src => src.RefreshToken.Token));



        // Create ADMIN

        CreateMap<RequestAccountAdmin, Admin>()
            .ForMember(ad=>ad.FullName,act=>act.MapFrom(re=>re.FullName))
            .ForMember(ad=>ad.Email,act=>act.MapFrom(re=>re.Email))
            .ForMember(ad=>ad.Phone, act=>act.MapFrom(re=>re.Phone))
            .ForMember(ad=>ad.Address, act=>act.MapFrom(re=>re.Address))
            .ForPath(ad=>ad.Account.UserName, act=>act.MapFrom(re=>re.UserName))
            .ForPath(ad=>ad.Account.Password, act=>act.MapFrom(re=>re.Password));
        CreateMap<Admin, ResponseAccountAdmin>()
            .ForMember(ad=>ad.AdminId,act=>act.MapFrom(re=>re.AdminId))
            .ForMember(ad=>ad.AccountId,act=>act.MapFrom(re=>re.AccountId))
            .ForMember(ad=>ad.FullName,act=>act.MapFrom(re=>re.FullName))
            .ForMember(ad=>ad.Email,act=>act.MapFrom(re=>re.Email))
            .ForMember(ad=>ad.Phone, act=>act.MapFrom(re=>re.Phone))
            .ForMember(ad=>ad.Address, act=>act.MapFrom(re=>re.Address))
            .ForPath(ad=>ad.Role,act=>act.MapFrom(re=>re.Account.Role))
            .ForPath(ad=>ad.UserName,act=>act.MapFrom(re=>re.Account.UserName))
            .ForPath(ad=>ad.Password,act=>act.MapFrom(re=>re.Account.Password));

        
        
        //Land
        CreateMap<RequestLand, Land>()
            .ForMember(dest => dest.NameLand, opt => opt.MapFrom(src => src.NameLand))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Policy, opt => opt.MapFrom(src => src.Policy))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.TotalPitch, opt => opt.MapFrom(src => 0)) // Assign 0 to TotalPitch
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => LandStatus.Active.ToString()))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
        CreateMap<Land, ResponseLand>()
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.NameLand, opt => opt.MapFrom(src => src.NameLand))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.TotalPitch, opt => opt.MapFrom(src => src.TotalPitch))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
        
        
        //Pitch
        CreateMap<RequestPitch, Pitch>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PitchStatus.Active.ToString()))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

        CreateMap<Pitch, ResponsePitch>()
            .ForMember(dest => dest.PitchId, opt => opt.MapFrom(src => src.PitchId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
    }

}