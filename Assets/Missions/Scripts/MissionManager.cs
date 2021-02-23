using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct MissionDefinition
{
    public string type;

    public string text;

    public string[] arguments;
}

[Serializable]
public struct MissionArray
{
    public MissionDefinition[] missions;
}

public class MissionManager
{
    public  MissionDefinition[] missions;
    public int index = 0;

    public int Count { get => this.index; }
    public bool Empty { get => this.index == 0; }

    public MissionManager()
    {
        this.LoadDeck();
        this.index = this.missions.Length;
    }

    public MissionDefinition? Draw() {
        if (this.Empty) return null;
        return this.missions[--this.index];
    }

    private void LoadDeck() {
        try {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/Missions/Scripts/Validators/missions-definition.json"))
            {
                string json = reader.ReadToEnd();
                this.missions = JsonUtility.FromJson<MissionArray>(json).missions;
            }
        } catch(Exception e) {
            Debug.Log("Error loading missions: " + e.Message);
            this.missions = new MissionDefinition[0];
        }
    }
    

}
