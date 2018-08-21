using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonComponent : MonoBehaviour
{
	public GameObject projectilePrefab;
	public Transform spawnT;

	private float forwardForce = 10f;
	
	public void Fire()
	{
		GameObject go = Instantiate(projectilePrefab);
		go.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		go.transform.position = spawnT.position;
		go.transform.rotation = spawnT.rotation;
		go.GetComponent<Rigidbody>().AddForce(spawnT.forward * forwardForce, ForceMode.Impulse);
	}
}
