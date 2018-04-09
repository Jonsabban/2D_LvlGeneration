using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour {

    Sprite map;
    Texture2D tex;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("enter")) {
            CompleteGame();
        }
	}

    private void CompleteGame() {
        map = Resources.Load<Sprite>("lvlMap");
        

    }
}
