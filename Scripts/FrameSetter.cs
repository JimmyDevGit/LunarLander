using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSetter : MonoBehaviour
{
	public static FrameSetter instance;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	private void Start()
	{
		Application.targetFrameRate = 60;
	}
}
