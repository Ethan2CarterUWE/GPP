using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SA
{
    public class RailReturn : MonoBehaviour
    {
        public Rail rail;
        public Transform lookAt;
        public bool smoothMove = true;

        public bool exitTrue = false;
        public GameObject pivot;
        public GameObject target;


        private Transform thisTransform;
        private Vector3 lastPosition;
        private Vector3 originalPosition;
        //private Vector3 finalPos;


        bool check1 = false;
        bool check2 = false;
        bool check3 = false;

        int jww = 0;


        public float speed = 2.0f;
       public Vector3 currentEulerAngles;
        Quaternion currentRotation;


        private void Start()
        {
            thisTransform = transform;
            lastPosition = thisTransform.position;
            Debug.Log("fuckoff");
            originalPosition = pivot.transform.position;
            //originalPosition[0] = [0];
            //new Vector3(0, 0.5, -2);
        }

        //FixedUpdate over Update so that the camera does not wobble
        private void LateUpdate()
        {
            Vector3 pos = new Vector3(0.0f, 0.56f, -2.860001f);
            if (exitTrue)
            {
                pivot.transform.position = Vector3.Lerp(pivot.transform.position, target.transform.position, Time.deltaTime * speed);
                pivot.transform.rotation = target.transform.rotation;




                if (pivot.transform.position == target.transform.position)
                {
                    check1 = true;
                    Debug.Log("check1");

                }
                if (pivot.transform.rotation == target.transform.rotation)
                {
                    check2 = true;
                    Debug.Log("check2");

                }



                // pivot.transform.rotation = currentRotation;
                //Debug.Log(currentRotation);

                //when target is reached, end it
                // Debug.Log("wot");
            }

            if (check1 && check2)
            {

                exitTrue = false;
                check1 = false;
                check2 = false;
            }

        }
        private void FixedUpdate()
        {
   
        }

    }
}


