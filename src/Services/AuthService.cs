using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Notes.Api.Dto;
using Notes.Api.Models;
using Notes.Api.Repository;

namespace Notes.Api.Services;

public class AuthService(IAuthRepository _repository) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request) 
        => await _repository.CreateUser(request);

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        => await _repository.Login(request);

    public async Task<bool> UserExistsAsync(string userName, string email)
        => await _repository.UserExists(userName, email);
}