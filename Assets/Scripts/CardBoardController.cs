﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardController : BasePlayer {

    [SerializeField]
    GameObject[] points;
    int pointIndex = -1;

    private bool onArrive = false;
    Vector3 currentDestination = Vector3.zero;

    private void Awake()
    {
        if (points == null)
        {
            Debug.LogError("Need points to move to");
        }
        generateDestination();
    }

    private void incrementPoint() {
        pointIndex++;
        if (pointIndex >= points.Length)
        {
            pointIndex = 0;
        }
    }



    protected override void Move()
    {
        if (!onArrive) { 
            transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * _speed);
        }

        if (transform.position == currentDestination)
        {
            onArrive = true;
            generateDestination();
        }
    }

    protected override void Melee()
    {

    }

    protected override void Shoot() {}


    void generateDestination()
    {
        incrementPoint();
        currentDestination = points[pointIndex].transform.position;
        onArrive = false;
    }

}