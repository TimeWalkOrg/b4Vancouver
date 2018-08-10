using UnityEngine;
using System.Collections;

public class RiverFishSeekFood : MonoBehaviour {
    public GameObject goalGameObject;

    RiverFishCharacter riverFishCharacter;
    Vector3 goalRelPos;
    float goalDistance = 0f;
    Vector3 initialPosition;
    float goalChangeTime = 0f;


    void Start()
    {
        riverFishCharacter = GetComponent<RiverFishCharacter>();
        goalGameObject = new GameObject();
        initialPosition = transform.position;
        GoalChange();
        goalChangeTime = Random.Range(0f, 5f);
        goalRelPos = goalGameObject.transform.position - transform.position;
    }

    void GoalChange()
    {
        goalChangeTime = Random.Range(0f, 5f);
        goalGameObject.transform.position = initialPosition + Random.insideUnitSphere * 2f;
    }

    void FixedUpdate()
    {
        goalDistance = (goalGameObject.transform.position - transform.position).sqrMagnitude;
        riverFishCharacter.forwardAcceleration = Mathf.Clamp(goalDistance, riverFishCharacter.minForwardAcceleration, riverFishCharacter.minForwardAcceleration + 2f);

        goalRelPos = transform.InverseTransformPoint(goalGameObject.transform.position).normalized;
        if (goalRelPos.z > 0f)
        {
            riverFishCharacter.turnAcceleration = goalRelPos.x;
        }
        else
        {
            if (goalRelPos.x > 0f)
            {
                riverFishCharacter.turnAcceleration = 1f;
            }
            else
            {
                riverFishCharacter.turnAcceleration = -1f;
            }
        }
        riverFishCharacter.upDownAcceleration = goalRelPos.y;

        goalChangeTime -= Time.deltaTime;
        if (goalDistance < 1f || goalChangeTime < 0f)
        {
            GoalChange();
        }
    }
}
