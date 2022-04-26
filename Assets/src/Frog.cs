using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour{
    public int scoreGiven;
    
    private float frogMoveSpeed = 5.0f;
    private UIManager uiManagerHandler; 
    private Player playerHandler;
    SpriteRenderer frogSpriteRenderer;

    void MoveFrogDown(){
        transform.Translate(Vector3.down * frogMoveSpeed * Time.deltaTime);
    }

    void DeleteFrogIfOffScreen(){
        if(transform.position.y <= -6.0f){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        scoreGiven = 10;
        uiManagerHandler = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerHandler = GameObject.Find("Player").GetComponent<Player>();

        uiManagerHandler.UpdateScore(playerHandler.playerScore);
    }

    // Update is called once per frame
    void Update(){
        MoveFrogDown();
        DeleteFrogIfOffScreen();
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Spell"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            playerHandler.playerScore += scoreGiven;
            playerHandler.frogsFreezed++;
            uiManagerHandler.UpdateScore(playerHandler.playerScore);
        }

        else if(other.tag == "Player"){
            Destroy(this.gameObject);

            if(playerHandler != null){
                playerHandler.DamagePlayer();
            }
        }
    }
}
