using Notes.Api.Dto;

namespace Notes.Api.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<bool> UserExistsAsync(string userName, string email);
}