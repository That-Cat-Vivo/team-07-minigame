/* Ethan Gapic-Kott */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    public enum PowerupType { None, Barrier, Speed }

    public class Powerup : MonoBehaviour
    {
        [SerializeField] private PowerupType type = PowerupType.None;
        [SerializeField] private float rotateSpeed = 50f;

        public PowerupType Type => type;

        private void Update()
        {
            // Spin for visual feedback
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerPowerupHandler>();
            if (player != null)
            {
                player.CollectPowerup(type);
                Destroy(gameObject);
            }
        }
    }
}

