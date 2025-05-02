namespace Danger_Money;

public interface IUserServices
{
    public Task<Response<bool>> Login(LoginDTO loginDTO);
    public Task<Response<bool>> Register(RegisterDTO registerDTO);
    public Task<Response<bool>> Logout();
}
