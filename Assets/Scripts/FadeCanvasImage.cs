using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvasImage : MonoBehaviour {

    public bool fadeOut = false;
    public bool fadeIn = true;

    public float fadeSpeed = 1f;

    private Image img;

	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        float alpha = img.color.a;

        if (fadeOut)
        {
            alpha = img.color.a - (Time.deltaTime * fadeSpeed);
            if (alpha < 0f)
            {
                alpha = 0f;
            }
        }
        else if(fadeIn)
        {
            alpha = img.color.a + (Time.deltaTime * fadeSpeed);
            if (alpha > 1f)
            {
                alpha = 1f;
            }
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
	}

    public bool IsFadedIn()
    {
        return (img.color.a >= 1f);
    }

    public bool IsFadedOut()
    {
        return (img.color.a <= 0f);
    }
}
