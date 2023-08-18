using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Infrastructure.Entities;

namespace Infrastructure.RepositoryImp.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly FootBall_PitchContext _context;

    public UnitOfWork(FootBall_PitchContext context)
    {
        // ReSharper disable once RedundantAssignment
        _context = context;
        Account = new AccountRepository(_context);
        Admin = new AdminRepository(_context);
        Booking = new BookingRepository(_context);
        Customer = new CustomerRepository(_context);
        FeedBack = new FeedBackRepository(_context);
        Land = new LandRepository(_context);
        Owner = new OwnerRepository(_context);
        Pitch = new PitchRepository(_context);
        PitchImage = new PitchImageRepository(_context);
        Price = new PriceRepository(_context);
        Schedule = new ScheduleRepository(_context);
    }

    public IAccountRepository Account { get; }
    public IAdminRepository Admin { get; }
    public IBookingRepository Booking { get; }
    public ICustomerRepository Customer { get; }
    public IFeedBackRepository FeedBack { get; }
    public ILandRepository Land { get; }
    public IOwnerRepository Owner { get; }
    public IPitchRepository Pitch { get; }
    public IPitchImageRepository PitchImage { get; }
    public IPriceRepository Price { get; }
    public IScheduleRepository Schedule { get; }

    public void Save()
    {
        _context.SaveChanges();
    }
}