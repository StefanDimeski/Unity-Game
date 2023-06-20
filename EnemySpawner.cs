using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public Player player;

	public float delay = 3f;

	public int maxSpawns = 100;

	bool seen = false;

	int spawns = 0;
	float count;

	// Use this for initialization
	void Start () {
		count = delay;
		player = GameManager.instance.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.enabled)
		{
			RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
			
			//Debug.Log(count);

			if (seen)
			{
				count -= Time.deltaTime;
			}
			
			if (hitInfo.transform.tag == "Player")
			{
				seen = true;
				//Debug.Log((count<=0f) + " , " + (spawns < maxSpawns));
				if (count <= 0f)
				{
					Debug.Log("In");
					GameObject enemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
					enemy.GetComponent<ChasePlayer>().player = player;

					count = delay;
					spawns++;
				}
			}

			if (player.dead)
			{
				count = delay;
				spawns = 0;
				seen = false;
			}
		}

	}
}
