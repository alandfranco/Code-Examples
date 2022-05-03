using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerPlayer : IController {

    ModelPlayer _model;
    ViewPlayer _viewPlayer;

    public ControllerPlayer(ModelPlayer _m, ViewPlayer _v)
    {        
        _model = _m;
    }

    public override void OnUpdate()
    {
        // KEYBOARD INPUTS
        _model.OnMove(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        _model.LookAtMouse(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _model.SpecialAttack();
        }

        // MOUSE INPUTS
        if (Input.GetMouseButtonUp(0))
        {
            _model.Shoot();
        }
    }
}
