using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public int lives = 3;
    public int frogsFreezed = 0;
    public int playerScore;
    public float fireRate = 0.5f;

    [SerializeField] private AudioClip shootEffect;
    [SerializeField] private AudioClip damageEffect;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private GameObject spellPrefab;
    
    private bool playerGotHit;
    private bool playerInputEnabled;
    private float playerMovSpeed = 14.0f;
    private UIManager uiManagerHandler;
    private Game_Manager gameManagerHandler;

    private float spellShootCooldown = 0.0f;
    
    void MovePlayer(){
        MovePlayerHorizontally();
        MovePlayerVertically();
    }

    void GetPlayerActions(){
        ShootSpell();
        TurnOnSlowDown();
    }

    void MovePlayerHorizontally(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(playerInputEnabled){
            transform.Translate(Vector3.right * playerMovSpeed * horizontalInput * Time.deltaTime);
        }
    }

    void MovePlayerVertically(){
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(playerInputEnabled){
            transform.Translate(Vector3.up * playerMovSpeed * verticalInput * Time.deltaTime);
        }
    }

    void LimitPlayerMovementPositiveX(){
        if(transform.position.x > 8.37f){
            transform.position = new Vector3(8.37f, transform.position.y, transform.position.z);
        }
    }

    void LimitPlayerMovementNegativeX(){
        if(transform.position.x < -8.37f){
            transform.position = new Vector3(-8.37f, transform.position.y, transform.position.z);
        }
    }

    void LimitPlayerMovementPositiveY(){
        if(transform.position.y > 4.4f){
            transform.position = new Vector3(transform.position.x, 4.4f, transform.position.z);
        }
    }

    void LimitPlayerMovementNegativeY(){
        if(transform.position.y < -4.4f){
            transform.position = new Vector3(transform.position.x, -4.4f, transform.position.z);
        }
    }

    void LimitPlayerMovement(){
        LimitPlayerMovementPositiveX();
        LimitPlayerMovementNegativeX();
        LimitPlayerMovementPositiveY();
        LimitPlayerMovementNegativeY();
    }

    void ApplySpellCooldown(){
        if(Time.time > spellShootCooldown){
            Instantiate(spellPrefab, transform.position, Quaternion.identity);
            sfxSource.PlayOneShot(shootEffect);
            spellShootCooldown = Time.time + fireRate;
        }
    }

    void ShootSpell(){
        if(playerInputEnabled && Input.GetKey(KeyCode.Z)){
            ApplySpellCooldown();
        }
    }

    void TurnOnSlowDown(){
        GameObject hitboxGO = gameObject.transform.Find("Hitbox").gameObject;
        if(playerInputEnabled && Input.GetKey(KeyCode.LeftShift)){
            hitboxGO.SetActive(true);
            playerMovSpeed = 5.0f;
        } else{
            hitboxGO.SetActive(false);
            playerMovSpeed = 20.0f;
        }
    }

    void MovePlayerWhileInvunerable(){
        if(playerGotHit){
            Vector3 targetPosition = new Vector3(0, -3.75f, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (14.0f * Time.deltaTime));
        }
    }

    IEnumerator ChangePlayerPropertiesOnHit(){
        SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
        CircleCollider2D playerHitbox = GetComponent<CircleCollider2D>();

        playerHitbox.enabled = false;
        playerInputEnabled = false;
        playerGotHit = true;
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, .3f);
        yield return new WaitForSeconds(1.5f);

        playerSpriteRenderer.color = new Color(1f, 1f, 1f, .7f);
        playerInputEnabled = true;
        playerGotHit = false;

        yield return new WaitForSeconds(1.5f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        playerHitbox.enabled = true;
    }

    public void DamagePlayer(){
        lives--;
        uiManagerHandler.UpdateLives(lives);
        gameManagerHandler.SFXSource.PlayOneShot(damageEffect);

        if(lives == 0){
            SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
            CircleCollider2D playerHitbox = GetComponent<CircleCollider2D>();

            playerInputEnabled = false;
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            playerHitbox.enabled = false;
            return;
        }

        StartCoroutine(ChangePlayerPropertiesOnHit());
    }

    // Start is called before the first frame update
    void Start(){
        playerGotHit = false;
        playerScore = 0;
        playerInputEnabled = true;
        transform.position = new Vector3(0, -3.75f, 0);

        gameManagerHandler = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        uiManagerHandler = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(uiManagerHandler != null){
            uiManagerHandler.UpdateLives(lives);
        }
    }

    // Update is called once per frame
    void Update(){
        MovePlayer();
        GetPlayerActions();
        LimitPlayerMovement();
        MovePlayerWhileInvunerable();
    }
}
