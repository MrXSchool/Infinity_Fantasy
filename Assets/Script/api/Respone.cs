using Newtonsoft.Json;

[System.Serializable]
public class Respone
{
    [JsonConstructor]
    public Respone(string message, bool status, User user)
    {
        this.message = message;
        this.status = status;
        this.user = user;
    }

    public string message { get; set; }

    public bool status { get; set; }

    public User user { get; set; }
}
