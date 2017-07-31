using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTextPicker : MonoBehaviour {

    public List<string> strs;

	void Start () {
        GetComponent<TextMesh>().text = strs[Random.Range(0, strs.Count)];
	}
}
