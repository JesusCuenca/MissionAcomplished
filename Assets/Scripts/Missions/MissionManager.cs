using System;
using System.Collections.Generic;
using UnityEngine;

namespace MissionAcomplished.Missions
{
    [Serializable]
    public struct MissionDefinition
    {
        public string type;
        public string[] arguments;
    }

    [Serializable]
    public struct MissionArray
    {
        public MissionDefinition[] missions;
    }

    [Serializable]
    public class MissionManager
    {
        [SerializeField]
        public Queue<MissionDefinition> missions;
        [SerializeField]
        public List<MissionDefinition> activeMissions;
        [SerializeField]
        public List<MissionDefinition> acomplishedMissions;

        public int Count { get => this.missions.Count; }
        public int CountActive { get => this.activeMissions.Count; }
        public int CountAcomplished { get => this.acomplishedMissions.Count; }
        public int Total { get; private set; }

        public MissionManager(Queue<MissionDefinition> missions, List<MissionDefinition> active = null, List<MissionDefinition> acomplished = null)
        {
            this.missions = missions;
            this.activeMissions = active ?? new List<MissionDefinition>();
            this.acomplishedMissions = acomplished ?? new List<MissionDefinition>();
        }

        public MissionManager() : this(MissionManager.LoadAndShuffleDeck()) { }

        public MissionDefinition? ActivateNext()
        {
            if (this.missions.Count == 0)
            {
                return null;
            }

            var mission = this.missions.Dequeue();
            this.activeMissions.Add(mission);
            return mission;
        }

        public MissionDefinition? Acomplish(MissionDefinition mission)
        {
            if (this.activeMissions.Remove(mission))
            {
                this.acomplishedMissions.Add(mission);
                return mission;
            }
            return null;
        }

        private static Queue<MissionDefinition> LoadAndShuffleDeck()
        {
            try
            {
                TextAsset json = Resources.Load<TextAsset>("missions-definition");
                var missions = JsonUtility.FromJson<MissionArray>(json.ToString()).missions;

                // Shuffle
                System.Random rnd = new System.Random();
                int count = missions.Length;
                int n = count;
                while (n > 1)
                {
                    n--;
                    int r = rnd.Next(count);
                    var temp = missions[r];
                    missions[r] = missions[n];
                    missions[n] = temp;
                }

                return new Queue<MissionDefinition>(missions);
            }
            catch (Exception e)
            {
                Debug.Log("Error loading missions: " + e.Message);
                return new Queue<MissionDefinition>();
            }
        }
    }
}
