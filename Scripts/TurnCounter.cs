using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounter : MonoBehaviour {

    public static int turnCount;
        
    void Awake()
    { 
        int turnCounterCount = FindObjectsOfType<TurnCounter>().Length;
        if (turnCounterCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void TurnLoger()
    {
        turnCount = turnCount + 1;
        Debug.Log("Number of plays = " + turnCount);
    }

    public void TurnReset()
    {
        turnCount = 0;
    }
}
