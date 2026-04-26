namespace ITCS_3112_Final_Project.Domain;

public class User
{
    private string _name { get; set; };
    private string _id { get; set; };
    public string Id { get { return _id; } }
    private string _email { get; set; };
    
    public  User(string name, string id, string email)
    {
        _name = name;
        _id = id;
        _email = email;
    }
}