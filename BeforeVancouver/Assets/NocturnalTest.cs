using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NocturnalTest : MonoBehaviour
{
    bool isDayLast;
    bool isDayNow;
    public bool visibleInDay = true;
    public bool visibleInNight = true;

    // Start is called before the first frame update
    void Start()
    {
        isDayNow = GameObject.Find("ControlManager").GetComponent<ControlManager>().isDay;
        isDayLast = isDayNow;
        if (isDayNow && visibleInDay)
        {
            Debug.Log("Daytime: show objects");
            showChildren(true);
        }
        else
        {
            Debug.Log("Daytime: hide objects");
            showChildren(false);
        }
        if (!isDayNow && visibleInNight)
        {
            Debug.Log("Night: show objects");
            showChildren(true);
        }
        else
        {
            Debug.Log("Night: hide objects");
            showChildren(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isDayNow = GameObject.Find("ControlManager").GetComponent<ControlManager>().isDay;
        if (isDayNow != isDayLast)
        {
            isDayLast = isDayNow;
            if(isDayNow && visibleInDay)
            {
                Debug.Log("Daytime: show objects");
                showChildren(true);
            }
            else
            {
                Debug.Log("Daytime: hide objects");
                showChildren(false);
            }
            if (!isDayNow && visibleInNight)
            {
                Debug.Log("Night: show objects");
                showChildren(true);
            }
            else
            {
                Debug.Log("Night: hide objects");
                showChildren(false);
            }
        }
    }
    void showChildren(bool isVisible)
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(isVisible);
        }
    }
}
