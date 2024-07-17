namespace MRC.Application.Services
{
    public interface ILoginService{
        bool Login(string username, string password);
        Task<bool> LoginAsync(string username, string password);
    }
}
