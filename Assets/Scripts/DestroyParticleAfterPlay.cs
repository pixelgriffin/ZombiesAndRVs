using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleAfterPlay : MonoBehaviour {

    private ParticleSystem sys;

	void Start () {
        sys = GetComponent<ParticleSystem>();
	}

	void Update () {
		if(!sys.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
