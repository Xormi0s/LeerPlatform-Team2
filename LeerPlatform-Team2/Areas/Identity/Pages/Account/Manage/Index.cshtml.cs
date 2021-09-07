using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeerPlatform_Team2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeerPlatform_Team2.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<TblGebruiker> _userManager;
        private readonly SignInManager<TblGebruiker> _signInManager;

        public IndexModel(
            UserManager<TblGebruiker> userManager,
            SignInManager<TblGebruiker> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }
            [DataType(DataType.Date)]
            public DateTime Geboortedatum { get; set; }
            public string  Adres { get; set; }
            public string UcllNummer { get; set; }
        }

        private async Task LoadAsync(TblGebruiker user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Voornaam = user.Voornaam,
                Achternaam = user.Achternaam,
                PhoneNumber = phoneNumber,
                Geboortedatum = user.Geboortedatum,
                Adres = user.Adres,
                UcllNummer = user.UcllNummer
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            if (Input.Voornaam != user.Voornaam)
            {
                user.Voornaam = Input.Voornaam;
            }
            if (Input.Achternaam != user.Achternaam)
            {
                user.Achternaam = Input.Achternaam;
            }
            if (Input.Geboortedatum != user.Geboortedatum)
            {
                user.Geboortedatum = Input.Geboortedatum;
            }
            if (Input.Adres != user.Adres)
            {
                user.Adres = Input.Adres;
            }
            //if (Input.UcllNummer != user.UcllNummer)
            //{
            //    user.UcllNummer = Input.UcllNummer;
            //}
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
