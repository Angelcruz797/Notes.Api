using Notes.Api.Dto;
using Notes.Api.Models;

namespace Notes.Api.Repository;

public interface IAuthRepository
{
    Task<bool> UserExists(string userName, string email);
    Task<AuthResponse> CreateUser(RegisterRequest request);
    Task<AuthResponse> Login(LoginRequest request);
}