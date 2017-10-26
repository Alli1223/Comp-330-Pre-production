using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShoot : MonoBehaviour
{
	
    [SerializeField]
    private GameObject bullet;

    private PlayerManager tPlayer;

    [SerializeField]
    private float force, gunDamage;

    // Use this for initialization
    void Start()
    {
        tPlayer = PlayerManager.gPlayer;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray testRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(testRay, out hit, 500))
            {
                if (hit.transform.tag == "enemy")
                {
                    Vector3 dir = (hit.transform.position - tPlayer.currentPlayer.transform.position);
                    GameObject firedBull = Instantiate(bullet, tPlayer.currentPlayer.transform.position, Quaternion.LookRotation(dir));
                    firedBull.GetComponent<TempBullets>().damage = (int)gunDamage;
                    dir.y = hit.transform.position.y / 2;
					firedBull.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
                }
            }
        }
    }
}
