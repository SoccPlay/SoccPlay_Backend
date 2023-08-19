using Application.Common.Security.Token;
using Application.Model.Request.RequestAccount;
using Application.Model.Request.RequestBooking;
using Application.Model.Request.RequestFeedback;
using Application.Model.Request.RequestFile;
using Application.Model.Request.RequestLand;
using Application.Model.Request.RequestPitch;
using Application.Model.Request.RequestPrice;
using Application.Model.Response.ResponseAccount;
using Application.Model.Response.ResponseBooking;
using Application.Model.Response.ResponseFeedback;
using Application.Model.Response.ResponseFile;
using Application.Model.Response.ResponsePitch;
using Application.Model.Response.ResponsePrice;
using Application.Model.Response.ResponseSchedule;
using Application.Model.ResponseLand;
using AutoMapper;
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
            .ForMember(ad => ad.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(ad => ad.Email, act => act.MapFrom(re => re.Email))
            .ForMember(ad => ad.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(ad => ad.Address, act => act.MapFrom(re => re.Address))
            .ForPath(ad => ad.Account.UserName, act => act.MapFrom(re => re.UserName))
            .ForPath(ad => ad.Account.Password, act => act.MapFrom(re => re.Password));
        CreateMap<Admin, ResponseAccountAdmin>()
            .ForMember(ad => ad.AdminId, act => act.MapFrom(re => re.AdminId))
            .ForMember(ad => ad.AccountId, act => act.MapFrom(re => re.AccountId))
            .ForMember(ad => ad.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(ad => ad.Email, act => act.MapFrom(re => re.Email))
            .ForMember(ad => ad.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(ad => ad.Address, act => act.MapFrom(re => re.Address))
            .ForPath(ad => ad.Role, act => act.MapFrom(re => re.Account.Role))
            .ForPath(ad => ad.UserName, act => act.MapFrom(re => re.Account.UserName));

        // CREATE CUSTOMER
        CreateMap<RequestAccountCustomer, Customer>()
            .ForMember(customer => customer.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(customer => customer.Email, act => act.MapFrom(re => re.Email))
            .ForMember(customer => customer.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(customer => customer.Address, act => act.MapFrom(re => re.Address))
            .ForPath(customer => customer.Account.UserName, act => act.MapFrom(re => re.UserName))
            .ForPath(customer => customer.Account.Password, act => act.MapFrom(re => re.Password));
        CreateMap<Customer, ResponseAccountCustomer>()
            .ForMember(customer => customer.CustomerId, act => act.MapFrom(re => re.CustomerId))
            .ForMember(customer => customer.AccountId, act => act.MapFrom(re => re.AccountId))
            .ForMember(customer => customer.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(customer => customer.Email, act => act.MapFrom(re => re.Email))
            .ForMember(customer => customer.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(customer => customer.Address, act => act.MapFrom(re => re.Address))
            .ForPath(customer => customer.Role, act => act.MapFrom(re => re.Account.Role))
            .ForPath(customer => customer.UserName, act => act.MapFrom(re => re.Account.UserName));

        //CREATE OWNER
        CreateMap<RequestAccountOwner, Owner>()
            .ForMember(owner => owner.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(owner => owner.Email, act => act.MapFrom(re => re.Email))
            .ForMember(owner => owner.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(owner => owner.Address, act => act.MapFrom(re => re.Address))
            .ForPath(owner => owner.Account.UserName, act => act.MapFrom(re => re.UserName))
            .ForPath(owner => owner.Account.Password, act => act.MapFrom(re => re.Password));
        CreateMap<Owner, ResponseAccountOwner>()
            .ForMember(owner => owner.OwnerId, act => act.MapFrom(re => re.OwnerId))
            .ForMember(owner => owner.AccountId, act => act.MapFrom(re => re.AccountId))
            .ForMember(owner => owner.FullName, act => act.MapFrom(re => re.FullName))
            .ForMember(owner => owner.Email, act => act.MapFrom(re => re.Email))
            .ForMember(owner => owner.Phone, act => act.MapFrom(re => re.Phone))
            .ForMember(owner => owner.Address, act => act.MapFrom(re => re.Address))
            .ForPath(owner => owner.Role, act => act.MapFrom(re => re.Account.Role))
            .ForPath(owner => owner.UserName, act => act.MapFrom(re => re.Account.UserName));

        //Land
        CreateMap<RequestLand, Land>()
            .ForMember(dest => dest.NameLand, opt => opt.MapFrom(src => src.NameLand))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Policy, opt => opt.MapFrom(src => src.Policy))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => "KM"))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.TotalPitch, opt => opt.MapFrom(src => 0)) // Assign 0 to TotalPitch
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => LandStatus.Active.ToString()))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));


        CreateMap<Land, ResponseLand_2>()
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.NameLand, opt => opt.MapFrom(src => src.NameLand))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.TotalPitch, opt => opt.MapFrom(src => src.TotalPitch))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

        CreateMap<Land, ResponseLand>()
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.NameLand, opt => opt.MapFrom(src => src.NameLand))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.TotalPitch, opt => opt.MapFrom(src => src.TotalPitch))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.AverageRate,
                opt => opt.MapFrom(src => src.Feedbacks.Any() ? src.Feedbacks.Average(rate => rate.Rate) : 0.0))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.MinPrice,
                opt => opt.MapFrom(src => src.Prices.Any() ? src.Prices.Min(price => price.Price1) : 0))
            .ForMember(dest => dest.MaxPrice,
                opt => opt.MapFrom(src => src.Prices.Any() ? src.Prices.Max(price => price.Price1) : 0))
            .ForPath(dest => dest.PitchImages,
                opt => opt.MapFrom(src => src.Images.Any() ? src.Images.Select(i => i.Name).ToList() : null))
            .ForPath(dest => dest.image, opt => opt.MapFrom(src => src.Images.Any() ? src.Images.Last().Name : null));

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

        CreateMap<Pitch, ResponsePitchV2>()
            .ForMember(dest => dest.PitchId, opt => opt.MapFrom(src => src.PitchId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId));


        //Price
        CreateMap<RequestPrice, Price>()
            .ForMember(dest => dest.StarTime, opt => opt.MapFrom(src => src.StarTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.LandLandId, opt => opt.MapFrom(src => src.LandLandId));

        CreateMap<Price, ResponsePrice>()
            .ForMember(dest => dest.PriceId, opt => opt.MapFrom(src => src.PriceId))
            .ForMember(dest => dest.StarTime, opt => opt.MapFrom(src => src.StarTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.LandLandId, opt => opt.MapFrom(src => src.LandLandId));


        //Booking
        CreateMap<RequestBooking, Booking>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Active.ToString()))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId));

        CreateMap<RequestBookingV2, Booking>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Active.ToString()))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<RequestBooking_v3, Booking>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Active.ToString()))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));


        CreateMap<Booking, ResponseBooking>()
            .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Land.Location))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => src.DateBooking))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Booking, ResponseBooking_v2>()
            .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Land.Location))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => src.DateBooking))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Booking, ResponseManageBooking>()
            .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Land.Location))
            .ForMember(dest => dest.DateBooking, opt => opt.MapFrom(src => src.DateBooking))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Land.NameLand));

        //Schedule
        CreateMap<Schedule, ResponseSchedule>()
            .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId))
            .ForMember(dest => dest.StarTime, opt => opt.MapFrom(src => src.StarTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PitchPitchId, opt => opt.MapFrom(src => src.PitchPitchId));

        CreateMap<Schedule, ResponseSchedule_v2>()
            .ForMember(dest => dest.StarTime,
                opt => opt.MapFrom(src => src != null ? src.StarTime.TimeOfDay : TimeSpan.Zero))
            .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(src => src != null ? src.EndTime.TimeOfDay : TimeSpan.Zero));

        //File
        CreateMap<RequestFile, PitchImage>()
            .ForMember(f => f.LandId, act => act.MapFrom(a => a.LandId))
            .ForMember(f => f.Name, act => act.MapFrom(a => a.ImageLogo));

        CreateMap<PitchImage, ResponseFile>()
            .ForMember(p => p.Name, act => act.MapFrom(a => a.Name))
            .ForPath(p => p.OwnerId, act => act.MapFrom(a => a.Land.OwnerId))
            .ForMember(p => p.LandId, act => act.MapFrom(a => a.LandId))
            .ForMember(p => p.PitchImageId, act => act.MapFrom(a => a.ImageId));

        //FeedBack
        CreateMap<RequestFeedback, Feedback>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Feedback, ResponseFeedback>()
            .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.FeedbackId))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));
    }
}