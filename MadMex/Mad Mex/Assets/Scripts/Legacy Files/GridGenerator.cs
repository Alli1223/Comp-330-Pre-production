using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour 
{
    public float MoveDistanceOfPlayer { get; set; }
    public static GridGenerator gridInstance;
	private Terrain terrainBase; //terrain grid is attached to
    [SerializeField]
    private bool showGrid = false;
    private int CellSize = 10;

    public Material x;

    private Transform character;

	private Vector3 terrainSize;
	private Vector3 origin;

    public int width;
    public int height;

    public Transform[,] objectArray;

	private List<GameObject> objects;

    public bool combatEnabled;
    public bool isMoving;

    public bool moveFinished;

    bool moveEnd;

	public List<Transform> objectsInRange = new List<Transform>();

    void Awake()
    {
        gridInstance = this;

        character = GameObject.FindGameObjectWithTag("Player").transform;
        terrainBase = GameObject.FindGameObjectWithTag("MainTerrain").GetComponent<Terrain>();
		terrainSize = terrainBase.terrainData.size;
        origin = terrainBase.transform.position;
            //new Vector3(character.transform.position.z - CellSize, character.transform.position.y, character.transform.position.x - CellSize);

		width = (int)terrainSize.z / CellSize;
		height = (int)terrainSize.x / CellSize;

		objects = new List<GameObject>();

        objectArray = new Transform[width,height];

		BuildGrid();  
	}

	void Update ()
	{
//        Debug.Log(MoveDistanceOfPlayer);
        if (!combatEnabled)
        {
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<MeshRenderer>().enabled = showGrid;
                obj.GetComponent<Collider>().enabled = showGrid;
            }
        }

		RaycastHit abc;
        Debug.DrawRay(Input.mousePosition, Camera.main.transform.forward, Color.red);
        if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out abc)) 
		{
			if (abc.transform.gameObject.tag == "GridPiece") 
			{
                if(Input.GetMouseButton(0))
                abc.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
			}
		}

        if (combatEnabled)
        {
            if (!moveFinished)
            {
                if (!isMoving)
                {
                    Transform currentPositionGrid = GetClosestGrid(objectArray, character.position);
                    GetGridWithinRange(currentPositionGrid, objectArray);
                    Debug.Log("A");
                    moveEnd = true;
                }
                if (moveEnd)
                {
                    foreach (Transform x in objectsInRange)
                    {
                        if (!x.gameObject.activeSelf)
                        {
                            x.GetComponent<MeshRenderer>().enabled = true;
                            x.GetComponent<Collider>().enabled = true;
                        }
                    }
                    moveEnd = false;
                }
                moveFinished = true;
            }

        }

	}
    public void SPAWNTHEMOTHERCLUCKINGGRID()
    {
        Transform currentPositionGrid = GetClosestGrid(objectArray, character.position);
        GetGridWithinRange(currentPositionGrid, objectArray);
    }

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public Transform GetClosestGrid(Transform[,] Grids, Vector3 targetPosition)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = targetPosition;
        foreach (Transform t in Grids)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
//        tMin.GetComponent<MeshRenderer>().enabled = true;
//        tMin.GetComponent<Collider>().enabled = true;
        return tMin;
    }
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


    public void GetGridWithinRange(Transform a, Transform[,] Grids)
    {
        
        foreach (Transform b in Grids)
        {
            if (DistanceCalculation(Vector3.Distance(a.position, b.position)) <= MoveDistanceOfPlayer)
            {
                b.gameObject.GetComponent<MeshRenderer>().material = x;
                b.GetComponent<MeshRenderer>().enabled = true;
                b.GetComponent<Collider>().enabled = true;
//                b.gameObject.SetActive(true);
            }
            else
            {
                if (objectsInRange.Count != 0)
                {
                    foreach (Transform x in objectsInRange)
                    {
                        if (b != x)
                        {
                           // b.gameObject.SetActive(false);
                            b.GetComponent<MeshRenderer>().enabled = false;
                            b.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                else
                {
                   // b.gameObject.SetActive(false);
                    b.GetComponent<MeshRenderer>().enabled = false;
                    b.GetComponent<Collider>().enabled = false;
                }
            }
        }
        return;
    } 

    public float DistanceCalculation(float a)
    {  
        float mainDistValue = Mathf.Round(a / 10);
        if(mainDistValue != (a / 10))
        {
            mainDistValue = (mainDistValue + 1) * 10;
            return mainDistValue;
        }
        else
        {
            return a;
        }

    }


	void BuildGrid()
	{  
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				go.tag = "GridPiece";
				Vector3 pos = GetWorldPosition(new Vector2(x,y));
				pos += new Vector3(CellSize / 2, terrainBase.SampleHeight(GetWorldPosition(new Vector2(x,y))), CellSize /2);
				go.transform.position = new Vector3 (pos.x, transform.position.y, pos.z);
				pos.y = terrainBase.SampleHeight (go.transform.position);
				go.transform.position = pos;				

				go.transform.localScale = new Vector3(CellSize, 2, CellSize);
				go.transform.parent = transform;
                go.GetComponent<MeshRenderer>().enabled = false;
                go.GetComponent<Collider>().enabled = false;
                objectArray[x, y] = go.transform;
				objects.Add(go);
			}
		} 
	}

	public Vector3 GetWorldPosition(Vector2 gridPosition)
	{
		return new Vector3(origin.z + (gridPosition.x * CellSize), origin.y, origin.x + (gridPosition.y * CellSize));
	}

	public Vector2 GetGridPosition(Vector3 worldPosition)
	{
        return new Vector2(worldPosition.z / CellSize, worldPosition.x / CellSize);
	}
}