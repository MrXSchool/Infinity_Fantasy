

public class error
{
    public error(string message, bool status)
    {
        this.message = message;
        this.status = status;
    }

    public string message { get; set; }

    public bool status { get; set; }
}
