using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject reference;
	public Vector3 spawnValues;


	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int asteroidCount;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves());
	}
	
	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < asteroidCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (reference, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
}
