

public class Respone
{
    public Respone(string message, bool status, user data)
    {
        this.message = message;
        this.status = status;
        this.data = data;
    }

    public string message { get; set; }

    public bool status { get; set; }

    public user data { get; set; }
}
