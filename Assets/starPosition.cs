using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPosition : MonoBehaviour
{
    // Start is called before the first frame update
    float x;
    float y;
    float z;
    Vector3 pos;
    [SerializeField] GameObject parent;
  
    void Start()
    {
        int size = parent.transform.childCount;
        for(int i = 0; i < size; i++)
        {
            x = Random.Range(-60, 60);
            y = Random.Range(2, 35);
            z = transform.position.z;
            pos = new Vector3(x, y, z);
            parent.transform.GetChild(i).gameObject.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
