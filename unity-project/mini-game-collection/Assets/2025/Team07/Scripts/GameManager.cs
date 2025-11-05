/* Ethan Gapic-Kott */

using System.Collections;
using TMPro;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    public class GameManager : MiniGameBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private Rigidbody2D puckRb;
        [SerializeField] private Transform puckStartPos;
        [SerializeField] private Transform player1Spawn;
        [SerializeField] private Transform player2Spawn;
        [SerializeField] private MiniGameScoreUI scoreUI;
        [SerializeField] private TMP_Text goalMessage;

        private MiniGameManager miniGameManager;
        private bool gameRunning = true;

        private void Start()
        {
            miniGameManager = FindObjectOfType<MiniGameManager>();
            if (miniGameManager != null)
                miniGameManager.OnGameEnd += HandleGameEnd;

            if (goalMessage != null)
                goalMessage.gameObject.SetActive(false);
        }

        // Adds score to UI when a goal is scored
        public void AddScore(int playerID)
        {
            if (!gameRunning) return;
            scoreUI.IncrementPlayerScore(playerID);
            StartCoroutine(HandleGoalScored(playerID));
        }


        // Freeze gameplay when a goal is scored
        private IEnumerator HandleGoalScored(int scoringPlayer)
        {
            gameRunning = false;
            Time.timeScale = 0f;

            var players = FindObjectsOfType<PlayerController>();
            foreach (var p in players) p.SetCanMove(false);

            if (puckRb != null)
            {
                puckRb.velocity = Vector2.zero;
                puckRb.angularVelocity = 0f;
            }

            if (goalMessage != null)
            {
                goalMessage.text = $"PLAYER {scoringPlayer} SCORED!";
                goalMessage.gameObject.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(2f);
            if (goalMessage != null) goalMessage.gameObject.SetActive(false);

            // Resumes gameplay after time is unfrozen
            ResetPositions();
            yield return new WaitForSecondsRealtime(0.5f);

            Time.timeScale = 1f;
            foreach (var p in players) p.SetCanMove(true);

            gameRunning = true;
        }

        private void ResetPositions()
        {
            // Resets puck position
            if (puckRb != null && puckStartPos != null)
            {
                puckRb.velocity = Vector2.zero;
                puckRb.angularVelocity = 0f;
                puckRb.transform.position = puckStartPos.position;
                puckRb.position = puckStartPos.position;
                puckRb.rotation = 0f;
            }

            // Reset player positions
            var players = FindObjectsOfType<PlayerController>();
            foreach (var player in players)
            {
                int id = (int)typeof(PlayerController)
                    .GetField("playerID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .GetValue(player);

                Vector3 spawn = id == 1 ? player1Spawn.position :
                                id == 2 ? player2Spawn.position : Vector3.zero;

                player.transform.position = spawn;

                var rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.position = spawn;
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
        }

        private void HandleGameEnd()
        {
            // Decides the winner based on score
            if (scoreUI.PlayerScores[0] > scoreUI.PlayerScores[1])
                miniGameManager.Winner = MiniGameWinner.Player1;
            else if (scoreUI.PlayerScores[1] > scoreUI.PlayerScores[0])
                miniGameManager.Winner = MiniGameWinner.Player2;
            else
                miniGameManager.Winner = MiniGameWinner.Draw;
        }
    }
}
