using C._Domain.Entities;

namespace D._Repository.Services
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public int ValidateJwtToken(string? token);
    }
}
