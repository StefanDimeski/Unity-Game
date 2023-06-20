using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	[System.Serializable]
	public class TutorialPoint
	{
		public string trigger, title, text;
		public Sprite image;
		public bool pause = true;
		public UnityEvent onDone;
        public bool shown = false;
	}

	public TutorialPoint[] tutorialPoints;
	public int tutorialToCheck = - 1;
	public GameObject tutorialGM;
	public Image tutorialImage;
	public Text tutorialTitle;
	public Text tutorialText;
    public Player playerScript;

    [HideInInspector]
	public bool checkInput = false;

	void Start()
	{
        tutorialToCheck = GameManager.instance.tutorialTo;
	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_STANDALONE
		if (checkInput)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				OnTutorialDone();
		}
#endif
#if UNITY_ANDROID
		if (checkInput)
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					OnTutorialDone();
				}
			}
		}
#endif
#if UNITY_IPHONE
		if (checkInput)
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					OnTutorialDone();
				}
			}
		}
#endif
	}

	public void InvokeTutorial(string trigge)
	{
        //if (tutorialPoints[tutorialToCheck + 1].trigger == trigge)
        //{
        //	tutorialToCheck++;
        //	StartTutorial(tutorialToCheck, tutorialPoints[tutorialToCheck].pause);
        //}
        bool anyTut = false;

        for (int i = 0; i < tutorialPoints.Length; i++)
        {
            if (tutorialPoints[i].trigger == trigge && !tutorialPoints[i].shown)
            {
                tutorialToCheck = i;
                StartTutorial(i, tutorialPoints[tutorialToCheck].pause); // tutorialToCheck
                anyTut = true;
                tutorialPoints[i].shown = true;
            }
        }

        if (!anyTut)
            playerScript.canMove = true;
	}

	void StartTutorial(int num, bool pauseGame = true)
	{
        playerScript.canMove = false;
		//turn_gui_stuff_on
		tutorialTitle.text = tutorialPoints[num].title;
		tutorialText.text = tutorialPoints[num].text;
		tutorialImage.sprite = tutorialPoints[num].image;

		tutorialGM.SetActive(true);

		checkInput = true;

		if (pauseGame)
			Time.timeScale = 0f;
	}

	void OnTutorialDone()
	{
		tutorialPoints[tutorialToCheck].onDone.Invoke();
		if (tutorialToCheck + 1 < tutorialPoints.Length)
		{
			if (tutorialPoints[tutorialToCheck + 1].trigger == "")
			{
				tutorialToCheck++;
				StartTutorial(tutorialToCheck);
			}
			else
			{
				//turn_gui_stuff_off
				tutorialGM.SetActive(false);
				Time.timeScale = 1f;
                checkInput = false;

                // Make player be able to move
                playerScript.canMove = true;
			}
		}
		else
		{
			tutorialGM.SetActive(false);
			Time.timeScale = 1f;
			//Destroy(this.gameObject);

            // Make player be able to move
            playerScript.canMove = true;
		}
	}


    public void DisableGUI()
    {
        tutorialGM.SetActive(false);
    }


    public void EnableGUI()
    {
        tutorialGM.SetActive(true);
    }
	
}
