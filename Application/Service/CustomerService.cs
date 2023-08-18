using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;

namespace Application.Service;

public interface CustomerService
{
    Task<ResponseAccountCustomer> CreateCustomer(RequestAccountCustomer request);
}