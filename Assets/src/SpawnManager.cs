using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour{
    [SerializeField] private GameObject[] frogsArray;

    private void OnEnable() {
        StartCoroutine(FrogSpawner());
    }

    IEnumerator FrogSpawner(){
        yield return new WaitForSeconds(1.2f);
        while(true){
            int frogToSpawn = Random.Range(0,frogsArray.Length);
            Instantiate(frogsArray[frogToSpawn], new Vector3(Random.Range(-8.34f, 8.34f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.2f);
        }
    }
}
