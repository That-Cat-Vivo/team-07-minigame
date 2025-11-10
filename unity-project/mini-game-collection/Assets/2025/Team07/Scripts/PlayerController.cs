/* Ethan Gapic-Kott */

using System.Collections;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    public class PlayerController : MiniGameBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private int playerID = 1;
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private Rigidbody2D rb;

        private bool canMove;
        private bool attackOnCooldown;
        private Vector2 movement;

        protected override void OnGameStart()
        {
            base.OnGameStart();
            canMove = true;
            if (rb != null) rb.freezeRotation = true;
        }

        protected override void OnGameEnd()
        {
            base.OnGameEnd();
            canMove = false;
        }

        private void Update()
        {
            if (!canMove) return;

            // Player movement
            movement = Vector2.zero;

            if (playerID == 1)
            {
                if (Input.GetKey(KeyCode.A)) movement.x = -1f;
                if (Input.GetKey(KeyCode.D) && transform.position.x < -0.5) movement.x = 1f;
                if (Input.GetKey(KeyCode.W)) movement.y = 1f;
                if (Input.GetKey(KeyCode.S)) movement.y = -1f;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > 0.5) movement.x = -1f;
                if (Input.GetKey(KeyCode.RightArrow)) movement.x = 1f;
                if (Input.GetKey(KeyCode.UpArrow)) movement.y = 1f;
                if (Input.GetKey(KeyCode.DownArrow)) movement.y = -1f;
            }

            movement = movement.normalized;
        }

        private void FixedUpdate()
        {
            if (!canMove) return;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        public void SetCanMove(bool value)
        {
            canMove = value;
            if (!value && rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        public int PlayerID() => playerID;
    }
}
