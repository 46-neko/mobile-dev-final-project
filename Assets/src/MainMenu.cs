using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    [SerializeField] private GameObject playButtonGO;
    [SerializeField] private GameObject exitButtonGO;
    [SerializeField] private Animator transitionAnimOverworld;
    [SerializeField] private AudioClip buttonPressSFX;
    [SerializeField] private AudioSource mainMenuSFX;
    [SerializeField] private GameObject cirnoPlayButtonImage;
    [SerializeField] private GameObject cirnoExitButtonImage;
    [SerializeField] private Sprite selectedImage;

    public void playGame(){
        playButtonGO.GetComponent<ButtonHover>().buttonIsClicked = true;
        cirnoPlayButtonImage.GetComponent<Image>().sprite = selectedImage;
        mainMenuSFX.PlayOneShot(buttonPressSFX);
        exitButtonGO.GetComponent<Button>().interactable = false;
        playButtonGO.GetComponent<Button>().interactable = false;
        StartCoroutine(animationPlayGame());
    }

    public void exitGame(){
        exitButtonGO.GetComponent<ButtonHover>().buttonIsClicked = true;
        cirnoExitButtonImage.GetComponent<Image>().sprite = selectedImage;
        mainMenuSFX.PlayOneShot(buttonPressSFX);
        exitButtonGO.GetComponent<Button>().interactable = false;
        playButtonGO.GetComponent<Button>().interactable = false;
        StartCoroutine(animationExitGame());
    }

    IEnumerator animationPlayGame(){
        transitionAnimOverworld.SetTrigger("loadGameAnimation");
        yield return new WaitForSeconds(3f);
    }

    IEnumerator animationExitGame(){
        transitionAnimOverworld.SetTrigger("loadGameAnimation");
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
