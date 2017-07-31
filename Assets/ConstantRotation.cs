using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {

    public float speed = 3f;

	void Update () {
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Time.time * speed));
    }
}
