using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
	public GameObject prefabs;
	public float[] EnemiesForSeconds = { 10f, 15f };


	// Use this for initialization
	void Start()
	{
		Invoke("Spawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Spawn()
	{
		// instantiate a random enemy past the right egde of the screen, facing left
		Instantiate(prefabs, new Vector3(transform.position.x, Random.Range(-11f, 10f), 27.7f), Quaternion.Euler(-35f, -90f, 0f));

		Invoke("Spawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}
}
