using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopList.Models.Database;
using ShopList.Models.Database.Entities;
using ShopList.Models.Constants;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System;
using System.Collections.Generic;

namespace ShopList.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        public UserRepository(UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> Login(string username, string password)
        {
            return await _signInManager.PasswordSignInAsync(username, password, false, true);
        }

        public async Task<IdentityResult> AddRoleToUser(UserEntity user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<UserEntity> GetUserByUsername(string userName)
        {
            return await _userManager.Users
                .Where(p => p.UserName == userName)
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
        {
            return await _userManager.Users
                .Where(p => p.RefreshToken == refreshToken)
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
