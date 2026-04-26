namespace ITCS_3112_Final_Project.Repositories;

using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Contracts;

public class UserRepository : IUserRepository
{
    private List<User> _users { get; set; }

    public UserRepository()
    {
        
    }

    public User GetUserById(string id)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }
        return null;
    }

    public void AddUser(User user)
    {
        _users.add(user);
    }
}