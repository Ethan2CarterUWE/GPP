using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class StateManager : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeModel;
        public GameObject hand;
        public GameObject Respawn;
        public GameObject character;


        [Header("Inputs")]
        public float vertical;
        public float horizontal;
        public float moveAmount;
        public Vector3 moveDir;

        public bool useGravity = true;
     

        [Header("Stats")]
        public float moveSpeed = 5;
        public float runSpeed = 3.5f;
        public float rotateSpeed = 5;
        public float toGround = 0.5f;
        public float toAir = 2;
        public float jumpAmount = 0;
        public int health = 3;
        private int healthSave = 0;

        [Header("States")]
        public bool run;
        public bool onGround;
        public bool inAttack = false;
        public bool inInteract = false;
        public bool inCutscene = false;
        public bool inNESWcam = false;
        public ParticleSystem doublePS;
        public ParticleSystem fastPS;

        [Header("Camera")]
        public bool camLeft = false;
        public bool camRight = false;

        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody rigid;
        [HideInInspector] public float delta;

        [HideInInspector] public LayerMask ignoreLayers;


        public void Init()
        {
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);

            anim.SetBool("onGround", true);
            prevGround = true;

             healthSave = health;

           doublePS.enableEmission = false;
            fastPS.enableEmission = false;

        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                transform.parent = other.transform;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                transform.parent = null;
            }
        }


        void SetupAnimator()
        {
                if (activeModel == null)
                {
                    anim = GetComponent<Animator>();
                    if (anim == null)
                    {
                        Debug.Log("No model found");
                    }
                    else
                    {
                        activeModel = anim.gameObject;
                    }
                }

                if (anim == null)
                    anim = activeModel.GetComponent<Animator>();
                anim.applyRootMotion = false;
            
        }
        public void FixedTick(float d)
        {

            // gravity 
            rigid.useGravity = false;
            if (useGravity) rigid.AddForce(Physics.gravity * (rigid.mass * rigid.mass));

            delta = d;
            if (!inCutscene)
            { 

                if (moveAmount > 0 || onGround == false)
                {
                   rigid.drag = 0;
                }
                else
                {
                  // rigid.drag = 4;
                }

                float targetSpeed = moveSpeed;
                if (run)
                    targetSpeed = runSpeed;


                //movement
                if (onGround)
                    rigid.velocity = moveDir * (targetSpeed * moveAmount);
             


                


               // rigid.velocity =  moveDir * (targetSpeed * -moveAmount);
               //rigid.useGravity

                /*if (!onGround)
                    rigid.velocity = moveDir * (targetSpeed * moveAmount);*/


                jumping();
                NESWcamera();
                



                //rotates the player model
                Vector3 targetDir = moveDir;
                targetDir.y = 0;
                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
                transform.rotation = targetRotation;
                HandleMovementAnimation();
            }
            attacking();

            interacting();
            Death();
        }

        void Death()
        {


            if (health == 0)
            {

                character.transform.position = Respawn.transform.position;
                health = healthSave;

            }





        }


        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
            anim.SetBool("onGround", onGround);
        }

        void HandleMovementAnimation()
        {
            anim.SetFloat("vertical", moveAmount, 0.4f,delta);
        }
        
        float attackTimer = 0f;
        public bool canAttack = false;

        void attacking()
        {
            // if canattack is true, allow damage to be dealt

           if (canAttack)
            {
                attackTimer += delta;
                //attacktimer not used
                if (attackTimer > 0.5f)
                {
                    attackTimer += delta;
                    Debug.Log("ataccking");
                    inAttack = true;
                    canAttack = false;
                }
              

            }

            if (inAttack)
            {
                anim.SetBool("attack", false);
                Debug.Log("finished");
                attackTimer = 0;
                inAttack = false;
            }
            if (!inAttack)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetBool("attack", true);
                    Debug.Log("startedattac");
                    canAttack = true;
                }
            }
        


        }

        void cutscene()
        {
            //freezes animations for touching button
            anim.speed = 0;
            //freezes position of character
            rigid.constraints = RigidbodyConstraints.FreezePosition;

        }

        void cutsceneExit()
        {
            anim.speed = 1;
        }
        float interactTimer = 0f;

        public bool canInteract = false;
        void interacting()
        {
            if (canInteract)
            {
                interactTimer += delta;

                if (interactTimer > 0.5f)
                {
                    if (inCutscene)
                    {
                        cutscene();
                    }
                    else
                    {
                        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                        cutsceneExit();
                        Debug.Log("checkign");
                        inInteract = true;
                        canInteract = false;
                    }           
                }
            }

            if (inInteract)
            {
               anim.SetBool("interact", false);
                Debug.Log("interactDone");
                interactTimer = 0;
                inInteract = false;
                //use delay
            }
            if (!inInteract && onGround)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {     
                    anim.SetBool("interact", true);
                    Debug.Log("Interacting");
                    canInteract = true;
                }
            }
        }


        float cameraTimer = 0f;
        bool timerFin = false;
        void NESWcamera()
        {        
            if (Input.GetKeyDown(KeyCode.J) /* add controller button*/ )
            {
                inNESWcam = true;
            }

            if (inNESWcam)
            {
                //Debug.Log("incam");
                cameraTimer += delta;

                if (cameraTimer > 0.5f)
                {
                   // Debug.Log("timer");

                    if (Input.GetKeyDown(KeyCode.J) /* add controller button*/ )
                    {
                        inNESWcam = false;
                        timerFin = true;
                    }
                        
                }                
            }

            if (timerFin)
            {
                //Debug.Log("timefin");
                cameraTimer = 0f;
                timerFin = false;

            }

            /*if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                camRight = true;
            }
            else
            {

            }*/




        }





        public bool doubleJump = false;


        void jumping()
        {

            //if (jumpAmount == 1 && !onGround)
            if (doubleJump && !onGround && jumpAmount < 2)
            {
                if (Input.GetKeyDown(KeyCode.Space) )
                {
                    anim.SetBool("Jumping", true);


                    if (transform.position.y < 0.5)
                    {
                        Debug.Log("broke2");

                    }
                    if (transform.position.y > 0.5)
                    {
                        Debug.Log("doublejump");

                    }

                    skipGroundCheck = true;
                    Vector3 targetVel = transform.forward * 5;
                    //Vector3 targetVel = transform.forward * 7;

                    targetVel.y = 6;
                    rigid.velocity = targetVel;
                    jumpAmount++;

                }
            }



            if (onGround)
            {
                jumpAmount = 0;

                if (Input.GetKeyDown(KeyCode.Space) )
                {
                    anim.SetBool("Jumping", true);


                    if (transform.position.y < 0.5)
                    {
                        Debug.Log("ohwow");

                    }
                    if (transform.position.y > 0.5)
                    {
                        Debug.Log("jump");

                    }

                    skipGroundCheck = true;
                    //Vector3 targetVel = transform.forward * 7;
                    Vector3 targetVel = transform.forward * 5;

                    targetVel.y = 6;
                   rigid.velocity = targetVel;
                    //rigid.velocity = moveDir * (targetSpeed * moveAmount);
                    jumpAmount++;

                }
            }

        }


        bool skipGroundCheck;
        float skipTimer;
        bool prevGround;
        public bool OnGround()
        {
            bool r = false;

            if (skipGroundCheck)
            {
                anim.SetBool("Jumping", false);
                skipTimer += delta;
                if (skipTimer > 0.2f)
                    skipGroundCheck = false;

                prevGround = false;
                return false;
            }
            skipTimer = 0;

            Vector3 orign = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.1f;

            RaycastHit hit;
           

            if(Physics.Raycast(orign,dir,out hit, dis, ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
            }

            if (r && !prevGround)
            {

            }
            prevGround = r;
            return r;
        }

        public void Jump()
        {

        }


        public void FootR()
        {
        }

        public void FootL()
        {
        }

        public void Land()
        {
        }

        public void Hit()
        {
        }

    }
}