﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System;

namespace Project.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly String _webRoot;
        public ProfileController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _webRoot = env.WebRootPath;
        }
        public IActionResult Index()
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var rank = _context.Ranks.SingleOrDefault(r => r.Id == user.Profile.RankId);
            if (rank != null) {
                ViewBag.Rank = rank.Name;
            }
            else
            {
                ViewBag.Rank = "";
            }
          
            var existingProfile = user.Profile;
            return View(existingProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Index([Bind("Avatar,FirstName,LastName,Gender,Dob,Phone,Address,Nationality")] Profile profile, IFormFile avatar)
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            profile.UserId = user.Id;
            profile.RankId = 1;
            var existingProfile = user.Profile;
            if (ModelState.IsValid)
            {
                if (avatar != null)
                {
                    //Save file to physical storage
                    string fileName = Guid.NewGuid() + ".jpg";
                    Directory.CreateDirectory(Path.Combine(_webRoot, "profile", "avatar"));
                    var filePath = Path.Combine(_webRoot, "profile", "avatar", fileName);

                    using(var stream = System.IO.File.Create(filePath))
                    {
                        await avatar.CopyToAsync(stream);
                    }
                    filePath = Path.Combine("profile", "avatar", fileName);
                    profile.Avatar = filePath;
                }

                if(existingProfile == null)
                {
                    _context.Add(profile);
                }
                else
                {
                    await TryUpdateModelAsync<Profile>(existingProfile, "", p => p.LastName, p => p.FirstName, p => p.Gender, p => p.Dob, p => p.Address, p => p.Phone);
                        if(avatar != null)
                    {
                        existingProfile.Avatar = profile.Avatar;
                    }
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(profile);
        }


    }
}
