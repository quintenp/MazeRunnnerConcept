using UnityEngine;

[ExecuteInEditMode]
// This script is based on David Dion-Paquet's great article on http://www.gamasutra.com/blogs/DavidDionPaquet/20140601/218766/Creating_a_parallax_system_in_Unity3D_is_harder_than_it_seems.php
public class ParallaxElement : MonoBehaviour 
{
	public float HorizontalSpeed;
	public float VerticalSpeed;
	public bool MoveInOppositeDirection;

	private Vector3 _previousCameraPosition;
	private bool _previousMoveParallax;
	private ParallaxCamera _parallaxCamera;
	private CameraController _camera;
	private Transform _cameraTransform;

	void OnEnable() 
	{
		if (GameObject.FindGameObjectWithTag("MainCamera")==null)
			return;
			
		_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
		_parallaxCamera = _camera.GetComponent<ParallaxCamera>();
		_cameraTransform = _camera.transform;		
		_previousCameraPosition = _cameraTransform.position;
	}

	void Update () 
	{
		if (_parallaxCamera==null)
			return;
	
		if(_parallaxCamera.MoveParallax && !_previousMoveParallax)
			_previousCameraPosition = _cameraTransform.position;

		_previousMoveParallax = _parallaxCamera.MoveParallax;

		if(!Application.isPlaying && !_parallaxCamera.MoveParallax)
			return;

		Vector3 distance = _cameraTransform.position - _previousCameraPosition;
		float direction = (MoveInOppositeDirection) ? -1f : 1f;
		transform.position += Vector3.Scale(distance, new Vector3(HorizontalSpeed, VerticalSpeed)) * direction;

		_previousCameraPosition = _cameraTransform.position;
	}
}
