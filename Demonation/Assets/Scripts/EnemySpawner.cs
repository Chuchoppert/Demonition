using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] prefabs;

	// Use this for initialization
	void Start()
	{

		StartCoroutine(SpawnEnemies()); //invoke("SpawnEnemies, Random.Range(1, 4)");
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator SpawnEnemies()
	{
		while (true)
		{

			// instantiate a random enemy past the right egde of the screen, facing left
			Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(57.7f, Random.Range(-12f, 14.5f), 27.7f), Quaternion.Euler(-90f, -90f, 0f));

			// pause this coroutine for 3-10 seconds and then repeat loop
			yield return new WaitForSeconds(Random.Range(1, 4));
		}
	}
}
