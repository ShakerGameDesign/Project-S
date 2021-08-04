using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSpawner : MonoBehaviour {
    public GameObject playerObj;
    public GameObject mountainPrefab;

    //see the README for more information.
    [Header("Mountain Settings")]
    [Range(0f, 50.0f)] public float zAxisOffset = 17.5f;
    [Range(0f, 50.0f)] public float spawningDistance = 30f;
    [Range(-50.0f, 0f)] public float whereToStartGeneration = -17.5f;
    [Range(10, 0)] public int preGeneratedMountains = 10;
    [Range(1, 50)] public int mountainLimit = 20;

    private Renderer mountainRenderer;
    private List<GameObject> activeMountains;
    private float prefabWidth;

    void Start() {
        activeMountains = new List<GameObject>(); //initialize list (won't work without this line)
        gameObject.transform.position = playerObj.transform.position + new Vector3(whereToStartGeneration, 0, zAxisOffset); //set spawner to predefined locations
        for (int i = 0; i < preGeneratedMountains; i++) SpawnMountain(); //spawn the pregenerated mountains
    }

    void Update() {
        //if the distance from the player to the spawner is less than spawningDistance, spawn a mountain
        float xDifference = gameObject.transform.position.x - playerObj.transform.position.x;
        if (xDifference < spawningDistance) SpawnMountain();
    }

    void SpawnMountain() {
        GameObject mountainInstance = (GameObject)Instantiate(mountainPrefab, gameObject.transform.position, Quaternion.identity); //have to cast type, idk why
        activeMountains.Add(mountainInstance);
        
        //get the width of the prefab, which is used to dynamically generate the mountains so that they're always the right distance apart
        prefabWidth = mountainPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x;
        gameObject.transform.position += new Vector3(prefabWidth, 0, 0);
        
        //if the mountain count surpasses mountainLimit, delete the first mountain.
        if (activeMountains.Count > mountainLimit) {
            Destroy(activeMountains[0]);
            activeMountains.RemoveAt(0);
        }
    }
}
