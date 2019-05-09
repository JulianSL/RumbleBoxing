using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private Image playerBar;
    [SerializeField] private Image enemmyBar;
    [SerializeField] private Text round_txt;
    [SerializeField] private Image timerBar;
    [SerializeField] private List<GameObject> listOfButtonAnimation = new List<GameObject>();
	void Start () {

	}

    public void playButtonAnimation(int _idButton)
    {
        listOfButtonAnimation[_idButton - 1].gameObject.GetComponent<Animator>().SetTrigger("isClick");
    }

    public void updateTimerBar(float _fillAmount)
    {
        timerBar.fillAmount = _fillAmount;
    }
    // Update is called once per frame
    float a = 1;
    int round = 0;
	void Update () {
        //a -= 0.01f;
        //round++;
        //playerBar.fillAmount = a;
        //enemmyBar.fillAmount = a;
        //round_txt.text = "Round "+round.ToString();
	}
}
