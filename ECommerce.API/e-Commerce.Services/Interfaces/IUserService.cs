using e_Commerce.Data.Entities;
using e_Commerce.Services.Dtos;
using e_Commerce.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> Regiser(RegisterUserRequest user);
        public Task<string> Login(UserDto userDto);
        public Task<User> Get(Guid user);
        public Task<User[]> All();
        public Task<List<Product>> ShowCart(Guid userId);
    }
}
