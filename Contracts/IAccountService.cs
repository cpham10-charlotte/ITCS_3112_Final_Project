using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Contracts;

public interface IAccountService
{
    void Login(string id);
    void Logout();
    bool IsLoggedIn();
    User? GetCurrentUser();
}