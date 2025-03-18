using AutoMapper;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;
using System.Runtime.Intrinsics.X86;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                throw new Exception("User could not e created.");

            if (userDto.Roles.Count > 0) {
                var roleResult = await _userManager.AddToRoleAsync(user, userDto.Password);
                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with roles.");
                    
                
            }
            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string username)
        {
            var user = await GetOneUser(username);
            var result= await _userManager.DeleteAsync(user);
            return result;


        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string username)
        {
            var user= await _userManager.FindByNameAsync(username);
            if(user is not null)
                return user;
            throw new Exception("An error occured");
        }

        public async Task<UserDtoForUpdate> GetOneUserForUpdate(string username)
        {
            var user= await GetOneUser(username);
           
            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            userDto.Roles= new HashSet<string>(Roles.Select(x => x.Name).ToList());
            userDto.UserRoles=new HashSet<string>(await _userManager.GetRolesAsync(user));//usera ait rol bilgileri
            return userDto;


           
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await GetOneUser(model.Username);
            await _userManager.RemovePasswordAsync(user);//password kaldırılacak.
            var result = await _userManager.AddPasswordAsync(user, model.Password);
            return result;
        
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await GetOneUser(userDto.UserName);
            user.PhoneNumber= userDto.PhoneNumber;
            user.Email= userDto.Email;
           
            var result = await _userManager.UpdateAsync(user);
            if (userDto.Roles.Count > 0)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var v1 = await _userManager.RemoveFromRolesAsync(user, userRoles);
                var v2 = await _userManager.AddToRolesAsync(user, userDto.Roles);
            }
            return;            
        }
    }
}
