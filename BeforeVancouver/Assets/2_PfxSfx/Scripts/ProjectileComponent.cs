using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
	public GameObject destructPfx;
	private float destructTime = 5f;
	
	private void Start()
	{
		//Invoke("DestructPfx", destructTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (destructPfx != null)
		{
			GameObject go = Instantiate(destructPfx);
			go.transform.position = this.transform.position;
			go.GetComponent<DestroyPfxComponent>().SetColor(this.GetComponent<Renderer>().material.color);
		}
		Destroy(this.gameObject);
	}

	private void DestructPfx()
	{
		if (destructPfx != null)
		{
			GameObject go = Instantiate(destructPfx);
			go.transform.position = this.transform.position;
			go.GetComponent<DestroyPfxComponent>().SetColor(this.GetComponent<Renderer>().material.color);
		}
		Destroy(this.gameObject);
	}
}
