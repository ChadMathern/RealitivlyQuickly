using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public Transform Player;
    public Transform enemyMelee;
    private bool IsAttacking = false;
    private Vector3 Distance;
    private float DistanceFrom;
    private int minDist = 1;
    private int moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Attacking();

        Distance = (enemyMelee.position - Player.position);
        Distance.y = 0;
        DistanceFrom = Distance.magnitude;
        Distance /= DistanceFrom;


        if (DistanceFrom < 20)
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }


        void Attacking()
        {
            if (IsAttacking)
            {


                enemyMelee.LookAt(Player);
                if (Vector3.Distance(transform.position, Player.position) >= minDist)
                {

                    transform.position += transform.forward * moveSpeed * Time.deltaTime;

                }

            }
        }
    }
}