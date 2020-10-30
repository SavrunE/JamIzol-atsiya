using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float gameTime;
    private int time;
    private float endTime;

    public Text TextObject;

    private void Start()
    {
       
    }
    void Update()
    {
        gameTime += Time.deltaTime * 100f;
        time = (int)gameTime;
        endTime = time/ 100f;

        TextObject.text = endTime.ToString();
    }
}

