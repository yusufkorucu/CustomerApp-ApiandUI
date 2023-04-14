using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Api.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        protected IMediator? Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? mediator;


    }
}
