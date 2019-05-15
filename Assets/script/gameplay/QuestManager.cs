using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    // Use this for initialization
    private const int totalQuestDefault = 5;
    private int totalQuest;
    [SerializeField] private List<SingleQuest> listOfSingleQuests = new List<SingleQuest>();
    private List<Vector3> singleQuestDefaultPosition = new List<Vector3>();

    public int TotalQuest
    {
        get
        {
            return totalQuest;
        }

        set
        {
            totalQuest = value;
        }
    }

    private void Start()
    {
        for (int i = 0; i < totalQuestDefault; i++)
        {
            listOfSingleQuests[i].gameObject.SetActive(false);
            singleQuestDefaultPosition.Add(listOfSingleQuests[i].gameObject.transform.position);
        }
        Debug.Log("START QUEST");
    }
    public void generateQuest()
    {
        for (int i = 0; i < totalQuestDefault; i++)
        {
            listOfSingleQuests[i].gameObject.SetActive(false);
            listOfSingleQuests[i].gameObject.transform.position = singleQuestDefaultPosition[i];
        }
        TotalQuest = Random.Range(3, 6);
        for (int i = 0; i < TotalQuest; i++)
        {
            listOfSingleQuests[i].gameObject.SetActive(true);
            if(TotalQuest == 3)
            {
                listOfSingleQuests[i].gameObject.transform.position = listOfSingleQuests[i + 1].gameObject.transform.position;
            }else if(TotalQuest == 4)
            {
                Vector3 newPos = new Vector3(listOfSingleQuests[i + 1].gameObject.transform.position.x - (listOfSingleQuests[i + 1].gameObject.GetComponent<Image>().sprite.bounds.size.x / 2.5f),
                                            listOfSingleQuests[i + 1].gameObject.transform.position.y);
                listOfSingleQuests[i].gameObject.transform.position = newPos;
            }
            int idRandom = Random.Range(0, 4);
            listOfSingleQuests[i].setId(idRandom + 1);
        }
    }

    public int getSingleQuestId(int _indexSingleQuest)
    {
        int _id = listOfSingleQuests[_indexSingleQuest].getId();
        return _id;
    }

    public bool checkAnswer(List<int> _listOfAnswer)
    {
        for (int i = 0; i < _listOfAnswer.Count; i++)
        {
            if(getSingleQuestId(i) != _listOfAnswer[i])
            {
                return false;
            }
        }
        return true;
    }
}
