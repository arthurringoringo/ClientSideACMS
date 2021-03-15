using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ACMS.DAL.Models;
using ClientSideACMS.Data;

namespace ClientSideACMS.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IAPIServiceExtension _emailSender;
        private readonly IStudentApiServices _studentApi;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IAPIServiceExtension emailSender,
            IStudentApiServices studentapi
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _studentApi = studentapi;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Username")]
            public string Username{ get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string Lastname { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string Phonenumber{ get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Birth of Date")]
            public DateTime BirthOfDate { get; set; }

            [Required]
            [Display(Name = "Parent/Guardian Name")]
            public string ParentGuardianName{ get; set; }

            [Required]
            [Display(Name = "School Name")]
            public string SchoolName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public string Sex { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("~/");
            }
            else { return Page(); }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.Username, Email = Input.Email,PhoneNumber=Input.Phonenumber };
                var student = new Student {FirstName = Input.Firstname, LastName = Input.Lastname, BirthDate = Input.BirthOfDate, Sex = Input.Sex, ParentGuardianName = Input.ParentGuardianName, Address = Input.Address, SchoolName = Input.SchoolName,CreatedOn=DateTime.Now };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    student.UserId = user.Id;
                    var createStudent = await _studentApi.CreateAccount(student);

                    if (createStudent == "true")
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code, returnUrl },
                            protocol: Request.Scheme);
                        
                        var confirmLink =$"<h1>Thank you for registering to ACMS</h1> <b>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                        EmailDto email = new EmailDto { To = user.Email, Subject = "Confirm Your Account - ACMS", Body = confirmLink };
                        //TODO Beutify confirmation page
                        // Disabled until congirm page is usefull
                        // await _emailSender.SendEmail(email);
                    }
                    else 
                    {
                        ModelState.AddModelError("Try again later", "Internal server error");
                    }
                   
                 

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
