using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocity2D : MonoBehaviour {

    public float maxSpeed = 5f;

    private Rigidbody2D bod;

	// Use this for initialization
	void Start () {
        bod = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("velocity mag: " + bod.velocity.magnitude);
		if(bod.velocity.magnitude > maxSpeed)
        {
            //Debug.Log("Limiting velocity by: " + (bod.velocity.magnitude - maxSpeed));
            //lower velocity
            bod.AddForce(-bod.velocity.normalized * (bod.velocity.magnitude - maxSpeed) * maxSpeed);
        }
	}
}
