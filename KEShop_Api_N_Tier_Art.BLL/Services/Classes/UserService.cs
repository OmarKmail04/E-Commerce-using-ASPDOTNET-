using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository,UserManager <ApplicationUser> userManager ) 
        {
        _userRepository=userRepository;
            _userManager = userManager;
        }

        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            return await _userRepository.BlockUserAsync(userId, days);
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
            return await _userRepository.ChangeUserRoleAsync(userId, roleName);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
             var users= await _userRepository.GetAllAsync();
            var userDtos=new List<UserDto>();
            
            foreach (var user in users) 
            { 
            var role=await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto {
                Id=user.Id,
                FullName=user.FullName,
                UserName=user.UserName,
                PhoneNumber=user.PhoneNumber,
                Email=user.Email,
                RoleName=role.FirstOrDefault()


                
                });

            }

            return userDtos;
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user= await _userRepository.GetByIdAsync(userId);
            return user.Adapt<UserDto>();   
        }

        public Task<bool> IsBlockUserAsync(string userId)
        {
            return _userRepository.IsBlockUserAsync(userId);
        }

        public Task<bool> UnBlockUserAsync(string userId)
        {
            return _userRepository.UnBlockUserAsync(userId);
        }
    }
}
