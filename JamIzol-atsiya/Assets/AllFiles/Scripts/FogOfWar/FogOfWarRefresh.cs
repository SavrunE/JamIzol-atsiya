using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FogOfWarRefresh : MonoBehaviour
{
	public bool isDynamic;
	private Camera _camera;

	void Start()
	{
		_camera = GetComponent<Camera>();
		_camera.clearFlags = CameraClearFlags.Color;
	}

	void OnPostRender()
	{
		if (!isDynamic)
		{
			_camera.clearFlags = CameraClearFlags.Depth;
		}
	}
}
