using UnityEngine;
using System.Collections;

public class FishCharacter : MonoBehaviour {
	Animator fishAnimator;
	Rigidbody fishRigid;

	public bool isLived=true;
    public float forwardSpeed=1f;
	public float turnSpeed=.3f;
	public float upDownSpeed=0f;
    public float maxForwardSpeed=1f;
	public float maxTurnSpeed=100f;
	public float maxUpDownSpeed=1f;

    
	void Start () {
		fishAnimator = GetComponent<Animator> ();
		fishRigid=GetComponent<Rigidbody>();

	}
	
	void FixedUpdate(){
		Move ();
	}
	

	public void Hit(){
		fishAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		fishAnimator.SetTrigger("Eat");
	}

	public void EatUp(){
		fishAnimator.SetTrigger("EatUp");
	}

	public void EatDown(){
		fishAnimator.SetTrigger("EatDown");
	}

	public void EatLeft(){
		fishAnimator.SetTrigger("EatLeft");
	}

	public void EatRight(){
		fishAnimator.SetTrigger("EatRight");
	}

	public void Death(){
		fishAnimator.SetTrigger("Death");
		isLived = false;
	}
	
	public void Rebirth(){
		fishAnimator.SetTrigger("Rebirth");
		isLived = true;
	}

	public void Move(){
		if (isLived) {
			fishRigid.velocity = transform.forward * forwardSpeed * maxForwardSpeed + transform.up * upDownSpeed * maxUpDownSpeed;
			transform.RotateAround (transform.position, Vector3.up, turnSpeed * Time.deltaTime * maxTurnSpeed);

			fishAnimator.SetFloat ("Forward", forwardSpeed);
			fishAnimator.SetFloat ("Turn", turnSpeed);
			fishAnimator.SetFloat ("UpDown", upDownSpeed);
		}
	}
}
