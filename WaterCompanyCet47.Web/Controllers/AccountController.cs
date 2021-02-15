using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        public AccountController(
            IUserHelper userHelper,
            DataContext dataContext,
            UserManager<User> userManager)
        {
            _userHelper = userHelper;
            _dataContext = dataContext;
            _userManager = userManager;
        }

        //  Account/Index
        public IActionResult Index()
        {
            return View(_userHelper.GetAll().OrderBy(u => u.FullName));
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: Account/Details/5
        public async Task<IActionResult> Profile()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }
                       
            return View(user);
        }

        // GET: Account/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }


            var view = this.ToUserViewModel(user);

            return View(view);
        }

        private UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        // POST: Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (user.Id == null)
            {
                return NotFound();
            }

            var userUpdate = await _dataContext.Users.FirstOrDefaultAsync(s => s.Id == user.Id);

            if (await TryUpdateModelAsync<User>(userUpdate, "", c => c.FirstName, c => c.LastName, c => c.Email, c => c.UserName))
            {

                try
                {
                    await _dataContext.SaveChangesAsync();
                    return this.RedirectToAction("Index", "Account");
                }
                catch (DbUpdateException)
                {

                    ModelState.AddModelError(string.Empty, "Não foi possível salvar as atualizações.");
                }
            }

            return this.RedirectToAction("Index", "Account");

        }

        private User ToUser(UserViewModel view)
        {
            return new User
            {
                Id = view.Id,
                FirstName = view.FirstName,
                LastName = view.LastName,
                Email = view.Email,
                UserName = view.UserName
            };
        }

        // GET: Account/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }




        /*------- Action to Users Manager ------------ */

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Login Falhou");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            //if (this.ModelState.IsValid)
            //{
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user == null)
            {
                user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.UserName,
                    UserName = model.UserName,
                    Nif = model.Nif,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userHelper.AddUserAsync(user, model.Password);
                await _userHelper.AddUserToRoleAsync(user, "Customer");
                if (result != IdentityResult.Success)
                {
                    this.ModelState.AddModelError(string.Empty, "O cliente não pode ser criado.");
                    return this.View(model);

                }

                ViewBag.Message = string.Format("O Consumidor foi inserido com sucesso.");
                //return this.View(model);
                return RedirectToAction("Index");
            }

            this.ModelState.AddModelError(string.Empty, "O Usuário já se encontra registado.");
            //}

            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Profile");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Username não encontrado.");
                }
            }

            return this.View(model);
        }



    }
}
