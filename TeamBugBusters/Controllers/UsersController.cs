﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamBugBusters.Data;
using TeamBugBusters.Migrations;
using TeamBugBusters.Models;

namespace TeamBugBusters.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //This method we display all the users that have been created
            // I was going to add so we also can display what role all users have, but i didn't have time to fix that
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var thisViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }
    }
}

