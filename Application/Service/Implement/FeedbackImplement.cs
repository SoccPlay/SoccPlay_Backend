using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestFeedback;
using Application.Model.Respone.ResponseFeedback;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Implement;

public class FeedbackImplement : FeedbackService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public FeedbackImplement(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseFeedback> CreateFeedBack(RequestFeedback requestFeedback)
    {
        var feedBack = _mapper.Map<Feedback>(requestFeedback);
        _unitOfWork.FeedBack.Add(feedBack);
        _unitOfWork.Save();
        return _mapper.Map<ResponseFeedback>(feedBack);
    }

    public async Task<List<ResponseFeedback>> GetFeedBackByLandId(Guid landId)
    {
        var feedBacks = await _unitOfWork.FeedBack.GetByFeedBackLandId(landId);
        var response = _mapper.Map<List<ResponseFeedback>>(feedBacks);
        return response;
    }
}