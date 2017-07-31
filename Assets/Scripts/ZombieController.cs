using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public GameObject bloodParticlePrefab;
    public GameObject headGib, legGib, legGib2, bodyGib;

    public AudioClip hitSound, deathSound;

    public Transform player;

    public int hp = 25;

    private Rigidbody2D bod;
    private SpriteRenderer rend;

    private float jumpTimer = 0f;
    private bool canJump = true;

    private bool onGround = false;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        bod = this.GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

	void Update () {
        if (!player)
            return;

        jumpTimer += Time.deltaTime;
        if(jumpTimer > 1.5f)
        {
            canJump = true;
            jumpTimer = 0f;
        }

        float dist = Vector3.Distance(this.transform.position, player.position);

        if(dist > 20)
        {
            Destroy(this.gameObject);
        }

        if (player.position.x > this.transform.position.x)
        {
            if (dist > 1f)
            {
                if (this.bod.velocity.x < 3.5f)
                    bod.AddForce(Vector2.right * 850f * Time.deltaTime);
            }

            if(dist < 1.1f)
            {
                if (canJump && onGround)
                {
                    canJump = false;
                    bod.AddForce((player.position - this.transform.position) * 5500f * Time.deltaTime);
                }
            }
        }

        if(this.bod.velocity.x == 0)
        {
            bod.AddForce(Vector2.up * 100f * Time.deltaTime);
        }

        if(this.transform.position.y < -25f)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Zombie" || col.gameObject.tag == "Platform")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
        else if(col.gameObject.tag == "Road")
        {
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Road")
        {
            onGround = false;
        }
    }

    void OnDestroy()
    {
        if(ZombieSpawner.Instance)
            ZombieSpawner.Instance.RemoveZombie();
    }


    public void Damage(int amount)
    {
        GetComponent<AudioSource>().clip = hitSound;
        GetComponent<AudioSource>().volume = 0.25f;
        GetComponent<AudioSource>().Play();

        hp -= amount;

        rend.color = Color.red;
        Invoke("StopFlashing", 0.05f);

        if(hp <= 0)
        {
            GameObject newGib1 = Instantiate(headGib, this.transform.position, new Quaternion());
            newGib1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-1f, 1f), Random.Range(-1f, 1f)));
            GameObject newGib2 = Instantiate(bodyGib, this.transform.position, new Quaternion());
            newGib1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
            GameObject newGib3 = Instantiate(legGib, this.transform.position, new Quaternion());
            newGib1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
            GameObject newGib4 = Instantiate(legGib2, this.transform.position, new Quaternion());
            newGib1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

            Destroy(newGib1, 1f + Random.Range(0f, 3f));
            Destroy(newGib2, 1f + Random.Range(0f, 3f));
            Destroy(newGib3, 1f + Random.Range(0f, 3f));
            Destroy(newGib4, 1f + Random.Range(0f, 3f));

            EffectManager.Instance.zombiesKilled++;

            GetComponent<AudioSource>().clip = deathSound;
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().Play();

            Destroy(this.gameObject);
        }
    }

    void StopFlashing()
    {
        rend.color = Color.white;
    }
}
