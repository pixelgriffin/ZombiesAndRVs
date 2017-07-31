using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject life;

    public Vector3 stillArmPosition;
    public Vector3 moveArmPosition;

    public float jumpPower = 150f;
    public float moveSpeed = 3f;

    public int health = 12;

    private Animator anim;

    private Rigidbody2D bod;
    private SpriteRenderer rend;

    private Rigidbody2D lastBodStoodOn;
    private float lastRecordedStoodVelocity = 0f;

    private bool onGround = false;

    private GameObject lifeCounter;

    private float immuneTimer = 0f;
    private bool isImmune = false;

    void Start () {
        bod = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        lifeCounter = GameObject.FindGameObjectWithTag("LifeContainer");
        anim = GetComponent<Animator>();

        this.health += EffectManager.Instance.GetExtraHealth();

        int extra = EffectManager.Instance.GetExtraHealth();

        int healthMoveRight = 100;
        while(extra > 0)
        {
            healthMoveRight += 100;
            GameObject newLife =  Instantiate(life, lifeCounter.transform);
            newLife.transform.position += new Vector3(healthMoveRight, 0, 0);

            extra -= 4;
        }

    }

    void Update()
    {
        if(isImmune)
        {
            immuneTimer += Time.deltaTime;
            if(immuneTimer > 1f)
            {
                isImmune = false;
                immuneTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            bod.AddForce(Vector2.up * jumpPower);
            GetComponent<AudioSource>().Play();
        }

        bool leftKeyDown = Input.GetKey(KeyCode.A);
        bool rightKeyDown = Input.GetKey(KeyCode.D);

        if (leftKeyDown && rightKeyDown)
        {
            anim.SetBool("Moving", false);
            this.transform.GetChild(0).localPosition = new Vector3(stillArmPosition.x * (rend.flipX ? -1 : 1), stillArmPosition.y, stillArmPosition.z);
        }
        else if (leftKeyDown)
        {
            anim.SetBool("Moving", true);
            this.transform.GetChild(0).localPosition = moveArmPosition;

            if (VelocityAboveNegativeMaxX())
                bod.AddForce(new Vector2(-moveSpeed * Time.deltaTime * (onGround ? 1f : 0.75f), 0));
        }
        else if(rightKeyDown)
        {
            anim.SetBool("Moving", true);
            this.transform.GetChild(0).localPosition = moveArmPosition;

            if (VelocityBelowMaxX())
                bod.AddForce(new Vector2(moveSpeed * Time.deltaTime * (onGround ? 1f : 0.75f), 0));
        }
        else
        {
            anim.SetBool("Moving", false);
            this.transform.GetChild(0).localPosition = new Vector3(stillArmPosition.x * (rend.flipX ? -1 : 1), stillArmPosition.y, stillArmPosition.z);
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x < this.transform.position.x)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Zombie")
        {
            if (!isImmune)
            {
                transform.GetChild(0).GetComponent<PlayRandomAudio>().PlayAudio();

                health -= 1;
                Debug.Log("health: " + health);
                Transform child = lifeCounter.transform.GetChild(lifeCounter.transform.childCount - 1);
                child.GetComponent<LifeCounter>().RemoveQuarters(1);

                isImmune = true;

                rend.color = Color.red;
                Camera.main.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile.chromaticAberration.enabled = true;
                Invoke("StopFlashing", 0.25f);


                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Road")
        {
            lastBodStoodOn = col.gameObject.GetComponent<Rigidbody2D>();
            lastRecordedStoodVelocity = lastBodStoodOn.velocity.x;


            onGround = true;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Road")
        {
            lastRecordedStoodVelocity = lastBodStoodOn.velocity.x;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Road")
        {
            onGround = false;
        }
    }

    private bool VelocityBelowMaxX()
    {
        if (lastBodStoodOn == null)
        {
            return this.bod.velocity.x < 2f;
        }

        return this.bod.velocity.x - lastBodStoodOn.velocity.x < 2f;
    }

    private bool VelocityAboveNegativeMaxX()
    {
        if (lastBodStoodOn == null)
        {
            return this.bod.velocity.x > -2f;
        }

        return this.bod.velocity.x - lastBodStoodOn.velocity.x > -2f;
    }

    public float GetAdjustedVelocityX()
    {
        if (lastBodStoodOn == null)
        {
            Debug.Log("LAST STOOD ON IS NULL!!!");
            return this.bod.velocity.x;
        }
        return this.bod.velocity.x - lastBodStoodOn.velocity.x;
    }

    private void StopFlashing()
    {
        rend.color = Color.white;
        Camera.main.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile.chromaticAberration.enabled = false;
    }
}
