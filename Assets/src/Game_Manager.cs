using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour{
    public AudioSource SFXSource;
    public Player playerHandler;
    public int playerFinalScore;

    [SerializeField] private GameObject suwakoPrefab;
    [SerializeField] private AudioClip suwakoDeathSFX;
    [SerializeField] private GameObject gameOverScreenGO;

    private MusicManager musicManagerHandler;
    private GameObject spawnManagerGO;
    private GameObject suwakoGO;
    private Suwako suwakoHandler;
    private UIManager uiManagerHandler;

    bool PlayerFrozeTenFrogs(){
        if (playerHandler.frogsFreezed == 10){
            SuwakoEvent();
            return true;
        }
        return false;
    }

    void GivePlayerExtraLife(){
        if(playerHandler.lives <= 4){
            playerHandler.lives++;
            uiManagerHandler.UpdateLives(playerHandler.lives);
        }
    }

    IEnumerator CheckIfPlayerKilledSuwako(){
        if(suwakoHandler){
            if(suwakoHandler.isSuwakoDead){
                Destroy(suwakoGO);
                playerHandler.frogsFreezed = 0;
                playerHandler.fireRate = 0.5f;
                playerHandler.playerScore += 300;

                SFXSource.PlayOneShot(suwakoDeathSFX);
                GivePlayerExtraLife();
                uiManagerHandler.SetHealthBarInactive();

                uiManagerHandler.UpdateScore(playerHandler.playerScore);
                musicManagerHandler.ChangeBGMNormal();

                yield return new WaitForSeconds(1.3f);
                spawnManagerGO.SetActive(true);
            }
        }
    }

    void SuwakoEvent(){
        spawnManagerGO.SetActive(false);
        playerHandler.frogsFreezed = 0;
        playerHandler.fireRate = 0.05f;
        Instantiate(suwakoPrefab, new Vector3(0f, 8f, 0f), Quaternion.identity);
        suwakoGO = GameObject.Find("Suwako(Clone)");
        suwakoHandler = suwakoGO.GetComponent<Suwako>();
        uiManagerHandler.SetHealthBarActive();
        musicManagerHandler.ChangeBGMSuwako();
    }

    void CheckIfPlayerIsDead(){
        if(playerHandler.lives <= 0){
            playerFinalScore = playerHandler.playerScore;
            gameOverScreenGO.SetActive(true);
        }
    }

    void Start(){
        musicManagerHandler = GameObject.Find("Main Camera").GetComponent<MusicManager>();
        uiManagerHandler = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerHandler = GameObject.Find("Player").GetComponent<Player>();
        spawnManagerGO = GameObject.Find("SpawnManager");
    }

    void Update(){
        CheckIfPlayerIsDead();
        PlayerFrozeTenFrogs();
        StartCoroutine(CheckIfPlayerKilledSuwako());
    }
}
