using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] vehiclePrefabs;
    [SerializeField] GameObject[] vehicles;
    [SerializeField] GameObject[] propPrefabs;

    [SerializeField] float spawnRate;
    private float propZMax = 165;
    private float propZMin = -25;
    private float propXrange = 15;
    [SerializeField] int propNumber = 8;

    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
        StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawns a propnumber of random props spread evenly on the road
    private void SpawnProps(){
        float propDistanceZ = (propZMax-propZMin)/propNumber;
        for (int i = 0; i<propNumber; i++){
            float spawnZ = propZMin+(i*propDistanceZ);
            float spawnX = Random.Range(-propXrange, propXrange);
            GameObject prop = propPrefabs[Random.Range(0, propPrefabs.Length)];
            Instantiate(prop, new Vector3(spawnX, 0.2f, spawnZ), prop.transform.rotation);
        }
    }

    //Spawns the traffic. Currently only on one side and uses instantiate, but instead should be activation
    IEnumerator SpawnCars(){
        float laneOneX = 15;
        float laneTwoX = 5;
        while (true){
            yield return new WaitForSeconds(spawnRate);
            GameObject vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];
            Instantiate(vehicle, new Vector3(laneTwoX, 0.2f, -25), vehicle.transform.rotation);
            vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];
            Instantiate(vehicle, new Vector3(laneOneX, 0.2f, -25), vehicle.transform.rotation);
        }
        


    }
}
