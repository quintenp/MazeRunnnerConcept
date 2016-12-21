using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public bool FollowsPlayer { get; set; }

    [Space(10)]
    [Header("Distances")]
    public float HorizontalLookDistance = 3;
    public float VerticalLookDistance = 3;
    public float LookAheadTrigger = 0.1f;

    [Space(10)]
    [Header("Movement Speed")]
    public float ResetSpeed = 0.5f;
    public float CameraSpeed = 0.3f;

    [Space(10)]
    [Header("Camera Zoom")]
    [Range(1, 20)]
    public float MinimumZoom = 5f;
    [Range(1, 20)]
    public float MaximumZoom = 10f;
    public float ZoomSpeed = 0.4f;

    protected Transform _target;
    private PlayerController _targetController;
    protected LevelLimits _levelBounds;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    protected float offsetZ;
    protected Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;

    private float shakeIntensity;
    private float shakeDecay;
    private float shakeDuration;

    protected float _currentZoom;
    protected Camera _camera;

    private Vector3 _lookDirectionModifier = new Vector3(0, 0, 0);

    protected virtual void Start()
    {
        _camera = GetComponent<Camera>();

        FollowsPlayer = true;
        _currentZoom = MinimumZoom;
        _target = FindObjectOfType<PlayerController>().transform;

        _targetController = _target.GetComponent<PlayerController>();
        _levelBounds = GameObject.FindGameObjectWithTag("LevelBounds").GetComponent<LevelLimits>();

        lastTargetPosition = _target.position;
        offsetZ = (transform.position - _target.position).z;
        transform.parent = null;

        //_lookDirectionModifier=new Vector3(0,0,0);

        Zoom();
    }

    void LateUpdate()
    {
        if (!FollowsPlayer)
            return;

        Zoom();

        float xMoveDelta = (_target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > LookAheadTrigger;

        if (updateLookAheadTarget)
        {
            lookAheadPos = HorizontalLookDistance * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * ResetSpeed);
        }

        Vector3 aheadTargetPos = _target.position + lookAheadPos + Vector3.forward * offsetZ + _lookDirectionModifier;
        Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, CameraSpeed);
        Vector3 shakeFactorPosition = new Vector3(0, 0, 0);

        if (shakeDuration > 0)
        {
            shakeFactorPosition = Random.insideUnitSphere * shakeIntensity * shakeDuration;
            shakeDuration -= shakeDecay * Time.deltaTime;
        }

        newCameraPosition = newCameraPosition + shakeFactorPosition;

        float posX = Mathf.Clamp(newCameraPosition.x, xMin, xMax);
        float posY = Mathf.Clamp(newCameraPosition.y, yMin, yMax);
        float posZ = newCameraPosition.z;

        transform.position = new Vector3(posX, posY, posZ);

        lastTargetPosition = _target.position;
    }

    protected void Zoom()
    {
        float characterSpeed = Mathf.Abs(_targetController.MovementSpeed);
        float currentVelocity = 0f;

        _currentZoom = Mathf.SmoothDamp(_currentZoom, (characterSpeed / 10) * (MaximumZoom - MinimumZoom) + MinimumZoom, ref currentVelocity, ZoomSpeed);

        _camera.orthographicSize = _currentZoom;
        GetLevelBounds();
    }

    private void GetLevelBounds()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        xMin = _levelBounds.LeftLimit + (cameraWidth / 2);
        xMax = _levelBounds.RightLimit - (cameraWidth / 2);
        yMin = _levelBounds.BottomLimit + (cameraHeight / 2);
        yMax = _levelBounds.TopLimit - (cameraHeight / 2);
    }

    public void Shake(Vector3 shakeParameters)
    {
        shakeIntensity = shakeParameters.x;
        shakeDuration = shakeParameters.y;
        shakeDecay = shakeParameters.z;
    }

    public void LookUp()
    {
        _lookDirectionModifier = new Vector3(0, VerticalLookDistance, 0);
    }

    public void LookDown()
    {
        _lookDirectionModifier = new Vector3(0, -VerticalLookDistance, 0);
    }

    public void ResetLookUpDown()
    {
        _lookDirectionModifier = new Vector3(0, 0, 0);
    }
}
