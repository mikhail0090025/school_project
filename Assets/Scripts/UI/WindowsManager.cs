using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public List<Window> windows;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in windows) item.TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in windows)
        {
            if (Input.GetKeyDown(item.KeyCode))
            {
                item.Switch();
                Cursor.lockState = AreOpenedWindows ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = AreOpenedWindows;
            }
        }
    }
    public static bool AreOpenedWindows
    {
        get
        {
            var windows = FindObjectOfType<WindowsManager>().windows;
            foreach (var item in windows)
            {
                if (item.IsOpened) return true;
            }
            return false;
        }
    }
}
[Serializable]
public class Window
{
    [SerializeField] GameObject Window_;
    [SerializeField] KeyCode Key;
    public void TurnOn() => Window_.SetActive(true);
    public void TurnOff() => Window_.SetActive(false);
    public void Switch()
    {
        Window_.SetActive(!Window_.activeSelf);
    }
    public bool IsOpened
    {
        get { return Window_.activeSelf; }
    }
    public KeyCode KeyCode { get { return Key; } }
}