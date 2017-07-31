using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGeneratorAfterTime : MonoBehaviour {

    public float stopTime;

    public SinBob bob;
    public ParticleSystem sys;

    private float time;

    private bool didStop = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!didStop)
        {
            time += Time.deltaTime;

            if(time >= stopTime)
            {
                bob.enabled = false;
                sys.enableEmission = false;
            }
        }
	}
}
