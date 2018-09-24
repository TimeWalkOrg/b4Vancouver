using UnityEngine;
using System.Collections;

public class OrcaCharacter : MonoBehaviour {
	Animator orcaAnimator;
	Rigidbody orcaRigid;
	
	public bool isLived=true;
	
	public float forwardSpeed=1f;
	public float turnSpeed=.3f;
	public float upDownSpeed=0f;
	public float rollSpeed = 0f;


	public float maxForwardSpeed=1f;
	public float maxTurnSpeed=100f;
	public float maxUpDownSpeed=100f;
	public float maxRollSpeed=100f;	
	
	
	
	void Start () {
		orcaAnimator = GetComponent<Animator> ();
		orcaRigid=GetComponent<Rigidbody>();
		
	}
	
	void FixedUpdate(){
		Move ();
	}
	
	
	public void Hit(){
		orcaAnimator.SetTrigger("Hit");
	}
	
	public void Bite(){
		orcaAnimator.SetTrigger("Bite");
	}	

	
	public void Death(){
		orcaAnimator.SetTrigger("Death");
		isLived = false;
	}
	
	public void Rebirth(){
		orcaAnimator.SetTrigger("Rebirth");
		isLived = true;
	}
	
	
	
	
	
	public void Move(){
		if (isLived) {
			//orcaRigid.velocity = transform.forward * forwardSpeed * maxForwardSpeed + transform.up * upDownSpeed * maxUpDownSpeed;

			if(forwardSpeed>.1f){
				orcaRigid.velocity = transform.forward * forwardSpeed * maxForwardSpeed;
				if(Mathf.Abs(rollSpeed)<.1f){
					transform.RotateAround (transform.position, -transform.right, upDownSpeed * Time.deltaTime * maxUpDownSpeed);
					transform.RotateAround (transform.position, transform.up, turnSpeed * Time.deltaTime * maxTurnSpeed);
				}
				transform.RotateAround (transform.position, transform.forward, rollSpeed * Time.deltaTime * maxRollSpeed);
			}

			orcaAnimator.SetFloat ("Forward", forwardSpeed);
			orcaAnimator.SetFloat ("Turn", turnSpeed);
			orcaAnimator.SetFloat ("UpDown", upDownSpeed);
			orcaAnimator.SetFloat ("Roll", rollSpeed);
		}
	}
}
