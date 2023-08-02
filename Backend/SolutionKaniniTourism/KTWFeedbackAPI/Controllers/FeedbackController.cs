using KTWFeedbackAPI.Interfaces;
using KTWFeedbackAPI.Models;
using KTWFeedbackAPI.Models.DTO;
using KTWFeedbackAPI.Services.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static KTWFeedbackAPI.Models.Error;

namespace KTWFeedbackAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackServices _service;

        public FeedbackController(IFeedbackServices service)
        {
            _service = service;
        }

        //Get All Feedbacks
        [HttpGet]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Feedback>>> GetFeedbacks()
        {
            try
            {
                var Feedbacks = await _service.GetAllFeedback();
                if (Feedbacks.Count >= 1)
                {
                    return Ok(Feedbacks);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get All Categorized Feedbacks
        [HttpGet]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Feedback>>> GetCategorizedFeedbacks(string category)
        {
            try
            {
                var Feedbacks = await _service.GetCategorizedFeedback(category);
                if (Feedbacks.Count >= 1)
                {
                    return Ok(Feedbacks);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }


        //Add Feedbacks
        [HttpPost]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Feedback>> AddFeedback(FeedbackDTO dto)
        {
            try
            {
                if (dto.Email == null)
                {
                    return NotFound(new Error((int)(ErrorCode.NotFound), "Email is null!!!"));
                    throw new NullValueException("Email is null");
                }
                var feedback = await _service.AddFeedback(dto);
                if (feedback != null)
                {
                    return Created("Details added Successfully!!!", feedback);
                }
            }
            catch (UnableToAddException)
            {
                {
                    return BadRequest(new Error((int)(ErrorCode.BadRequest), "Duplicates not allowed!!!"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Details not added!!!"));
        }


        //Update(Edit) Feedback
        [HttpPut]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Feedback>> EditFeedback(UpdateFeedbackDTO dto)
        {
            try
            {
                var result = await _service.UpdateFeedback(dto);
                if (result != null)
                {
                    return Ok("Feedback Edited Successfully");
                }
            }
            catch (NullValueException nex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), nex.Message));
            }
            catch (EmptyValueException eex)
            {
                return NotFound(new Error((int)(ErrorCode.NotFound), eex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Feedback not Edited!!!"));
        }
    }
}
