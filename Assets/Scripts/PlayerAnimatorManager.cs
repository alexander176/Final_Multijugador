using UnityEngine;
using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
namespace Com.Simba.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {

        #region Private Fields
        [SerializeField]
        private float directionDampTime = 1f;
        public float velocidadMovimiento = 5.0f;
        public float velocidadRotacion = 200.0f;
        private Animator animator;
        #endregion
        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }
            if (!animator)
            {
                return;
            }
            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))
            {
                // When using trigger parameter
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x < 0)
            {
                y = 0;
            }
            //transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            //transform.Translate(0,0,y*Time.deltaTime*velocidadMovimiento);
            animator.SetFloat("Speed", x * x + y * y);
            animator.SetFloat("Direction", x, directionDampTime, Time.deltaTime);
            //animator.SetFloat("VelX", x);
            //animator.SetFloat("VelY", y);
        }

        #endregion
    }


}