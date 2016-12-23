using System.Collections;
using UnityEngine;

public class ParticleDisable : MonoBehaviour
{

    private ParticleSystem attachedParticleSystem;

    void Start()
    {
        attachedParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (attachedParticleSystem.isPlaying)
        {
            StartCoroutine(DisableParticle());
        }
    }

    IEnumerator DisableParticle()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
