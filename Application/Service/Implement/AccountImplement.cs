using Application.IRepository.IUnitOfWork;
using Application.Service;

namespace Infrastructure.Implement;

public class AccountImplement : AccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountImplement(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

}