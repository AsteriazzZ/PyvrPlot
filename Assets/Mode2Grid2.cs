﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode2Grid2 : MonoBehaviour {

    public int resolution = 100;
    private int currentResolution;
    private ParticleSystem.Particle[] points;
    private float[,] A;

    public float fps = 60f;
    private float currentfps;

    // Use this for initialization
    void Start () {
        CreatePoints();
    }

    private void CreatePoints()
    {
        if (resolution < 10 || resolution > 100)
        {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
            resolution = 10;
        }
        currentResolution = resolution;
        currentfps = fps;
        points = new ParticleSystem.Particle[resolution * resolution];
        float increment = 1f / (resolution - 1);
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                Vector3 p = new Vector3(x * increment, 0f, z * increment);
                points[i].position = p;
                points[i].startColor = new Color(p.x, 0f, p.z);
                points[i++].startSize = 0.01f;
            }
        }

        A = run_server.arr2.Clone() as float[,];
    }

    // Update is called once per frame
    void Update () {
        float t = Time.timeSinceLevelLoad;

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;
            currentfps = fps;
            p.y = A[((int)(t * currentfps)) % A.GetLength(0), i];
            points[i].position = p;
            Color c = points[i].startColor;
            c.g = p.y;
            points[i].startColor = c;
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }
}
