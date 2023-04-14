using CustomerApp.Api.Handlers.Command;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : CustomBaseController
{
    #region Methods

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    #endregion
}