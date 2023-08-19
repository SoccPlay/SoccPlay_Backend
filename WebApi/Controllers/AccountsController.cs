using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;
using Application.Service;
using Domain.Entities;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly FootBall_PitchContext _context;

    public AccountsController(FootBall_PitchContext context, AccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    // GET: api/Accounts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        if (_context.Accounts == null) return NotFound();

        return await _context.Accounts.ToListAsync();
    }

    // GET: api/Accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(Guid id)
    {
        if (_context.Accounts == null) return NotFound();

        var account = await _context.Accounts.FindAsync(id);

        if (account == null) return NotFound();

        return account;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<List<ResponseAccountCustomer>>> GetCustomerByAccountId(Guid id)
    {
        var bookings = await _accountService.GetCustomer(id);
        return Ok(bookings);
    }


    [HttpGet]
    public async Task<ActionResult<List<ResponseAccountOwner>>> GetOwnerByAccountId(Guid id)
    {
        var bookings = await _accountService.GetOwner(id);
        return Ok(bookings);
    }


    [HttpGet]
    public async Task<ActionResult<List<ResponseAccountAdmin>>> GetAdminByAccountId(Guid id)
    {
        var bookings = await _accountService.GetAdmin(id);
        return Ok(bookings);
    }
    
    [HttpPut]
    public async Task<ActionResult<ResponseAccountCustomer>> UpdateProfileOfCustomer(RequestUpdateProfile requestUpdateProfile)
    {
        var bookings = await _accountService.UpdateProfileCustomer(requestUpdateProfile);
        return Ok(bookings);
    }

    // PUT: api/Accounts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(Guid id, Account account)
    {
        if (id != account.AccountId) return BadRequest();

        _context.Entry(account).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AccountExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Accounts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        if (_context.Accounts == null) return Problem("Entity set 'SanBongContext.Accounts'  is null.");

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
    }

    // DELETE: api/Accounts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        if (_context.Accounts == null) return NotFound();

        var account = await _context.Accounts.FindAsync(id);
        if (account == null) return NotFound();

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AccountExists(Guid id)
    {
        return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
    }
}