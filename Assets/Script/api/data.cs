

public class data
{
    public data(string id, string email, string username, string password, user user)
    {
        _id = id;
        this.email = email;
        this.username = username;
        this.password = password;
        User = user;
    }

    public string _id { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public user User { get; set; }

}
