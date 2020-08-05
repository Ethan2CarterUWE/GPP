using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class LAVARESPA : MonoBehaviour
    {

        public GameObject Player;
        public GameObject Respawn;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerExit(Collider other)
        {
           if (other.transform.gameObject.tag == "Player")
            {
                Debug.Log("ENTERED");
                Player.transform.position = Respawn.transform.position;


            }
        }
    }
}

