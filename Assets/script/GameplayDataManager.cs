using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataManager : MonoBehaviour {

    private static GameplayDataManager gameplayDataManager = new GameplayDataManager();
    public static GameplayDataManager getInstance()
    {
        return gameplayDataManager;
    }

    private List<bool> listOfUnlockedUnit;
    private int totalUnits;
    private int idEquipedUnit;
    private int totalMedals;
    private int highScore;

    public int TotalUnits
    {
        get
        {
            return totalUnits;
        }

        set
        {
            totalUnits = value;
        }
    }

    public int IdEquipedUnit
    {
        get
        {
            return idEquipedUnit;
        }

        set
        {
            idEquipedUnit = value;
        }
    }

    public int TotalMedals
    {
        get
        {
            return totalMedals;
        }

        set
        {
            totalMedals = value;
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }


    public void init()
    {
        reset();
        bool exists = SaveGame.Exists("idEquipedUnit");
        Debug.Log("is save game exist ? : " + exists);
        
        if (exists)
        {
            
            loadGame();
        }
        else
        {
            saveGame();
        }
    }

    public void clearSaveData()
    {
        SaveGame.Clear();
        reset();
        saveGame();
    }


    public void loadGame()
    {
        idEquipedUnit   = SaveGame.Load<int>("idEquipedUnit");
        TotalUnits      = SaveGame.Load<int>("TotalUnits");
        totalMedals     = SaveGame.Load<int>("totalMedals");
        highScore       = SaveGame.Load<int>("highScore");
        listOfUnlockedUnit = SaveGame.Load<List<bool>>("listOfUnlockedUnit");
    }

    public void saveGame()
    {
        SaveGame.Save<List<bool>>("listOfUnlockedUnit", listOfUnlockedUnit);
        SaveGame.Save<int>("totalUnits", TotalUnits);
        SaveGame.Save<int>("idEquipedUnit", idEquipedUnit);
        SaveGame.Save<int>("totalMedals", totalMedals);
        SaveGame.Save<int>("highScore", highScore);
    }

    public void reset()
    {
        idEquipedUnit = 1;
        TotalUnits = 3;
        totalMedals = 0;
        highScore = 0;
        resetListOfUnlockedUnit();
    }

    private void resetListOfUnlockedUnit()
    {
        listOfUnlockedUnit = new List<bool>();
        for (int i = 0; i < TotalUnits; i++)
        {
            listOfUnlockedUnit.Add(false);
        }
        listOfUnlockedUnit[0] = true;
    }

    public bool isUnitUnlocked(int _idUnit)
    {
        return listOfUnlockedUnit[_idUnit - 1];
    }

    public void unlockUnit(int _idUnit)
    {
        listOfUnlockedUnit[_idUnit - 1] = true;
    }
}
