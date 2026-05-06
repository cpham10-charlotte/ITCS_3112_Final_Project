using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Services;

public class UserFileLoader : IFileLoader
{
    private readonly IUserRepository _userRepo;

    public UserFileLoader(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var sections = line.Split(',');

            if (sections.Length != 3)
                continue;

            var id = sections[0];
            var name = sections[1];
            var email = sections[2];

            var user = new User(name, id, email);

            _userRepo.AddUser(user);
        }
    }
}