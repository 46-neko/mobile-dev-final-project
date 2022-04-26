using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour{
    [SerializeField] private InGameDataManager inGameDataMangerHandler;
    [SerializeField] private Game_Manager game_ManagerHandler;
    [SerializeField] private Text finalScoreNumberText;
    [SerializeField] private Text highScoreNumberText;

    void OnEnable(){
        finalScoreNumberText.text = game_ManagerHandler.playerFinalScore.ToString();
        inGameDataMangerHandler.Load();
        highScoreNumberText.text = inGameDataMangerHandler.playerData.playerScore.ToString();

        if(game_ManagerHandler.playerFinalScore > inGameDataMangerHandler.playerData.playerScore){
            inGameDataMangerHandler.Save(game_ManagerHandler.playerFinalScore);
        }
    }
}
