using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCorrectBar : MonoBehaviour {

    public List<ProgressBarMover> bars;

	void Start () {
        for(int i = 0; i < bars.Count; i++)
        {
            if (i < EffectManager.Instance.currentLevel)
            {
                bars[i].GetComponent<Slider>().value = 1f;
            }

            bars[i].enabled = (i == EffectManager.Instance.currentLevel);
        }
	}
}
