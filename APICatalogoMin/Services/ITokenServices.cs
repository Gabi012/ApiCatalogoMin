using APICatalogoMin.Models;

namespace APICatalogoMin.Services
{
    public interface ITokenServices
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);
    }
}
