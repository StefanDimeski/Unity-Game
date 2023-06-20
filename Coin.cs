using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int coinCost = 1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameManager.instance.coins += coinCost;
			GameManager.instance.coinsTxt.text = "Coins: " + GameManager.instance.coins;
		    gameObject.GetComponent<CircleCollider2D>().enabled = false;
		    transform.GetChild(0).gameObject.SetActive(false);
		}

	}

	public void Activate()
	{
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		transform.GetChild(0).gameObject.SetActive(true);
	}

    public void Deactivate()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
