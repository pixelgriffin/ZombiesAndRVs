using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroChanger : MonoBehaviour {

    private FadeCanvasImage fader;
    private Image img;

    public string scene = "Intro";

	void Start () {
        fader = GetComponent<FadeCanvasImage>();
        img = GetComponent<Image>();
	}

	void Update () {
		if(img.color.a == 0f && fader.fadeOut)
        {
            fader.fadeIn = true;
            fader.fadeOut = false;
        }
        else if(img.color.a == 1f && fader.fadeIn)
        {
            SceneManager.LoadScene(scene);
        }
	}
}
