using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour {

    public Sprite sprite14, sprite24, sprite34, sprite44;

    private Image img;

    private int currentQuarters = 4;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        SetLifeLeft(currentQuarters);
	}

    void Update()
    {
        if (this.transform.parent.GetChild(this.transform.parent.childCount - 1) == this.transform)
        {
            float size = 1f + (Mathf.Sin(Time.time * 4f) * 0.1f);
            img.transform.localScale = new Vector3(size, size, 0);
        }
    }

    public void RemoveQuarters(int amt)
    {
        currentQuarters -= amt;

        if(currentQuarters <= 0)
        {
            Destroy(this.gameObject);
        }

        SetLifeLeft(currentQuarters);
    }

    public void SetLifeLeft(int quarters)
    {
        switch(quarters)
        {
            case 0:
                img.sprite = null;
                break;

            case 1:
                img.sprite = sprite14;
                break;

            case 2:
                img.sprite = sprite24;
                break;

            case 3:
                img.sprite = sprite34;
                break;

            case 4:
                img.sprite = sprite44;
                break;
        }
    }
}
