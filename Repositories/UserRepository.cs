namespace ITCS_3112_Final_Project.Repositories;

using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Contracts;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public UserRepository()
    {
        
    }

    public User GetUserById(string id)
    {
        foreach (User user in _users)
        {
            if (user.id == id)
            {
                return user;
            }
        }
        return null;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }
}