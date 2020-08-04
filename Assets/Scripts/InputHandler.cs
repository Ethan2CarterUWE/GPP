using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        public bool test;
        float vertical;
        float horizontal;
        //float jumping;

        StateManager states;
        CameraManager camManager;

        float delta;


        void Start()
        {
            states = GetComponent<StateManager>();
            states.Init();

            camManager = CameraManager.singleton;
            camManager.Init(this.transform);
             
        }

       
        private void FixedUpdate()
        {
            delta = Time.deltaTime;
            GetInput();
            UpdateStates();
            states.FixedTick(delta);
            //camManager.


        }


        private void LateUpdate()
        {
            camManager.Tick(delta);

        }
        private void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);
        }

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            //jumping = Input.GetAxis("Jump");
            //jumping

        }

        void UpdateStates()
        {   
            //
            //Movement
            //

            states.horizontal = horizontal;
            states.vertical = vertical;

            
            // put incutscene if statement to stop movement
            if (!test)
            {
                Vector3 v = vertical * camManager.transform.forward;
                Vector3 h = horizontal * camManager.transform.right;

                states.moveDir = (v + h).normalized;
                float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                states.moveAmount = Mathf.Clamp01(m);
            }
            else
            {
                /*Vector3 v = vertical * 1;
                Vector3 h = horizontal;*/
            }
            
            


        }





    }
}


