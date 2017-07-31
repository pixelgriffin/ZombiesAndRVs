using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour {

    public float speed = 2f;

	void Update () {
        this.transform.localScale += new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed);
	}
}
