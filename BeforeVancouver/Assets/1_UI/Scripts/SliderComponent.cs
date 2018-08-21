using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderComponent : MonoBehaviour
{
	public GameObject characterPrefab;
	private Text textRef;
	private Slider sliderRef;
	private GameObject characterGO;

	private void Awake()
	{
		textRef = GetComponentInChildren<Text>();
		sliderRef = GetComponent<Slider>();
	}

	private void Start()
	{
		textRef.text = "0%";
	}

	public void StartTimer()
	{
		StopCoroutine("Timer");
		StartCoroutine("Timer");
	}

	public void ResetTimer()
	{
		StopCoroutine("Timer");
		sliderRef.value = 0f;
		textRef.text = "0%";
		if (characterGO != null)
			Destroy(characterGO);
	}

	IEnumerator Timer()
	{
		float sliderVal = 0f;
		sliderRef.value = sliderVal;

		float elapsedTime = 0;
		while (elapsedTime < 3f)
		{
			sliderVal = Mathf.Lerp(0, sliderRef.maxValue, (elapsedTime / 3f));
			sliderRef.value = sliderVal;
			textRef.text = ((int) sliderVal).ToString() + "%";
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		sliderRef.value = sliderRef.maxValue;
		textRef.text = ((int)sliderRef.maxValue).ToString() + "%";
		characterGO = Instantiate(characterPrefab, this.transform.root);
	}
}
