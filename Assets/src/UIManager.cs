using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
    public GameObject suwakoHPBarGO;
    
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private GameObject suwakoHPBarBackgroundGO;

    void Start(){
        suwakoHPBarBackgroundGO = gameObject.transform.Find("SuwakoHealthBar").gameObject;
        suwakoHPBarGO = gameObject.transform.Find("SuwakoHealthBarBackground").gameObject;
    }

    public void UpdateLives(int currentLives){
        livesText.text = "x" + currentLives;
    }

    public void UpdateScore(int playerScore){
        scoreText.text = "Score: " + playerScore;
    }

    public void SetHealthBarActive(){
        suwakoHPBarBackgroundGO.SetActive(true);
        suwakoHPBarGO.SetActive(true);
    }

    public void SetHealthBarInactive(){
        suwakoHPBarGO.SetActive(false);
        suwakoHPBarBackgroundGO.SetActive(false);
    }
}
