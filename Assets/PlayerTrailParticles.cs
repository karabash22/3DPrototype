using UnityEngine;

public class PlayerTrailParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float minMoveSpeed = 0.1f;
    
    private Rigidbody playerRigidbody;
    private ParticleSystem.EmissionModule emissionModule;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (particleSystem != null)
        {
            emissionModule = particleSystem.emission;
            emissionModule.enabled = false;
        }
    }

    private void Update()
    {
        if (playerRigidbody != null && particleSystem != null)
        {
            float speed = playerRigidbody.velocity.magnitude;
            
            if (speed > minMoveSpeed)
            {
                if (!emissionModule.enabled)
                {
                    emissionModule.enabled = true;
                    particleSystem.Play();
                }
            }
            else
            {
                if (emissionModule.enabled)
                {
                    emissionModule.enabled = false;
                }
            }
        }
    }
}