using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleHitbox : MonoBehaviour{
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    
    [SerializeField] private Player playerHandler;

    void OnEnable(){
        playerHandler = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnParticleTrigger(){
        ParticleSystem system = GetComponent<ParticleSystem>();
        int numEnter = system.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if(numEnter > 0){
            playerHandler.DamagePlayer();
        }
    }
}
