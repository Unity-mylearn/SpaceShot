using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public float nextShot;
	public float shotRate;

	public GameObject bullet;
	public Transform bulletSpawn1;
	public Transform bulletSpawn2;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if (Time.time > nextShot) 
		{
			nextShot = Time.time + shotRate;
			Instantiate (bullet, bulletSpawn1.position, bulletSpawn1.rotation);
			Instantiate (bullet, bulletSpawn2.position, bulletSpawn2.rotation);
		}
	}
}
