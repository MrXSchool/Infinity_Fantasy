using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    protected static InputManager Instance { get => instance; }
    [SerializeField] protected Vector3 mouseWoldPos;
    protected Vector3 MouseWoldPos { get => mouseWoldPos; }


    private void Awake()
    {
        if (InputManager.instance != null) { Debug.LogError("Đang có  nhiều hơn 1 InputManager"); }
        InputManager.instance = this;
    }
    void FixedUpdate()
    {
        this.GetMousePos();

    }

    protected virtual void GetMousePos()
    {
        this.mouseWoldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
}

