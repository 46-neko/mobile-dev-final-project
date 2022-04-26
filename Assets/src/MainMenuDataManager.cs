using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDataManager : MonoBehaviour{
    public string file = "record.json";

    [SerializeField] private Text highScoreText;
    [SerializeField] Player playerBase;

    void CheckIfFileDoesntExist(){
        if(File.Exists(GetFilePath(file))){
            return;
        } else{
            Save();
        }
    }

    void Save(){
        string json = JsonUtility.ToJson(playerBase, true);
        WriteToFile(file, json);
    }

    void Load(){
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, playerBase);
        highScoreText.text = playerBase.playerScore.ToString();
    }

    void WriteToFile(string fileName, string json){
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream)){
            writer.Write(json);
        }
    }

    string ReadFromFile(string fileName){
        string path = GetFilePath(fileName);
        if(File.Exists(path)){
            using (StreamReader reader = new StreamReader(path)){
                string json = reader.ReadToEnd();
                return json;
            }
        } else{
            Debug.LogWarning("JSON with data not found!");
            return "";
        }
    }

    string GetFilePath(string fileName){
        return Application.persistentDataPath + "/" + fileName;
    }

    void Start(){
        CheckIfFileDoesntExist();
        Load();
    }
}
