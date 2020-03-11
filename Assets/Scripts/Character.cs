﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    protected Vector2 direction;


    private Rigidbody2D myRigidbody;
    //protected Vector3 targetPosition;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    private Animator myAnimator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()//Update runs once per frame
    {
        HandleLayers();
    }
    private void FixedUpdate()//FixedUpdate can run once, zero, or several times per frame, depending on how many physics frames per second are set in the time settings, and how fast/slow the framerate is. fixed Update garanteed to run and if you make them heavy then your framerate will drop. you can easily test it
    {
        Move();
    }
    //Move the Player
    public void Move()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
        myRigidbody.velocity = direction.normalized * speed;//make sure that the player moves

    }
    public void HandleLayers()
    {       

        if (IsMoving)
        {
            //Make Player Animate move
            ActivateLayer("WallkLayer");

            //Sets the animation parameter so that he faces the correct direction
            myAnimator.SetFloat("X", direction.x);
            myAnimator.SetFloat("Y", direction.y);
        }
        else
        {
            // Player movment Animate return to  be Idle
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);

    }

}
