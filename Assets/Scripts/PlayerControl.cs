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

	bool isTouching=false;

	private float moveHorizontal;
	private float moveVertical;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
#if UNITY_STANDALONE
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
					if(hit.collider.gameObject.name == "Player")
						isTouching=true;
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
