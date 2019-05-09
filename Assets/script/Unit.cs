using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    // Use this for initialization
    private int id;
    private int damage;
    private int healthPoint;
    private int currentHealthPoint;
    private Animator animator;

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

    public void init(int _id)
    {
        Id = _id;
        animator = this.gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character"+Id.ToString(), typeof(RuntimeAnimatorController));
    }

    public void getHit(int _damage)
    {
        CurrentHealthPoint -= _damage;
        animator.SetTrigger("idle");
    }

    public void attack()
    {
        animator.SetTrigger("punch");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
