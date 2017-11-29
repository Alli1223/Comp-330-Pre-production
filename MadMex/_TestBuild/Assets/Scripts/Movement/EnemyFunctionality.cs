using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EnemyFunctionality : MonoBehaviour 
{

	private static ManagersManager tManage;

    private List<GameObject> playersInRange = new List<GameObject>();
    private List<GameObject> targetsInRange = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		tManage = ManagersManager.manager;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}

//    public stateEnum Search(int range)
//	{
//        stateEnum foundPlayers = FindPlayersInRange(range);
//        if (foundPlayers == stateEnum.FAILURE)
//        {
//
//        }else if(foundPlayers == stateEnum.SUCCESS)
//        {
//            Vector3 player = tManage.tDetect.RealtivePosition(tManage.tPlayer.currentPlayers, EnemyManager.currentEnemy.transform);
//            int closestDist = tManage.tDetect.DistCheck(EnemyManager.currentEnemy.transform.position, player);
//            player = (Vector3)tManage.tDetect.RealtivePosition(tManage.tGrid.currentTiles, player);
//            if (EnemyManager.currentEnemy.GetComponent<TempEnemyVar>().moveDist > closestDist)
//            {
//                if (ClosetBetweenCover(player) < closestDist)
//                {
//
//                }
//            }
//        }
//	}

//    public static int ClosetBetweenCover(Vector3 point)
//    {  
//        Tile currentTile;
//        int bestResult = 10 ^ 6;
//        foreach (Tile t in tManage.tGrid.tileVariables)
//        {
//            if (t.tileCover != coverDirection.NONE)
//            {
//                if (tManage.tDetect.DistCheck(point, t.tPos) < bestResult)
//                {
//                    if(point.x > t.tPos.x )
//                    currentTile = t;
//                    bestResult = tManage.tDetect.DistCheck(point, t.tPos);
//                }
//            }
//        }
//    }

//    public static Transform ClosetBetweenCover()
//    {
//
//    }


    public stateEnum FindPlayersInRange(int range)
    {
        playersInRange = new List<GameObject>();
        playersInRange = GridPositionDetection._CloseObjs(tManage.tPlayer.currentPlayers, EnemyManager.currentEnemy.transform.position, range);
        if (playersInRange.Count == 0)
        {
            return stateEnum.FAILURE;
        }
        else
        {
            return stateEnum.SUCCESS;
        }
    }

    public stateEnum FindTargetsAtPoint(int range, Vector3 point)
    {
        targetsInRange = new List<GameObject>();
        targetsInRange = GridPositionDetection._CloseObjs(tManage.tPlayer.currentPlayers, point, range);
        if (targetsInRange.Count == 0)
        {
            return stateEnum.FAILURE;
        }
        else
        {
            return stateEnum.SUCCESS;
        }
    }
}
