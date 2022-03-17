using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
	public int side;
	public Canvas scoreboardCanvas;
	private void OnTriggerEnter(Collider other)
    {
	    if (other.tag == "CBT")
	    {
			scoreboardCanvas.GetComponent<Scorekeeper>().scoreGoal(side);
		}
    }
}
