using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public Vector3 centerOfMass;

    public float speed = -500f;

    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;

    private JointMotor2D frontMotor;
    private JointMotor2D backMotor;

	// Use this for initialization
	void Start () {
        frontMotor = new JointMotor2D();
        backMotor = new JointMotor2D();


        frontWheel.useMotor = true;
        frontWheel.motor = frontMotor;


        backWheel.useMotor = true;
        backWheel.motor = backMotor;

        this.GetComponent<Rigidbody2D>().centerOfMass = centerOfMass;

	}
	
	// Update is called once per frame
	void Update () {
        if(Vector2.Dot(Vector2.up, this.transform.up) < -0.8f)
        {
            Destroy(this.gameObject);
        }

        frontMotor.motorSpeed = speed;
        frontMotor.maxMotorTorque = 5000f;
        backMotor.motorSpeed = speed;
        backMotor.maxMotorTorque = 5000f;

        frontWheel.motor = frontMotor;
        backWheel.motor = backMotor;
	}

    void OnDestroy()
    {
        if(this.transform.position.y > -3)
            if(EffectManager.Instance)
                EffectManager.Instance.rvsDestroyed++;
    }
}
