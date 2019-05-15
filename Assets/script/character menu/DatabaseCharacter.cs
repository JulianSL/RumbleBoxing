using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseCharacter : MonoBehaviour {

    private static DatabaseCharacter databaseMachine = new DatabaseCharacter();
    public DatabaseCharacter()
    {
        loadDatabase();
    }

    public static DatabaseCharacter getInstance()
    {
        return databaseMachine;
    }

    private List<ArrayList> arrayOfCharacter;

    private void loadDatabase()
    {
        arrayOfCharacter = new List<ArrayList>()
        {
            new ArrayList() {"Name",             "price"},
            new ArrayList() {"Johny",                   0},
            new ArrayList() {"Captain 'Steve' America", 50},
            new ArrayList() {"Mad 'Thonas' Titan",      100},
            new ArrayList() {"Alex",                    0}

        };
    }

    public string getName(int _idCharacter)
    {
        return (string)arrayOfCharacter[_idCharacter][0];
    }

    public int getPrice(int _idCharacter)
    {
        return (int)arrayOfCharacter[_idCharacter][1];
    }
}
