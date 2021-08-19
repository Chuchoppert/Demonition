using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] EnemyPrefabs;
	public float[] EnemiesForSeconds = { 2f, 4f };

	private GameObject prefabInstance;
	public int AmountEnemies = 7;
	public int Cuantostenemos;
	public int Cuantostenemos1;
	int iforeach;

	public static List<GameObject> InstancesEnemies;
	public float[] DistanceToStop = { 7f, 11f };
	public float MovingSpeedEnemy = 2f;
	GameObject[] EachEnemy;

	void Start()
	{
			Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

	// Update is called once per frame
	void Update()
	{
		Cuantostenemos = transform.childCount;
		//Cuantostenemos1 = InstancesEnemies.Length;
	}
	void PreSpawn()
    {
		if(transform.childCount < AmountEnemies)
        {
			Invoke("SpawnEnemies", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1])); //ARRGLAR: EL QUE NO PUEDAN GENERARSE MAS TRAS MORIR Y EL QUE SE GENEREN 7 AL MISMO TIEMPO
		}	
	}
	void SpawnEnemies()
	{
		for (int iforeach = 0; iforeach < AmountEnemies; iforeach++)
		{
			//InstancesEnemies = new List<GameObject>(AmountEnemies);

			// instantiate a random enemy past the right egde of the screen, facing left
			prefabInstance = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], new Vector3(57.7f, Random.Range(-12f, 14.5f), 27.7f), Quaternion.Euler(-90f, 0f, 0f));
			prefabInstance.transform.SetParent(transform);
			//InstancesEnemies.Add(prefabInstance);
		}
		/*2 if (InstancesEnemies[iforeach].transform.position.x > Random.Range(DistanceToStop[0], DistanceToStop[1]))
			//					45							>			7 - 11
		{
			//transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);
			InstancesEnemies[iforeach].transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);
		}*/

		/* 1 if(iforeach == AmountEnemies)
        {
			iforeach = 0;
        }*/
		Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

}
