using KTWFeedbackAPI.Models;
using KTWFeedbackAPI.Models.DTO;

namespace KTWFeedbackAPI.Interfaces
{
    public interface IAdapter
    {
        Feedback ConvertToFeedbackAdapter(FeedbackDTO feedbackdto , int count);
    }
}
