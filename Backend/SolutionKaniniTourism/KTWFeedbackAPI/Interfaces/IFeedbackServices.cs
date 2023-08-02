using KTWFeedbackAPI.Models;
using KTWFeedbackAPI.Models.DTO;

namespace KTWFeedbackAPI.Interfaces
{
    public interface IFeedbackServices
    {
        Task<FeedbackDTO> AddFeedback(FeedbackDTO dto);
        Task<Feedback> UpdateFeedback(UpdateFeedbackDTO dto);

        Task<ICollection<Feedback>> GetAllFeedback();

        Task<ICollection<Feedback>> GetCategorizedFeedback(string servicename);
    }
}
