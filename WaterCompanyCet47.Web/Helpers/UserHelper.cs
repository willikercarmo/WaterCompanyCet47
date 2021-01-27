using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager; // gestão do utilizador
        private readonly SignInManager<User> signInManager; // trata dos logins
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext _context;

        public UserHelper(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _context = context;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public IQueryable<User> GetAll()
        {
            return _context.Set<User>();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public IEnumerable<SelectListItem> GetComboUsers()
        {
            var list = _context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.Id.ToString(),
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecionar o Consumidor...)",
                Value = "0"
            });

            return list;

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }
    }
}
