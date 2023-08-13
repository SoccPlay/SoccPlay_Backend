using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;


namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly CustomerService _customerService;
        private readonly OwnerService _ownerService;

        public RegisterController(AdminService adminService, 
            CustomerService customerService, OwnerService ownerService)
        {
            _adminService = adminService;
            _customerService = customerService;
            _ownerService = ownerService;
        }

        [HttpPost]
        public async Task<ResponseAccountAdmin> PostAdmin(RequestAccountAdmin request)
        {
            return await _adminService.CreateAdmin(request);
        }
        [HttpPost]
        public async Task<ResponseAccountOwner> PostOwner(RequestAccountOwner request)
        {
            return await _ownerService.CreateOwner(request);
        }

        [HttpPost]
        public async Task<ResponseAccountCustomer> PostCustomer(RequestAccountCustomer request)
        {
            return await _customerService.CreateCustomer(request);
        }
    }
}
