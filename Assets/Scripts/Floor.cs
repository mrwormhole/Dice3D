using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public Material planeMaterial;
    public Material wood;
    public Material stone;
    Color blue = new Color32(34, 167, 240, 255);
    Color green = new Color32(38, 222, 129, 255);
    Color red = new Color32(252, 92, 101, 255);
    Color royal = new Color32(100, 80, 196, 255);
    Color turquoise = new Color32(15, 185, 177, 255);
    Color yellow = new Color32(247, 183, 49, 255);

    void Start()
    {   
        planeMaterial.color = blue;
    }

    public void setTexture(short n)
    {
        switch (n)
        {
            case 0:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = royal;
                break;
            case 1:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = blue;
                break;
            case 2:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = turquoise;
                break;
            case 3:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = green;
                break;
            case 4:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = yellow;
                break;
            case 5:
                GetComponent<Renderer>().material = planeMaterial;
                planeMaterial.color = red;
                break;
            case 6:
                GetComponent<Renderer>().material = wood;
                break;
            case 7:
                GetComponent<Renderer>().material = stone;
                break;

        }
    }

    public void setRandomTextureTOfFloor()
    {
        byte randN = (byte)Random.Range(0, 8);
        setTexture(randN);
    }
}
