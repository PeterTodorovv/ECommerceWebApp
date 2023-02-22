using AutoMapper;
using e_Commerce.Data.Entities;
using e_Commerce.Data.Repository;
using e_Commerce.Services.Dtos;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Requests;
using FootballApp.Services.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private User user = new User();

        public UserService(IRepository<User> repository, IMapper mapper, IConfiguration configuration)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        public async Task<User> Regiser(RegisterUserRequest createUserRequest)
        {

            if(_repository.AllAsNoTracking().Any(x => x.Username == createUserRequest.Username))
            {
                throw new BusinessError("This username is already in use.");
            }

            if (_repository.AllAsNoTracking().Any(x => x.Email == createUserRequest.Email))
            {
                throw new BusinessError("This email is already in use.");
            }

            CreatePasswordHash(createUserRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.FirstName = createUserRequest.FirstName;
            user.LastName = createUserRequest.LastName;
            user.Address = createUserRequest.Address;
            user.Username = createUserRequest.Username;
            user.Email = createUserRequest.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = "Customer";

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return user;
        }

        public async Task<string> Login(UserDto userDto)
        {
            var user = _repository.GetFirstBy(x => x.Username == userDto.Username);

            if (user == null)
            {
                throw new BusinessError("User not foud", HttpStatusCode.BadRequest);
            }

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BusinessError("Wrong Password", HttpStatusCode.BadRequest);
            }

            string token = CreateToken(user);

            return token;
        }

        public async Task<User> Get(Guid id)
        {
            var user = _repository.GetFirstBy(x => x.Id == id);
            
            if( user == null)
            {
                throw new BusinessError("This user does not exist!", System.Net.HttpStatusCode.NotFound);
            }

            return user;
        }

        public async Task<User[]> All()
        {
            return _repository.AllAsNoTracking().ToArray();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<List<Product>> ShowCart(Guid userId)
        {
            return _repository.GetFirstBy(x => userId == x.Id).ShoppingCart;
        }
    }
}
