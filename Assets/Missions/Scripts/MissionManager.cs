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
    public int Total { get; private set; }
    public bool Empty { get => this.index == 0; }

    public MissionManager()
    {
        this.LoadDeck();
        this.index = this.missions.Length;
        this.Total = this.missions.Length;
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

                // Shuffle
                System.Random rnd = new System.Random();
                int count = this.missions.Length;
                int n = count;
                while (n > 1)
                {
                    n--;
                    int r = rnd.Next(count);
                    var temp = this.missions[r];
                    this.missions[r] = this.missions[n];
                    this.missions[n] = temp;
                }
            }
        } catch(Exception e) {
            Debug.Log("Error loading missions: " + e.Message);
            this.missions = new MissionDefinition[0];
        }
    }
    

}
