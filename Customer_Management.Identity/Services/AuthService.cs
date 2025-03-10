﻿using Customer_Management.Application.Constants;
using Customer_Management.Application.Contracts.Identity;
using Customer_Management.Application.Models.Identity;
using Customer_Management.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Customer_Management.Identity.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }



        #region Register

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"username'{request.UserName}' already exists");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.UserName);
            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"username'{request.UserName}' already exists");
            }
        }
        #endregion



        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"user with {request.Email} not found");
            }
            else
            {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"credentials for {request.Email} arent vaild.");
                    }
                    JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

                    AuthResponse response = new AuthResponse
                    {
                        Id = user.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Email = user.Email,
                        UserName = user.UserName,
                    };
                    return response;
                
             }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
           
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id),

            }.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var siginCredentials = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                 issuer: _jwtSettings.Issuer,
                 audience: _jwtSettings.Audience,
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                 signingCredentials: siginCredentials
                );
            return jwtSecurityToken;
        }

    }
}
