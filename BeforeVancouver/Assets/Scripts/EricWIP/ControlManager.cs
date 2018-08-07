using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    #region Singleton
    private static ControlManager _instance = null;
    public static ControlManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ControlManager>();
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnApplicationQuit()
    {
        _instance = null;
        DestroyImmediate(gameObject);
    }
    #endregion

    // controller
    public GameObject fpsController;
    public GameObject vrController;
    public Transform playerStartT;
    private GameObject player;
    private GameObject avatar;

    // time active
    public YearData[] yearData;
    public int currentYearIndex;// { get; private set; }

    // night day
    public Material daySkyboxMat;
    public Material nightSkyboxMat;
    public Color dayLightColor;
    public Color nightLightColor;
    public Color daySkyColor;
    public Color nightSkyColor;
    public float dayLightIntensity = 0.4f;
    public float nightLightIntensity = 0.1f;
    public Light lightComponent;
    public bool isDay = true;

    private void Start()
    {
        if (UnityEngine.XR.XRSettings.enabled)
        {
            player = Instantiate(vrController, playerStartT.position, playerStartT.rotation);
        }
        else
        {
            player = Instantiate(fpsController, playerStartT.position, playerStartT.rotation);
        }
        SetYear(1800);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
            SetYear();

        if (Input.GetKeyUp(KeyCode.N))
            ToggleDayNight();


        if (Input.GetKeyUp(KeyCode.Q))
            Application.Quit();
        #if UNITY_STANDALONE_WIN
        if (UnityEngine.XR.XRSettings.enabled)
		{
			OVRInput.Button oculusTouchButtonA = OVRInput.Button.PrimaryIndexTrigger;
            // OVRInput.Button oculusTouchButtonB = OVRInput.Button.One; Commented out for Oculus Rift - check if GO running

            OVRInput.Button oculusTouchButtonB = OVRInput.Button.Two;
            

            OVRInput.Controller activeController = OVRInput.GetActiveController();

			if (OVRInput.GetUp(oculusTouchButtonA))
			{
				SetYear();
			}

			if (OVRInput.GetUp(oculusTouchButtonB))
			{
				ToggleDayNight();
			}
		}
        #endif
	}

	private void SetYear(bool isIncrement = true)
	{
		currentYearIndex = isIncrement ? (currentYearIndex >= yearData.Length - 1 ? 0 : currentYearIndex + 1) : (currentYearIndex > 0 ? currentYearIndex - 1 : yearData.Length - 1);
		SendYearDataMissive(yearData[currentYearIndex]);
	}

	private void SetYear(int year)
	{
		for (int i = 0; i < yearData.Length; i++)
		{
			if (year == yearData[i].year)
			{
				currentYearIndex = i;
			}
		}
		currentYearIndex = 0;
		SendYearDataMissive(yearData[currentYearIndex]);
	}

	private void SendYearDataMissive(YearData data)
	{
		YearDataMissive missive = new YearDataMissive();
		missive.data = data;
		Missive.Send(missive);
	}

	private void ToggleDayNight()
	{
		isDay = !isDay;
		RenderSettings.skybox = isDay ? daySkyboxMat: nightSkyboxMat;
		RenderSettings.ambientLight = isDay ? daySkyColor : nightSkyColor;
		RenderSettings.ambientIntensity = isDay ? dayLightIntensity : nightLightIntensity;
		lightComponent.color = isDay ? dayLightColor : nightLightColor;
	}
}



[System.Serializable]
public class YearData
{
	public int year;
}

public class YearDataMissive : Missive
{
	public YearData data;
}
