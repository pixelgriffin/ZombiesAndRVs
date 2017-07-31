using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticSetter : MonoBehaviour {

    public List<Text> stats;

	void Start () {
        stats[0].text += "" + EffectManager.Instance.zombiesKilled;
        stats[1].text += "" + EffectManager.Instance.bulletsFired;
        stats[2].text += "" + (((float)EffectManager.Instance.bulletsHit / (float)EffectManager.Instance.bulletsFired) * 100f) + "%";
        stats[3].text += "" + (EffectManager.Instance.rvsDestroyed);
        stats[4].text += "" + EffectManager.Instance.CannisterCount();
        stats[5].text += " " + (((Mathf.Min(1000f, EffectManager.Instance.zombiesKilled) / 1000f) * 0.33f) + 
                                ((((float)EffectManager.Instance.bulletsHit / (float)EffectManager.Instance.bulletsFired) * 0.33f)) +
                                (Mathf.Min(12f, EffectManager.Instance.CannisterCount()) / 12f) * 0.34f);
	}
}
