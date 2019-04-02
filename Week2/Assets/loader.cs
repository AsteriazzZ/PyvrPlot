using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class loader
    {

        // Use this for initialization
        public static float[] load_bin(string path)
        {
            byte[] array = File.ReadAllBytes(path);
            float[] narray = new float[array.Length / 4];
            for (int i = 0; i < narray.Length; i++)
            {
                narray[i] = System.BitConverter.ToSingle(array, i * 4);
            }
            //Debug.Log(narray);
            return narray;
        }
    }
