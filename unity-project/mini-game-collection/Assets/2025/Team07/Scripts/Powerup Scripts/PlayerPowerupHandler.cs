/* Ethan Gapic-Kott */

using System.Collections;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerPowerupHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject barrierPrefab;
        [SerializeField] private Rigidbody2D rb;

        [Header("Settings")]
        [SerializeField] private float speedBoostMultiplier = 2f;
        [SerializeField] private float speedBoostDuration = 5f;
        [SerializeField] private float barrierDuration = 7f;

        private PowerupType currentPowerup = PowerupType.None;
        private PlayerController controller;
        private bool powerupActive;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            if (rb == null) rb = GetComponent<Rigidbody2D>();
        }

        public void CollectPowerup(PowerupType type)
        {
            currentPowerup = type;
        }

        private void Update()
        {
            if (currentPowerup == PowerupType.None || powerupActive)
                return;

            if (Input.GetKeyDown(controller.PlayerID() == 1 ? KeyCode.E : KeyCode.RightControl))
            {
                ActivatePowerup();
            }
        }

        private void ActivatePowerup()
        {
            switch (currentPowerup)
            {
                case PowerupType.Barrier:
                    PlaceBarrier();
                    break;
                case PowerupType.Speed:
                    StartCoroutine(SpeedBoost());
                    break;
            }

            currentPowerup = PowerupType.None;
        }

        private void PlaceBarrier()
        {
            if (barrierPrefab != null)
            {
                GameObject barrier = Instantiate(barrierPrefab, transform.position + transform.up * 1.5f, Quaternion.identity);
                Destroy(barrier, barrierDuration); // Despawn after 7 seconds
            }
        }

        private IEnumerator SpeedBoost()
        {
            powerupActive = true;

            float originalSpeed = controller.GetMoveSpeed();
            controller.SetMoveSpeed(originalSpeed * speedBoostMultiplier); // Double speed

            yield return new WaitForSeconds(speedBoostDuration);

            controller.SetMoveSpeed(originalSpeed);
            powerupActive = false;
        }
    }
}
