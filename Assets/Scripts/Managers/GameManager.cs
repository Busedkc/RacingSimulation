using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] List<GameData> saveSlots = null;
    [SerializeField] Material defaultMaterial = null;
    [SerializeField] AudioManager audioManager = null;
    public static GameData gameData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Events.CarColorChosen += HandleCarColorSet;
        Events.RimColorChoosen += HandleRimColorSet;
        Events.UsernameSubmitted += HandleUserNameSubmitted;
        Events.CarChosen += HandleCarChoosen;
        Events.BackButtonPressed += PlayBackButtonAudio;
        Events.OkButtonClicked += PlayOkButtonAudio;
        Events.CancelButtonClicked += PlayCancelButtonAudio;
        Events.SaveSlotClicked += PlaySaveSlotSound;
        Events.LeftOrRightButtonClicked += PlayLeftOrRightButtonAudio;
        Events.PurchaseButtonClicked += PlayPurchaseAudio;
    }

    private void OnDestroy()
    {
        if(gameData!=null)
        {
            gameData.lastPlayed=DateTime.Now.Ticks;
            SaveScriptableObject(gameData);
        }
        Events.CarColorChosen -= HandleCarColorSet;
        Events.RimColorChoosen -= HandleRimColorSet;
        Events.UsernameSubmitted -= HandleUserNameSubmitted;
        Events.CarChosen -= HandleCarChoosen;
        Events.BackButtonPressed -= PlayBackButtonAudio;
        Events.OkButtonClicked -= PlayOkButtonAudio;
        Events.CancelButtonClicked -= PlayCancelButtonAudio;
        Events.SaveSlotClicked -= PlaySaveSlotSound;
        Events.LeftOrRightButtonClicked -= PlayLeftOrRightButtonAudio;
        Events.PurchaseButtonClicked -= PlayPurchaseAudio;
    }

    private void Update()
    {
        if(gameData!=null)
        {
            gameData.playTime+=Time.deltaTime;
        }
    }

    public static Color GetCarColor()
    {
        return gameData.carColors[GetCurrentActiveCarIndex()];
    }

    public static Material GetRimMaterial()
    {
        return gameData.rimMaterials[GetCurrentActiveCarIndex()];
    }

    public static int GetCurrentActiveCarIndex()
    {
        return gameData.currentActiveCar;
    }

    public static List<bool> GetCarStates()
    {
        return gameData.unlockedCars;
    }

    public static float GetMoneyAsFloat()
    {
        return gameData.money;  
    }
    
    public static string GetMoneyAsString()
    {
        if(gameData==null) {return "";}
        return gameData.money.ToString("C");
    }

    public static void UnlockCar(int index)
    {
        gameData.unlockedCars[index]=true;
        SaveScriptableObject(gameData);
    }

    public static void PurchaseCar(float price, int index)
    {
        UnlockCar(index);
        gameData.money-=price;
        SaveScriptableObject(gameData);
    }

    public static void SetActiveCar(int index)
    {
        gameData.currentActiveCar=index;
        SaveScriptableObject(gameData);
    }

    public static string GetUsername()
    {
        return gameData.username;
    } 

    public static float GetPlayTime()
    {
        return gameData.playTime;
    }

    public static float GetMetallic()
    {
        return gameData.metallic;
    }

    public static float GetSmoothness()
    {
        return gameData.smoothness;
    }

    private void HandleUserNameSubmitted(string username)
    {
        ResetGameData();
        gameData.username=username;
        SaveScriptableObject(gameData);
    }

    private void HandleCarChoosen(int car)
    {
        SaveScriptableObject(gameData);
    }

    private void HandleCarColorSet(Color color, float metallic, float smoothness)
    {
        gameData.carColors[GetCurrentActiveCarIndex()] = color;
        gameData.metallic=metallic;
        gameData.smoothness=smoothness;
        SaveScriptableObject(gameData);
    }

    private void HandleRimColorSet(Material material)
    {
        gameData.rimMaterials[GetCurrentActiveCarIndex()]=material;
        SaveScriptableObject(gameData);
    }

    private void ResetGameData()
    {
        gameData.username="";
        gameData.carColors=new List<Color>() {Color.white, Color.white, Color.white};
        gameData.rimMaterials=new List<Material>() {defaultMaterial, defaultMaterial, defaultMaterial};
        gameData.playTime=0f;
        gameData.money=100000f;
        gameData.lastPlayed=DateTime.Now.Ticks;
        gameData.unlockedCars= new List<bool>();
        gameData.currentActiveCar=0;
        SaveScriptableObject(gameData);
    }

    public void PlaySaveSlotSound()
    {
        audioManager.PlaySaveSlotSound();
    }

    public void PlayBackButtonAudio()
    {
        audioManager.PlayBackButtonSound();
    }

    public void PlayOkButtonAudio()
    {
        audioManager.PlayOkButtonSound();
    }

    public void PlayCancelButtonAudio()
    {
        audioManager.PlayCancelButtonSound();
    }

    public void PlayLeftOrRightButtonAudio()
    {
        audioManager.PlayRightAndLeftButtonSound();
    }

    public void PlayPurchaseAudio()
    {
        audioManager.PlayChaChingButtonSound();
    }
    private static void SaveScriptableObject(GameData gameData)
    {
        EditorUtility.SetDirty(gameData);
        AssetDatabase.SaveAssets();
    }
}
