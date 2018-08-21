using UnityEngine;
using System.Collections;

public class AyuUserController : MonoBehaviour {
    AyuCharacter ayuCharacter;

    void Start()
    {
        ayuCharacter = GetComponent<AyuCharacter>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ayuCharacter.Bite();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ayuCharacter.MouthOpen();
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            ayuCharacter.MouthClose();
        }

        if (Input.GetKey(KeyCode.U))
        {
            ayuCharacter.upDownAcceleration = Mathf.Lerp(ayuCharacter.upDownAcceleration, 1f, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.N))
        {
            ayuCharacter.upDownAcceleration = Mathf.Lerp(ayuCharacter.upDownAcceleration, -1f, Time.deltaTime);
        }


        ayuCharacter.upDownAcceleration = Mathf.Lerp(ayuCharacter.upDownAcceleration, 0f, Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        ayuCharacter.forwardAcceleration = v + 1f + ayuCharacter.minForwardAcceleration;
        ayuCharacter.turnAcceleration = h;

    }
}
