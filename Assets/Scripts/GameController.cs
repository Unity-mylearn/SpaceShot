using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject reference;
	public Vector3 spawnValues;


	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int asteroidCount;
	private int score;
	public Text scoreText;


	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;

	// Use this for initialization
	void Start () {
		score = 0;

		gameOver = false;
		restart = false;
		setScroe ();
		StartCoroutine (SpawnWaves());
	}

	void Update()
	{
		if (restart) {

#if UNITY_ANDROID || UNITY_IPHONE
			if(Input.touchCount==1){
				if(Input.touches[0].phase == TouchPhase.Began){
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)){
						if(hit.collider.gameObject.name == "Bound"){
							Application.LoadLevel(Application.loadedLevel);
						}
					}
				}
			}
#endif

#if UNITY_STANDALONE || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_WEBPLAYER || UNITY_STANDALONE_LINUX
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
#endif
		}
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

			if(gameOver)
			{
#if UNITY_ANDROID || UNITY_IPHONE
				restartText.text = "Touch to restart";
#endif
#if UNITY_STANDALONE || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_WEBPLAYER || UNITY_STANDALONE_LINUX
				restartText.text = "Press R to restart";
#endif
				restart = true;
				break;
			}
		}
	}

	public void addScore(int newScore)
	{
		score = score + newScore;
		setScroe ();
	}
	void setScroe()
	{
		scoreText.text = "Score: " + score;		
	}

	public void gameIsOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
