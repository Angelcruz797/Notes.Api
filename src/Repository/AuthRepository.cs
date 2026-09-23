using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;
using Notes.Api.Dto;
using Notes.Api.Models;
using Notes.Api.Handler;

namespace Notes.Api.Repository;

public class AuthRepository(AppDbContext context, IJwtHandler jwtHandler) : IAuthRepository
{
    public async Task<bool> UserExists(string userName, string email)
        => await context.Users.AnyAsync(u => u.UserName == userName || u.Email == email);

    public async Task<AuthResponse> CreateUser(RegisterRequest request)
    {
        var user = new Users
        {
            Name = request.Name,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new AuthResponse
        {
            Token = jwtHandler.GenerateToken(user),
            UserName = user.UserName,
            Email = user.Email,
        };
    }


    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await GetUserByUserName(request.UserName);
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return null;

        return new AuthResponse
        {
            Token = jwtHandler.GenerateToken(user),
            UserName = user.UserName,
            Email = user.Email,
        };
    }
    
    private async Task<Users?> GetUserByUserName(string userName)
        => await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    
}