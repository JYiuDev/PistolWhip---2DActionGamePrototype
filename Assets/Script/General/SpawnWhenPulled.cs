using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhenPulled : MonoBehaviour
{
    [SerializeField] GameObject[] meleePositions;
    [SerializeField] GameObject[] rangePositions;
    [SerializeField] GameObject meleePrefab;
    [SerializeField] GameObject rangePrefab;
    private bool spawned = false;
    void Start()
    {

    }
    void Update()
    {
        if(!spawned && transform.parent && transform.parent.CompareTag("WeaponPos"))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        for(int i = 0; i < meleePositions.Length; i++)
        {
            Instantiate(meleePrefab, meleePositions[i].transform.position, Quaternion.identity);
        }

        for(int i = 0; i < rangePositions.Length; i++)
        {
            Instantiate(rangePrefab, rangePositions[i].transform.position, Quaternion.identity);
        }

        spawned = true;
    }
}
