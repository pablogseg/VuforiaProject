using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {

    public GameObject prefab;
    public float tilingRight;
    public float tilingUp;
    public int rows;
    public int cols;
    private float xPos;
    private float yPos;


    [ContextMenu("Generate wall")]
    public void GenerateWall()
    {
        foreach(Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        xPos = 0;
        yPos = 0;
        for(int i=0;i<rows;i++)
        {
            for(int j=0;j<cols;j++)
            {
                Instantiate(prefab, transform);
                if(i%2!=0)
                {
                    prefab.transform.position = new Vector3(xPos+(tilingRight/2), yPos, 0);
                }
                else
                {
                    prefab.transform.position = new Vector3(xPos, yPos, 0);
                }
                xPos += tilingRight;
            }
            xPos = 0;
            yPos += tilingUp;
        }
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
