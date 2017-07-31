using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectManager : SingletonComponent<EffectManager> {

    public int currentLevel = 0;

    public int bulletsFired = 0;
    public int bulletsHit = 0;
    public int zombiesKilled = 0;
    public int rvsDestroyed = 0;

    public bool finishedFirst = false;

    public enum POWERS
    {
        EXTENDED_MAG,
        WILLY_GRENADE,
        GATLING_GIRL,
        EXTRA_DMG,
        EXTRA_HEALTH
    }

    private Transform player;

    private int gasCannisters = 0;

    private int extendedMag = 0;
    private int extendedHealth = 0;
    private int extendedDmg = 0;

    private bool willyGrenade = false;
    private bool gatlingGirl = false;

    private bool isFading = false;

    void Awake()
    {
        if(EffectManager.Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;

        base.Awake();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(!player && SceneManager.GetActiveScene().name.Contains("Play"))
        {
            if (!isFading)
            {
                FadeCanvasImage img = GameObject.FindGameObjectWithTag("Black").GetComponent<FadeCanvasImage>();
                img.fadeIn = true;
                img.fadeOut = false;

                isFading = true;

                Invoke("YouDead", 1.5f);
            }
        }
    }

    private void YouDead()
    {
        SceneManager.LoadScene("Dead");
    }

    public int GetExtraHealth()
    {
        return extendedHealth;
    }

    public void AddHealth(int hp)
    {
        extendedHealth += hp;
    }

    public int GetExtraDamage()
    {
        return extendedDmg;
    }

    public void AddDamage(int dmg)
    {
        extendedDmg += dmg;
    }

    public void AddExtendedMag(int extraRounds)
    {
        extendedMag += extraRounds;
    }

    public void GiveWilly()
    {
        willyGrenade = true;
    }

    public void GiveGatling()
    {
        gatlingGirl = true;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        isFading = false;
        Camera.main.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile.chromaticAberration.enabled = false;

        if (SceneManager.GetActiveScene().name.Contains("Play"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;


            PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            ShootGun gun = pc.transform.GetChild(0).GetComponent<ShootGun>();

            gun.clipSize += extendedMag;
        }
        else if (SceneManager.GetActiveScene().name.Contains("SelectPower"))
        {
            AddCannisters(Random.Range(1, 4));
        }
    }

    public void AddCannisters(int count)
    {
        gasCannisters += count;
    }

    public int CannisterCount()
    {
        return gasCannisters;
    }

    public bool SpendCannisters(int count)
    {
        if (gasCannisters - count < 0)
            return false;

        gasCannisters -= count;
        return true;
    }
}
