using UnityEngine;
using System.Collections;

public class RiverFishCharacter : MonoBehaviour {
    Animator riverFishAnimator;
    Rigidbody riverFishRigid;

    public float forwardSpeed = 0f;
    public float turnAcceleration = .3f;
    public float upDownAcceleration = 0f;
    public float forwardAccelerationMultiplier = 50f;
    public float minForwardAcceleration = 1f;
    public float turnAccelerationMultiplier = 5f;
    public float upDownAccelerationMultiplier = 100f;
    public float forwardAcceleration = 0f;
    public float minAnimatorSpeed = .3f;
    public Vector3 waterFlow;

    void Start()
    {
        riverFishAnimator = GetComponent<Animator>();
        riverFishRigid = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Move();
    }

    public void Bite()
    {
        riverFishAnimator.SetTrigger("Bite");
    }

    public void MouthOpen()
    {
        riverFishAnimator.SetBool("MouthOpen", true);
    }

    public void MouthClose()
    {
        riverFishAnimator.SetBool("MouthOpen", false);
    }

    public void Move()
    {
        riverFishRigid.AddForce(transform.forward * forwardAcceleration * forwardAccelerationMultiplier + transform.up * upDownAcceleration * upDownAccelerationMultiplier + waterFlow);
        riverFishRigid.AddTorque(transform.up * turnAcceleration * turnAccelerationMultiplier);
        forwardSpeed = riverFishRigid.velocity.magnitude;
        riverFishAnimator.speed = forwardAcceleration + minAnimatorSpeed;
        riverFishAnimator.SetFloat("ForwardSpeed", forwardSpeed);
        riverFishAnimator.SetFloat("ForwardAcceleration", forwardAcceleration);
        riverFishAnimator.SetFloat("TurnAcceleration", turnAcceleration);
        riverFishAnimator.SetFloat("UpDownAcceleration", upDownAcceleration);
    }
}
