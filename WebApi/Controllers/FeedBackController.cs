using Application.Model.Request.RequestFeedback;
using Application.Model.Response.ResponseFeedback;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FeedBackController : ControllerBase
{
    private readonly FeedbackService _feedbackService;

    public FeedBackController(FeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseFeedback>> CreateFeedBack(RequestFeedback request)
    {
        var create = await _feedbackService.CreateFeedBack(request);
        return Ok(create);
    }


    [HttpGet]
    public async Task<ActionResult<List<ResponseFeedback>>> GetFeedBackByLandId(Guid id)
    {
        var bookings = await _feedbackService.GetFeedBackByLandId(id);
        return Ok(bookings);
    }
}