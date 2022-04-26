using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int spellDamage = 20;
    
    private float spellSpeed = 20.0f;

    void MoveSpell(){
        transform.Translate(Vector3.up * spellSpeed * Time.deltaTime);
    }

    void DespawnSpell()
    {
        if(transform.position.y > 6.0f){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update(){
        MoveSpell();
        DespawnSpell();
    }
}
