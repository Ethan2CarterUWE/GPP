using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SA
{
    public class Enemy : MonoBehaviour
    {

        public GameObject enemy;
        public Transform spawn1;
        public Transform spawn2;
        public GameObject Enemy1;
        public GameObject Enemy2;

        public Rigidbody enemyRigidBody;
        public Vector3 moveDirection;
        public Vector3 movement;

        StateManager states;

        public int health = 3;
        public int Edamage = 1;
        public int Pdamage = 1;

        private int limit = 0;
        private int limitP = 0;

        public bool spawnMore = true;

        public float LookRadius = 10f;

        NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            states = GameObject.FindObjectOfType<StateManager>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

            float dis = Vector3.Distance(states.character.transform.position, transform.position);

            if (dis <= LookRadius)
            {
                agent.SetDestination(states.character.transform.position);

                if (dis <= agent.stoppingDistance)
                {


                    FaceTarget();
                }
            }




            onDeath();
        }


        void FaceTarget()
        {
            Vector3 direction = (states.character.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        public void onDeath()
        {
            if (health == 0)
            {
                if (spawnMore)
                {
                    Enemy1 = Instantiate(Enemy1, spawn1.position, spawn1.rotation);
                    Enemy2 = Instantiate(Enemy2, spawn2.position, spawn2.rotation);
                    spawnMore = false;

                }

                Destroy(enemy);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.tag == "Weapon")
            {
                if(states.canAttack == true)
                {
                    if (limit < 1)
                    {
                        Debug.Log("1 hit");
                        health -= Pdamage;
                        Vector3 moveDirection = this.transform.position - states.character.transform.position;
                        enemyRigidBody.AddForce(moveDirection.normalized * 500f);
                        limit++;
                    }             
                                
                }
            }
            else
            {
                limit = 0;
            }


            if (other.transform.gameObject.tag == "Player")
            {
                if (limitP < 1)
                {
                    Debug.Log("daNGER");
                    Vector3 moveDirection = this.transform.position - states.character.transform.position;
                    enemyRigidBody.AddForce(moveDirection.normalized * 500f);
                    states.health -= Edamage;
                    limitP++;
                }
                  
            }
            else
            {
                limitP = 0;
            }
        }












    }

}
