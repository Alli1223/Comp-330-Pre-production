using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour 
{
	private PlayerManager tPlayer;
	private GridPositionDetection tDetect;
	private GridGeneration tGrid;
	Transform moveToPos;
	Transform playerTile;

	private float moveDist;

	// Use this for initialization
	void Start () 
	{
		tPlayer = PlayerManager.gPlayer;
		tGrid = GridGeneration.gridSingle;
		tDetect = GridPositionDetection.gridDectect;

		moveDist = tDetect.playerMoveDist;

		playerTile = tDetect.GetClosestGrid (tPlayer.player.transform, tGrid.currentTiles);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray testRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;
			if(Physics.Raycast(testRay, out hit, 500))
			{
				if (hit.transform.GetComponent<MeshRenderer> ().enabled == true) 
				{
					moveToPos = hit.transform;
				}
			}
		}
		if (playerTile == moveToPos) 
		{
			moveToPos = null;
		}
		if(moveToPos != null)
		{
//			if (Vector3.Distance (tPlayer.player.transform.position, moveToPos.position) >= 0.3f) 
//			{
				tPlayer.player.transform.position = Vector3.MoveTowards (tPlayer.player.transform.position, new Vector3(moveToPos.position.x, tPlayer.player.transform.position.y, moveToPos.position.z), 1);
//			} else 
//			{
			if (Vector3.Distance (tPlayer.player.transform.position, moveToPos.position) <= 0.3f) 
			{
				moveDist -= Vector3.Distance (playerTile.position, moveToPos.position);
				moveToPos = null;
				playerTile = tDetect.GetClosestGrid (tPlayer.player.transform, tGrid.currentTiles);
//				tDetect.gridRefresh = true;
//				tDetect.EnableTilesInDist (playerTile, moveDist);
			}
//			}
		}
		if (moveDist <= 0) 
		{
			moveDist = tDetect.playerMoveDist;
		}
		Debug.DrawRay (testRay.origin, testRay.direction * 20, Color.green);
	}
}
