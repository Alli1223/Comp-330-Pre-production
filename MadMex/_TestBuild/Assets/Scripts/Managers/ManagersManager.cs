using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersManager : MonoBehaviour 
{
	public static ManagersManager manager;


	public TurnManager tTurn;
	public GridPositionDetection tDetect;
	public GridGeneration tGrid;
	public PlayerManager tPlayer;
	public EnemyManager tEnemyMan;
    public APManager tActionManage;
	public CameraManager tCamManage;
	public Material actionMat;

	// Use this for initialization
	void Awake ()
	{
		if (manager == null)
			manager = this;
		else
			DestroyImmediate (this);  
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
