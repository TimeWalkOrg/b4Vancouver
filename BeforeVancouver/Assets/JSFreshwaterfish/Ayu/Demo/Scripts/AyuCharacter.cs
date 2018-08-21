using UnityEngine;
using System.Collections;

public class AyuCharacter : MonoBehaviour {
    Animator ayuAnimator;
    Rigidbody ayuRigid;

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
        ayuAnimator = GetComponent<Animator>();
        ayuRigid = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Move();
    }

    public void Bite()
    {
        ayuAnimator.SetTrigger("Bite");
    }

    public void MouthOpen()
    {
        ayuAnimator.SetBool("MouthOpen", true);
    }

    public void MouthClose()
    {
        ayuAnimator.SetBool("MouthOpen", false);
    }

    public void Move()
    {
        ayuRigid.AddForce(transform.forward * forwardAcceleration * forwardAccelerationMultiplier + transform.up * upDownAcceleration * upDownAccelerationMultiplier+waterFlow);
        ayuRigid.AddTorque(transform.up * turnAcceleration * turnAccelerationMultiplier);
        forwardSpeed = ayuRigid.velocity.magnitude;
        ayuAnimator.speed = forwardAcceleration + minAnimatorSpeed;
        ayuAnimator.SetFloat("ForwardSpeed", forwardSpeed);
        ayuAnimator.SetFloat("ForwardAcceleration", forwardAcceleration);
        ayuAnimator.SetFloat("TurnAcceleration", turnAcceleration);
        ayuAnimator.SetFloat("UpDownAcceleration", upDownAcceleration);
    }
}
