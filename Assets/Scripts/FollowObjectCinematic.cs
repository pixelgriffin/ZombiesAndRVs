using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectCinematic : MonoBehaviour {

    public float followSpeed;

    public Transform follow;

    public bool centerBetweenMouse = true;

    private float initialZ;

    /*public Vector3 wobbleStrength;
    public int wobbleStep = 2;

    private Vector3 wobble;*/

    void Start()
    {
        initialZ = this.transform.position.z;
    }


	void Update () {
        if (!follow)
            return;
        //wobble = Mathf.Sin(Time.time);
        /*if(Time.time % 2 == 0)
        {
            wobble = new Vector3(wobbleStrength.x * Random.Range(-1f, 1f), wobbleStrength.y * Random.Range(-1f, 1f), wobbleStrength.z * Random.Range(-1f, 1f));
        }*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 followPos = follow.position + ((mousePos - follow.position) / 3f);

        this.transform.position = Vector3.Lerp(this.transform.position, followPos + new Vector3(0, 0, initialZ), Time.deltaTime * followSpeed);
	}
}
