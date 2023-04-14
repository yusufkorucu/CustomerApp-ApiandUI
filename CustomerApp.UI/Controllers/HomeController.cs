using AspNetCoreHero.ToastNotification.Abstractions;
using CustomerApp.Ui.Services;
using CustomerApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerIntegrationService _service;
        private readonly INotyfService _toastNotification;

        public HomeController(ILogger<HomeController> logger, ICustomerIntegrationService service, INotyfService toastNotification)
        {
            _logger = logger;
            _service = service;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"{GetType().Name} Trigged");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _service.Login(model);

            if (result.Status)
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                _toastNotification.Success("Login Success");
                return RedirectToAction("Index", "Panel");
            }

            _toastNotification.Error(result.Data);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}