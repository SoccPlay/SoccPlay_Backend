using Application.IRepository;

namespace  Application.IRepository.IUnitOfWork;

public interface IUnitOfWork
{
    IAccountRepository Account { get; }
    IAdminRepository Admin { get; }
    IBookingRepository Booking { get; }
    ICustomerRepository Customer { get; }
    IFeedBackRepository FeedBack { get; }
    ILandRepository Land { get; }
    IOwnerRepository Owner { get; }
    IPitchRepository Pitch { get; }
    IPitchImageRepository PitchImage { get; }
    IPriceRepository Price { get; }
    IScheduleRepository Schedule { get; }

    void Save();
}
