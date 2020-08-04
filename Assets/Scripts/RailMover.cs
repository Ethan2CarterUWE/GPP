using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class RailMover : MonoBehaviour
    {
        public Rail rail;
        public Transform lookAt;
        public bool smoothMove = true;

        public bool exitTrue = false;
        public GameObject pivot;

        private Transform thisTransform;
        private Vector3 lastPosition;

        private void Start()
        {
            thisTransform = transform;
            lastPosition = thisTransform.position;
        }

        //FixedUpdate over Update so that the camera does not wobble
        private void FixedUpdate()
        {




            //smooths the transitions of the camera between points
            //EITHER HAVE THIS ONE OR THE ONE ABOVE WHICH ISNT AS SMOOTH BUT ITS CLEANER
            if (!exitTrue)
            {
                if (smoothMove)
                {
                    thisTransform.position = lastPosition;
                    //lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(lookAt.position), Time.deltaTime);
                    // change multiplier on the end if you want faster cam
                    lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(lookAt.position), Time.deltaTime * 4);
                }
                else
                {
                    thisTransform.position = rail.ProjectPositionOnRail(lookAt.position);
                }

                thisTransform.LookAt(lookAt.position);
            }
         



        }

    }
}


