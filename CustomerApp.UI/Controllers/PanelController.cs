using AspNetCoreHero.ToastNotification.Abstractions;
using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Ui.Models;
using CustomerApp.Ui.Services;
using CustomerApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomerApp.Ui.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        #region Field
        private readonly ILogger<PanelController> _logger;
        private readonly ICustomerIntegrationService _service;
        private readonly INotyfService _toastNotification;
        #endregion

        #region Ctor
        public PanelController(ILogger<PanelController> logger, ICustomerIntegrationService service, INotyfService toastNotification)
        {
            _logger = logger;
            _service = service;
            _toastNotification = toastNotification;
        }
        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");

            var result = await _service.GetAllCustomer(token);

            if (result.Status)
                return View(result.Data);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CustomerGetFilteredViewModel model)
        {
            var result = new CustomerDetailResponseDto<List<CustomerDetailsDto>>();
            var token = HttpContext.Session.GetString("JWToken");

            if (model != null)
                result = await _service.GetFilteredCustomer(model, token);

            else
                result = await _service.GetAllCustomer(token);

            if (result.Status)
                return View(result.Data);

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var result = await _service.DeleteCustomer(id, token);
            if (result.Status)
            {
                _toastNotification.Success(CoreMessage.DeleteCustomerSuccessfuly);
                return RedirectToAction("Index", "Panel");
            }

            _toastNotification.Error(CoreMessage.FailAddedCustomer);

            return RedirectToAction("Index", "Panel");

        }


        public async Task<IActionResult> AddCustomer()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerAddViewModel model)
        {
            var token = HttpContext.Session.GetString("JWToken");

            var result = await _service.CreateCustomer(model, token);

            if (result.Status)
            {
                _toastNotification.Success(result.Data);
                return RedirectToAction("Index", "Panel");
            }

            _toastNotification.Error(result.Data);
            return View();
        }
        #endregion
    }
}
