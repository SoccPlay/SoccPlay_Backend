using Application.Model.Request.RequestFeedback;
using Application.Model.Response.ResponseFeedback;

namespace Application.Service;

public interface FeedbackService
{
    Task<ResponseFeedback> CreateFeedBack(RequestFeedback requestFeedback);

    Task<List<ResponseFeedback>> GetFeedBackByLandId(Guid landId);
}