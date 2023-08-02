using UserAPI.Models.DTO;

namespace UserAPI.Interfaces
{
    public interface ITokenGenerate
    {
        public string GenerateToken(UserDTO user);
    }
}
