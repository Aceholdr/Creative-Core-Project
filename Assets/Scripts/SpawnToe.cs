using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToe : MonoBehaviour
{
    public float nailLength;
    public GameObject nail;
    public GameObject toe;

    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        nailLength = Random.Range(1.0f, 4.0f);
        float randomX = Random.Range(-5.0f, 5.0f);
        float randomZ = Random.Range(-5.0f, 5.0f);

        nail.transform.localScale = new Vector3(1, 1, nailLength);

        spawnPos = new Vector3(randomX, 1, randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(nail, spawnPos, nail.transform.rotation);
            Instantiate(toe, spawnPos, nail.transform.rotation);
        }
    }
}
