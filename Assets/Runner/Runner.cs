//This was made following the excellent runner tutorial by cat like coding ( catlikecoding.com) 
//and concepts by Drakfyre (pushy pixels on youtube.) 
//lil stuff by me (GrimDerp)
using UnityEngine;
using MonoBehavior;
using UnityENgine.EventSystems;
using UnityStandardAssets.ImageEffects;

public class Runner : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public static float distanceTraveled;
	public Vector3 boostVelocity, jumpVelocity;
	public float acceleration;
	public float gameOverY;
	private bool touchingPlatform;
	private Vector3 startPosition;

	private static int boosts;

	void OnCollisionEnter(){
		touchingPlatform = true;
	}
	
	void OnCollisionExit(){
		touchingPlatform = false;
	}
	
	
	void Awake(){
        DontDestroyOnLoad(transform.Runner);
	 }
	
	void Update () {
		transform.Rotate(rotationVelocity * Time.deltaTime);
		if (Input.GetButtonDown ("Jump")) {
					if (touchingPlatform) {
							GetComponent<Rigidbody>().AddForce (jumpVelocity, ForceMode.VelocityChange);
							touchingPlatform = false;
							
								
						}
						else if (boosts > 0) {
								GetComponent<Rigidbody>().AddForce (boostVelocity, ForceMode.VelocityChange);
								boosts -= 1;
								GUIManager.SetBoosts(boosts);
						}
				}
			distanceTraveled = transform.localPosition.x;
			GUIManager.SetDistance (distanceTraveled);


			if(transform.localPosition.y < gameOverY){
				GameEventManager.TriggerGameOver();
			}
		}
	
		private void GameStart () {
			boosts = 3;
			GUIManager.SetBoosts(boosts);
			distanceTraveled = 0f;
			GUIManager.SetDistance(distanceTraveled);
			transform.localPosition = startPosition;
			GetComponent<Renderer>().enabled = true;
			GetComponent<Rigidbody>().isKinematic = false;
			enabled = true;
		}

	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);

	}

	private void GameOver () {
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
		
	}

	void fixedUpdate (){
		if (touchingPlatform) {
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);		
		}
	}



	void Start () {
				GameEventManager.GameStart += GameStart;
				GameEventManager.GameOver += GameOver;
				startPosition = transform.localPosition;
				GetComponent<Renderer>().enabled = false;
				GetComponent<Rigidbody>().isKinematic = true;
				enabled = false;
		}

	}

	
