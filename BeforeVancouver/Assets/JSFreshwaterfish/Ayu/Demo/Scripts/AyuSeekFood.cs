using UnityEngine;
using System.Collections;

public class AyuSeekFood : MonoBehaviour {
    public GameObject goalGameObject;

    AyuCharacter ayuCharacter;
    Vector3 goalRelPos;
    float goalDistance = 0f;
    Vector3 initialPosition;
    float goalChangeTime = 0f;


    void Start()
    {
        ayuCharacter = GetComponent<AyuCharacter>();
        goalGameObject = new GameObject();
        initialPosition = transform.position;
        GoalChange();
        goalChangeTime = Random.Range(0f, 5f);
        goalRelPos = goalGameObject.transform.position - transform.position;
    }

    void GoalChange()
    {
        goalChangeTime = Random.Range(0f,5f);
        goalGameObject.transform.position = initialPosition + Random.insideUnitSphere *2f;
    }

    void FixedUpdate()
    {
        goalDistance = (goalGameObject.transform.position - transform.position).sqrMagnitude;
        ayuCharacter.forwardAcceleration = Mathf.Clamp(goalDistance, ayuCharacter.minForwardAcceleration, ayuCharacter.minForwardAcceleration + 2f);

        goalRelPos = transform.InverseTransformPoint(goalGameObject.transform.position).normalized;
        if (goalRelPos.z > 0f)
        {
            ayuCharacter.turnAcceleration = goalRelPos.x;
        }
        else
        {
            if (goalRelPos.x > 0f)
            {
                ayuCharacter.turnAcceleration = 1f;
            }
            else
            {
                ayuCharacter.turnAcceleration = -1f;
            }
        }
        ayuCharacter.upDownAcceleration = goalRelPos.y;

        goalChangeTime -= Time.deltaTime;
        if (goalDistance<1f || goalChangeTime<0f) {
            GoalChange();
        }
    }
}
