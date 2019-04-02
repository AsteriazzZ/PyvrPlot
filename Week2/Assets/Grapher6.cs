﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher6 : MonoBehaviour {

    private ParticleSystem.Particle[] points = new ParticleSystem.Particle[0];
    void Start()
    {
        CreateScatterplotPoints();
    }
    
    private void CreateScatterplotPoints()
    {
        
        if (run_server.mode == 2)
        {
            float[,] A = run_server.arr2;
            points = new ParticleSystem.Particle[A.GetLength(0)];
            print(A.GetLength(0));
            for (int i = 0; i < A.GetLength(0); i++)
            {
                points[i].position = new Vector3(A[i, 0], A[i, 1], A[i, 2]);
                points[i].startColor = new Color(A[i, 0], A[i, 1], A[i, 2]);
                points[i++].startSize = 0.1f;
            }
        }
        
    }

    //private int last_mode = 0;
    void Update()
    {
        CreateScatterplotPoints();
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }
}
