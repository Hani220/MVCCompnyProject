
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.IKEA.DAL.Entities.Identity;
using Route.IKEA.PL.ViewModels.Identity;

namespace Route.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager <ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) 
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		

		#region Sign Up
		//Get : AccountController/SignUp 
		public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.UserName);

			if (user is { })
			{
				ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This Username is Already Used By Another Account");
				return View(model);
			}
			
				user = new ApplicationUser()
				{
					FName = model.FirstName,
					LName = model.LastName,
					UserName = model.UserName,
					Email = model.Email,
					IsAgree = model.IsAgree
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
					return RedirectToAction(nameof(SignIn));

				foreach (var error in result.Errors)
					return RedirectToAction(string.Empty, error.Description);

			return View(model);

		}



        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }

		[HttpPost]
		public async Task  <IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is { })
			{
				var checkPassword =await _userManager.CheckPasswordAsync(user, model.Password);

				if (checkPassword)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

					if (result.IsNotAllowed)
						ModelState.AddModelError(string.Empty, "Your account is not verified yet !");

					if (result.IsLockedOut)
						ModelState.AddModelError(string.Empty, "Your account is Locked Sign In Later !");

					if (result.Succeeded)
						return RedirectToAction(nameof(HomeController.Index), "Home");

				}

			}

			ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
			return View(model);
		}
        #endregion

        #region Sign Out
        // GET: /Account/SignOut
        [HttpGet]
        public async new Task  <IActionResult> SignOut()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Redirect to login page after sign out
            return RedirectToAction("SignIn", "Account");
        }
        #endregion
    }
}