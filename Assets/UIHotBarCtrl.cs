using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotBarCtrl : MonoBehaviour
{
    private static UIHotBarCtrl instance;

    public static UIHotBarCtrl Instance { get => instance; }

    protected void Awake()
    {
        if (UIHotBarCtrl.instance != null)
            UIHotBarCtrl.instance = this;
    }
}
