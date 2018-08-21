using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
	#region Singleton
	private static VideoManager _instance = null;
	public static bool hasInstance
	{
		get { _instanceCheck(); return _instance != null; }
	}
	private static VideoManager _instanceCheck()
	{
		if (_instance == null)
			_instance = GameObject.FindObjectOfType<VideoManager>();
		return _instance;
	}
	public static VideoManager instance
	{
		get
		{
			_instanceCheck();
			if (_instance == null)
				Debug.LogError("<color=red>VideoManager Not Found!</color>");
			return _instance;
		}
	}

	void OnApplicationQuit()
	{
		_instance = null;
		DestroyImmediate(gameObject);
	}
	#endregion

	public VideoPlayer videoPlayerRef;
	public GameObject nutGO;

	private void Start()
	{
		videoPlayerRef.loopPointReached += EndReached;
		nutGO.SetActive(true);
	}

	public void StartVideo()
	{
		videoPlayerRef.gameObject.SetActive(true);
		nutGO.SetActive(false);
	}

	private void EndReached(VideoPlayer vp)
	{
		videoPlayerRef.gameObject.SetActive(false);
		nutGO.SetActive(true);
	}

	private void OnDestroy()
	{
		videoPlayerRef.loopPointReached -= EndReached;
	}
}
