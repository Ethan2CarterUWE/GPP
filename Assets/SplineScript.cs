using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{


    
    public class SplineScript : MonoBehaviour
    {
        public GameObject maincamera;
        public Collider collider1;
        public bool StopAim = false;
     

        StateManager states;
        RailMover railmove;
        RailReturn railretur;


        // Start is called before the first frame update
        void Start()
        {
            states = GameObject.FindObjectOfType<StateManager>();
            railmove = GameObject.FindObjectOfType<RailMover>();
            railretur = GameObject.FindObjectOfType<RailReturn>();

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
            

            if (other.transform.gameObject.tag == "Player")
            {
                Debug.Log("entering function");

                if (maincamera.GetComponent<RailMover>().enabled == false)
                 {
                    Debug.Log("enabling rail mover");
                    railretur.exitTrue = false;

                    maincamera.GetComponent<RailMover>().enabled = true;
                    
                         //               railmove.exitTrue = true;

                    //Enable a bool so i can use it to stop mouse movement in another script
                     //Debug.Log("false");
                 }
                 else
                 {
                    Debug.Log("disabling rail mover");

                    maincamera.GetComponent<RailMover>().enabled = false;
                    railretur.exitTrue = true;

                    //railmove.exitTrue = true;
                    // Debug.Log("true");

                }



                /* maincamera.GetComponent<RailMover>().enabled = true;
                    Debug.Log("true");*/
                // any more issues, just make 2 colliders for entry and exit

            }



         
        }








    }
}


