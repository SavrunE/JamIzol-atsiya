using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController gameController = GetComponent<GameController>();
        gameController.OnStopTime += GameController_OnStopTime;
    }

    private void GameController_OnStopTime(object sender, GameController.OnSpacePressedEventArgs e)
    {
        Debug.Log("stoped game = " + e.KeyCount);
    //    GameController gameController = GetComponent<GameController>();
    //    gameController.OnStopTime -= GameController_OnStopTime;
    }
}
