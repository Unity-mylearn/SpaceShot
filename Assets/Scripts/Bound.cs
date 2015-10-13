using UnityEngine;
using System.Collections;

public class Bound : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}
