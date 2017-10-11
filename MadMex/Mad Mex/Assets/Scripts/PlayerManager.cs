/*
	Created On:		27/09/2017 10:34
	Created By: 	Marc Andrews
	Last Edit: 		27/09/2017 10:34
	Last Edit By: 	Marc Andrews
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	TileEnum test;

    //Could be changed to player group
    public GameObject player;

    public static PlayerManager gPlayer;
    //Makes Grid gen script a singleton
    void Awake()
    {
        if (gPlayer == null)
            gPlayer = this;
        else
            Destroy(this); 

        player = GameObject.FindGameObjectWithTag("Player");
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
