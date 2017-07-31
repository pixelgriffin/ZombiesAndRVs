using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour {

    public SpriteRenderer playerSprite;

    private SpriteRenderer rend;

    private bool oldFlip = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

	void Update () {
        //rend.flipY = playerSprite.flipX;
        //if((rend.flipY && !oldFlip) || (!rend.flipY && oldFlip))
        if((this.transform.localScale.y == -1f && !oldFlip) || (this.transform.localScale.y == 1f && oldFlip))
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * 1f, this.transform.localScale.y * -1f, 1f);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x * -1f, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        oldFlip = playerSprite.flipX;


        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        mouse -= new Vector3(this.transform.position.x, this.transform.position.y, 0f);


        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg));
	}
}
