using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour {

    public SpriteRenderer playerSprite;

    private bool oldFlip = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((playerSprite.flipX && !oldFlip) || (!playerSprite.flipX && oldFlip))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x * -1f, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        oldFlip = playerSprite.flipX;
    }
}
