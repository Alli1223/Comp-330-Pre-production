/*
	Created On:		27/09/2017 10:34
	Created By: 	Marc Andrews
	Last Edit: 		28/09/2017 15:26
	Last Edit By: 	Marc Andrews
*/
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GridPositionDetection : MonoBehaviour
{
	private ManagersManager tManage;
	private Transform currentPozGrid;
	//Should be moved to a turn manager later
	[SerializeField]
	private bool playersTurn;
	[SerializeField]
	public bool gridRefresh;

	public static GridPositionDetection gridDetect;
    
	//Grabs player manager and the grid generator for the current tiles
	void Awake()
    {
		if (gridDetect == null)
			gridDetect = this;
		else
			Destroy(this);


		//currentPozGrid = GetClosestGrid(tPlayer.player.transform, tGrid.currentTiles);
	}

	void Start()
	{
		tManage = ManagersManager.manager;
		tManage.tDetect = gridDetect;
	}

	/// <summary>
	/// Gets the closest grid to the player/units current position.
	/// </summary>
	/// <returns>The closest grid.</returns>
	/// <param name="startPos">Player/units current position.</param>
	/// <param name="grids">The list of grid pieces currently in the level.</param>
	public Transform GetClosestGrid(Transform startPos, List<GameObject> grids)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = startPos.position;
        foreach (GameObject g in grids)
        {
			
			Transform t = g.transform.tag == "gridPiece" ? g.transform : null;
			if (t) 
			{
				float dist = Vector3.Distance (t.position, currentPos);
				if (dist < minDist) 
				{
					tMin = t;
					minDist = dist;
				}
			}
        }
        return tMin;
    }

	/// <summary>
	/// Currently old system Do Not Use
	/// </summary>
	/// <param name="playerTileLocal">The tile beneath the player.</param>
	/// <param name="tilesInGame">Mesh Renderer of all tiles in the level.</param>
	public void FindTilesInDist(Transform playerTileLocal,  List<MeshRenderer> tilesInGame, float dist)
	{
		foreach (MeshRenderer b in tilesInGame) 
		{
			Transform tile = b.transform;
			float xAxis = playerTileLocal.position.x;
			float zAxis = playerTileLocal.position.z;

			//
			if((Mathf.RoundToInt(Mathf.Abs(tile.position.x - xAxis)) + Mathf.RoundToInt(Mathf.Abs(tile.position.z - zAxis))) <= dist)
			{
				b.enabled = true;
			} 
			else
			{
				b.enabled = false;
			}

		}
		gridRefresh = false;
	}

	public int DistModifier(Transform startTile, Transform currentTile, int currentDist)
	{
		int newDist = currentDist;
		newDist -= (Mathf.RoundToInt(Mathf.Abs(currentTile.position.x - startTile.position.x)) + Mathf.RoundToInt(Mathf.Abs(currentTile.position.z - startTile.position.z)));
		return newDist;
	}
}
