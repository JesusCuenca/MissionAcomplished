using UnityEngine;
using SWNetwork; 

public class GameIndex
{
    public static SWLobbyIndexData MakeRandomIndexData() {
        return GameIndex.MakeIndexData("ABCD");
    }

    public static SWLobbyIndexData MakeIndexData(string gameName) {
        SWLobbyIndexData data = new SWLobbyIndexData();
        data.AddIndex("name", gameName);
        return data;
    }

    public static SWLobbyFilterData MakeFilterData(string gameName) {
        SWLobbyFilterData data = new SWLobbyFilterData();
        data.AddFilter("name", gameName);
        return data;
    }
}
