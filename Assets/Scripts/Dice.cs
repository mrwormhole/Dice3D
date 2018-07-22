using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    public Material diceMaterial;
    Color blue = new Color32(34, 167, 240, 255);
    Color green = new Color32(38, 222, 129, 255);
    Color red = new Color32(252, 92, 101, 255);
    public Color royal = new Color32(100, 80, 196, 255);
    Color turquoise = new Color32(15, 185, 177, 255);
    Color yellow = new Color32(247, 183, 49, 255);
    public bool vibrationAllowed = false;
    public bool soundAllowed = false;
    public bool wizardAllowed = false;
    AudioSource auSource;

    void Start()
    {
        auSource = GetComponent<AudioSource>(); 
    }

    void FixedUpdate()
    {
        if (wizardAllowed)
        {
            doWizardStuff();
        }
    }

    public void setTexture(short n)
    {
        switch (n)
        {
            case 0:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = royal;
                break;
            case 1:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = blue;
                break;
            case 2:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = turquoise;
                break;
            case 3:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = green;
                break;
            case 4:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = yellow;
                break;
            case 5:
                GetComponent<Renderer>().material = diceMaterial;
                diceMaterial.color = red;
                break;

        }
    }

    public void doWizardStuff()
    {
        byte randN = (byte)Random.Range(0, 6);
        setTexture(randN);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dice" || collision.gameObject.tag == "ColliderLeft" || collision.gameObject.tag == "ColliderTop" || collision.gameObject.tag == "ColliderRight" || collision.gameObject.tag == "ColliderBottom")
        {
            if (soundAllowed)
            {
                auSource.Play(0);
            }
            if (vibrationAllowed)
            {
                Handheld.Vibrate();   
            }
        }
    }
}
