﻿/*
	Created On:		25/09/2017
	Created By: 	Marc Andrews
	Last Edit: 		27/09/2017 10:32
	Last Edit By: 	Marc Andrews
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGeneration : MonoBehaviour 
{
    public static GridGeneration gridSingle;
	//Makes Grid gen script a singleton
    void Awake()
    {
        if (gridSingle == null)
            gridSingle = this;
        else
            Destroy(this); 

		ManagersManager.manager.tGrid = gridSingle;
    }



    [Range (1,10)]
    public int tileSize = 1;
	[Range(0.1f,1)]
	public float tileHeight = 1;
	public Tile[,,] tileVariables;
    public List<GameObject> currentTiles;
	public List<MeshRenderer> tileMeshs;
	[SerializeField]
	private Material tile_Tex;

    private float halfSize;
    private GameObject tileHolder;

	private int currentGridNoRef;

	private int objScaleX, objScaleZ;
	private List<GameObject> walkableObjs = new List<GameObject>();



	/// <summary>
	/// Creates a grid when called.
	/// </summary>
    public void CreateGrid()
    {
		walkableObjs = new List<GameObject> ();
		halfSize = tileSize;
		halfSize /= 2;

		//

		//Finds all of the objects tagged walkable and puts them into a list
        walkableObjs.AddRange(GameObject.FindGameObjectsWithTag("WalkTer"));

		currentGridNoRef = 0;
		objScaleX = 0;
		objScaleZ = 0;
		DimensionScale (walkableObjs);
		tileVariables = new Tile[walkableObjs.Count,objScaleX,objScaleZ];

		//Will gen a grid of tiles for every walkable obj
        foreach (GameObject x in walkableObjs)
        {
			//Adds a Gameobject to hold the grid for each obj
			tileHolder = new GameObject ();
			tileHolder.name = "Tile Holder: " + x.name;
			tileHolder.transform.parent = x.transform;
			currentTiles.Add (tileHolder);
            TileGen(x.transform);
			currentGridNoRef++;
        }			
    }

	/// <summary>
	/// Generates a grid of tiles on start obj.
	/// </summary>
	/// <param name="startObj">The object that you want to spawn a grid of tiles onto.</param>
    private void TileGen(Transform startObj)
    {
		//bottom left position (bottom left being the closest point to negative x and z)
        Vector3 objBotLeft = cornerSurfacePoint(startObj);

		int Xpos = 0;
		int Ypos = 0;

        List<Tile> coverTiles = new List<Tile>();
		//creates a tile piece for every loop and adds it to current tiles;
		for (int y = 0; y < startObj.localScale.z; y += tileSize)
        {
			for (int x = 0; x < startObj.localScale.x; x += tileSize)
            {
                GameObject nwTilePiece = GameObject.CreatePrimitive(PrimitiveType.Cube);
				nwTilePiece.transform.position = objBotLeft + new Vector3(x, 0, y);
				nwTilePiece.transform.localScale = new Vector3 (tileSize, tileHeight, tileSize);
                nwTilePiece.tag = "gridPiece";              
				nwTilePiece.transform.parent = tileHolder.transform;
				nwTilePiece.GetComponent<MeshRenderer> ().material = tile_Tex;
                nwTilePiece.GetComponent<MeshRenderer>().enabled = tileShown;

				Tile nwTile = new Tile ();
				nwTile.SetPosition (objBotLeft + new Vector3 (x, 0, y));
                nwTile.CurrentGridCoods(currentGridNoRef, Xpos, Ypos);
                Collider[] objectsInRange = Physics.OverlapSphere (nwTile.tPos, halfSize);
				for (int i = 0; i < objectsInRange.Length; i++) 
				{
                    if (!walkableObjs.Contains (objectsInRange[i].gameObject) && objectsInRange[i].tag != "gridPiece" && objectsInRange[i].gameObject.layer == 11) 
					{
						nwTile.NotWalkable ();
						nwTilePiece.SetActive (false);
					}
				}
                tileVariables [currentGridNoRef, Xpos, Ypos] = nwTile;
                if (nwTile.isCover)
                {
                    coverTiles.Add(nwTile);
                }
                else
                {
                    nwTile.tileCover = coverDirection.NONE;
                }
//                nwTilePiece.GetComponent<Collider>().enabled = false;

//				TileEnum nwTileVar = new TileEnum();
//				nwTileVar.thisTile = nwTilePiece;
//				nwTileVar.CurrentPosition (nwTilePiece.transform.position);
//				nwTileVar.CurrentGridCoods (currentGridNoRef, Ypos, Xpos);
//				tileVariables [currentGridNoRef, Ypos, Xpos] = nwTileVar;

//				nwTileValues.CurrentPosition (nwTilePiece.transform.position);
//				nwTileValues.CurrentGridCoods(currentGridNoRef,Ypos,Xpos);
                currentTiles.Add(nwTilePiece);
				tileMeshs.Add (nwTilePiece.GetComponent<MeshRenderer> ());
				Xpos++;
            }
            Xpos = 0;
			Ypos++;
        }

        foreach (Tile t in coverTiles)
        {
            t.tileCover = coverDirection.NONE;
            tileVariables[t.currentGrid, t.Xpos + 1, t.Ypos].tileCover = coverDirection.North;
            tileVariables[t.currentGrid, t.Xpos - 1, t.Ypos].tileCover = coverDirection.South;
            tileVariables[t.currentGrid, t.Xpos, t.Ypos + 1].tileCover = coverDirection.West;
            tileVariables[t.currentGrid, t.Xpos, t.Ypos - 1].tileCover = coverDirection.East;
        }           

    }

	/// <summary>
	/// Gets a bottom left point (bottom left being the closest point to negative x and z) at the surface of the object
	/// </summary>
	/// <returns>The closest point to negative x and z at the surface of the object.</returns>
	/// <param name="centredObj">The object you want a bottom left surface point of.</param>
    private Vector3 cornerSurfacePoint(Transform centredObj)
    {
        Vector3 outPoint = centredObj.position;
        outPoint -= centredObj.localScale * 0.5f;
		outPoint += new Vector3 (halfSize, 0, halfSize);
        outPoint.y += centredObj.localScale.y;
        return outPoint;
    }

	/// <summary>
	/// Destroys all tile grids and their parents.
	/// </summary>
    public void DestroyGrid()
    {
		tileMeshs.Clear ();
		GameObject tempTilHold = tileHolder;
		currentTiles.Remove (tileHolder);
		DestroyImmediate(tempTilHold);
		currentTiles.AddRange(GameObject.FindGameObjectsWithTag("gridPiece"));
        foreach(GameObject c in currentTiles)
        {
			DestroyImmediate(c);
        }
		currentTiles.Clear ();
    }

	private void DimensionScale(List<GameObject> walkableObjects)
	{
		float highestX = 0;
		float highestZ = 0;
		foreach (GameObject x in walkableObjects) 
		{
			float sizeX = x.transform.localScale.x;
			float sizeZ = x.transform.localScale.z;
			if (sizeX > highestX) 
			{
				highestX = sizeX;
			}
			if (sizeZ > highestZ) 
			{
				highestZ = sizeZ;
			}

		}
		objScaleX = Mathf.RoundToInt(highestX);
		objScaleZ = Mathf.RoundToInt(highestZ);
        Debug.Log(objScaleX + " : " + objScaleZ);
	}

	private bool tileShown; 

	public void ShowTiles() 
	{ 
		tileShown = !tileShown; 
		foreach (MeshRenderer x in tileMeshs) 
		{ 
			if (tileShown) 
			{ 
			x.enabled = true; 
			} 
			else 
			{ 
				x.enabled = false; 
			} 
		} 
	}

	public void RevertTileColour()
	{
		foreach (MeshRenderer x in tileMeshs) 
		{
			x.material.color = tile_Tex.color;	
		}
	}
}
