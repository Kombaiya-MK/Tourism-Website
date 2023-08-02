using KTWFeedbackAPI.Interfaces;
using KTWFeedbackAPI.Models;
using KTWFeedbackAPI.Models.DTO;

namespace KTWFeedbackAPI.Utilities.Adapters
{
    public class FeedbackDTOtoFeedback : IAdapter
    {
        public Feedback ConvertToFeedbackAdapter(FeedbackDTO feedbackdto , int count)
        {
            var feedback = new Feedback()
            {
                FeedbackId = "FB00" + count.ToString(),
                Email = feedbackdto.Email,
                ServiceName = feedbackdto.Servicename,
                PackId = feedbackdto.PackId,
                ServiceType = feedbackdto.ServiceType,
                Rating = feedbackdto.Rating,
                Review = feedbackdto.Review,
                FeedbackDate = DateTime.Now
            };
            return feedback;
        }
    }
}
