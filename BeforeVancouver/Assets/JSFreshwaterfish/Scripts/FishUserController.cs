using UnityEngine;
using System.Collections;

public class FishUserController : MonoBehaviour {
	FishCharacter fishCharacter;

	void Start () {
		fishCharacter = GetComponent < FishCharacter> ();
	}

	private void FixedUpdate()
	{

		if (Input.GetKeyDown (KeyCode.H)) {
			fishCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.G)) {
			fishCharacter.Eat();
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			fishCharacter.EatUp();
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			fishCharacter.EatLeft();
		}

		if (Input.GetKeyDown (KeyCode.V)) {
			fishCharacter.EatDown();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			fishCharacter.EatRight();
		}
        		
		if (Input.GetKeyDown (KeyCode.K)) {
			fishCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			fishCharacter.Rebirth();
		}		

		if (Input.GetKey(KeyCode.U)) {
			fishCharacter.upDownSpeed=Mathf.Lerp(fishCharacter.upDownSpeed,1f,Time.deltaTime);
		}		

		if (Input.GetKey(KeyCode.N)) {
			fishCharacter.upDownSpeed=Mathf.Lerp(fishCharacter.upDownSpeed,-1f,Time.deltaTime);
		}		


		fishCharacter.upDownSpeed=Mathf.Lerp(fishCharacter.upDownSpeed,0f,Time.deltaTime);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		fishCharacter.forwardSpeed = v;
		fishCharacter.turnSpeed = h;

	}
}
