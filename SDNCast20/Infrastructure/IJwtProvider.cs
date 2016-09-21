using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace ConsoleApplication.Infrastructure
{
    public interface IJwtProvider
    {
        JwtBearerOptions Options { get; }
        string GenerateToken(string consumerId);
    }
}