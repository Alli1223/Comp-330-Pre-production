using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float playerMoveDistance;

    UnityEngine.AI.NavMeshAgent thisObjectAgent;
    private bool miscCombatStart;
    private bool movingCombat;
    private float combatMoveDist;

    // Use this for initialization
    void Start()
    {
        thisObjectAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        GridGenerator.gridInstance.MoveDistanceOfPlayer = playerMoveDistance * 10;
    }

	
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ReachDestination();
        MoveDistCombatReset();
    }

    void PlayerMovement()
    {
        if (TurnManager.turnManagerInstance.playersTurn)
        {
            if (GridGenerator.gridInstance.MoveDistanceOfPlayer != 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit nextPosition;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out nextPosition))
                    {
                        if (nextPosition.collider.gameObject.tag == "GridPiece")
                        {
                            if (!GridGenerator.gridInstance.combatEnabled)
                            {
                                Debug.LogError("Grid is active while out of combat! See MovePlayer Script.");
                            }
                            if (!movingCombat)
                            {
                                combatMoveDist = GetCombatDistance(Vector3.Distance(GridGenerator.gridInstance.GetClosestGrid(GridGenerator.gridInstance.objectArray, transform.position).position, nextPosition.collider.transform.position));
                                thisObjectAgent.SetDestination(nextPosition.collider.transform.position);
//                                TurnManager.turnManagerInstance.startMove = true;
                                GridGenerator.gridInstance.isMoving = true;
                                movingCombat = true;
                            }


                        }
                        if (!GridGenerator.gridInstance.combatEnabled)
                        {
                            thisObjectAgent.SetDestination(nextPosition.point);

                        }
                    }
                }
            }
        }
        else
        {
            if (GridGenerator.gridInstance.MoveDistanceOfPlayer != playerMoveDistance * 10)
            {
                GridGenerator.gridInstance.MoveDistanceOfPlayer = playerMoveDistance * 10;
            }
        }
    }

    void ReachDestination()
    {
        if (movingCombat)
        {
            if (!thisObjectAgent.pathPending)
            {
                if (thisObjectAgent.remainingDistance <= thisObjectAgent.stoppingDistance)
                {
                    if (!thisObjectAgent.hasPath || thisObjectAgent.velocity.sqrMagnitude == 0f)
                    {
//                        Debug.Log(combatMoveDist);
                        GridGenerator.gridInstance.MoveDistanceOfPlayer -= combatMoveDist;
                        GridGenerator.gridInstance.isMoving = false;
                        GridGenerator.gridInstance.moveFinished = false;
//                        TurnManager.turnManagerInstance.playersTurn = false;
                        movingCombat = false;
                        PlayerCheckAttack();
                    }

                }
            }
        }
    }

    void PlayerCheckAttack()
    {
        if (GetCombatDistance(Vector3.Distance(GridGenerator.gridInstance.GetClosestGrid(GridGenerator.gridInstance.objectArray, transform.position).position, GridGenerator.gridInstance.GetClosestGrid(GridGenerator.gridInstance.objectArray, GameObject.Find("Enemy").transform.position).position)) <= 10)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            GridGenerator.gridInstance.combatEnabled = false;
        }
    }

    void MoveDistCombatReset()
    {
        if (!GridGenerator.gridInstance.combatEnabled)
        {
            if (!miscCombatStart)
            {
                if (GridGenerator.gridInstance.MoveDistanceOfPlayer != playerMoveDistance * 10)
                {
                    GridGenerator.gridInstance.MoveDistanceOfPlayer = playerMoveDistance * 10;
                }
                miscCombatStart = true;
            }
        }
        else
        {
            if (miscCombatStart)
            {
                miscCombatStart = false;
            }
        }

//        if (TurnManager.turnManagerInstance.waitFor)
//        {
//            GridGenerator.gridInstance.MoveDistanceOfPlayer = playerMoveDistance * 10;
//            TurnManager.turnManagerInstance.waitFor = false;
//        }

    }

    float GetCombatDistance(float b)
    {
        float mainDist = Mathf.Round(b / 10);
        if (mainDist != (b / 10))
        {
            mainDist = (mainDist + 1) * 10;
            return mainDist;
        }
        else
        {
            return b;
        }
    }

}
