using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollectParticleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particlePrefab;

    [SerializeField]
    int initialCount;

    List<ParticleSystem> collectParticles = new List<ParticleSystem>();



    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
        BaseGameManager.Manager.OnPointsReceivedAtPosition.AddListener(PlaceParticle);
    }

    void InitializePool()
    {
        for (int i = 0; i < initialCount; i++)
        {
            ParticleSystem ps = Instantiate(particlePrefab, transform) as ParticleSystem;
            ps.Stop();
            collectParticles.Add(ps);
        }
    }

    ParticleSystem GetParticle()
    {
        for(int i = 0; i < collectParticles.Count; i++)
        {
            if(!collectParticles[i].isPlaying && !collectParticles[i].isPaused)
            {
                return collectParticles[i];
            }
        }

        ParticleSystem ps = Instantiate(particlePrefab, transform) as ParticleSystem;
        ps.Stop();
        collectParticles.Add(ps);

        return ps;
    }


    void PlaceParticle(Vector3 pos, Color c)
    {
        ParticleSystem ps = GetParticle();
        ps.transform.position = pos;
        ParticleSystem.MainModule mm = ps.main;
        c.a = 1;
        mm.startColor = c;
        ps.Play();
    }
}
