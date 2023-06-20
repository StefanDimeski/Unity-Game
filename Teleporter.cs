using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour {

	public Transform teleportTo;
	public bool working = true;
	public bool isDouble = false;
	bool canTeleport = true;
	// bool justEntered = false;
    public UnityEvent onTeleport;

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (working && canTeleport)
			{
                if (teleportTo)
                {
                    other.transform.position = teleportTo.position;
                    if (isDouble)
                    {
                        canTeleport = false;
                        teleportTo.GetComponent<Teleporter>().canTeleport = false;
                    }
                }


                // Destroy all enemies when successfully teleporting
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyHard");

                foreach (GameObject gm in enemies)
                    GameObject.Destroy(gm);

                onTeleport.Invoke();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (isDouble)
		{
			if (other.tag == "Player")
			{
				canTeleport = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (isDouble)
		{
			if (other.tag == "Player")
			{
				canTeleport = true;
			}
		}
	}

	void SetWorking(bool flag)
	{
		working = flag;
	}
}
