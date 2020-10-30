using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public KeyCode KeyCodePlaceWall = KeyCode.Space;
    public GameObject WallPrefab;
    public float WallDistancePlace;

    public KeyCode OpenMenu = KeyCode.I;
    public event Action<String> OnOpenMenu;
    public event Action OnPlaceWall;

    private int keyCount;
    
    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
  
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCodePlaceWall))
        {
            OnPlaceWall?.Invoke();
        }
        if (Input.GetKeyDown(OpenMenu)) 
        {
            OnOpenMenu?.Invoke("f");
        }
    }
}
