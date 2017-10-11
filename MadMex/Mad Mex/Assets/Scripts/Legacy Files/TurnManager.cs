using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour 
{
    public static TurnManager turnManagerInstance;
    public bool playersTurn = true;

    public bool enemyMoved;
    public bool waitFor;

    public bool startMove;

	// Use this for initialization
	void Start () 
    {
        turnManagerInstance = this;
	}
	
	// Update is called once per frame
    void Update () 
    {
        
        if (GridGenerator.gridInstance.combatEnabled)
        {
            if (playersTurn)
            {
                    if (!waitFor)
                    {
//                        GridGenerator.gridInstance.moveFinished = false;
                        if (GridGenerator.gridInstance.MoveDistanceOfPlayer == 0)
                        {
                            playersTurn = false;
                            startMove = true;
                        }
                    }
                    else if (waitFor)
                    {
//                    GridGenerator.gridInstance.moveFinished = false;
//                    GridGenerator.gridInstance.GetGridWithinRange(GameObject.FindGameObjectWithTag("Player").transform, GridGenerator.gridInstance.objectArray);
//                    waitFor = false;
//                    Debug.Log(GridGenerator.gridInstance.MoveDistanceOfPlayer);
                    }

            }
            else
            {

                if (enemyMoved)
                {
                    Debug.Log("Go");
                    playersTurn = true;
                    enemyMoved = false;
//                    waitFor = true;

                }
            }

        }
	}
}
