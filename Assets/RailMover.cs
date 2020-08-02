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

            //thisTransform.position = rail.ProjectPositionOnRail(lookAt.position);
            //smooths the transitions of the camera between points
            if (smoothMove)
            {
                thisTransform.position = lastPosition;
                lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(lookAt.position), Time.deltaTime);
            }
            else
            {
                thisTransform.position = rail.ProjectPositionOnRail(lookAt.position);
            }

            thisTransform.LookAt(lookAt.position);
        }

    }
}


