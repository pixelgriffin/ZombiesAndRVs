using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour {

    public AudioClip reloadSound;
    public GameObject shellPrefab;

    public float clipReloadTime = 1.5f;
    public float reloadTime = 0.1f;
    public int damage = 3;
    public int clipSize = 32;

    public Transform muzzle;
    public Transform ejector;

    private bool reload = false;
    private bool canShoot = true;
    private float time = 0f;

    private int rounds;

    private LineRenderer rend;

	void Start () {
        rend = GetComponent<LineRenderer>();

        rounds = clipSize;//full mag start
	}


	void Update () {
        if (!canShoot)
        {
            time += Time.deltaTime;

            //mag reload
            if (reload)
            {
                if(time >= clipReloadTime)
                {
                    rounds = clipSize;

                    reload = false;
                    canShoot = true;
                    time = 0f;
                }
            }
            else
            {
                //round load
                if (time > reloadTime)
                {
                    canShoot = true;
                    time = 0f;
                }
            }
        }
        else
        {
            //mag check
            if((rounds - 1) <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<AudioSource>().clip = reloadSound;
                GetComponent<AudioSource>().Play();

                reload = true;
                canShoot = false;
                time = 0f;

                if(Input.GetKeyDown(KeyCode.R))
                    return;
            }

            //shoot
            if (Input.GetMouseButton(0))
            {
                EffectManager.Instance.bulletsFired++;

                muzzle.GetComponent<AudioSource>().Play();

                Vector3 mouseDir = Input.mousePosition;
                mouseDir.z = 0f;
                mouseDir = Camera.main.ScreenToWorldPoint(mouseDir);
                mouseDir = mouseDir - muzzle.position;
                mouseDir.z = 0f;

                RaycastHit2D hit = Physics2D.Raycast(muzzle.position, mouseDir);
                if (hit)
                {
                    rend.SetPosition(0, muzzle.position);
                    rend.SetPosition(1, hit.point);

                    if(hit.collider.tag == "Zombie")
                    {
                        hit.collider.gameObject.GetComponent<ZombieController>().Damage(3 + EffectManager.Instance.GetExtraDamage());
                        Instantiate(hit.collider.gameObject.GetComponent<ZombieController>().bloodParticlePrefab, new Vector3(hit.point.x, hit.point.y, -0.25f), new Quaternion());

                        EffectManager.Instance.bulletsHit++;

                    }
                }
                else
                {
                    rend.SetPosition(0, muzzle.position);
                    rend.SetPosition(1, mouseDir * 1000f);
                }

                rend.enabled = true;
                Invoke("HideBulletLine", reloadTime / 2f);

                canShoot = false;

                rounds--;

                if(shellPrefab != null)
                    Instantiate(shellPrefab, ejector.position, new Quaternion());
            }
        }
	}

    private void HideBulletLine()
    {
        rend.enabled = false;
    }
}
