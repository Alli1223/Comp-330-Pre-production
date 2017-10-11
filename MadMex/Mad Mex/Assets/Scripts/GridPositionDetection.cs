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
    private PlayerManager tPlayer;
    private GridGeneration tGrid;
	private Transform currentPozGrid;
	//Should be moved to a turn manager later
	[SerializeField]
	private bool playersTurn;
	[SerializeField]
	public bool gridRefresh;

	public static GridPositionDetection gridDectect;
    
	//Grabs player manager and the grid generator for the current tiles
	void Awake()
    {
		if (gridDectect == null)
			gridDectect = this;
		else
			Destroy(this);

        tPlayer = PlayerManager.gPlayer;
		tGrid = GridGeneration.gridSingle;
		//currentPozGrid = GetClosestGrid(tPlayer.player.transform, tGrid.currentTiles);
    }

	void Update()
	{
		if (playersTurn) 
		{
//			EnableTilesInDist (tPlayer.player.transform, playerMoveDist);
			FindTilesInDist(GetClosestGrid(tPlayer.player.transform, tGrid.currentTiles), tGrid.tileMeshs);
			playersTurn = false;
		}

		if (gridRefresh) 
		{
			//				FindTilesInDist (GetClosestGrid (tPlayer.player.transform, tGrid.currentTiles), tGrid.tileMeshs);
//			DisableTilesInDist(tGrid.tileMeshs);
//			gridRefresh = false;
		}
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

	//temp variable
	public int playerMoveDist = 6;

	public void EnableTilesInDist(Transform playerTile, float dist)
	{		
		Collider[] tilesInRange = Physics.OverlapSphere (playerTile.position, dist);
		foreach (Collider c in tilesInRange) 
		{
			c.GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	private void DisableTilesInDist(List<MeshRenderer> objMeshs)
	{		
		foreach (MeshRenderer m in objMeshs) 
		{
			m.enabled = false;
		}
	}


//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	/// <summary>
	/// Currently old system Do Not Use
	/// </summary>
	/// <param name="playerTileLocal">The tile beneath the player.</param>
	/// <param name="tilesInGame">Mesh Renderer of all tiles in the level.</param>
	void FindTilesInDist(Transform playerTileLocal,  List<MeshRenderer> tilesInGame)
	{
		foreach (MeshRenderer b in tilesInGame) 
		{
			Transform tile = b.transform;
			float xAxis = playerTileLocal.position.x;
			float zAxis = playerTileLocal.position.z;

			//
			if((Mathf.RoundToInt(Mathf.Abs(tile.position.x - xAxis)) + Mathf.RoundToInt(Mathf.Abs(tile.position.z - zAxis))) <= playerMoveDist)
				/*
				(b - a) + (z - c) =     5
				5 - 2 + 4 - 3 = 1
				(0.5  - 3.5) + (11.5 - -10.5) = 19
				25
				*/
				//Dist variant for diaginal directions
//			if (Mathf.Round(Vector3.Distance (playerTileLocal.position, tile.position)) <= playerMoveDist / 1.4f) 
			{
				b.enabled = true;
			} 
			//Cross Section for the tiles (Only x tiles and only z tiles with the other cood being 0 Example X:5,Z:0 or X:0,Z:8)
//			else if(Mathf.Round(xAxis - tile.position.x) <= playerMoveDist && zAxis == tile.position.z && Mathf.Round(xAxis - tile.position.x) >= -playerMoveDist || Mathf.Round(zAxis - tile.position.z) <= playerMoveDist && xAxis == tile.position.x  && Mathf.Round(zAxis - tile.position.z) >= -playerMoveDist)
//			{
//				b.enabled = true;
//			}
			else
			{
				b.enabled = false;
			}

		}
		gridRefresh = false;
	}

	public float DistanceCalculation(float a)
	{  
		float mainDistValue = Mathf.Round(a);
		if(mainDistValue != (a))
		{
			mainDistValue = (mainDistValue + 0.1f);
			return mainDistValue;
		}
		else
		{
			return a;
		}

	}
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

}
