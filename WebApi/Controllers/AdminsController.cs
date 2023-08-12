using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;


namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminsController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAccountAdmin>> PostAdmin(RequestAccountAdmin requestAccountAdmin)
        {
            var response = await _adminService.CreateAdmin(requestAccountAdmin);
            return response; 
        }

    }
}
