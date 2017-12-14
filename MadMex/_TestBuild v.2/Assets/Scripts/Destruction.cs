using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is going to be the script for when the building is destroyed

public class Destruction : MonoBehaviour {

    public int BuildingHealth = 100;
    public int currentHealth;
    public int DestructionOdds = 7;
    public int RocketDamage = 50;
    public int BulletDamage = 25;
	// Make private unless required in other scripts

	[SerializeField]
	private ParticleSystem smokeOne;

    void Start() {
        currentHealth = BuildingHealth;
    }

    void /*OnCollisionEnter(Collision col)*/ FixedUpdate()
    {
        if /*(col.gameObject.tag == "rocket")*/(Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("key down o");
            currentHealth -= RocketDamage;
            if (currentHealth <= 0)
            {
                DestructionType();
            }
        }
        else if /*(col.gameObject.tag == "bullets")*/(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("key down p");
            currentHealth -= BulletDamage;
            if (currentHealth <= 0)
            {
                DestructionType();
            }

        }
    }

    void DestructionType() // this section is used to determine how the building is destroyed, falls to create more cover or completly destroyed
    {
        int DestructionChance = Random.Range(0, 10);
        Debug.Log(DestructionChance);
        if (DestructionChance > DestructionOdds)
        {
            CompleteDestruction();
        }
        else
        {
			StartCoroutine(FallenTower());
        }
    }

    void CompleteDestruction()
    {
        Debug.Log("Complete");
        Object.Destroy(gameObject, 0.2f);
        GameObject Rubble = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Rubble.transform.position = new Vector3(0, 0, 0);
        Rubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

	IEnumerator FallenTower()
    {		
		smokeOne.gameObject.SetActive (true);
		yield return new WaitForSeconds (1);
        Debug.Log("Fallen");
		Vector3 savedPos = gameObject.transform.position;
        Object.Destroy(gameObject, 0.2f);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0, 0);
        cube.transform.localScale = new Vector3(1.0f, 3.0f, 1.0f);
        cube.transform.Rotate(0, 0, 90);
		cube.transform.position = savedPos;
    }

}
