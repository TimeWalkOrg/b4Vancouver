using UnityEngine;
using System.Collections;

public class BaldEagleUserControllerScript : MonoBehaviour {
	public BaldEagleCharacterScript baldEagleCharacter;
	public float upDownInputSpeed=3f;

	void Start () {
		baldEagleCharacter = GetComponent<BaldEagleCharacterScript> ();	
	}

	void Update(){
		if (Input.GetButtonDown ("Jump")) {
			baldEagleCharacter.Soar ();
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			baldEagleCharacter.BarrelRoll ();
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			baldEagleCharacter.Jump ();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			baldEagleCharacter.Hit ();
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			baldEagleCharacter.SitDown ();
		}

		if (Input.GetKeyDown (KeyCode.U)) {
			baldEagleCharacter.StandUp ();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			baldEagleCharacter.Down ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			baldEagleCharacter.Rebirth ();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			baldEagleCharacter.Grooming ();
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			baldEagleCharacter.TailGrooming ();
		}

		if (Input.GetButtonDown ("Fire2")) {
			baldEagleCharacter.Attack2 ();
		}
		
		if (Input.GetKeyDown (KeyCode.V)) {
			baldEagleCharacter.Call ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			baldEagleCharacter.Eat ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			baldEagleCharacter.CrouchStart ();
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			baldEagleCharacter.CrouchEnd ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			baldEagleCharacter.RunStart ();
		}
		
		if (Input.GetKeyUp (KeyCode.R)) {
			baldEagleCharacter.RunEnd ();
		}

		if (Input.GetButtonDown ("Fire1")) {
			baldEagleCharacter.Attack ();
		}
		if (Input.GetKey (KeyCode.N)) {
			baldEagleCharacter.upDown=Mathf.Clamp(baldEagleCharacter.upDown-Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
		if (Input.GetKey (KeyCode.U)) {
			baldEagleCharacter.upDown=Mathf.Clamp(baldEagleCharacter.upDown+Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
	}
	
	void FixedUpdate(){
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");	

		baldEagleCharacter.forwardAcceleration = v;
		baldEagleCharacter.yawVelocity = h;
	}
}
