﻿using Data.DTO;
using Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
  public class AuthenticationManager : IAuthenticationManager
  {

    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private User _user;

    public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
      _user = await _userManager.FindByNameAsync(userForAuth.UserName);

      return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
    }

    public async Task<string> CreateToken()
    {
      var claims = await GetClaims();
      var signingCredentials = GetSigningCredentials();

      var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
      var key = Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);
      var secret = new SymmetricSecurityKey(key);

      return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
      var claims = new List<Claim>{
        new Claim(ClaimTypes.NameIdentifier, _user.Id),
        new Claim(ClaimTypes.Name, _user.UserName),
        new Claim(ClaimTypes.Email, _user.Email)
      };

      var roles = await _userManager.GetRolesAsync(_user);

      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claimApis)
    {
      var jwtSettings = _configuration.GetSection("JwtSettings");

      var tokenOptions = new JwtSecurityToken
      (
        issuer: jwtSettings.GetSection("validIssuer").Value,
        audience: jwtSettings.GetSection("validAudience").Value,
        claims: claimApis,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
        signingCredentials: signingCredentials
      );

      return tokenOptions;
    }
  }
}
