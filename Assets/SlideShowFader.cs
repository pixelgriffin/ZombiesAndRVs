using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideShowFader : MonoBehaviour {

    public FadeCanvasImage fader;
    public Image storyImage;

    public AudioSource audio;

    public AudioClip clip2, clip3;

    public List<GameObject> images;

    private int currentImage = 0;
    private float time = 0f;

    private bool swapping = false;

	// Update is called once per frame
	void Update () {
        if (!swapping)
        {
            time += Time.deltaTime;

            if (time >= 10f)
            {
                NextStory();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Controls");
        }
	}

    public void NextStory()
    {
        swapping = true;
        time = 0f;

        fader.fadeIn = true;
        fader.fadeOut = false;

        Invoke("SwitchImage", 2.1f);
    }

    public void SwitchImage()
    {
        Debug.Log("Switching image..");
        currentImage++;

        if(currentImage > images.Count - 1)
        {
            SceneManager.LoadScene("Controls");
        }

        images[currentImage].SetActive(true);

        for(int i = 0; i < currentImage; i++)
        {
            images[i].SetActive(false);
        }

        fader.fadeOut = true;
        fader.fadeIn = false;

        audio.clip = (currentImage == 1 ? clip2 : clip3);
        audio.volume = 0.75f;
        audio.loop = true;

        if(!audio.isPlaying)
            audio.Play();

        Invoke("SetSwapping", 2.1f);
    }

    public void SetSwapping()
    {
        Debug.Log("Swapping finished..");
        swapping = false;
    }
}
