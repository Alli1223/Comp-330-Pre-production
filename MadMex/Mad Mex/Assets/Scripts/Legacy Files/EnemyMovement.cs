using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent enemyAgent;
    Transform[,] moveblePositions;

    float enemyMoveDistance;
    public float mainEnemyMoveDist = 40;
    bool endMovement;
    bool isMovingToPoint;
	Transform player;

    // Use this for initialization
    void Start()
    {	
		player = GameObject.FindGameObjectWithTag ("Player").transform;	
        moveblePositions = GridGenerator.gridInstance.objectArray;
        enemyAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyMoveDistance = mainEnemyMoveDist;
    }
	
    // Update is called once per frame
    void Update()
    {
        ReachDestination();
        if (!TurnManager.turnManagerInstance.playersTurn)
        {  
            if (!endMovement)
            {
                CheckForAttack();
                if (GetGridWithinRange(GridGenerator.gridInstance.GetClosestGrid(moveblePositions, transform.position), moveblePositions, player.position) != GridGenerator.gridInstance.GetClosestGrid(moveblePositions, transform.position))
                {
                    Debug.Log("GO! GO! GO!");
                    enemyAgent.SetDestination(GetGridWithinRange(GridGenerator.gridInstance.GetClosestGrid(moveblePositions, transform.position), moveblePositions, player.position).position);
                    enemyMoveDistance -= GridGenerator.gridInstance.DistanceCalculation(Vector3.Distance(transform.position, enemyAgent.destination));
                    endMovement = true;
                    isMovingToPoint = true;
                }
            }
        }
        else if(TurnManager.turnManagerInstance.playersTurn && enemyMoveDistance != mainEnemyMoveDist)
        {
            Debug.Log("STOP STOP STOP");
            enemyMoveDistance = mainEnemyMoveDist;
            endMovement = false;
        }
    }

    void CheckForAttack()
    {
        if (GridGenerator.gridInstance.DistanceCalculation(Vector3.Distance(GridGenerator.gridInstance.GetClosestGrid(moveblePositions, transform.position).position, GridGenerator.gridInstance.GetClosestGrid(moveblePositions, player.position).position)) <= 10)
        {
			Debug.Log ("Kill it");
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            TurnManager.turnManagerInstance.enemyMoved = true;
            return;
        }

    }

    void ReachDestination()
    {
        if(isMovingToPoint)
        {
            if (!enemyAgent.pathPending)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                    {
                        isMovingToPoint = false;
                        Debug.Log("Doot Doot");
                        GridGenerator.gridInstance.SPAWNTHEMOTHERCLUCKINGGRID();
                        TurnManager.turnManagerInstance.enemyMoved = true;
                    }

                }
            }
        }
    }

    Transform GetGridWithinRange(Transform a, Transform[,] Grids, Vector3 enemyPosition)
    {
        foreach (Transform b in Grids)
        {
            if (GridGenerator.gridInstance.DistanceCalculation(Vector3.Distance(a.position, b.position)) <= enemyMoveDistance)
            {
                if (b != GridGenerator.gridInstance.GetClosestGrid(moveblePositions, GameObject.FindGameObjectWithTag("Player").transform.position))
                {
					GridGenerator.gridInstance.objectsInRange.Add(b);
//                    b.gameObject.SetActive(true);
                }
            }
            else
            {
                //b.gameObject.SetActive(false);
            }
        }
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = enemyPosition;
		foreach (Transform c in GridGenerator.gridInstance.objectsInRange)
        {
            float dist = Vector3.Distance(c.position, currentPos);
            if (dist < minDist)
            {
                tMin = c;
                minDist = dist;
            }
        }
        tMin.gameObject.SetActive(true);
        tMin.GetComponent<MeshRenderer>().material.color = Color.red;
        return tMin;
    } 


//    public Transform GetClosestGridEnemy(Transform[,] Grids)
//    {
//        Transform tMin = null;
//        float minDist = Mathf.Infinity;
//        Vector3 currentPos = transform.position;
//        foreach (Transform t in Grids)
//        {
//            float dist = Vector3.Distance(t.position, currentPos);
//            if (dist < minDist)
//            {
//                tMin = t;
//                minDist = dist;
//            }
//        }
////        tMin.gameObject.SetActive(true);
//        return tMin;
//    }

}
