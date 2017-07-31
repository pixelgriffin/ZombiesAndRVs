using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RVBonus : MonoBehaviour {

    private Text text;

    void Awake()
    {
        EffectManager.Instance.AddCannisters((EffectManager.Instance.finishedFirst ? 2 : 0));
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        text.text += "" + (EffectManager.Instance.finishedFirst ? 2 : 0);
	}
}
