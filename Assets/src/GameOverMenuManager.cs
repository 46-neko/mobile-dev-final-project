using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour{
    [SerializeField] private GameObject playButtonGO;
    [SerializeField] private GameObject exitButtonGO;
    [SerializeField] private AudioClip buttonPressSFX;
    [SerializeField] private AudioSource mainMenuSFX;
    [SerializeField] private GameObject cirnoPlayButtonImage;
    [SerializeField] private GameObject cirnoExitButtonImage;
    [SerializeField] private Sprite selectedImage;

    public void PlayAgain(){
        playButtonGO.GetComponent<ButtonHover>().buttonIsClicked = true;
        cirnoPlayButtonImage.GetComponent<Image>().sprite = selectedImage;
        mainMenuSFX.PlayOneShot(buttonPressSFX);
        exitButtonGO.GetComponent<Button>().interactable = false;
        playButtonGO.GetComponent<Button>().interactable = false;
    }

    public void ExitToMainMenu(){
        exitButtonGO.GetComponent<ButtonHover>().buttonIsClicked = true;
        cirnoExitButtonImage.GetComponent<Image>().sprite = selectedImage;
        mainMenuSFX.PlayOneShot(buttonPressSFX);
        exitButtonGO.GetComponent<Button>().interactable = false;
        playButtonGO.GetComponent<Button>().interactable = false;
    }

    public void ReloadLevel(string sceneName){
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadMainMenu(string sceneName){
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
