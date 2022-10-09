﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestProject.Models;
using NestProject.ViewModels;

namespace NestProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {
            if (!ModelState.IsValid) return View();
            var existUser = await _userManager.FindByNameAsync(user.Username);
            if(existUser != null)
            {
                ModelState.AddModelError("Username", "This User already exists");
            }
            AppUser appUser = new AppUser()
            {
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.Username,
                Email = user.Email,
            };
            var result = await _userManager.CreateAsync(appUser, user.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.ToString());
                }
                return View();
            }
            await _signInManager.SignInAsync(appUser, true);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
