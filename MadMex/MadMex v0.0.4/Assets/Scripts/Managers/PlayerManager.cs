/*
	Created On:		27/09/2017 10:34
	Created By: 	Marc Andrews
	Last Edit: 		11/10/2017 10:34
	Last Edit By: 	Marc Andrews
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	
	public int maxMoveDist;
    public GameObject currentPlayer;
    public static PlayerManager gPlayer;

	private List<GameObject> currentPlayers = new List<GameObject>();
	private ManagersManager tManage;


    //Makes Grid gen script a singleton
    void Awake()
	{
		if (gPlayer == null)
			gPlayer = this;
		else
			Destroy (this); 


        currentPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        currentPlayer = currentPlayers[0];
		foreach (GameObject x in currentPlayers) 
		{
			if (!x.GetComponent<TempPlayerVar> ()) 
			{
                x.AddComponent<TempPlayerVar>();
			}
            ResetDist(x.transform);
		}
    }

	void Start()
	{
		tManage = ManagersManager.manager;
		tManage.tPlayer = gPlayer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (tManage.tTurn.playersTurn) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray cameraToPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(cameraToPoint, out hit))
                {
                    if (hit.transform.tag == "Player" && hit.transform.GetComponent<TempPlayerVar>().currentDist > 0)
                    {
                        currentPlayer.GetComponent<TempPlayerVar>().currentDist = GetComponent<TempMove>().modifiedMovDist;
                        currentPlayer = hit.transform.gameObject;
                        GetComponent<TempMove>().ShowMovable();
                    }
                }
            }
        }
	}

    public Transform FindPlayer(Transform pCompare)
    {
        Transform returnPlayer = pCompare;
        foreach (GameObject x in currentPlayers)
        {
            if (x.transform == pCompare)
            {               
                returnPlayer = x.transform;
            }
        }
        return returnPlayer;
    }

	private void ResetDist(Transform player)
	{
		player.GetComponent<TempPlayerVar> ().currentDist = maxMoveDist;
	}
}
