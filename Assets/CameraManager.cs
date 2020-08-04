using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class CameraManager : MonoBehaviour
    {
        public bool lockon;

        public float followSpeed = 9;
        public float mouseSpeed = 2;
        public float controllerSpeed = 4;

        public Transform target;

         public Transform pivot;
        public Transform camTrans;

        
        float turnSmoothing = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        float smoothX;
        float smoothY;
        float smoothXvelocity;
        float smoothYvelocity;
        public float lookAngle;
        public float tiltAngle;
        public float leftAngle = -90f;

        StateManager states;

        Quaternion from;
        Quaternion to;
        //public float someshitidk;
        private float turningTime = 0f;

        bool lerp = false;


        public bool stopMovement = false;

        private void Start()
        {
        


        }
        private void Update()
        {
           
         
        }



        public void Init(Transform t)
        {
            target = t;
            camTrans = Camera.main.transform;
            pivot = camTrans.parent;
            states = GameObject.FindObjectOfType<StateManager>();

            transform.rotation = Quaternion.identity;
            //Transform from = lookAngle;
        }

     
        public void Tick(float d)
        {

            if (!stopMovement)
            {
                //mouse
                float h = Input.GetAxis("Mouse X");
                float v = Input.GetAxis("Mouse Y");

                //controller
                float c_h = Input.GetAxis("RightAxis X");
                float c_v = Input.GetAxis("RightAxis Y");

                float targetSpeed = mouseSpeed;

                if (c_h != 0 || c_v != 0)
                {
                    h = c_h;
                    v = c_v;
                    targetSpeed = controllerSpeed;
                }
                HandleRotation(d, v, h, targetSpeed);
            }

            transform.rotation = Quaternion.Euler(0, lookAngle, 0);
            FollowTarget(d);


          
        }

        void FollowTarget(float d)
        {
            //follows the player
            float speed = d * followSpeed;
            Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = targetPosition;
        }
        bool testing1 = false;
        void HandleRotation(float d, float v, float h, float targetSpeed)
        {
            if(turnSmoothing > 0)
            {

                
                


                
                
                
                  smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing);
                    smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
                    testing1 = false;
                
                

            }
            else
            {
                /*  smoothX = h;
                  smoothY = v;*/

                  smoothX = h;
                  smoothY = v;

            }

       

            //lookangle is the actual angle of the camera, ie add/suubtract 90
            //lookAngle += smoothX * targetSpeed;
            //transform.rotation = Quaternion.Euler(0, lookAngle, 0);
            //lookangle is the actual angle of the camera, ie add/suubtract 90
            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
        }


  
        public static CameraManager singleton;
        void Awake()
        {
            singleton = this;
        }


    }


}

