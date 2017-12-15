using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSystemManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
    public GameObject player;
    public GameObject enemy;
    public GameObject LeftArm;
    public GameObject RightArm;

    public float playerHeight = 1.3f;
    public float playerWidthOffset = 1.2f;
    enum Coverstate
    {
        NoCover,
        HalfCover,
        FullCover
    } Coverstate cover;
	
   
    bool CheckCoverWithObject(GameObject objectOne, GameObject objectTwo)
    {
        RaycastHit hit;
        // Calcualte direction of ray
        Vector3 fromPosition = objectOne.transform.position;
        fromPosition.y += playerHeight;
         Vector3 toPosition = objectTwo.transform.position;
        Vector3 direction = toPosition - fromPosition;

        // Raycast from top of mech
        if(Physics.Raycast(fromPosition, direction,out hit))
        {
            Debug.DrawRay(fromPosition, direction);
           // print("Body can see : " + hit.collider.gameObject.name);
            if(hit.collider.gameObject.name == "temp_enemy (1)")
                print("Body can see : " + hit.collider.gameObject.name);

            return true;
        }
        return false;

    }

    bool LeftArmRay(GameObject leftArm, GameObject target)
    {
        RaycastHit hit;
        Vector3 fromPosition = leftArm.transform.position;
        fromPosition.x -= playerWidthOffset;
        Vector3 toPosition = target.transform.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(fromPosition, direction, out hit))
        {
            Debug.DrawRay(fromPosition, direction);
            if (hit.collider.gameObject.name == "temp_enemy (1)")
                print("Left arm can see : " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }
    bool RightArmRay(GameObject RightArm, GameObject target)
    {
        RaycastHit hit;
        Vector3 fromPosition = RightArm.transform.position;
        fromPosition.x += playerWidthOffset;
        Vector3 toPosition = target.transform.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(fromPosition, direction, out hit))
        {
            Debug.DrawRay(fromPosition, direction);
            if (hit.collider.gameObject.name == "temp_enemy (1)")
                print("Right arm can see : " + hit.collider.gameObject.name);
            return true;
        }
        return false;

    }


    // Update is called once per frame
    void Update ()
    {
        CheckCoverWithObject(player, enemy);
        LeftArmRay(LeftArm, enemy);
        RightArmRay(RightArm, enemy);
        /*
        if (LeftArmRay(LeftArm, enemy))
            print("Left Arm can hit");
        if(RightArmRay(RightArm, enemy))
            print("Right Arm can hit");
            */

    }
}
