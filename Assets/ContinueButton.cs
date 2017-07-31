using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public FadeCanvasImage fader;

    public string scene = "Play";

    public void Continue()
    {
        //LET'S PLAY
        fader.fadeOut = false;
        fader.fadeIn = true;

        Invoke("SwitchScene", 1.1f);
    }

    private void SwitchScene()
    {
        if(scene == "Splash")
        {
            EffectManager.Instance.currentLevel = 0;
        }

        SceneManager.LoadScene(scene);
    }
}
