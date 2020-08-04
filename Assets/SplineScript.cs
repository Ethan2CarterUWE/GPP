using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{


    
    public class SplineScript : MonoBehaviour
    {
        public GameObject maincamera;
        public GameObject pivotww;

        public GameObject camHolder;
        public Collider collider1;
        public bool StopAim = false;

        public bool okEthan = false;
     

        StateManager states;
        RailMover railmove;
        RailReturn railretur;
        CameraManager cameraMan;
        StateManager stateman;


        private int camLock = 0;
        // Start is called before the first frame update
        void Start()
        {
            states = GameObject.FindObjectOfType<StateManager>();
            railmove = GameObject.FindObjectOfType<RailMover>();
            railretur = GameObject.FindObjectOfType<RailReturn>();
            cameraMan = GameObject.FindObjectOfType<CameraManager>();
            stateman = GameObject.FindObjectOfType<StateManager>();


        }

        // Update is called once per frame
        void Update()
        {

            /*if (okEthan)
            {
                if (stateman.moveAmount >= 0.1f)
                {

                }
                else if (stateman.moveAmount == 0)
                {
                    maincamera.transform.parent = null;
                    camHolder.transform.parent = maincamera.transform;
                }
            }*/
       

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
                    okEthan = true;
                    cameraMan.stopMovement = true;
                 

                    //COMMENT THIS OUT FOR MOVEMENTSPLINECHECK
                   maincamera.transform.parent = null;
                    camHolder.transform.parent = maincamera.transform;

                    cameraMan.lookAngle = -90f;
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
                    
                    camHolder.transform.parent = null;
                    maincamera.transform.parent = pivotww.transform;
                    maincamera.GetComponent<RailMover>().enabled = false;
                    cameraMan.stopMovement = false;

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


