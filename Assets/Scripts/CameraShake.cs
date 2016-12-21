using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;

    void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Spike"))
        {
            shakeAmt = coll.relativeVelocity.magnitude * .0025f;
            InvokeRepeating("Shake", 0, .01f);
            Invoke("StopShaking", 0.3f);
        }
    }

    void Shake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("Shake");
        mainCamera.transform.position = originalCameraPosition;
    }

}
