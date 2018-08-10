using UnityEngine;
using System.Collections;

public class RiverFishUserController : MonoBehaviour {
    RiverFishCharacter riverFishCharacter;

    void Start()
    {
        riverFishCharacter = GetComponent<RiverFishCharacter>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            riverFishCharacter.Bite();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            riverFishCharacter.MouthOpen();
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            riverFishCharacter.MouthClose();
        }

        if (Input.GetKey(KeyCode.U))
        {
            riverFishCharacter.upDownAcceleration = Mathf.Lerp(riverFishCharacter.upDownAcceleration, 1f, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.N))
        {
            riverFishCharacter.upDownAcceleration = Mathf.Lerp(riverFishCharacter.upDownAcceleration, -1f, Time.deltaTime);
        }


        riverFishCharacter.upDownAcceleration = Mathf.Lerp(riverFishCharacter.upDownAcceleration, 0f, Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        riverFishCharacter.forwardAcceleration = v + 1f + riverFishCharacter.minForwardAcceleration;
        riverFishCharacter.turnAcceleration = h;

    }
}
