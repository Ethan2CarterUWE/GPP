using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Button : MonoBehaviour
    {
        int current = 0;

        public GameObject[] Door;
        public GameObject target;
        public GameObject standingTarget;

        public Camera cam;

        StateManager states;


        public bool isActive = false;
        public bool doorFinished = false;
        public float speed = 2.0f;
        public float rotate = 1.0f;

        public bool ActualFinish = false;

        private void Start()
        {
            states =  GameObject.FindObjectOfType<StateManager>();
            

            if (isActive)
                doormovement();
        }
        private void Update()
        {
            if (isActive)
            {
                doormovement();
            }
            if (doorFinished)
            {
                StartCoroutine(Waiting());

            }


        }

    

        private void OnTriggerEnter(Collider other)
        {
            if (!ActualFinish)
            {
                //checks if hand is in collider
                if (other.transform.gameObject.tag == "Hand")
                {
                    //checks if the hand is allowed to trigger 
                    if (states.canInteract == true)
                    {
                        //enable cutscene function
                        states.inCutscene = true;
                        if (states.inCutscene)
                        {
                            //sets perfect placement of the character for correct collision
                            //create a gameobject where player stands so that no manuel imput is required each time
                            // states.gameObject.transform.position = new Vector3(-11.57f, 0.5f, -6.14f);
                            // states.gameObject.transform.rotation = Quaternion.Euler(-0.014f, -182.006f, -0.032f);
                            states.gameObject.transform.position = standingTarget.transform.position;
                            states.gameObject.transform.rotation = standingTarget.transform.rotation;
                            isActive = true;
                        }
                    }
                }
           
            }
        }


        void doormovement()
        {
            if (!ActualFinish)
            {
                if (!doorFinished)
                {
                    cam.enabled = true;

                    if (isActive)
                        Door[current].transform.position = Vector3.MoveTowards(Door[current].transform.position, target.transform.position, Time.deltaTime * speed);

                    if (Door[current].transform.position == target.transform.position)
                    {

                        doorFinished = true;

                    }
                }
            }
            

         
        }
        private void OnTriggerStay(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }

        IEnumerator Waiting()
        {
            Debug.Log("preWait");
            yield return new WaitForSeconds(2);
            Debug.Log("AfterWait");
            isActive = false;
            doorFinished = false;
            states.inCutscene = false;
            cam.enabled = false;
            ActualFinish = true;
        }

    }

}

