using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxPunchText : MonoBehaviour {

    // Use this for initialization

    public event EventHandler EVENT_REMOVE;
    [SerializeField] private List<Sprite> listOfVfxPunchText = new List<Sprite>();

    public void init(float _targetPositionX)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.0f);
        int vfxId = UnityEngine.Random.Range(0, 4);
        this.GetComponent<SpriteRenderer>().sprite = listOfVfxPunchText[vfxId];
        this.transform.position = new Vector3(_targetPositionX, 2.59f);
        DOVirtual.DelayedCall(0.3f, startAnimation);
    }

    private void startAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
        this.transform.DOMoveY(this.transform.position.y + 1, 1.5f).SetEase(Ease.OutCirc);
        DOTweenModuleSprite.DOFade(this.GetComponent<SpriteRenderer>(), 0.0f, 1.5f).OnComplete(remove);
    }

    private void remove()
    {
        dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void dispatchEvent(EventHandler _event, object _sender, EventArgs _eventArgs)
    {
        if (_event != null)
        {
            _event(_sender, _eventArgs);
        }
    }
}
