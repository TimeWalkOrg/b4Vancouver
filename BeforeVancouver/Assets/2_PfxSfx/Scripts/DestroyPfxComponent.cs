using UnityEngine;
using System.Collections;

public class DestroyPfxComponent : MonoBehaviour
{
	public ParticleSystem[] pfxs;
	public AudioClip[] clips;
	private AudioSource thisAudioSource;

	private void Start()
	{
		thisAudioSource = GetComponent<AudioSource>();
		thisAudioSource.PlayOneShot(clips[(int)Random.Range(0, 3)]);
		Destroy(this.gameObject, this.GetComponent<ParticleSystem>().main.duration);
	}

	public void SetColor(Color color)
	{
		for (int i = 0; i < pfxs.Length; i++)
		{
			var mainPfx = pfxs[i].main;
			mainPfx.startColor = color;
		}
	}
}
