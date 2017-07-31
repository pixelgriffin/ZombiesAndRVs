using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFriction : MonoBehaviour {

    public float friction = 0.5f;

    public float speed = 2;

    private float realSpeed;

    void Start()
    {
        realSpeed = speed;
    }

	void Update () {
        this.transform.position += new Vector3(0, realSpeed * Time.deltaTime, 0);
	}
}
