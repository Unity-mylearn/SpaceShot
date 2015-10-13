using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour {

	public float tumble;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitCircle * tumble;
	}
}
