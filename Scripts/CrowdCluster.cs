using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCluster : MonoBehaviour
{
    // Instantiate a bunch of npcs

    // variable to control how many npcs should be generated
    [SerializeField] private int _crowd;

    // List of game objects to hold the four different agents
    [SerializeField] List<GameObject> _agents;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _crowd; i++)
        {
            Instantiate(_agents[Random.Range(1, 4)], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
