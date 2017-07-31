using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpendGasForPower : MonoBehaviour {

    public FadeCanvasImage fader;

    public EffectManager.POWERS power;

    public AudioClip money, noMoney;

    public void Spend()
    {
        int count = 9999;

        //asign value
        switch(power)
        {
            case EffectManager.POWERS.EXTENDED_MAG:
                count = 2;
                    break;
            case EffectManager.POWERS.GATLING_GIRL:
                count = 4;
                break;
            case EffectManager.POWERS.WILLY_GRENADE:
                count = 3;
                break;
            case EffectManager.POWERS.EXTRA_DMG:
                count = 2;
                break;
            case EffectManager.POWERS.EXTRA_HEALTH:
                count = 3;
                break;
        }

        if(EffectManager.Instance.SpendCannisters(count))
        {
            //give power
            switch (power)
            {
                case EffectManager.POWERS.EXTENDED_MAG:
                    EffectManager.Instance.AddExtendedMag(15);
                    break;
                case EffectManager.POWERS.GATLING_GIRL:
                    EffectManager.Instance.GiveGatling();
                    break;
                case EffectManager.POWERS.WILLY_GRENADE:
                    EffectManager.Instance.GiveWilly();
                    break;
                case EffectManager.POWERS.EXTRA_DMG:
                    EffectManager.Instance.AddDamage(3);
                    break;
                case EffectManager.POWERS.EXTRA_HEALTH:
                    EffectManager.Instance.AddHealth(4);
                    break;
            }

            GetComponent<AudioSource>().clip = money;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().clip = noMoney;
            GetComponent<AudioSource>().Play();
        }
    }
}
