using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GAF.Core;
using GAF.Assets;

public class Unit : MonoBehaviour
{

    // Use this for initialization
    public event EventHandler EVENT_UNIT_DEATH;
    public event EventHandler EVENT_UNIT_READY;
    private int id;
    private int damage;
    private int healthPoint;
    private int currentHealthPoint;
    private bool isEnemy;
    private bool isDead;
    private bool isRespawn;
    private Animator animator;
    private GAFMovieClip clip;
    [SerializeField] List<GAFAnimationAsset> listOfGAFAnimationAssets = new List<GAFAnimationAsset>();
    private const float animationIdleDuration   = 0.6333f;
    private const float animationKODuration     = 0.6f;
    private const float animationPunchDuration  = 0.65f;
    private const float animationWalkDuration   = 0.6333f;
    private const float animationHitDuration    = 0.6f;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int HealthPoint
    {
        get
        {
            return healthPoint;
        }

        set
        {
            healthPoint = value;
        }
    }

    public int CurrentHealthPoint
    {
        get
        {
            return currentHealthPoint;
        }

        set
        {
            currentHealthPoint = value;
        }
    }

    public bool IsEnemy
    {
        get
        {
            return isEnemy;
        }

        set
        {
            isEnemy = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public void init(int _id, bool _isEnemy)
    {
        DOTween.Init();
        Id = _id;
        IsEnemy = _isEnemy;
        HealthPoint = 100;
        CurrentHealthPoint = HealthPoint;
        Damage = 20;
        IsDead = false;
        isRespawn = false;
        clip = GetComponent<GAFMovieClip>();
        setupGafAnimation();
        //animator = GetComponent<Animator>();
        //animator.enabled = true;
        //animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + Id.ToString(), typeof(RuntimeAnimatorController));
        setWalk();
    }

    private void setupGafAnimation()
    {
        clip.clear(false);
        clip.asset = listOfGAFAnimationAssets[id - 1];
        clip.initialize(listOfGAFAnimationAssets[id - 1]);
        clip.settings.init(listOfGAFAnimationAssets[id - 1]);
        clip.reload();
        clip.settings.zLayerScale = 0.02f;
    }

    private void setWalk()
    {
        this.GetComponent<GAFMovieClip>().setSequence("walk", true);
        Debug.Log("walk" + this.GetComponent<GAFMovieClip>().timelineID);
        //animator.SetTrigger("walk");
        //Debug.Log(this.transform.position);
        if (!IsEnemy)
        {
            this.transform.position = new Vector3(-6.07f, 3.2f, 88);
            this.transform.DOMove(new Vector3(-3.2f, 3.2f, 88), 2).SetEase(Ease.Linear).OnComplete(setUnitReady);
        }
        else
        {
            this.transform.localScale = new Vector3(-0.009f, 0.009f, 1f);
            this.transform.position = new Vector3(6.07f, 3.2f, 88);
            this.transform.DOMove(new Vector3(3.2f, 3.2f, 88), 2).SetEase(Ease.Linear).OnComplete(setUnitReady);
        }
    }

    private void setUnitReady()
    {
        //animator.SetTrigger("idle");
        setIdle();
        Debug.Log("idle" + this.GetComponent<GAFMovieClip>().asset.getSequences(this.GetComponent<GAFMovieClip>().timelineID));
        dispatchEvent(EVENT_UNIT_READY, this.gameObject, EventArgs.Empty);
    }

    private void setIdle()
    {
        clip.setSequence("idle", true);
    }

    public void getHit(int _damage)
    {
        CurrentHealthPoint -= _damage;
        setHit();
        Debug.Log("hit" + this.GetComponent<GAFMovieClip>().timelineID);
        //animator.SetTrigger("hit");
        if (CurrentHealthPoint <= 0)
        {
            IsDead = true;
            setIsDead();
        }
    }

    private void setHit()
    {
        clip.setSequence("hit", true);
        DOVirtual.DelayedCall(animationHitDuration, setIdle);
    }

    private void generateNewEnemyModel()
    {
        //int idEnemy = UnityEngine.Random.Range(11, 15);
        init(2, true);
    }

    private void setIsDead()
    {
        //animation death here
        setKo();
        Debug.Log("ko" + this.GetComponent<GAFMovieClip>().timelineID);
        //animator.SetTrigger("ko");
    }

    private void setKo()
    {
        clip.setSequence("ko", true);
        DOVirtual.DelayedCall(animationKODuration, dispatchKo);
    }

    private void dispatchKo()
    {
        dispatchEvent(EVENT_UNIT_DEATH, this.gameObject, EventArgs.Empty);
    }

    public void attack()
    {
        setPunch();
        Debug.Log("punch" + this.GetComponent<GAFMovieClip>().timelineID);
        //animator.SetTrigger("punch");
    }

    private void setPunch()
    {
        clip.setSequence("punch", true);
        DOVirtual.DelayedCall(animationPunchDuration, setIdle);
    }

    // Update is called once per frame
    public void update()
    {
        updateCheckKOAnimation();
    }

    private void updateCheckKOAnimation()
    {
        
        /*if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator.GetCurrentAnimatorStateInfo(0).length - 0.2f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("character ko") &&
            IsDead)
        {
            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + " - " + animator.GetCurrentAnimatorStateInfo(0).length);
            IsDead = false;
            animator.enabled = false;
            
        }*/
    }

    private void pullOutKoEnemy()
    {
        transform.DOMove(new Vector3(5, 0.8f, 90), 1.5f).SetEase(Ease.Linear).OnComplete(generateNewEnemyModel);
    }

    private void dispatchEvent(EventHandler _event, object _sender, EventArgs _eventArgs)
    {
        if (_event != null)
        {
            _event(_sender, _eventArgs);
        }
    }
}
