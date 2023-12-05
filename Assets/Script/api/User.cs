
using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class User
{
    [JsonConstructor]
    public User(string id, string email, string username, string password, List<MapModel> data)
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
    public List<MapModel> data { get; set; }

}
