using UnityEngine;
using System.Collections;

public class BaldEagleCharacterScript : MonoBehaviour {
	public Animator baldEagleAnimator;
	Rigidbody baldEagleRigid;
	public bool isFlying=false;
	public float upDown=0f;
	public float forwardAcceleration=0f;
	public float yawVelocity=0f;
	public float groundCheckDistance=5f;
	public bool soaring=false;
	public bool isGrounded=true;
	public float forwardSpeed=0f;
	public float maxForwardSpeed=3f;
	public float meanForwardSpeed=1.5f;
	public float speedDumpingTime=.1f;
	public float groundedCheckOffset=1f;
	public float runSpeed=1f;

    float soaringTime=0f;

	void Start(){
		baldEagleAnimator = GetComponent<Animator> ();
		baldEagleRigid = GetComponent<Rigidbody> ();
	}

    void Update()
    {
        Move();

        if (soaring)
        {
            soaringTime += Time.deltaTime;
            if (soaringTime > 2f)
            {
                soaring = false;
                baldEagleAnimator.SetBool("IsSoaring", false);
                baldEagleAnimator.applyRootMotion = false;
            }
        }
        /*
        if (baldEagleAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "GlideForward")
        {
            if (soaring)
            {
                soaring = false;
                baldEagleAnimator.SetBool("IsSoaring", false);
                baldEagleAnimator.applyRootMotion = false;
            }
        }
        else if (baldEagleAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HoverOnce")
        {
            forwardSpeed = meanForwardSpeed;
            baldEagleAnimator.applyRootMotion = false;
            isFlying = true;
        }
        */

		GroundedCheck ();
	}

	void GroundedCheck(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position+Vector3.up*groundedCheckOffset, Vector3.down, out hit, groundCheckDistance)) {
			if (!soaring && !isGrounded ) {
				Landing ();
				isGrounded = true;		
			}
		} else {
			isGrounded=false;
		}
	}

	public void Landing(){
		baldEagleAnimator.SetBool ("Landing",true);
		baldEagleAnimator.applyRootMotion = true;
		baldEagleRigid.useGravity = true;
		isFlying = false;
	}
	
	public void Soar(){
		if(isGrounded){
			baldEagleAnimator.SetBool ("Landing",false);
			baldEagleAnimator.SetBool ("IsSoaring", true);
			baldEagleRigid.useGravity = false;
            baldEagleAnimator.applyRootMotion = false;
            forwardSpeed =0f;
            isFlying = true;
            soaring = true;
			isGrounded = false;

            soaringTime = 0f;
		}
	}

	public void Attack(){
		baldEagleAnimator.SetTrigger ("Attack");
	}

	public void Jump(){
		baldEagleAnimator.SetTrigger ("JumpLeftFoot");
	}

	public void Hit(){
		baldEagleAnimator.SetTrigger ("Hit");
	}

	public void SitDown(){
		baldEagleAnimator.SetTrigger ("SitDown");
	}

	public void StandUp(){
		baldEagleAnimator.SetTrigger ("StandUp");
	}

	public void Down(){
		baldEagleAnimator.SetTrigger ("Down");
	}

	public void Rebirth(){
		baldEagleAnimator.SetTrigger ("Rebirth");
	}

	public void Grooming(){
		baldEagleAnimator.SetTrigger ("Grooming");
	}

	public void TailGrooming(){
		baldEagleAnimator.SetTrigger ("Grooming2");
	}

	public void Attack2(){
		baldEagleAnimator.SetTrigger ("Attack2");
	}

	public void Call(){
		baldEagleAnimator.SetTrigger ("Call");
	}
	
	public void Eat(){
		baldEagleAnimator.SetTrigger ("Eat");
	}

	public void CrouchStart(){
		baldEagleAnimator.SetBool ("Crouch",true);
	}

	public void CrouchEnd(){
		baldEagleAnimator.SetBool ("Crouch",false);
	}

	public void RunStart(){
		runSpeed = 2f;
	}
	
	public void RunEnd(){
		runSpeed = 1f;
	}

	public void BarrelRoll(){
		baldEagleAnimator.SetTrigger ("Roll");
	}

	public void Move(){
		baldEagleAnimator.SetFloat ("Forward",forwardAcceleration*runSpeed);
		baldEagleAnimator.SetFloat ("Turn",yawVelocity);
		baldEagleAnimator.SetFloat ("UpDown",upDown);
		baldEagleAnimator.SetFloat ("UpVelocity",baldEagleRigid.velocity.y);

        if (isFlying) {

            if (forwardAcceleration < 0f)
            {
                baldEagleRigid.velocity = transform.up * upDown * 2f + transform.forward * forwardSpeed;
            }
            else
            {
                baldEagleRigid.velocity = transform.up * (upDown * 2f + (forwardSpeed - meanForwardSpeed)) + transform.forward * forwardSpeed;

            }

            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * yawVelocity * 100f);

            if (soaring)
            {
                forwardSpeed = soaringTime;
                upDown = soaringTime/2f;
            }
            else
            {
                forwardSpeed = Mathf.Lerp(forwardSpeed, Mathf.Max(meanForwardSpeed, forwardSpeed), Time.deltaTime * speedDumpingTime);
                forwardSpeed = Mathf.Clamp(forwardSpeed + forwardAcceleration * Time.deltaTime, 0f, maxForwardSpeed);
                upDown = Mathf.Lerp(upDown, 0, Time.deltaTime * 3f);

            }
		}
	}
}
