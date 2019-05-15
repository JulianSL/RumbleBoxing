using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GVM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        bool exists = SaveGame.Exists("savegame.txt");
        
    }


    // Update is called once per frame
    void Update () {
		
	}
}
