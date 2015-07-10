using UnityEngine;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	public Vector3 jumpVelocity;
	public float acceleration;
	public float torque;
	private bool touchingPlatform;
	
	void Update () {
		if(touchingPlatform && Input.GetButtonDown("Jump")){
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			float turn = Input.GetAxis("Horizontal");
        		rb.AddRelativeTorque(Vector3.up * torque * turn);
		}
		distanceTraveled = transform.localPosition.x;
	}
	
	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void OnCollisionExit () {
		touchingPlatform = false;
	}
}
