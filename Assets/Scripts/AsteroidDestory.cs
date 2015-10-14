using UnityEngine;
using System.Collections;

public class AsteroidDestory : MonoBehaviour 
{

	public GameObject explosion;
	public GameObject playerExplosion;


	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameC = GameObject.FindWithTag("GameController");
		if (gameC != null) 
		{
			gameController = gameC.GetComponent<GameController>();
		}
		if (gameC == null) 
		{
			Debug.Log ("can't find gamecontroller");
		}
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		Instantiate (explosion, transform.position, transform.rotation);
		gameController.addScore (scoreValue);
		if (other.tag == "Player") 
		{
			Instantiate(playerExplosion,other.transform.position,other.transform.rotation);
			gameController.gameIsOver();
		}
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
