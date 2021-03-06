﻿using UnityEngine;

public class Tile 
{
	public GameObject occupyingObj;
	public GameObject parent;
	public bool isWalkable, isOnFire, isCover;

    public coverDirection tileCover;

    /// <summary>
    /// The gameobject of this tile.
    /// </summary>
    public GameObject thisTile;
    
    /// <summary>
    /// Current tile position.
    /// </summary>
    public Vector3 tPos;
    
    /// <summary>
    /// Tile point in cood based on other tiles starting from the closest point to negative x and z. [CurrentObject Number reference, Y position, X position]
    /// </summary>
    public int currentGrid, Ypos, Xpos;


    public void NotWalkable()
    {
        isWalkable = false;
    }

    public virtual void NotWalkable(GameObject occupiedObj)
    {
        isWalkable = false;
        if (occupiedObj.layer == 11)
        {
            isCover = true;
        }
        occupyingObj = occupiedObj;
    }

    /// <summary>
    /// Changes the position to it's new position.
    /// </summary>
    /// <param name="newPosition">The new position of the tile.</param>
    public void SetPosition(Vector3 newPosition)
    {
        tPos = newPosition;
    }

    /// <summary>
    /// Changes the array coods applied to this object in reference to the original array.
    /// </summary>
    /// <param name="thisGrid">A number reference to the object the grid is based on.</param>
    /// <param name="Yposition">The z position in 3D space or y in 2d when looking at the grid.</param>
    /// <param name="Xposition">The x position in 2d and 3d space of the object.</param>
    public void CurrentGridCoods(int thisGrid, int Yposition, int Xposition)
    {
        currentGrid = thisGrid;
        Ypos = Yposition;
        Xpos = Xposition;
    }
}
