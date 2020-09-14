using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backround : MonoBehaviour {

    private SpriteRenderer backround;

    void Start()
    {
        backround = GetComponent<SpriteRenderer>();
        backround.size = new Vector2 (GameController.horizontalUnits, 10);
    }
}
