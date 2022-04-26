using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour{
    [SerializeField] Slider loadSlider;
    [SerializeField] GameObject loadingScreen;
    
    public void LoadLevel(string sceneName){
        StartCoroutine(loadAsync(sceneName));
    }

    IEnumerator loadAsync(string sceneName){
        yield return new WaitForSeconds(3f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
