using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : MonoBehaviour
{
    public Transform Player;
    public Transform enemyCharger;
    private bool IsAttacking = false;
    private Vector3 Distance;
    private float DistanceFrom;
    private int minDist = 1;
    private int moveSpeed = 50;
    private IEnumerator charge;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Attacking();

        Distance = (enemyCharger.position - Player.position);
        Distance.y = 0;
        DistanceFrom = Distance.magnitude;
        Distance /= DistanceFrom;


        if (DistanceFrom < 50)
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }


    void Attacking()
    {
        if (IsAttacking)
        {


            enemyCharger.LookAt(Player);
            StartCoroutine(Charge());


        }
    }
    IEnumerator Charge()
        {
            if (Vector3.Distance(transform.position, Player.position) >= minDist)
            {
                yield return new WaitForSeconds(4);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                IsAttacking = false;

            }
        }
    
}