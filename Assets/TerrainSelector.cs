using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelector : MonoBehaviour {


	void Start () {
        int rand = Random.Range(0, this.transform.childCount);

        this.transform.GetChild(rand).gameObject.SetActive(true);
	}
}
