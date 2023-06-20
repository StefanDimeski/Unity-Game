// #define TESTING_JOY_ON_COMP

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed = 3f;
	public ParticleSystem particlesSystem;
	public GameObject graphics;
	public float delaySpawn = 1f;
    public float smoothTime = 3f;
    public bool canMove = false;

	[HideInInspector]
	public bool dead = false;
	[HideInInspector]
	public float movementX = 0f, movementY = 0f;

	public float maxHealth = 100f;
	float health;

    Vector2 vel = Vector2.zero;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (dead)
		{
			particlesSystem.Stop();
			return;
		}

        if (canMove)
        {
            MovePlayerV1();
            //MovePlayerV2();
        }
	}

    void MovePlayerV1()
    {
        Vector2 curVel = GetComponent<Rigidbody2D>().velocity;


#if UNITY_EDITOR || UNITY_STANDALONE
#if TESTING_JOY_ON_COMP
        curVel = new Vector2(movementX, movementY);
#else
        curVel = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
#endif
#endif

#if UNITY_ANDROID
		curVel = new Vector2(movementX, movementY);
#endif

#if UNITY_IPHONE
		curVel = new Vector2(movementX, movementY);
#endif

        curVel *= (maxSpeed * Time.deltaTime);

        curVel.x = Mathf.Clamp(curVel.x, -maxSpeed, maxSpeed);
        curVel.y = Mathf.Clamp(curVel.y, -maxSpeed, maxSpeed);

        if (curVel.x > 0f || curVel.y > 0f || curVel.x < 0f || curVel.y < 0f)
            particlesSystem.Play();
        else
            particlesSystem.Stop();

        GetComponent<Rigidbody2D>().velocity = curVel;
    }

    void MovePlayerV2()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Vector2 newPos = (Vector2)transform.position + input;

        transform.position = Vector2.SmoothDamp(transform.position, newPos, ref vel, smoothTime, maxSpeed, Time.deltaTime);
    }

	public void RecieveDamage(float damageAmount)
	{
		health -= damageAmount;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Invoke("Spawn", delaySpawn);
		graphics.SetActive(false);
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
		dead = true;
		GameManager.instance.OnPlayerDeath();
	}

	void Spawn()
	{
		transform.position = GameManager.instance.lastCheckpoint;

        //if (GameManager.instance.currentLevel == 10)
        //    transform.position += new Vector3(-0.4f, 0f, 0f);

		graphics.SetActive(true);
		GetComponent<BoxCollider2D>().enabled = true;
		dead = false;

		GameManager.instance.OnPlayerSpawn();
	}

	public void SetSpeed(float newSpeed)
	{
		Debug.Log(newSpeed);
	}
}
