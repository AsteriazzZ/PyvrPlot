using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher3 : MonoBehaviour {

    [Range(100, 100)]
    public int resolution = 10;
    private int currentResolution;
    private ParticleSystem.Particle[] points;

    void Start()
    {
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
    }

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple,
        UserDefined
    }

    public FunctionOption function;
    private delegate float FunctionDelegate(Vector3 p, float t, float[] A, int i);
    private static FunctionDelegate[] functionDelegates = {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple,
        UserDefined
    };

    // Update is called once per frame
    void Update()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }
        FunctionDelegate f = functionDelegates[(int)function];
        float t = Time.timeSinceLevelLoad;

        float[] A = new float[10000];
        if (run_server.mode == 1)
        {

            //A = run_server.arr;
            print(A);
        }
        else
        {
            for (int i = 0; i < 10000; i++) A[i] = 0;
        }
        
        for (int i = 0; i < points.Length; i++)
        {
            //Debug.Log(points.Length);
            Vector3 p = points[i].position;
            p.y = f(p, t, A, i);
            points[i].position = p;
            Color c = points[i].startColor;
            c.g = p.y;
            points[i].startColor = c;
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private static float Linear(Vector3 p, float t, float[] A, int i)
    {
        return p.x;
    }

    private static float Exponential(Vector3 p, float t, float[] A, int i)
    {
        return p.x * p.x;
    }

    /*
    private static float Parabola(Vector3 p, float t)
    {
        p.x = 2f * p.x - 1f;
        return p.x * p.x;
    }

    private static float Sine(Vector3 p, float t)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * p.x + t);
    }
    */

    private static float Parabola(Vector3 p, float t, float[] A, int i)
    {
        p.x += p.x - 1f;
        p.z += p.z - 1f;
        return 1f - p.x * p.x * p.z * p.z;
    }

    private static float Sine(Vector3 p, float t, float[] A, int i)
    {
        return 0.50f +
            0.25f * Mathf.Sin(4f * Mathf.PI * p.x + 4f * t) * Mathf.Sin(2f * Mathf.PI * p.z + t) +
            0.10f * Mathf.Cos(3f * Mathf.PI * p.x + 5f * t) * Mathf.Cos(5f * Mathf.PI * p.z + 3f * t) +
            0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
    }

    private static float Ripple(Vector3 p, float t, float[] A, int i)
    {
        p.x -= 0.5f;
        p.z -= 0.5f;
        float squareRadius = p.x * p.x + p.z * p.z;
        return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
    }

    private static float UserDefined(Vector3 p, float t, float[] A, int i)
    {
        return A[i];
    }
}
