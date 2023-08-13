using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;

namespace Application.Service;

public interface CustomerService
{
    Task<ResponseAccountCustomer> CreateCustomer(RequestAccountCustomer request);
}