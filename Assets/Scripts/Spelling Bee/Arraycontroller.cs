using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arraycontroller : MonoBehaviour
{

    [SerializeField] private GameObject[] letterTiles;
    

    // Start is called before the first frame update
    void Start()
    {
        
        for(var i = 0; i < letterTiles.Length; i++)
        {
            Instantiate(letterTiles[i], new Vector2(1, 0), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
