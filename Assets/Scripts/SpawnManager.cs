using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    MainManager mainManager;

    [SerializeField] GameObject[] vehiclePrefabs;
    [SerializeField] GameObject[] vehicles;
    float[] laneXs = { 15, 5, -5, -15 };
    [SerializeField] GameObject[] propPrefabs;

    float spawnRate;
    [SerializeField] float propZMax = 165;
    [SerializeField] float propZMin = -25;
    [SerializeField] float propXrange = 15;
    int propNumber;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
        spawnRate = mainManager.spawnRateSetting;
        propNumber = (int) ((mainManager.propNumberSetting/5) * 15);

        SpawnProps();
        if (spawnRate > 0)
        {
            StartCoroutine(SpawnCars());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawns a propnumber of random props spread evenly on the road
    private void SpawnProps()
    {
        float propDistanceZ = (propZMax - propZMin) / (propNumber + 1);
        for (int i = 1; i <= propNumber; i++)
        {
            float spawnZ = propZMin + (i * propDistanceZ);
            float spawnX = Random.Range(-propXrange, propXrange);
            GameObject prop = propPrefabs[Random.Range(0, propPrefabs.Length)];
            Instantiate(prop, new Vector3(spawnX, 0.2f, spawnZ), prop.transform.rotation);
        }
    }

    //Spawns the traffic
    IEnumerator SpawnCars()
    {
        while (!mainManager.isGamePaused)
        {
            GameObject vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];
            Instantiate(vehicle, new Vector3(laneXs[Random.Range(0, 2)], 0.2f, propZMin-10), Quaternion.identity);
            vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];
            Instantiate(vehicle, new Vector3(laneXs[Random.Range(2, 4)], 0.2f, propZMax+10), new Quaternion(0, 180, 0, 0));
            yield return new WaitForSeconds(20 / spawnRate);
        }



    }
}
