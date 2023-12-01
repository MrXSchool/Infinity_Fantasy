using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest
{
    public LoginRequest(string userName, string passWord)
    {
        this.userName = userName;
        this.passWord = passWord;
    }

    public string userName { get; set; }
    public string passWord { get; set; }

}
