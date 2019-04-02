using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCPPP;

public class info{
	public string fname;
	public int time_dim;
	public bool have_time;
	public string c_type;
	public int x_dim;
	public int y_dim;
	public int z_dim;
}

public class test_server : MonoBehaviour {

	// Use this for initialization
	private tcpp my_server;

	void Start () {
		my_server = new tcpp();
		my_server.start();
	}
	
	// Update is called once per frame
	void Update () {
		string message = my_server.try_connect();
		if(message != ""){
			var my_info = JsonUtility.FromJson<info>(message) as info;
			// Debug.Log(my_info.time_dim);
			float[] temp = loader.load_bin(my_info.fname);
			if(my_info.c_type == "GRID"){
				Debug.Log("Grid");
				int reso = temp.Length / my_info.time_dim;
				float[,] arr1 = new float[my_info.time_dim, reso];
				for(int i = 0; i < (my_info.time_dim); i++){
					for(int j = 0; j < reso; j++){
						arr1[i, j] = temp[i * reso + j];
					}
				}
				int x_dim = my_info.x_dim;
				int y_dim = my_info.y_dim;
			}
			else if(my_info.c_type == "SCATTER"){
				Debug.Log("Scatter Plot");
				float[,] arr2 = new float[my_info.y_dim, 3];
				for(int i = 0; i < my_info.y_dim; i++){
					arr2[i, 0] = temp[i*3];
					arr2[i, 1] = temp[i*3+1];
					arr2[i, 2] = temp[i*3+2];
				}
			}
			else if(my_info.c_type == "HEATMAP"){
				Debug.Log("Heat Map");
				float[,,] arr3 = new float[my_info.x_dim, my_info.y_dim, my_info.z_dim];
				for(int i = 0; i < my_info.x_dim; i++){
					for(int j = 0; j < my_info.y_dim; j++){
						for(int k = 0; k < my_info.z_dim; k++){
							arr3[i,j,k] = temp[i * my_info.y_dim * my_info.z_dim + j * my_info.z_dim + k];
						}
					}
				}
				Debug.Log("Add time dimension later");
			}
			else{
				Debug.Log("TypeError: " + my_info.c_type);
			}
		}
	}
}