/* Ethan Gapic-Kott */

using System.Collections;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    public class PlayerController : MiniGameBehaviour
    {
        [field: Header("Player Settings")]
        [field: SerializeField] private int playerID = 1;
        [field: SerializeField] private float moveSpeed = 6f;
        [field: SerializeField] private Rigidbody2D rb;

        [field: SerializeField] private bool canMove;
        [field: SerializeField] private bool attackOnCooldown;
        [field: SerializeField] private Vector2 movement;

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
                float axisX = ArcadeInput.Players[(int)playerID].AxisX;
                float axisY = ArcadeInput.Players[(int)playerID].AxisY;
                float movementX = axisX * Time.deltaTime * moveSpeed;
                float movementY = axisY * Time.deltaTime * moveSpeed;
                Vector3 newPosition = transform.position + new Vector3(movementX, movementY, 0);
                if (!canMove) return;
                rb.MovePosition(newPosition);
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
