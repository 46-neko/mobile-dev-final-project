using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] private GameObject cirnoButtonImage;
    public bool buttonIsClicked;

    void Start(){
        buttonIsClicked = false;
    }

    public void OnPointerEnter(PointerEventData eventData){
        cirnoButtonImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(buttonIsClicked){
            cirnoButtonImage.SetActive(true);
        } else{
            cirnoButtonImage.SetActive(false);
        }
    }
}
