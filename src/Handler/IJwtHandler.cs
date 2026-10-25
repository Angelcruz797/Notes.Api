using Notes.Api.Models;

namespace Notes.Api.Handler;

public interface IJwtHandler
{
    string GenerateToken(Users user);
    Guid GetIdByToken();
}