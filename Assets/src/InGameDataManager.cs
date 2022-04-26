using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InGameDataManager : MonoBehaviour{
    public string file = "record.json";
    public Player playerData;

    [SerializeField] private Text highScoreText;

    public void Save(int playerFinalScore){
        playerData.playerScore = playerFinalScore;
        string json = JsonUtility.ToJson(playerData, true);
        WriteToFile(file, json);
    }

    public void Load(){
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, playerData);
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
}
