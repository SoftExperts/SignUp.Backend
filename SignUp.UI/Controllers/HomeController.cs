using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignUp.UI.Models;
using SignUp.UI.Services.Auth;
using System.Diagnostics;

namespace SignUp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger implementation.</param>
        /// <param name="authService">The authentication service implementation.</param>

        public HomeController(ILogger<HomeController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }


        #region Public Methods

        /// <summary>
        /// GET: Register
        /// Returns the registration view.
        /// </summary>
        public IActionResult Register()
        {
            return View();
        }

        // <summary>
        /// POST: Register
        /// Handles the registration form submission.
        /// </summary>
        /// <param name="user">The user object containing registration data.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // If the model state is not valid, return the view with the user object to display validation errors
                    return View(user);
                }

                var registrationResult = await _authService.RegisterUser(user);

                if (registrationResult.IsSuccessStatusCode)
                {
                    // Set success message in TempData if registration is successful
                    TempData["SuccessMessage"] = registrationResult.ReasonPhrase;
                }
                else
                {
                    // Set error message in TempData if registration fails
                    TempData["ErrorMessage"] = registrationResult.ReasonPhrase;
                }

                // Redirect to the Register view
                return View();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}