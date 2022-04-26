using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suwako : MonoBehaviour{
    public bool isSuwakoDead;
    public float suwakoHP = 15000f;
    public float suwakoCurrentHP;
    public int suwakoAttackPhase = 0;

    [SerializeField] private BulletHellSpawner bulletHellSpawnerHandler;
    [SerializeField] private GameObject bulletHellSpawnerPrefab;
    
    private bool secondPhaseBlocked;
    private bool thirdPhaseBlocked;
    private bool lastPhaseBlocked;
    private bool isInDefaultPosition;
    private GameObject bulletHellSpawnerGO;
    BoxCollider2D suwakoHitbox;

    void MoveTowardsDefaultPositionIfNotThere(){
        if(!isInDefaultPosition){
            Vector3 targetPosition = new Vector3(0, 3.1f, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (3.0f * Time.deltaTime));
        }
    }

    IEnumerator StartFirstBulletHell(){
        suwakoHitbox.enabled = false;

        yield return new WaitForSeconds(3f);

        bulletHellSpawnerGO = Instantiate(bulletHellSpawnerPrefab, transform.position, Quaternion.identity);
        bulletHellSpawnerGO.transform.parent = gameObject.transform;
        suwakoHitbox.enabled = true;
    }

    IEnumerator EnableAndDisablePatterns(){
        Destroy(bulletHellSpawnerGO);
        suwakoHitbox.enabled = false;

        yield return new WaitForSeconds(2f);
        
        bulletHellSpawnerGO = Instantiate(bulletHellSpawnerPrefab, transform.position, Quaternion.identity);
        bulletHellSpawnerGO.transform.parent = gameObject.transform;
        suwakoHitbox.enabled = true;
    }

    void Start(){
        suwakoHitbox = GetComponent<BoxCollider2D>();
        isInDefaultPosition = false;
        isSuwakoDead = false;
        secondPhaseBlocked = false;
        thirdPhaseBlocked = false;
        lastPhaseBlocked = false;
        suwakoCurrentHP = suwakoHP;
        StartCoroutine(StartFirstBulletHell());
    }

    void CheckIfSecondPhaseReady(){
        if(SecondPhaseReady() && !secondPhaseBlocked){
            suwakoAttackPhase = 1;
            StartCoroutine(EnableAndDisablePatterns());
            secondPhaseBlocked = true;
        }
    }

    void CheckIfThirdPhaseReady(){
        if(ThirdPhaseReady() && !thirdPhaseBlocked){
            suwakoAttackPhase = 2;
            StartCoroutine(EnableAndDisablePatterns());
            thirdPhaseBlocked = true;
        }
    }

    void CheckIfLastPhaseReady(){
        if(LastPhaseReady() && !lastPhaseBlocked){
            suwakoAttackPhase = 3;
            StartCoroutine(EnableAndDisablePatterns());
            lastPhaseBlocked = true;
        }
    }

    bool SecondPhaseReady(){
        return suwakoCurrentHP < suwakoHP * 0.75f && suwakoCurrentHP >= suwakoHP * 0.5f;
    }

    bool ThirdPhaseReady(){
        return suwakoCurrentHP < suwakoHP * 0.5f && suwakoCurrentHP >= suwakoHP * 0.25f;
    }

    bool LastPhaseReady(){
        return suwakoCurrentHP < suwakoHP * 0.25f;
    }

    void Update(){
        MoveTowardsDefaultPositionIfNotThere();
        CheckIfSecondPhaseReady();
        CheckIfThirdPhaseReady();
        CheckIfLastPhaseReady();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Spell"){
            Destroy(other.gameObject);
            Spell spellHandler = other.GetComponent<Spell>();
            suwakoCurrentHP -= spellHandler.spellDamage;

            if(suwakoCurrentHP <= 0){
                isSuwakoDead = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
