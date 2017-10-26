using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
	public static EnemyMove tEnemyMove;
	private ManagersManager tManage;

	//Makes Grid gen script a singleton
	void Awake()
	{
		if (tEnemyMove == null)
			tEnemyMove = this;
		else
			Destroy(this); 

		tManage = ManagersManager.manager;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Move(Transform enemyObject)
	{

	}

	private void FindMovePos(List<GameObject> players)
	{
		float distToPlayer = Mathf.Infinity;
		GameObject closestPlayer;
		foreach (GameObject x in players) 
		{
			float tempDist = Vector3.Distance (x.transform.position, tManage.tEnemyMan.currentEnemy.transform.position);
			if(tempDist < distToPlayer)
			{
				distToPlayer = tempDist;
				closestPlayer = x;
			}
		}
	}
}
