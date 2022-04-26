using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour{
    [SerializeField] private MeshRenderer meshRendererHandler;
    
    private float scrollSpeed = 0.1f;

    void Start(){
        meshRendererHandler = gameObject.GetComponent<MeshRenderer>();
    }

    void Update(){
        meshRendererHandler.material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
