using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class Collectible : MonoBehaviour
    {




        public bool doubleJ = false;
        public bool superF = false;

        public float amplitude = 1;          //Set in Inspector 
        public float speed = 1;                  //Set in Inspector 
        private float tempVal;
        private Vector3 tempPos;
        public float Timer = 15;
        StateManager states;
        public ParticleSystem PS;

        public bool isitEgg = false;


        void Start()
        {
            states = GameObject.FindObjectOfType<StateManager>();

            tempPos = transform.position; 
            tempVal = transform.position.y;
        }

        void Update()
        {
            //this makes the object move up and down
            tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
            transform.position = tempPos;

            //this makes the object rotate
            this.transform.Rotate(0, 50 * Time.deltaTime, 0);
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.gameObject.tag == "Player")
            {
                //states.doubleJump = true;

                if (isitEgg)
                {
                    states.Eggs++;
                    Debug.Log("egged");
                    Destroy(gameObject);
                }
                else;
                {
                    StartCoroutine(collectible(other));

                    GetComponent<MeshRenderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                }
                
            }
        }


        IEnumerator collectible(Collider other)
        {

            if (doubleJ)
            {
                states.doubleJump = true;
                PS.enableEmission = false;
                states.doublePS.enableEmission = true;
            }

            if (superF)
            {
                states.moveSpeed = 12;
                PS.enableEmission = false;
                states.fastPS.enableEmission = true;
            }

            yield return new WaitForSeconds(Timer);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;

            if (doubleJ)
            {
                states.doublePS.enableEmission = false;
                states.doubleJump = false;
                PS.enableEmission = true;
            }

            if (superF)
            {
                states.moveSpeed = 6;
                PS.enableEmission = true;
                states.fastPS.enableEmission = false;
            }
            /*  states.doublePS.enableEmission = false;
              states.doubleJump = false;
              PS.enableEmission = true;*/
        }







    }

}
