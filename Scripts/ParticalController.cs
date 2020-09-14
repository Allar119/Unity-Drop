using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalController : MonoBehaviour
{
    public int numOfParticles;

    [HideInInspector] public int obsticalLeght;
    [HideInInspector] public float LenghtOfSystem;

    private int particlecount;

    // Use this for initialization
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();        

        //Size of the particle system in X dimesion:
        var shape = ps.shape;
        shape.scale = new Vector3(LenghtOfSystem, 1f, 0.5f);

        //Number of elements in particle system:
        var emission = ps.emission;
        particlecount = numOfParticles * (obsticalLeght + 2);
        emission.enabled = true;
        emission.SetBurst(0, new ParticleSystem.Burst(0f, particlecount));

        Debug.Log("particle count: " + particlecount);

    }
}
	

