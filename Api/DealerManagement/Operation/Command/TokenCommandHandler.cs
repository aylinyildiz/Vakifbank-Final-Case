﻿using Base.Response;
using Base.Token;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Operation.Cqrs;
using Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Operation.Command
{

    public class TokenCommandHandler :
        IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>

    {
        private readonly DealerDbContext dbContext;
        private readonly JwtConfig jwtConfig;

        public TokenCommandHandler(DealerDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.dbContext = dbContext;
            this.jwtConfig = jwtConfig.CurrentValue;
        }


        public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<User>().Include(x=>x.Role).FirstOrDefaultAsync(x => x.Email == request.Model.Email, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }

            var md5 = Base.Encryption.Md5.Create(request.Model.Password.ToUpper());
            if (entity.Password != md5)
            {
                entity.LastActivityDate = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(cancellationToken);

                return new ApiResponse<TokenResponse>("Invalid user informations");
            }

            if (!entity.IsActive)
            {
                return new ApiResponse<TokenResponse>("Invalid user!");
            }

            string token = Token(entity);
            TokenResponse tokenResponse = new()
            {
                Token = token,
                ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                //UserNumber = entity.UserNumber,
                Email = entity.Email
            };

            return new ApiResponse<TokenResponse>(tokenResponse);
        }

        private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
            new Claim("Id", user.Id.ToString()),
            new Claim("Role", user.Role.RoleName),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim("FullName", $"{user.FirstName} {user.LastName}")
        };

            return claims;
        }
    }
}
