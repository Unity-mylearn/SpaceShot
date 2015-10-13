using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin,xMax,zMin,zMax;
}

public class PlayerControl : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundary boundary;

	public Rigidbody rb;
#if UNITY_ANDROID || UNITY_IPHONE
	bool isTouching=false;
#endif

	private float moveHorizontal;
	private float moveVertical;

	public float shotRate;
	public float nextShot;

	public GameObject bullet;
	public Transform bulletSpawn;

	AudioSource audio;
	void Start()
	{
		nextShot = 0.0f;
		audio = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Time.time > nextShot) 
		{
			nextShot = Time.time + shotRate;
			audio.Play();
			Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
#if UNITY_STANDALONE || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_WEBPLAYER || UNITY_STANDALONE_LINUX
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		
		rb.position = new Vector3 
			(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
				);
		
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
#endif

#if UNITY_ANDROID || UNITY_IPHONE
		if(Input.touchCount==1){
			if(Input.touches[0].phase == TouchPhase.Began){
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)){
					if(hit.collider.gameObject.name == "Bound"){
						isTouching=true;
					}
				}
			}
			//Move object
			if(isTouching&&(Input.touches[0].phase == TouchPhase.Moved)){                        
				Vector2 touchDeltaPosition = Input.touches[0].deltaPosition;
				rb.rotation = Quaternion.Euler (0.0f, 0.0f, touchDeltaPosition.x * -tilt);
				transform.Translate(touchDeltaPosition.x*Time.deltaTime*speed ,0.0f, touchDeltaPosition.y*Time.deltaTime*speed);
			}
			rb.position = new Vector3 
				(
					Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
					);
			if(isTouching&&Input.touches[0].phase == TouchPhase.Ended)
				isTouching=false;
		}
#endif

	}
}
