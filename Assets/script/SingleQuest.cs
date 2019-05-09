using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleQuest : MonoBehaviour {

    // Use this for initialization
    public List<Sprite> listOfImageQuest = new List<Sprite>(4);
    private Image render;
    private int id;


    void Start () {
        render = GetComponent<Image>();
        
    }
	
    public void setId(int _id)
    {
        id = _id;
        GetComponent<Image>().sprite = listOfImageQuest[id - 1];
    }

    public int getId()
    {
        return id;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
