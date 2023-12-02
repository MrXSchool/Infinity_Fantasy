
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RegisterRequest
{
    public RegisterRequest(string email, string password, string username, string confirmPassword)
    {
        this.email = email;
        this.password = password;
        this.username = username;
        this.confirmPassword = confirmPassword;
    }

    public string email { get; set; }
    public string password { get; set; }
    public string username { get; set; }
    public string confirmPassword { get; set; }

}
