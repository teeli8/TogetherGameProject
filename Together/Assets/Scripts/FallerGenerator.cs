using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallerGenerator : MonoBehaviour
{

    public Faller[] fallers;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("GenerateFaller");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GenerateFaller()
    {
        int length = fallers.Length;
        int randI = Random.Range(0,length);
        yield return new WaitForSeconds(2f);
        Instantiate(fallers[randI],transform);
        StartCoroutine("GenerateFaller");
    }
}
