using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer timer;
    private float gameTime;
    private int time;
    private float endTime;

    public bool CanCheckTime ;
    public Text TextObject;

    private void Start()
    {
        if (timer == null)
        {
            timer = this;
        }
        else if(timer == this)
        {
            Destroy(gameObject);
        }

        CanCheckTime = true;
;        StartCoroutine(CheckTime());
    }
    private IEnumerator CheckTime()
    {
        while (CanCheckTime)
        {
            gameTime += Time.deltaTime;
            time = (int)gameTime;

            TextObject.text = time.ToString();
            yield return null;
        }
    }
}

