using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour{
    public Color projectileColor;
    public Material projectileMaterial;
    public ParticleSystem system;
    public GameObject suwakoGO;

    private Player playerHandler;
    private int numberOfColumns;
    private float projectileSpeed;
    private float projectileLifeTime;
    private float projectileFireRate;
    private float projectileSize;
    private float spinSpeed;
    private Suwako suwakoHandler;
    private float time;
    private float projectileAngle;

    void Start(){
        playerHandler = GameObject.Find("Player").GetComponent<Player>();
        suwakoGO = this.transform.parent.gameObject;
        suwakoHandler = suwakoGO.GetComponent<Suwako>();

        if(suwakoHandler.suwakoAttackPhase == 0){
            CircularPattern();
        }

        else if(suwakoHandler.suwakoAttackPhase == 1){
            FanPattern();
        }

        else if(suwakoHandler.suwakoAttackPhase == 2){
            ExplosionPattern();
        }

        else{
            FlowerPattern();
        }
    }

    private void FixedUpdate(){
        time += Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(0,0, time * spinSpeed);
    }

    private void SetupParticleSystem(int iterator, GameObject particleSystemGO, float projectileAngle){
        particleSystemGO.transform.Rotate(projectileAngle * iterator, 90, 0);
        particleSystemGO.transform.parent = this.transform;
        particleSystemGO.transform.position = this.transform.position;
    }

    private void SetupMainModule(ParticleSystem.MainModule mainModule, float projectileSpeed){
        mainModule.startColor = Color.green;
        mainModule.startSize = 0.5f;
        mainModule.startSpeed = projectileSpeed;
        mainModule.maxParticles = 1105;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
    }

    private void SetupShapeModule(ParticleSystem.ShapeModule shapeModule, string ParticleSystemShapeString){
        shapeModule.enabled = true;
        if(ParticleSystemShapeString == "Sprite"){
            shapeModule.shapeType = ParticleSystemShapeType.Sprite;
        }
        
        else if(ParticleSystemShapeString == "Hemisphere"){
            shapeModule.shapeType = ParticleSystemShapeType.Hemisphere;
        }
        shapeModule.sprite = null;
    }

    void SetupVelocityOverLifetime(
            ParticleSystem.VelocityOverLifetimeModule velocityOverLifeTimeModule,
            float orbitalXValue,
            float orbitalYValue,
            float orbitalZValue){
        velocityOverLifeTimeModule.enabled = true;
        velocityOverLifeTimeModule.space = ParticleSystemSimulationSpace.World;

        velocityOverLifeTimeModule.orbitalX = orbitalXValue;
        velocityOverLifeTimeModule.orbitalY = orbitalYValue;
        velocityOverLifeTimeModule.orbitalZ = orbitalZValue;
    }

    void SetupTriggerModule(ParticleSystem.TriggerModule triggerModule){
        triggerModule.enabled = true;
        triggerModule.enter = ParticleSystemOverlapAction.Callback;
        triggerModule.inside = ParticleSystemOverlapAction.Ignore;
        triggerModule.radiusScale = 0.06f;
        triggerModule.colliderQueryMode = ParticleSystemColliderQueryMode.One;
        triggerModule.AddCollider(playerHandler.GetComponent<CircleCollider2D>());
    }

    void CircularPattern(){
        numberOfColumns = 50;
        projectileSpeed = 100f;
        projectileLifeTime = 3f;
        projectileFireRate = 0.8f;
        projectileSize = 6f;
        spinSpeed = 50f;

        projectileAngle = 360f / numberOfColumns;

        for(int i = 0; i < numberOfColumns; i++){
            Material particleMaterial = projectileMaterial;
            var particleSystemGO = new GameObject("Particle System");
            system = particleSystemGO.AddComponent<ParticleSystem>();
            particleSystemGO.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            particleSystemGO.AddComponent<ParticleHitbox>();

            var mainModule = system.main;
            var emissionModule = system.emission;
            var shapeModule = system.shape;
            var triggerModule = system.trigger;
            system.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";

            emissionModule.enabled = false;
            SetupParticleSystem(i, particleSystemGO, projectileAngle);
            SetupMainModule(mainModule, projectileSpeed);
            SetupTriggerModule(triggerModule);
            SetupShapeModule(shapeModule, "Sprite");
        }
        InvokeRepeating("DoEmit", 0f, projectileFireRate);
    }

    void FanPattern(){
        numberOfColumns = 8;
        projectileSpeed = 80f;
        projectileLifeTime = 3f;
        projectileFireRate = 0.1f;
        projectileSize = 4f;
        spinSpeed = 30f;
        
        projectileAngle = 360f / numberOfColumns;

        for(int i = 0; i < numberOfColumns; i++){
            Material particleMaterial = projectileMaterial;
            var particleSystemGO = new GameObject("Particle System");
            system = particleSystemGO.AddComponent<ParticleSystem>();
            particleSystemGO.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            particleSystemGO.AddComponent<ParticleHitbox>();
            system.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";

            var mainModule = system.main;
            var emissionModule = system.emission;
            var shapeModule = system.shape;
            var triggerModule = system.trigger;

            emissionModule.enabled = false;
            SetupParticleSystem(i, particleSystemGO, projectileAngle);
            SetupMainModule(mainModule, projectileSpeed);
            SetupTriggerModule(triggerModule);
            SetupShapeModule(shapeModule, "Sprite");
        }
        InvokeRepeating("DoEmit", 0f, projectileFireRate);
    }

    void ExplosionPattern(){
        numberOfColumns = 6;
        projectileSpeed = 50f;
        projectileLifeTime = 4f;
        projectileFireRate = 0.5f;
        projectileSize = 5f;
        spinSpeed = 200f;
        projectileAngle = 360f / numberOfColumns;

        for(int i = 0; i < numberOfColumns; i++){
            Material particleMaterial = projectileMaterial;
            var particleSystemGO = new GameObject("Particle System");
            system = particleSystemGO.AddComponent<ParticleSystem>();
            particleSystemGO.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            particleSystemGO.AddComponent<ParticleHitbox>();
            system.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";

            var mainModule = system.main;
            var emissionModule = system.emission;
            var shapeModule = system.shape;
            var triggerModule = system.trigger;
            var velocityOverLifeTimeModule = system.velocityOverLifetime;

            emissionModule.enabled = false;
            SetupParticleSystem(i, particleSystemGO, projectileAngle);
            SetupMainModule(mainModule, projectileSpeed);
            SetupTriggerModule(triggerModule);
            SetupShapeModule(shapeModule, "Hemisphere");
        }
        InvokeRepeating("DoEmit", 0f, projectileFireRate);
    }

    void FlowerPattern(){
        numberOfColumns = 4;
        projectileSpeed = 100f;
        projectileLifeTime = 7.5f;
        projectileFireRate = 0.2f;
        projectileSize = 6f;
        spinSpeed = 40f;
        projectileAngle = 360f / numberOfColumns;

        for(int i = 0; i < numberOfColumns; i++){
            Material particleMaterial = projectileMaterial;
            var particleSystemGO = new GameObject("Particle System");
            system = particleSystemGO.AddComponent<ParticleSystem>();
            particleSystemGO.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            particleSystemGO.AddComponent<ParticleHitbox>();
            system.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";

            var mainModule = system.main;
            var emissionModule = system.emission;
            var shapeModule = system.shape;
            var triggerModule = system.trigger;
            var velocityOverLifeTimeModule = system.velocityOverLifetime;

            emissionModule.enabled = false;
            SetupParticleSystem(i, particleSystemGO, projectileAngle);
            SetupMainModule(mainModule, projectileSpeed);
            SetupTriggerModule(triggerModule);
            SetupShapeModule(shapeModule, "Sprite");
            SetupVelocityOverLifetime(velocityOverLifeTimeModule, 0.7f, 0.7f, 0);
        }
        InvokeRepeating("DoEmit", 0f, projectileFireRate);
    }

    void DoEmit(){
        foreach(Transform child in transform){
            system = child.GetComponent<ParticleSystem>();

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = projectileColor;
            emitParams.startSize = projectileSize;
            emitParams.startLifetime = projectileLifeTime;

            system.Emit(emitParams, 10);
        }
    }
}
