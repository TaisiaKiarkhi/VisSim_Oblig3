using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using UnityEngine.UI;

public class Triangles_Script : MonoBehaviour
{
    // Start is called before the first frame update
    float[] x_y_z = new float[3];
    public List<float[]> index = new List<float[]>();
    public List<float> floats = new List<float>();

    public GameObject sphere;
    int skip_lines = 2000;
    private void Awake()
    {
        //read_files("/Koordinater_xyz.txt");
        read_files("/Coordinates.txt"); 
        create_cloud();
   

    }
    void Start()
    {
        

    }
    

    // Update is called once per frame
    void Update()
    {
        

    }

    void create_cloud()
    {

        for (int i = 0; i < floats.Count; i++)
        {
           
            GameObject point = Instantiate(sphere, new Vector3(floats[i], floats[i + 2], floats[i + 1]), Quaternion.identity);
            point.transform.parent = transform;

         //  point.transform.localPosition = new Vector3(floats[i], floats[i + 1], floats[i + 2]);
         // 
         //  point.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
         //  point.transform.localRotation = Quaternion.identity;

            

           
            i= i + 2;
        }
    }

    
    void read_files(string file_path)
    {
        try
        {
            string File_Path = Application.dataPath + file_path;

            using (StreamReader reader = new StreamReader(File_Path))
            {
                int line_counter = 0;
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    line_counter++;
                    if (line_counter % skip_lines == 0)
                    {
                        string[] floats_s = line.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                        foreach (string float_str in floats_s)
                        {
                            if (float.TryParse(float_str, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out float parse_f))
                            {
                                floats.Add(parse_f);
                            }
                            else
                            {
                                Debug.Log("Failed to parse");
                            }
                        }
                        foreach (float parsed_float in floats)
                        {
                            Debug.Log(parsed_float + " parsed");
                        }


                    }
                }
            }
        }
        catch (IOException a)
        {
            Debug.Log("Failed to read" + a.Message);
        }
    }


}
