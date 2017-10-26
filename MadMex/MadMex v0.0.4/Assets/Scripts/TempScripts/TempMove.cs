using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour 
{
	private ManagersManager tManage;
	Transform moveToPos;
	Transform playerTile;

	[SerializeField]
	private bool playersTurn;

	public int modifiedMovDist;

	// Use this for initialization
	void Start () 
	{		
		tManage = ManagersManager.manager;


		modifiedMovDist = tManage.tPlayer.currentPlayer.GetComponent<TempPlayerVar>().currentDist;

		playerTile = tManage.tDetect.GetClosestGrid(tManage.tPlayer.currentPlayer.transform, tManage.tGrid.currentTiles);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playersTurn) 
		{
			tManage.tDetect.FindTilesInDist (playerTile, tManage.tGrid.tileMeshs, modifiedMovDist);
			playerTile = tManage.tDetect.GetClosestGrid (tManage.tPlayer.currentPlayer.transform, tManage.tGrid.currentTiles);
			playersTurn = false;
		}
        if (Input.GetMouseButtonDown (0)) 
        {
            RaycastHit hit;
            Ray testRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(testRay, out hit, 500))
			{
                if (hit.transform.GetComponent<MeshRenderer> ().enabled == true && hit.transform.tag == "gridPiece") 
				{
					moveToPos = hit.transform;
				}
			}
		}

        if(moveToPos != null && modifiedMovDist > 0)
		{
			tManage.tPlayer.currentPlayer.transform.position = Vector3.MoveTowards (tManage.tPlayer.currentPlayer.transform.position, new Vector3(moveToPos.position.x, tManage.tPlayer.currentPlayer.transform.position.y, moveToPos.position.z), 7);

			Transform newCurrentTile = tManage.tDetect.GetClosestGrid (tManage.tPlayer.currentPlayer.transform, tManage.tGrid.currentTiles);
			modifiedMovDist = tManage.tDetect.DistModifier(playerTile, newCurrentTile, modifiedMovDist);
				moveToPos = null;
				playerTile = newCurrentTile;
				playersTurn = true;
		}

	}

    public void ShowMovable()
    {
		modifiedMovDist = tManage.tPlayer.currentPlayer.GetComponent<TempPlayerVar>().currentDist;
		playerTile = tManage.tDetect.GetClosestGrid(tManage.tPlayer.currentPlayer.transform, tManage.tGrid.currentTiles);
		tManage.tDetect.FindTilesInDist(playerTile, tManage.tGrid.tileMeshs, modifiedMovDist);
    }
}
