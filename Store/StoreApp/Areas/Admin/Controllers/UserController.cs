﻿using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]

    public class UserController:Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var users= _manager.AuthService.GetAllUsers();
            return View(users);
        }

        public IActionResult Create() {
            return View(
                new UserDtoForCreation
                {
                    Roles = new HashSet<string>(_manager.AuthService.Roles.Select(x => x.Name).ToList())
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result = await _manager.AuthService.CreateUser(userDto);
            return result.Succeeded
                ? RedirectToAction("Index") 
                : View();
            
        }

        public async Task<IActionResult> Update([FromRoute(Name ="id")] string id)
        {
            //kullanıcı bilgilerini oku, rolleri getir.
            
            var user = await _manager.AuthService.GetOneUserForUpdate(id);//id içerisinde username bilgisi olacak.
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.Update(userDto);
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> ResetPassword([FromRoute(Name ="id")] string id)
        {
            return View(new ResetPasswordDto()
            {
                Username = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            var result = await _manager.AuthService.ResetPassword(model);
            return result.Succeeded
                ? RedirectToAction("Index") 
                : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm] UserDto userDto)
        {
            var result = await _manager.AuthService.DeleteOneUser(userDto.UserName);
            return result.Succeeded 
                ? RedirectToAction("Index") 
                : View();
        }
    }
}
