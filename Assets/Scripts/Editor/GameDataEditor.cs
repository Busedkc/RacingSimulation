using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameData gameDataScript=(GameData)target;
        if(GUILayout.Button("Reset"))
        {
            gameDataScript.username="";
            gameDataScript.playTime=0;
            gameDataScript.money=0;
            gameDataScript.lastPlayed=0;
            gameDataScript.currentActiveCar=0;
            gameDataScript.carColors=new List<Color>() {Color.white, Color.white, Color.white};
            gameDataScript.rimMaterials=new List<Material>();
            gameDataScript.unlockedCars=new List<bool>();
        }
    }
}
