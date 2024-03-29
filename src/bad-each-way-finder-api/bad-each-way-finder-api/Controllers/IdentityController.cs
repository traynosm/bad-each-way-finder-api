﻿using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Settings;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DTO;
using bad_each_way_finder_api_domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<IdentitySettings> _identitySettings;
        private readonly ITokenService _tokenService;

        public IdentityController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            BadEachWayFinderApiContext context, IOptions<IdentitySettings> identitySettings,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identitySettings = identitySettings;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AppUser model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userJson = JsonConvert.SerializeObject(user);

                    var userRoles = await _userManager.GetRolesAsync(user);
                    var userRolesJson = JsonConvert.SerializeObject(userRoles);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    if (token == null)
                    {
                        return BadRequest("Could not get Valid Token");
                    }

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        user = userJson,
                        roles = userRolesJson
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AppUser model)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ApiErrorResponseDTO() { Status = "Error", Message = "User already exists!" });

                IdentityUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ApiErrorResponseDTO()
                        {
                            Status = "Error",
                            Message = "User creation failed! Please check user details and try again."
                        });

                foreach (var role in model.UserRoles!)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    await _userManager.AddToRoleAsync(user, role);
                }

                var userJson = JsonConvert.SerializeObject(user);

                var userRoles = await _userManager.GetRolesAsync(user);
                var userRolesJson = JsonConvert.SerializeObject(userRoles);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                if (token == null)
                {
                    return BadRequest("Could not get Valid Token");
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = userJson,
                    roles = userRolesJson
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Logout/{token}")]
        public async Task<IActionResult> Logout(string token)
        {
            try
            {
                _tokenService.RemoveToken(token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private JwtSecurityToken? GetToken(List<Claim> authClaims)
        {
            if(authClaims == null || !authClaims.Any())
            {
                return null;
            }

            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identitySettings.Value.Secret));
                var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _identitySettings.Value.ValidIssuer,
                    audience: _identitySettings.Value.ValidAudience,
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: credentials);

                _tokenService.AddToken(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
