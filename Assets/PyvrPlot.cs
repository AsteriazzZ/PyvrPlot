using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyvrPlot : MonoBehaviour {

    public GameObject baseGrid2;
    public GameObject baseScatterplot;
    public GameObject baseHeatmap;

    // Use this for initialization
    void Start () {
        
	}


    int last_graph_inx = 0;
    private Vector3 getNextGraphPosition()
    {
        Vector3[] positions = new Vector3[6];
        positions[0] = new Vector3(7.78f, 5.5f, 3.89f);
        positions[1] = new Vector3(5.89f, 5.5f, -4.8f);
        positions[2] = new Vector3(-1.16f, 5.5f, -11.01f);
        positions[3] = new Vector3(-9.18f, 5.5f, -6.63f);
        positions[4] = new Vector3(-11.13f, 5.5f, 3.18f);
        positions[5] = new Vector3(-1.03f, 5.5f, 10.77f);

        return positions[(last_graph_inx++) % 6];
    }

    int last_mode = -1;
    // Update is called once per frame
    void Update() {
        

        

        if (run_server.mode != -1)
        {
            //GameObject new_graph = Object.Instantiate(graph0, getNextGraphPosition(), this.gameObject.transform.rotation, this.gameObject.transform);

            if (run_server.mode == 2)
            {
                Object.Instantiate(baseGrid2, getNextGraphPosition(), baseGrid2.transform.rotation, this.transform);
            }

            if (run_server.mode == 3)
            {
                Object.Instantiate(baseScatterplot, getNextGraphPosition(), baseScatterplot.transform.rotation, this.transform);
                //CreateScatterplotPoints(new_graph);
            }

            if (run_server.mode == 4)
            {
                Object.Instantiate(baseHeatmap, getNextGraphPosition(), baseHeatmap.transform.rotation, this.transform);
            }

            run_server.mode = -1;
            //GetComponent<ParticleSystem>().SetParticles(points, points.Length);
        }

        
    }
}
