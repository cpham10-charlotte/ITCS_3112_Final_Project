namespace ITCS_3112_Final_Project.Contracts;

using ITCS_3112_Final_Project.Domain;

public interface IUserRepository
{
    User? GetUserById(string id);

    void AddUser(User user);
}