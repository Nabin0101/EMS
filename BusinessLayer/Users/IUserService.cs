using Common.ViewModel.LoginSignup;
using Data_Access_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection.Metadata;

namespace BusinessLayer.Users
{
    public interface IUserService
    {
        Task<APIResponseModel> SaveUser(SignUpDTO signupDto);
        Task<string> GenerateToken(string username);
        Task<string> SaveTokenToDatabase(string userId, string tokenString, DateTime expiresAt);
        Task<APIResponseModel> UserLogin(LoginViewModel loginViewModel);
        bool IsTokenValid(string tokenString);




    }
}
