using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantTorque : MonoBehaviour {

    public float torq;

    private Rigidbody2D bod;

    void Start()
    {
        bod = GetComponent<Rigidbody2D>();
    }

	void Update () {
        bod.AddTorque(torq);
	}
}
