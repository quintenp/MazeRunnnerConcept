using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController player;
    private Vector3 lastPlayerPosition;
    private float distanceToMove;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        distanceToMove = player.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = player.transform.position;
    }
}
