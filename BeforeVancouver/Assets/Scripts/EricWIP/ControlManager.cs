using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
	public GameObject vrMobileController;
    public GameObject yearDisplay;

    public Transform playerStartT;
    private GameObject player;
    private GameObject avatar;

	// public so OVRPlayerController can access
	public bool isGO { get; set; }
	private bool isTouch = false;

    // time active
    public YearData[] yearData;
    private string yearString;
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

	// teleport
	public float yAddition = 1.0f;
	public Transform[] teleportLocations;
	private int currentLocationIndex = 0;

	private void Start()
    {
        if (UnityEngine.XR.XRSettings.enabled)
        {
			OVRPlugin.SystemHeadset headsetType = OVRPlugin.GetSystemHeadsetType();
			isGO = headsetType == OVRPlugin.SystemHeadset.Oculus_Go ? true : false;
			isTouch = headsetType == OVRPlugin.SystemHeadset.Rift_CV1 || headsetType == OVRPlugin.SystemHeadset.Rift_DK1 || headsetType == OVRPlugin.SystemHeadset.Rift_DK2 ? true : false;

			if (isGO)
				Instantiate(vrMobileController, playerStartT.position, playerStartT.rotation);

			if (isTouch)
				Instantiate(vrController, playerStartT.position, playerStartT.rotation);
        }
        else
        {
            player = Instantiate(fpsController, playerStartT.position, playerStartT.rotation);
        }

		player = GameObject.FindGameObjectWithTag("Player");

        SetYear(1800);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
            SetYear();

		if (Input.GetKeyUp(KeyCode.N))
			ToggleDayNight();

        if (Input.GetKeyUp(KeyCode.T))
            TeleportToLocation();

        if (Input.GetKeyUp(KeyCode.Q))
            Application.Quit();
        
        if (UnityEngine.XR.XRSettings.enabled)
		{
			OVRInput.Button oculusTouchButtonA = OVRInput.Button.One; // Oculus GO touch pad click
            OVRInput.Button oculusTouchButtonB = OVRInput.Button.Two; // Oculus GO: Back button for Day/Night
			OVRInput.Button oculusTouchButtonC = isGO ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.PrimaryThumbstick; // Oculus GO trigger / Touch press thumbstick

			OVRInput.Controller activeController = OVRInput.GetActiveController();

			if (OVRInput.GetUp(oculusTouchButtonA))
			{
            #if UNITY_ANDROID // if Android, then running Oculus GO
                TeleportToLocation(); // setting for Oculus GO
            #else
                SetYear(); // setting for Oculus Rift
            #endif
            }

            if (OVRInput.GetUp(oculusTouchButtonB))
            {
				ToggleDayNight();
			}

			if (OVRInput.GetUp(oculusTouchButtonC))
			{
                //TeleportToLocation(); // setting for Oculus Rift

                #if UNITY_ANDROID // if Android, then running Oculus GO
                    SetYear(); // setting for Oculus GO
                #else
                    TeleportToLocation(); // setting for Oculus Rift
                #endif
            }
        }
	}

	public void MovePlayerAboveTerrain()
	{
		StartCoroutine(DelayUntilTerrainActive());
	}

	IEnumerator DelayUntilTerrainActive()
	{
		while (Terrain.activeTerrain == null || player == null)
			yield return null;

		player.transform.position = new Vector3(player.transform.position.x, Terrain.activeTerrain.SampleHeight(player.transform.position) + yAddition, player.transform.position.z);
	}

	private void SetYear(bool isIncrement = true)
	{
		currentYearIndex = isIncrement ? (currentYearIndex >= yearData.Length - 1 ? 0 : currentYearIndex + 1) : (currentYearIndex > 0 ? currentYearIndex - 1 : yearData.Length - 1);
        yearString = yearData[currentYearIndex].year.ToString();
 //       Debug.Log(yearString);

        TextMeshPro textmeshPro = yearDisplay.GetComponent<TextMeshPro>();
        textmeshPro.SetText(yearString);

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
        yearString = yearData[currentYearIndex].year.ToString();
 //       Debug.Log(yearString);
        TextMeshPro textmeshPro = yearDisplay.GetComponent<TextMeshPro>();
        textmeshPro.SetText(yearString);
        SendYearDataMissive(yearData[currentYearIndex]);
        currentYearIndex = 0;

    }

	private void SendYearDataMissive(YearData data)
	{
		YearDataMissive missive = new YearDataMissive();
		missive.data = data;
		Missive.Send(missive);
	}

    private void TeleportToLocation()
    {
		currentLocationIndex = currentLocationIndex >= teleportLocations.Length - 1 ? 0 : currentLocationIndex + 1;
		player.transform.position = new Vector3(teleportLocations[currentLocationIndex].position.x, Terrain.activeTerrain.SampleHeight(teleportLocations[currentLocationIndex].position) + yAddition, teleportLocations[currentLocationIndex].position.z);
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
