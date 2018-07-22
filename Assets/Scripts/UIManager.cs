using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameObject diceController;
    DiceController diceControllerScript;
    public GameObject dicePrefab;
    Dice diceScript;
    public GameObject Plane;
    Floor floorScript;
    public GameObject DirectionalLight;
    public GameObject SettingsPanel;
    public Button SecretHomeButton;
    public Button TouchControlButton;
    public Button TiltControlButton;
    public Sprite TrueGreen;
    public Sprite FalseRed;
    public Dropdown DiceColorDropdown;
    public Dropdown PlaneColorDropdown;
    public Slider BrightnessSlider;
    public Toggle VibrationToggle;
    public Toggle SoundToggle;
    static float t = 0.0f;
    bool slideInAnimationEnabled;
    bool slideOutAnimationEnabled;
    bool touchControlEnabled = true;
    bool tiltContolEnabled = false;

	void Start () {
        diceController = GameObject.FindGameObjectWithTag("diceController");
        diceControllerScript = diceController.GetComponent<DiceController>();
        diceScript = dicePrefab.GetComponent<Dice>();
        diceScript.diceMaterial.color = diceScript.royal;
        floorScript = Plane.GetComponent<Floor>();
        DiceColorDropdown.onValueChanged.AddListener(delegate { onValueChangeOfDiceColorDropdown(DiceColorDropdown); });
        PlaneColorDropdown.onValueChanged.AddListener(delegate { onValueChangeOfPlaneColorDropdown(PlaneColorDropdown); });
        BrightnessSlider.onValueChanged.AddListener(delegate { adjustBrightness(); });
	}

	void Update () {
        if (slideInAnimationEnabled)
        {
            SettingsPanel.transform.localPosition = new Vector3(Mathf.Lerp(300, 3, t), 0, 0);
            t += 1.2f * Time.deltaTime;
            if(t >= 1.0f)
            {
                t = 0.0f;
                slideInAnimationEnabled = false;
            }
        }
        if (slideOutAnimationEnabled)
        {
            SettingsPanel.transform.localPosition = new Vector3(Mathf.Lerp(3, 300, t), 0, 0);
            t += 1.5f * Time.deltaTime;
            if (t >= 1.0f)
            {
                t = 0.0f;
                slideOutAnimationEnabled = false;
            }
        }
	}

    public void onClickSettingsButton()
    {
        diceControllerScript.paused = true;
        slideInAnimationEnabled = true;
        SecretHomeButton.gameObject.SetActive(true);
    }

    public void onClickSecretHomeButton()
    {
        diceControllerScript.paused = false;
        slideOutAnimationEnabled = true;
        SecretHomeButton.gameObject.SetActive(false);
    }

    public void onClickAddButton()
    {
        diceControllerScript.createOne();
    }

    public void onClickClearButton()
    {
        diceControllerScript.destroyAll();
        diceScript.wizardAllowed = false;
    }

    public void onClickTouchControlButton()
    {
        if (touchControlEnabled)
        {
            TouchControlButton.GetComponent<Image>().sprite = FalseRed;
            touchControlEnabled = false;
            tiltContolEnabled = true;
            TiltControlButton.GetComponent<Image>().sprite = TrueGreen;
            diceControllerScript.controllerNumber = 1;
        }
        else if (!touchControlEnabled)
        {
            TouchControlButton.GetComponent<Image>().sprite = TrueGreen;
            touchControlEnabled = true;
            tiltContolEnabled = false;
            TiltControlButton.GetComponent<Image>().sprite = FalseRed;
            diceControllerScript.controllerNumber = 0;
        }
      
    }

    public void onClickTiltControlButton()
    {
        if (tiltContolEnabled)
        {
            TiltControlButton.GetComponent<Image>().sprite = FalseRed;
            tiltContolEnabled = false;
            touchControlEnabled = true;
            TouchControlButton.GetComponent<Image>().sprite = TrueGreen;
            diceControllerScript.controllerNumber = 0;
        }
        else if (!tiltContolEnabled)
        {
            TiltControlButton.GetComponent<Image>().sprite = TrueGreen;
            tiltContolEnabled = true;
            touchControlEnabled = false;
            TouchControlButton.GetComponent<Image>().sprite = FalseRed;
            diceControllerScript.controllerNumber = 1;
        }
    }

    public void onValueChangeOfDiceColorDropdown(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                diceScript.setTexture(0);
                break;
            case 1:
                diceScript.setTexture(1);
                break;
            case 2:
                diceScript.setTexture(2);
                break;
            case 3:
                diceScript.setTexture(3);
                break;
            case 4:
                diceScript.setTexture(4);
                break;
            case 5:
                diceScript.setTexture(5);
                break;
        }
    }

    public void onValueChangeOfPlaneColorDropdown(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                floorScript.setTexture(0);
                break;
            case 1:
                floorScript.setTexture(1);
                break;
            case 2:
                floorScript.setTexture(2);
                break;
            case 3:
                floorScript.setTexture(3);
                break;
            case 4:
                floorScript.setTexture(4);
                break;
            case 5:
                floorScript.setTexture(5);
                break;
            case 6:
                floorScript.setTexture(6);
                break;
            case 7:
                floorScript.setTexture(7);
                break;

        }
    }

    public void adjustBrightness() {
        DirectionalLight.GetComponent<Light>().intensity = BrightnessSlider.value;
    }

    public void onClickVibrationToggle()
    {
        if (VibrationToggle.isOn)
        {
            diceScript.vibrationAllowed = true; //for prefab
            diceControllerScript.setVibration(1); //for available objs
        }
        else if (!VibrationToggle.isOn) {
            diceScript.vibrationAllowed = false; //for prefab
            diceControllerScript.setVibration(0); //for available objs
        }
    }

    public void onClickSoundToggle()
    {
        if (SoundToggle.isOn)
        {
            diceScript.soundAllowed = true; //for prefab
            diceControllerScript.setSound(1); //for available objs
        }
        if (!SoundToggle.isOn)
        {
            diceScript.soundAllowed = false; //for prefab
            diceControllerScript.setSound(0); //for available objs
        }
    }

    public void onClickWizardButton()
    {
        floorScript.setRandomTextureTOfFloor();
        diceScript.wizardAllowed = true;
        diceControllerScript.setWizardStuffActive();
    }

    public void onClickExitButton()
    {
        Application.Quit();
    }


}
