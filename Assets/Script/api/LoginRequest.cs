using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest
{
    public LoginRequest(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public string email { get; set; }
    public string password { get; set; }

}
