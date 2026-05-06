namespace ITCS_3112_Final_Project.Domain;

public class User
{
    public string name { get; set; }
    public string id { get; set; }
    private string email { get; set; }
    
    public  User(string name, string id, string email)
    {
        this.name = name;
        this.id = id;
        this.email = email;
    }
}