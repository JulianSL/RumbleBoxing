using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private List<GameObject> listOfSingleQuests = new List<GameObject>(4);
    private List<int> listOfQuestId = new List<int>();

    public void generateQuest()
    {
        listOfQuestId = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int idRandom = Random.Range(1, 4);
            listOfQuestId.Add(idRandom);
            listOfSingleQuests[i].gameObject.GetComponent<SingleQuest>().setId(idRandom);
        }
    }

    public int getSingleQuestId(int _indexSingleQuest)
    {
        int _id = listOfSingleQuests[_indexSingleQuest].gameObject.GetComponent<SingleQuest>().getId();
        return _id;
    }

    public bool checkAnswer(List<int> _listOfAnswer)
    {
        for (int i = 0; i < listOfQuestId.Count; i++)
        {
            if(listOfQuestId[i] != _listOfAnswer[i])
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
