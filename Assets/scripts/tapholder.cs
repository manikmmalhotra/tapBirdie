﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class tapholder : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startpos;
    gamemanager game;

    Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = gamemanager.Instance;
        rigidbody.simulated = false;
    } 

    void OnEnable()
    {
        gamemanager.OnGameStarted += OnGameStarted;
        gamemanager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        gamemanager.OnGameStarted -= OnGameStarted;
        gamemanager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;

    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startpos;
        transform.rotation = Quaternion.identity;
    }



    void Update()
    {
        if (game.GameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
}

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth*Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "scorezone")
        {
            OnPlayerScored();
        }
        if(col.gameObject.tag == "deadzone")
        {
            rigidbody.simulated = false;
            OnPlayerDied();

        }
    }
   
}
