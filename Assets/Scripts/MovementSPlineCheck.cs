using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class MovementSPlineCheck : MonoBehaviour
    {

        public GameObject maincamera;
        public GameObject pivotww;
        public GameObject camHolder;

        public bool yesman = true;

        SplineScript splinscru;
        StateManager statemen;

        // Start is called before the first frame update
        void Start()
        {
            splinscru = GameObject.FindObjectOfType<SplineScript>();

            statemen = GameObject.FindObjectOfType<StateManager>();

        }

        // Update is called once per frame
        void Update()
        {
            /*if (splinscru.okEthan)
            {
                Debug.Log("in functuin");
                if (statemen.moveAmount >= 0.9f && yesman)
                {
                    Debug.Log("yesman is true");

                }
                else
                {
                    maincamera.transform.parent = null;
                    camHolder.transform.parent = maincamera.transform;
                    yesman = false;

                    Debug.Log("working");

                }
            }*/
        }
    }

}

