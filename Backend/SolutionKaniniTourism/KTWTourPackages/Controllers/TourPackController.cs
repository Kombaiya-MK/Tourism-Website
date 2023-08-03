using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using KTWTourPackages.Models.DTO;
using KTWTourPackages.Services.Commands;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models;
using static UserAPI.Models.Error;

namespace KTWTourPackages.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class TourPackController : ControllerBase
    {
        private readonly ITourPackService _service;

        public TourPackController(ITourPackService service)
        {
            _service = service;
        }


        //AddPackages
        [HttpPost]
        [ProducesResponseType(typeof(TourPackage), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PackageDTO>> AddPack(PackageDTO pack)
        {
            try
            {
                var package = await _service.AddPackage(pack);
                if (package != null)
                {
                    return Created("Package added Successfully!!!", package);
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

        //Add Itineraries
        [HttpPost]
        [ProducesResponseType(typeof(ItineraryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItineraryItem>> AddItineraryItem(ItemDTO item)
        {
            try
            {
                var item1 = await _service.AddItinerary(item);
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

        //Get All Packages
        [HttpGet]
        [ProducesResponseType(typeof(TourPackage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<TourPackage>>> GetAllPacks()
        {
            try
            {
                var packs = await _service.GetTourPackages();
                if (packs.Count >= 1)
                {
                    return Ok(packs);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get All Itineraries

        [HttpGet]
        [ProducesResponseType(typeof(Itinerary), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Itinerary>>> GetAllTouraries(string packid)
        {
            try
            {
                var itineraries = await _service.GetItineraries(packid);
                if (itineraries.Count >= 1)
                {
                    return Ok(itineraries);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Update Package
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdatePack(UpdatePackDTO packDTO)
        {
            try
            {
                var result = await _service.UpdatePackage(packDTO);
                if (result != null)
                {
                    return Ok("Password Changed Successfully");
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
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Details not added!!!"));
        }

        //Update Itineraries
        [HttpPut]
        [ProducesResponseType(typeof(ItineraryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItineraryItem>> UpdateTouraries(ItemDTO itemDTO)
        {
            try
            {
                var result = await _service.UpdateItinerary(itemDTO);
                if (result != null)
                {
                    return Created("Updation Successfull!!!", result);
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

        //Delete Itineraries
        [HttpPut]
        [ProducesResponseType(typeof(ItineraryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItineraryItem>> DeleteTouraries(UpdateItemDTO itemDTO)
        {
            try
            {
                var result = await _service.DeleteItinerary(itemDTO);
                if (result != null)
                {
                    return Created("Updation Successfull!!!", result);
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

        //Delete Packages
        [HttpPut]
        [ProducesResponseType(typeof(ItineraryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItineraryItem>> DeletePacks(UpdatePackStatusDTO itemDTO)
        {
            try
            {
                var result = await _service.DeletePackage(itemDTO);
                if (result != null)
                {
                    return Created("Updation Successfull!!!", result);
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
    }
}
