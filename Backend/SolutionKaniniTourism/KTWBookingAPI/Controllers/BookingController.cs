using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Models.DTO;
using KTWBookingAPI.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models;
using static UserAPI.Models.Error;

namespace KTWBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;   
        }

        //Add Booking
        [HttpPost]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booking>> AddBooking(BookingDTO book)
        {
            try
            {
                var booking = await _service.CreateBooking(book);
                if (booking)
                {
                    return Created("booking added Successfully!!!", booking);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Process Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToAddException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Add Customers
        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerDTO item)
        {
            try
            {
                var item1 = await _service.AddCustomer(item);
                if (item1 != null)
                {
                    return Created("Itinerary added Successfully!!!", item1);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Process Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToAddException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Get All Bookings
        [HttpGet]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Booking>>> GetAllBookings()
        {
            try
            {
                var bookings = await _service.GetAllBookings();
                if (bookings.Count >= 1)
                {
                    return Ok(bookings);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get User Bookings
        [HttpGet]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Booking>>> GetUserBookings(string userid)
        {
            try
            {
                var bookings = await _service.GetBookingById(userid);
                if (bookings.Count >= 1)
                {
                    return Ok(bookings);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Update Booking
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateBooking(UpdateBookingDTO bookingDTO)
        {
            try
            {
                var result = await _service.UpdateBooking(bookingDTO);
                if (result)
                {
                    return Ok("Booking Updated Successfully");
                }
            }
            catch (EmptyValueException nex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), nex.Message));
            }
            catch (UnableToUpdateException eex)
            {
                return NotFound(new Error((int)(ErrorCode.NotFound), eex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Details not updated!!!"));
        }

        //Update Customer Details
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> UpdateCustomers(CustomerDTO itemDTO)
        {
            try
            {
                var result = await _service.UpdateCustomer(itemDTO);
                if (result)
                {
                    return Ok("Updation Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Update Package Booking
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> UpdatePackage(PackageBooking item)
        {
            try
            {
                var result = await _service.UpdatePackageBooking(item);
                if (result)
                {
                    return Ok("Updation Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Remove Customer
        [HttpPut]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> DeleteTouraries(CustomerDTO customer)
        {
            try
            {
                var result = await _service.RemoveCustomer(customer);
                if (result != null)
                {
                    return Ok("Updation Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Cancel Booking
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CancelBooking(CancelPackDTO itemDTO)
        {
            try
            {
                var result = await _service.HandleTourPackageCancellation(itemDTO);
                if (result)
                {
                    return Ok("Updation Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Generate Price
        [HttpPost]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<double>> GeneratePrice(GeneratePriceDTO itemDTO)
        {
            try
            {
                var result = await _service.CalculateTourPackagePrice(itemDTO);
                if (result != 0)
                {
                    return Ok("Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Confirm Booking
        [HttpPost]
        [ProducesResponseType(typeof(BillDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BillDTO>> ConfirmBooking(UpdateStatusDTO itemDTO)
        {
            try
            {
                var result = await _service.GenerateBookingConfirmation(itemDTO);
                if (result != null)
                {
                    return Ok("Successfull!!!");
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Process Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToUpdateException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }
    }
}
