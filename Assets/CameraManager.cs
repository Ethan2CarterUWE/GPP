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

        [HideInInspector] public Transform pivot;
        [HideInInspector] public Transform camTrans;

        
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


        private void Start()
        {
        


        }
        private void Update()
        {
            if(states.inNESWcam)
            {
                if (lerp)
                {
                   // lookAngle = lookAngle - leftAngle;



                    from = transform.rotation;
                    to = Quaternion.Euler(0, -90, 0);

                    transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * 10);

                    if (lookAngle >= 5500)
lerp = false;

                    //lookAngle -= 90;
                    //lookAngle = Quaternion.Euler(lookAngle, , Time.deltaTime * 1);
                    /* Quaternion wantedRotation = Quaternion.Euler(0, lookAngle, 0);
                     transform.rotation = Quaternion.Lerp(transform.ro, wantedRotation, Time.deltaTime * 3);*/
                }
            }
         
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
            //mouse
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            //controller
            //float c_h = Input.GetAxis("RightAxis X");
           // float c_v = Input.GetAxis("RightAxis Y");

            float targetSpeed = mouseSpeed;

            /*if (c_h != 0 || c_v != 0)
            {
                h = c_h;
                v = c_v;
                targetSpeed = controllerSpeed;
            }*/

            FollowTarget(d);
            HandleRotation(d, v, h, targetSpeed);
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

                //boolean to stop player moving camera.
                //Move This camera to where the other one starts and freeze mouse movement 

                //to addminus 90, freeze this, change lookangle , tilt angle and move pivotcamerathing up
                /*
                 * if counter != 1
                 *      if input == 1
                 *              addremove 90
                 *              counter++
                 *              
                 *if counter == 1 && input != 1
                 *      counter = 0
                */
                if (states.inNESWcam)
                {
                  /*  smoothX = 0;
                    smoothY = 0;*/
                    Debug.Log("chicken");
                    if (!testing1)
                    {                        
                        lookAngle = 0;

                       if (lookAngle == 0)
                        {
                            testing1 = true;
                        }

                       
                    }

                    if (states.inNESWcam)
                    {
                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            lerp = true;


                            //lookAngle -= 90;
                            //lookAngle = Quaternion.Euler(lookAngle, , Time.deltaTime * 1);
                            /* Quaternion wantedRotation = Quaternion.Euler(0, lookAngle, 0);
                             transform.rotation = Quaternion.Lerp(transform.ro, wantedRotation, Time.deltaTime * 3);*/
                        }
                    }

                    /* if (Input.GetKeyUp(KeyCode.LeftArrow))
                     {
                          lookAngle = lookAngle - leftAngle;



                         from = transform.rotation;
                         to = Quaternion.Euler(0, -90, 0);

                         transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * 1);*/


                    //lookAngle -= 90;
                    //lookAngle = Quaternion.Euler(lookAngle, , Time.deltaTime * 1);
                    /* Quaternion wantedRotation = Quaternion.Euler(0, lookAngle, 0);
                     transform.rotation = Quaternion.Lerp(transform.ro, wantedRotation, Time.deltaTime * 3);*/
                    // }

                }
                else
                {
                   smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing);
                    smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
                    testing1 = false;
                }
                

            }
            else
            {
                /*  smoothX = h;
                  smoothY = v;*/

                  smoothX = h;
                  smoothY = v;

            }

            if (lockon)
            {

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

