using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LookAtIt : MonoBehaviour
{
    //public float angle = 30f;
    public Transform playerTf;

    [Range(0f, 1f)]
    public float preciseness = 1f; 
    private void OnDrawGizmos()
    {
        Vector2 center = transform.position;
        Vector2 playerPos = playerTf.position;
        Vector2 playerLookDir = playerTf.right;


        Vector2 playerTriggerDir = (center - playerPos).normalized;
        Gizmos.DrawLine(playerPos, playerPos + playerTriggerDir);


        float lookness = Vector2.Dot(center, playerPos);

        bool isLooking = lookness <= preciseness;

        Gizmos.color = isLooking ? Color.green:Color.red;
        Gizmos.DrawLine(playerPos, playerPos + playerLookDir);



    }
}
