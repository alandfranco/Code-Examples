using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public ViewPlayer view; 
    IController controller;
    ModelPlayer _m;

    Color originalColor = Color.green;

    void Awake()
    {
        view = FindObjectOfType<ViewPlayer>();
        _m = new ModelPlayer(transform);
        controller = new ControllerPlayer(_m, view);


        _m.autoFireMode = new AutofireMode(this.transform);
        _m.shotgunFireMode = new ShotgunMode(this.transform, 3, 5);
        _m.specialFireMode = new NovaMode(this.transform, 16);

        _m.currentFireMode = _m.autoFireMode;

        _m.maxHp = 100;
        _m.currentHp = _m.maxHp;
        _m.specialAmmo = 0;
        _m.specialAmmoMax = 5;
        _m.speed = 10;
        _m.fireRate = 6f;

        view.player = this;
        view.model = _m;
}
    void Update()
    {
        //Check Inputs from controller
        controller.OnUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent(typeof(IPowerUp)))
        {
            other.gameObject.GetComponent<IPowerUp>().Effect(_m);       //Activate the power up effect
            view.RepaintPopUpText(other.gameObject.GetComponent<IPowerUp>().Message());
        }

        if (other.gameObject.GetComponent<Enemy>())
        {
            _m.TakeDamage();                            //Take damage
            StartCoroutine(DamageFeedBack());
            view.RepaintLife(_m.currentHp);
            other.GetComponent<Enemy>().TakeDamage();   //We deal damage to the enemy
        }

        if (other.gameObject.GetComponent<Bullet>() && other.gameObject.layer != this.gameObject.layer)
        {
            BulletSpawner.Instance.ReturnBulletToPool(other.GetComponent<Bullet>());        //Give the bullet to the pool
            _m.TakeDamage();                                                                //I take damage
            StartCoroutine(DamageFeedBack());
            view.RepaintLife(_m.currentHp);
        }
    }

    IEnumerator DamageFeedBack()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Renderer>().material.color = originalColor;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _m.TakeDamage();                            
            other.GetComponent<Enemy>().TakeDamage();   
            view.RepaintLife(_m.currentHp);
        }
    }
}
