using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Services;

public class AuthenticationService : IAccountService
{
    private User? _currentUser;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public void Login(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID cannot be empty", nameof(id));
        _currentUser = _userRepository.GetUserById(id);
    }

    public void Logout()
    {
        _currentUser = null;
    }

    public bool IsLoggedIn() => _currentUser != null;

    public User? GetCurrentUser() => _currentUser;
}