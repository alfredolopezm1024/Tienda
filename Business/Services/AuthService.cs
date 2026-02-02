using Business.common;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTO;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public  class AuthService : IAuthService
    {

        private readonly IBaseRepository<Cliente> repository;
        private readonly IConfiguration configuration;

        public AuthService(IBaseRepository<Cliente> repository,
            IConfiguration configuration) 
        { 
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<Result<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            var cliente = await repository.FindByAsync(u => u.correo == loginDTO.correo);

            if (cliente == null) return Result<LoginResponseDTO>.Error("Credenciales inválidas");

            bool isValid = BCrypt.Net.BCrypt.Verify(loginDTO.password, cliente.password);

            if(!isValid) return Result<LoginResponseDTO>.Error("Credenciales inválidas");

            string token = createToken(cliente);

            return Result<LoginResponseDTO>.Ok(new LoginResponseDTO 
                { 
                    Id=cliente.Id,
                    Correo=cliente.correo,
                    Token=token
                }
            );
        }

        private string createToken(Cliente cliente) 
        {
            var secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()));

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(9),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(tokenConfig);
        }
    }
}
