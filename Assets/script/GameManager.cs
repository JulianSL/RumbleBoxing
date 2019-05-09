using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    private List<int> listOfAnswer = new List<int>();
    private float defaultTimer;
    private float currentBarTimer;
    private float timerSpeed;
	void Start () {
        initDataDefault();
        generateNewQuest();
        
        
    }

    private void initDataDefault()
    {
        defaultTimer = 2;
        currentBarTimer = defaultTimer;
        timerSpeed = 0.01f;
    }

    private void generateNewQuest()
    {
        questManager.gameObject.GetComponent<QuestManager>().generateQuest();
        listOfAnswer = new List<int>();
    }
    

    public void inputAnswer(int _idAnswer)
    {
        Debug.Log(_idAnswer);
        if (listOfAnswer.Count < 4)
        {
            listOfAnswer.Add(_idAnswer);
            if(listOfAnswer.Count == 4)
            {
                if (questManager.gameObject.GetComponent<QuestManager>().checkAnswer(listOfAnswer))
                {
                    Debug.Log("answer true");
                    playerPunch();
                }
                else
                {
                    Debug.Log("answer false");
                    enemyPunch();
                }
                generateNewQuest();

            }
        }
    }

    private void enemyPunch()
    {
        enemy.gameObject.GetComponent<Animator>().SetTrigger("punch");
        player.gameObject.GetComponent<Animator>().SetTrigger("hit");
    }

    private void playerPunch()
    {
        player.gameObject.GetComponent<Animator>().SetTrigger("punch");
        enemy.gameObject.GetComponent<Animator>().SetTrigger("hit");
    }

    private UIManager getUIManager()
    {
        return uiManager.gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update () {
        currentBarTimer -= timerSpeed;
        getUIManager().updateTimerBar(currentBarTimer/defaultTimer);
        if(currentBarTimer <= 0)
        {
            currentBarTimer = defaultTimer;
            enemyPunch();
            generateNewQuest();
        }
	}
}
