

public class user
{
    public user(string id, string email, string username, string password, string data)
    {
        _id = id;
        this.email = email;
        this.username = username;
        this.password = password;
        this.data = data;
    }

    public string _id { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string data { get; set; }

}
