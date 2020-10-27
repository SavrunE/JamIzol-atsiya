using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public KeyCode HotKey = KeyCode.Space;
    public event EventHandler<OnSpacePressedEventArgs> OnStopTime;

    public KeyCode OpenMenu = KeyCode.I;
    public event Action<String> OnOpenMenu;

    private int keyCount;
    public class OnSpacePressedEventArgs : EventArgs
    {
        public int KeyCount;
    }
    private void Start()
    {
        OnStopTime += TestingHotKey;
    }
  
    private void Update()
    {
        if (Input.GetKeyDown(HotKey))
        {
            keyCount++;
            if (OnStopTime != null) OnStopTime(this, new OnSpacePressedEventArgs { KeyCount = keyCount}); //OnStopTime?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(OpenMenu)) 
        {
            OnOpenMenu?.Invoke("f");
        }
    }
    private void TestingHotKey(object sender, EventArgs e)
    {
        Debug.Log(HotKey);
    }
}
