using KTWFeedbackAPI.Interfaces;
using KTWFeedbackAPI.Models;
using KTWFeedbackAPI.Models.DTO;

namespace KTWFeedbackAPI.Services
{
    public class FeedbackService : IFeedbackServices
    {
        private readonly ICommandRepo<Feedback, string> _cmdrepo;
        private readonly IQueryRepo<Feedback, string> _qryrepo;
        private readonly IAdapter _adapter;

        public FeedbackService(ICommandRepo<Feedback,string> cmdRepo , IQueryRepo<Feedback,string> queryRepo,
            IAdapter adapter)
        {
            _cmdrepo = cmdRepo;
            _qryrepo = queryRepo;
            _adapter = adapter;
        }
        public async Task<FeedbackDTO> AddFeedback(FeedbackDTO dto)
        {
            int count = await GetFeedbackCount();
            var feedback = _adapter.ConvertToFeedbackAdapter(dto, count+1);
            await _cmdrepo.Add(feedback);
            return dto;
        }

        public async Task<ICollection<Feedback>> GetAllFeedback()
        {
            return await _qryrepo.GetAll();
        }

        public async Task<ICollection<Feedback>> GetCategorizedFeedback(string servicename)
        {
            var feedbacks = await _qryrepo.GetAll();
            var categorizedfeedbacks = feedbacks.Where(x => x.ServiceName == servicename).ToList();
            return categorizedfeedbacks;
        }

        public async Task<Feedback> UpdateFeedback(UpdateFeedbackDTO dto)
        {
            Feedback feedback = new()
            {
                Email = dto.Email,
                ServiceName = dto.Servicename,
                PackId = dto.PackId,
                Review = dto.Description
            };
            return await _cmdrepo.Update(feedback);
        }

        private async Task<int> GetFeedbackCount()
        {
            var feedbacks = await _qryrepo.GetAll();
            return feedbacks.Count;
        }
    }
}
