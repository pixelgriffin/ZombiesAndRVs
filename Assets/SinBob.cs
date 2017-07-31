using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinBob : MonoBehaviour {

    public float speed, size;

	void Update () {
        this.transform.position += new Vector3(0f, Mathf.Sin(Time.time * speed) * size, 0);
	}
}
