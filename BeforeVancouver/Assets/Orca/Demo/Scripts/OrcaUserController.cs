using UnityEngine;
using System.Collections;

public class OrcaUserController : MonoBehaviour {
	OrcaCharacter orcaCharacter;
	
	void Start () {
		orcaCharacter = GetComponent < OrcaCharacter> ();
	}
	
	private void FixedUpdate()
	{
		
		if (Input.GetKeyDown (KeyCode.H)) {
			orcaCharacter.Hit();
		}

		if (Input.GetButtonDown ("Fire1")) {
			orcaCharacter.Bite();
		}	

		
		if (Input.GetKeyDown (KeyCode.K)) {
			orcaCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.L)) {
			orcaCharacter.Rebirth();
		}		
		
		if (Input.GetKey(KeyCode.U)) {
			orcaCharacter.upDownSpeed=Mathf.Lerp(orcaCharacter.upDownSpeed,1f,Time.deltaTime);
		}		
		
		if (Input.GetKey(KeyCode.N)) {
			orcaCharacter.upDownSpeed=Mathf.Lerp(orcaCharacter.upDownSpeed,-1f,Time.deltaTime);
		}		

		if (Input.GetKey(KeyCode.R)) {
			orcaCharacter.rollSpeed=Mathf.Lerp(orcaCharacter.rollSpeed,1f,Time.deltaTime);
		}		
		
		if (Input.GetKey(KeyCode.T)) {
			orcaCharacter.rollSpeed=Mathf.Lerp(orcaCharacter.rollSpeed,-1f,Time.deltaTime);
		}		



		
		orcaCharacter.upDownSpeed=Mathf.Lerp(orcaCharacter.upDownSpeed,0f,Time.deltaTime);
		orcaCharacter.rollSpeed=Mathf.Lerp(orcaCharacter.rollSpeed,0f,Time.deltaTime);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		
		orcaCharacter.forwardSpeed = v;
		orcaCharacter.turnSpeed = h;
		
	}
}
