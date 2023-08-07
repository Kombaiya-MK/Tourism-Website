using KTWLocationsAPI.Interfaces;
using KTWLocationsAPI.Models;
using KTWLocationsAPI.Models.DTO;
using KTWLocationsAPI.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models;
using static UserAPI.Models.Error;

namespace KTWLocationsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }
        //Add Location
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddLocation(LocationDTO locationDTO)
        {
            try
            {
                var booking = await _service.AddLocation(locationDTO);
                if (booking)
                {
                    return Created("Location added Successfully!!!", booking);
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

        //Add Speciality
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddSpeciality(LocationSpecialityDTO locationSpeciality)
        {
            try
            {
                var item1 = await _service.AddSpeciality(locationSpeciality);
                if (item1)
                {
                    return Created("Speciality added Successfully!!!", item1);
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

        //Add Image
        [HttpPost]
        [ProducesResponseType(typeof(Image), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Image>> AddImage(ImageDTO img)
        {
            try
            {
                var item1 = await _service.AddImage(img);
                if (item1 != null)
                {
                    return Created("Image added Successfully!!!", item1);
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

        //Get All locations
        [HttpGet]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Location>>> GetAllLocations()
        {
            try
            {
                var locations = await _service.GetAllLocations();
                if (locations.Count >= 1)
                {
                    return Ok(locations);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get all nearby locations
        [HttpGet]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Location>>> GetAllNearByLocations(string location)
        {
            try
            {
                var locations = await _service.GetAllNearByLocations(location);
                if (locations.Count >= 1)
                {
                    return Ok(locations);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get Images
        [HttpGet]
        [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Image>>> GetImages(string location)
        {
            try
            {
                var images = await _service.GetImages(location);
                if (images.Count >= 1)
                {
                    return Ok(images);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get specialities
        [HttpGet]
        [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Image>>> GetSpecialities(string location)
        {
            try
            {
                var specialities = await _service.GetSpecialities(location);
                if (specialities.Count >= 1)
                {
                    return Ok(specialities);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }
    }
}
