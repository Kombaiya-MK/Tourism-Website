using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models.DTO;
using UserAPI.Models;
using UserManagementAPI.Services;
using System.Numerics;
using UserManagementAPI.Models;
using UserManagementAPI.Interfaces;
using static UserAPI.Models.Error;
using UserAPI.Services;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IManageUser _service;

        public UserController(IManageUser service)
        {
            _service = service;
        }

        //Shared Controllers
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO user)
        {
            try
            {
                var usr = await _service.Login(user);
                if (usr != null)
                {
                    return Created("Login Successfull!!!", usr);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Login Failed"));
            }
            catch (InvalidUserException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Register
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Register(UserDTO user)
        {
            try
            {
                var user1 = await _service.Register(user);
                if (user1 != null)
                {
                    return Created("Registration Successfull!!!", user1);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Registration Failed"));
            }
            catch (InvalidUserException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Get All Users
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<User>>> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUsers();
                if (users.Count >= 1)
                {
                    return Ok(users);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get All User Details

        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<User>>> GetAllUserDetails()
        {
            try
            {
                var users = await _service.GetAllUserDetails();
                if (users.Count >= 1)
                {
                    return Ok(users);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get All Travel Agents

        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<User>>> GetAllTravelAgents()
        {
            try
            {
                var agents = await _service.GetAllTravelAgents();
                if (agents.Count >= 1)
                {
                    return Ok(agents);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Change Password
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ChangePassword(PasswordDTO password)
        {
            try
            {
                var result = await _service.ChangePassword(password);
                if (result)
                {
                    return Ok("Password Changed Successfully");
                }
            }
            catch (NullValueException nex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), nex.Message));
            }
            catch(EmptyValueException eex)
            {
                return NotFound(new Error((int)(ErrorCode.NotFound), eex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Details not added!!!"));
        }


        //Validate Verification Code
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ValidateVerificationCode(ForgotPasswordDTO item)
        {
            try
            {
                var result = await _service.ValidateCode(item);
                if (result)
                {
                    return Created("Updation Successfull!!!", result);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (InvalidUserException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }

        //Trigger Email
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<ActionResult<bool>> TriggerEmail(ForgotPasswordDTO item)
        {
            //try
            //{
            //    var user1 = await _service.TriggerVerificationCodeToEmail(item);
            //    if (user1 != null)
            //    {
            //        return Created("Updation Successfull!!!", user1);
            //    }
            //    return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            //}
            //catch (InvalidUserException ex)
            //{
            //    return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            //}

            //catch (Exception ex)
            //{
            //    return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            //}
            throw new NotImplementedException();

        }


        //Update Password
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> UpdatePassword(UpdatePasswordDTO password)
        {
            try
            {
                var user1 = await _service.UpdatePassword(password);
                if (user1 != null)
                {
                    return Created("Updation Successfull!!!", user1);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Updation Failed"));
            }
            catch (InvalidUserException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }

        }
        //Admin Controllers
        //Approve agency request
        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> ApprovalOfAgency(ApproveAgentDTO item)
        {
            try
            {
                var agency = await _service.ApproveAgent(item);
                if (agency != null)
                {
                    return Ok(agency);
                }
                return NotFound(new Error(1, "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(2, ex.Message));
            }
        }

        //Add User Details 
        [HttpPost]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDetails>> AddUserDetails(UserDetails user)
        {
            try
            {
                if (user.Email == null)
                {
                    return NotFound(new Error((int)(ErrorCode.NotFound), "Email is null!!!"));
                    throw new NullValueException("Email is null");
                }
                var user1 = await _service.AddUserDetails(user);
                if (user1 != null)
                {
                    return Created("Details added Successfully!!!", user1);
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

        //Add Travel Agency
        [HttpPost]
        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgent>> AddAgency(TravelAgent travelAgent)
        {
            try
            {
                if (travelAgent.Email == null)
                {
                    return NotFound(new Error((int)(ErrorCode.NotFound), "Email is null!!!"));
                    throw new NullValueException("Email is null");
                }
                var agent = await _service.AddTravelAgency(travelAgent);
                if (agent != null)
                {
                    return Created("Details added Successfully!!!", agent);
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
    }
}
