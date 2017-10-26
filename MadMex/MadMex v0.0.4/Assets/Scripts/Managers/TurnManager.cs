using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    public bool playersTurn = true;

    public static TurnManager gTurn;
    private PlayerManager tPlayer;

    private List<GameObject> currentPlayers;

    //Makes Grid gen script a singleton
    void Awake()
    {
        if (gTurn == null)
            gTurn = this;
        else
            Destroy(this); 

        tPlayer = PlayerManager.gPlayer;
    }

	// Use this for initialization
	void Start () 
	{
        
	}
	
	// Update is called once per frame
	void Update () 
	{
        
	}
}
