using UnityEngine;

public class LevelLimits : MonoBehaviour 
{
	public float LeftLimit;
	public float RightLimit;
	public float BottomLimit;
	public float TopLimit;

	private BoxCollider2D _collider;
	
	void Awake () 
	{
		_collider=GetComponent<BoxCollider2D>();
		
		LeftLimit=_collider.bounds.min.x;
		RightLimit=_collider.bounds.max.x;
		BottomLimit=_collider.bounds.min.y;
		TopLimit=_collider.bounds.max.y;		
	}	
}