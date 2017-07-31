using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyFollow : MonoBehaviour {

    public Transform follow;

    public float damping = 0.9f;

	void Update () {
        //this.transform.position += new Vector3(follow.GetComponent<Rigidbody2D>().velocity.x * 0.025f, 0, 0) * damping;
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(follow.transform.position.x * damping, follow.transform.position.y * damping, this.transform.position.z), Time.deltaTime * 10f);
	}
}
