using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{


    
    public class SplineScript : MonoBehaviour
    {
        public GameObject maincamera;
        public Collider collider1;
        public bool test = false;
        public bool shit = false;

        StateManager states;


        // Start is called before the first frame update
        void Start()
        {
            states = GameObject.FindObjectOfType<StateManager>();

        }

        // Update is called once per frame
        void Update()
        {

        }

      /*  private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.tag == "Player")
            {
                Debug.Log("ntered");
            }
        }*/

        private void OnTriggerExit(Collider other)
        {
            if (test == true)
            {
                maincamera.GetComponent<RailMover>().enabled = false;
                test = false;
                Debug.Log("false");
                shit = false;

            }

            while (!shit)
            {
                if (other.transform.gameObject.tag == "Player")
                {

                    if (test == false)
                    {
                        maincamera.GetComponent<RailMover>().enabled = true;
                        test = true;
                        Debug.Log("true");

                        shit = true;

                    }
                }
           

              

             

              

                //GameObject.FindGameObjectWithTag("Main Camera").GetComponent
                Debug.Log("exit");
            }
        }










    }
}


