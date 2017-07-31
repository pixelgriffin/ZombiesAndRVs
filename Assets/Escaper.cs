using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escaper : MonoBehaviour {

    void Start()
    {
        DontDestroyOnLoad(this);
    }

	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
