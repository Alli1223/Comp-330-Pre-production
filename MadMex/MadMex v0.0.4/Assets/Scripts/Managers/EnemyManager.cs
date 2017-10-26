using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
	public static EnemyManager gEnemyMan;
	public GameObject currentEnemy;

	private List<GameObject> currentEnemies = new List<GameObject>();
	private ManagersManager tManage;

	//Makes Grid gen script a singleton
	void Awake()
	{
		if (gEnemyMan == null)
			gEnemyMan = this;
		else
			Destroy (this); 
		

		currentEnemies.AddRange (GameObject.FindGameObjectsWithTag ("enemy"));
		currentEnemy = currentEnemies [0];
	}
		void Start()
		{
			
		tManage = ManagersManager.manager;
		tManage.tEnemyMan = gEnemyMan;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!tManage.tTurn.playersTurn) 
		{
			foreach(GameObject x in currentEnemies)
			{
//				tManage. (x.transform);
			}
		}
	}
}
