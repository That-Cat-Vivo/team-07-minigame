/* Ethan Gapic-Kott */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{

    // Adds score when the puck collides with a goal
    public class GoalTrigger : MonoBehaviour
    {
        [SerializeField] private int scoringPlayer = 1;
        [SerializeField] private GameManager hockeyManager;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Puck"))
            {
                hockeyManager.AddScore(scoringPlayer);
            }
        }
    }
}

