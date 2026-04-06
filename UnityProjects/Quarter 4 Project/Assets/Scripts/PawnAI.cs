using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class PawnAI : MonoBehaviour
{
   public GameObject player;
   public float Speed;

   private float Distance;

   void Awake()
    {

    }

    void Update()
    {
        Distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (Distance > 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, Speed * Time.deltaTime);
        }
    }
}
