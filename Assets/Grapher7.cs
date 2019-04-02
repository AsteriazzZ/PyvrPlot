﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher7 : MonoBehaviour {

    private ParticleSystem.Particle[] points = new ParticleSystem.Particle[0];

    void Start()
    {
        CreateHeatMap();
    }

    private float[,,] A;
    //int resolution = 100;
    private void CreateHeatMap()
    {

            A = run_server.arr4.Clone() as float[,,];
            
            points = new ParticleSystem.Particle[A.GetLength(0) * A.GetLength(1) * A.GetLength(2)];
            float increment0 = 1f / (A.GetLength(0) - 1);
            float increment1 = 1f / (A.GetLength(1) - 1);
            float increment2 = 1f / (A.GetLength(2) - 1);

            Vector3 increment = new Vector3(increment0, increment1, increment2);
            int i = 0;
            for (int x = 0; x < A.GetLength(0); x++)
            {
                for (int y = 0; y < A.GetLength(1); y++)
                {
                    for (int z = 0; z < A.GetLength(2); z++)
                    {
                        float v = A[x, y, z];
                        Vector3 p = Vector3.Scale(new Vector3(x, y, z), increment);
                        points[i].position = p;
                        points[i].startColor = new Color(v, 0, 1 - v);
                        points[i++].startSize = 0.05f;
                    }
                }
            }
        
        
    }
    

    void Update()
    {
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
        
    }
}
