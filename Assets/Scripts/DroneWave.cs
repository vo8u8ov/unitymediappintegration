using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWave : MonoBehaviour
{
    public GameObject cubePrefab;
    public int countX = 10;
    public int countZ = 10;
    public float spacing = 2f;
    public float waveHeight = 1f;
    public float waveSpeed = 1f;

    private Transform[,] cubes;

    void Start()
    {
        cubes = new Transform[countX, countZ];
        float offsetX = (countX - 1) * spacing / 2f;
        float offsetZ = (countZ - 1) * spacing / 2f;

        for (int x = 0; x < countX; x++)
        {
            for (int z = 0; z < countZ; z++)
            {
                var pos = new Vector3(x * spacing - offsetX, 0, z * spacing - offsetZ);
                var cube = Instantiate(cubePrefab, pos, Quaternion.identity, transform);
                cubes[x, z] = cube.transform;
            }
        }
    }

    void Update()
    {
        float time = Time.time * waveSpeed;
        float offsetX = (countX - 1) * spacing / 2f;
        float offsetZ = (countZ - 1) * spacing / 2f;

        for (int x = 0; x < countX; x++)
        {
            for (int z = 0; z < countZ; z++)
            {
                float y = Mathf.Sin(time + (x + z) * 0.3f) * waveHeight;
                var targetPos = new Vector3(x * spacing - offsetX, y, z * spacing - offsetZ);
                cubes[x, z].localPosition = Vector3.Lerp(cubes[x, z].localPosition, targetPos, Time.deltaTime * 5f);
            }
        }
    }
}
