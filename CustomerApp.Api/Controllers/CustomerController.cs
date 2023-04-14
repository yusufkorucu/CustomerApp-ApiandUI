using CustomerApp.Api.Handlers.Command;
using CustomerApp.Api.Handlers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        #region Methods

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerAddCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromQuery] GetCustomerByIdQuery query)
        {

            var result = await Mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomerById([FromBody] CustomerDeleteCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetCustomerByTckno")]
        public async Task<IActionResult> GetCustomerByTckno([FromQuery] GetCustomerByTcknoQuery query)
        {

            var result = await Mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }


        [HttpGet("GetFilteredCustomers")]
        public async Task<IActionResult> GetFilteredCustomers([FromQuery] GetFilteredCustomerQuery query)
        {

            var result = await Mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }


        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer([FromQuery] GetAllCustomerQuery query)
        {

            var result = await Mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        #endregion
    }
}
