using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBarMover : MonoBehaviour {

    public Transform player;

    public Transform startPos, endPos;

    public FadeCanvasImage fader;

    private Slider progress;

	void Start () {
        progress = GetComponent<Slider>();
	}
	
	void Update () {
        if (!player)
            return;

        progress.value = 1f - ((endPos.position.x - player.position.x) / endPos.position.x);

        if(progress.value >= 1f && !fader.fadeIn)
        {
            fader.fadeOut = false;
            fader.fadeIn = true;

            Invoke("SwitchScene", 1.1f);
        }
	}

    void SwitchScene()
    {
        EffectManager.Instance.currentLevel++;

        if(EffectManager.Instance.currentLevel == 4)
        {
            SceneManager.LoadScene("Outro");
            return;
        }

        GameObject frontRV = null;

        foreach(GameObject rv in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if(frontRV == null || rv.transform.position.x > frontRV.transform.position.x)
            {
                frontRV = rv;
            }
        }

        if(Vector3.Distance(player.position, frontRV.transform.position) < 1.5f)
            EffectManager.Instance.finishedFirst = true;
        else
            EffectManager.Instance.finishedFirst = false;

        SceneManager.LoadScene("SelectPower");
    }
}
