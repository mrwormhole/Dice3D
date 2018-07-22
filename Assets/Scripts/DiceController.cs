using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour {

    public bool paused = false;
    GameObject touchedObject = null;
    Plane plane;
    Vector3 offsetForDiceFinalPosition;
    Vector3 lastPos;
    Vector3 velocity; 
    float screenHalfWidthInWorldUnits;
    float screenHalfHeightInWorldUnits;
    GameObject CLeft;
    GameObject CTop;
    GameObject CRight;
    GameObject CBottom;
    public GameObject originalDicePrefab;
    GameObject[] allDice = new GameObject[8];
    public byte currentDiceNumber = 0;
    public byte controllerNumber = 0;

    void Start()
    {
        initWalls();
    }

    void Update () {
        if (!paused)
        {
            if(controllerNumber == 0)
            {
                injectTouchController();
            }
            else if(controllerNumber == 1)
            {
                injectTiltController();
            }
            checkYPositionOfallDice();
        }
	}

    void injectTouchController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = generateSafeMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit) && hit.transform.tag == "dice")
            {
                touchedObject = hit.transform.gameObject;
                plane = new Plane(Vector3.up, touchedObject.transform.position);
                touchedObject.GetComponent<Rigidbody>().velocity = Vector3.zero; //Resetting previous velocity
                touchedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //Resetting previous velocity

                //Calculating offset
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float rayDistance;
                plane.Raycast(myRay, out rayDistance);
                offsetForDiceFinalPosition = touchedObject.transform.position - myRay.GetPoint(rayDistance);
            }
        }
        else if (Input.GetMouseButton(0) && touchedObject)
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (plane.Raycast(myRay, out rayDistance)) 
            {
                touchedObject.transform.position = myRay.GetPoint(rayDistance) + offsetForDiceFinalPosition; 
                velocity = touchedObject.transform.position - lastPos;
                lastPos = touchedObject.transform.position;
            }
        }
        else if (Input.GetMouseButtonUp(0) && touchedObject)
        {
            touchedObject.GetComponent<Rigidbody>().AddForce(velocity * 1000,ForceMode.Acceleration);
            touchedObject = null;
        }
    }

    void injectTiltController()
    {
        Vector3 tilt = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
        for (int i = 0; i < currentDiceNumber; i++)
        {
            allDice[i].GetComponent<Rigidbody>().AddForce(tilt * 100, ForceMode.Acceleration);
        }
    }

    void initWalls()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        screenHalfHeightInWorldUnits = Camera.main.orthographicSize;
        CLeft = GameObject.FindGameObjectWithTag("ColliderLeft");
        CTop = GameObject.FindGameObjectWithTag("ColliderTop");
        CRight = GameObject.FindGameObjectWithTag("ColliderRight");
        CBottom = GameObject.FindGameObjectWithTag("ColliderBottom");
        CLeft.transform.position = new Vector3(-screenHalfWidthInWorldUnits - 0.50f, 0, 0);
        CTop.transform.position = new Vector3(0, 0, screenHalfHeightInWorldUnits + 0.50f);
        CRight.transform.position = new Vector3(screenHalfWidthInWorldUnits + 0.50f, 0, 0);
        CBottom.transform.position = new Vector3(0, 0, -screenHalfHeightInWorldUnits - 0.50f);
        createOne();
    }

    Ray generateSafeMouseRay()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mousePosFarInWorldUnits = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosNearInWorldUnits = Camera.main.ScreenToWorldPoint(mousePosNear);
        Ray ray = new Ray(mousePosNearInWorldUnits, mousePosFarInWorldUnits - mousePosNearInWorldUnits);
        return ray;
    }

    void checkYPositionOfallDice()
    {
        for(int i = 0; i < currentDiceNumber; i++)
        {
            if (allDice[i].transform.position.y < -4 )
            {
                Handheld.Vibrate();
                allDice[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                allDice[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                allDice[i].transform.rotation = Quaternion.Euler(-90, 0, 0);
                allDice[i].transform.position = new Vector3(0.40f, 2, 0.40f);
            }
        }
    }

    public void createOne()
    {
        if(currentDiceNumber < 8)
        {
            GameObject anotherDice = Instantiate(originalDicePrefab) as GameObject;
            allDice[currentDiceNumber] = anotherDice;
            currentDiceNumber++;
        }
    }

    public void destroyAll()
    {
        for(int i = 0; i < currentDiceNumber; i++)
        {
            Destroy(allDice[i]);
        }
        currentDiceNumber = 0;
    }

    public void setVibration(byte b)
    {
        if(b == 0)
        {
            for (int i = 0; i < currentDiceNumber; i++)
            {
                allDice[i].GetComponent<Dice>().vibrationAllowed = false;
            }
        }
        else if(b == 1)
        {
            for (int i = 0; i < currentDiceNumber; i++)
            {
                allDice[i].GetComponent<Dice>().vibrationAllowed = true;
            }
        }
       
    }

    public void setSound(byte b)
    {
        if (b == 0)
        {
            for (int i = 0; i < currentDiceNumber; i++)
            {
                allDice[i].GetComponent<Dice>().soundAllowed = false;
            }
        }
        else if (b == 1)
        {
            for (int i = 0; i < currentDiceNumber; i++)
            {
                allDice[i].GetComponent<Dice>().soundAllowed = true;
            }
        }
    }

    public void setWizardStuffActive()
    {
        for (int i = 0; i < currentDiceNumber; i++)
        {
            allDice[i].GetComponent<Dice>().wizardAllowed = true;
        }
    }
}
