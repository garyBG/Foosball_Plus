using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Scorekeeper : MonoBehaviour
{
	private int [] score = new int[2];
	private int scores = 0;
	private GameObject ball, table;
	public GameObject powerupObj;

	void Start()
	{
		ball = GameObject.Find("Hashish");
		table = GameObject.Find("TableNode");
		reset();
	}

	// Update is called once per frame
	void Update()
	{
		if (ball.transform.position.y <= 6f)
		{
			reset();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			reset();
		}
	}

	public void scoreGoal(int side)
    {
	    if (side >= 0 && side <= 1)
	    {
		    score[side]++;
		    scores++;
		    updateScoreboard();
		    reset();
			spawnPowerups();
	    }
    }

	private void spawnPowerups()
	{
		// between:
		// x 2 z -10
		// x 34 z 10
		Vector3 powerUpPos = new Vector3(
			Random.Range(2f, 34f),
			13f,
			Random.Range(-10f, 10f)
		);
		Instantiate(powerupObj, powerUpPos, Quaternion.identity);
	}

	private void reset()
	{
		// Reset ball
		Rigidbody rigid = ball.GetComponent<Rigidbody>();
		ball.transform.position = new Vector3(18f, 15f, 0f);
		ball.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

		rigid.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-6f, 6f));
		rigid.angularVelocity = new Vector3(0f, 0f, 0f);

		// Reset paddles
		foreach (SceneNodeTransform x in table.GetComponentsInChildren<SceneNodeTransform>())
		{
			x.reset();
		}

		// Delete powerups
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Powerup"))
		{
			Destroy(obj);
		}
	}
	[PunRPC]
	void updateScoreboardRPC(int[] scoreRPC) {
		Text[] t = gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < score.Length; i++)
        {
	        t[i].text = scoreRPC[i].ToString();
        }	
	}
    public void updateScoreboard()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Text[] t = gameObject.GetComponentsInChildren<Text>();
            for (int i = 0; i < score.Length; i++)
            {
                t[i].text = score[i].ToString();
            }
        }
		else {
			updateScoreboardRPC(score);
		}
	    
    }
}
