using System.Collections.Generic;
using UnityEngine;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Manages a player's collected ligands, points, and achievements
    /// </summary>
    [System.Serializable]
    public class PlayerInventory
    {
        [Header("Owner")]
        public Player owner;

        [Header("Collected Items")]
        public List<LigandData> collectedLigands = new List<LigandData>();
        public List<QuestionData> answeredQuestions = new List<QuestionData>();

        [Header("Scores")]
        public int totalScore = 0;
        public int ligandScore = 0;
        public int questionScore = 0;
        public int fateScore = 0;

        [Header("Stats")]
        public int correctAnswers = 0;
        public int wrongAnswers = 0;
        public int ligandsCollected = 0;
        public int fateCardsTriggered = 0;

        [Header("Special Effects")]
        public bool hasShield = false;  // Immunity to next fate card
        public bool skipNextTurn = false;

        /// <summary>
        /// Initialize inventory for a player
        /// </summary>
        public PlayerInventory(Player player)
        {
            owner = player;
        }

        /// <summary>
        /// Add a ligand to inventory
        /// </summary>
        public void AddLigand(LigandData ligand)
        {
            if (ligand == null) return;

            collectedLigands.Add(ligand);
            ligandsCollected++;

            // Score = denticity value
            int points = ligand.GetScoreValue();
            ligandScore += points;
            totalScore += points;

            Debug.Log($"Player {owner.playerIndex} collected {ligand.ligandName} (+{points} pts)");

            // Update UI - TODO: Implement when UIManager integration is ready
            // if (UIManager.Instance != null)
            // {
            //     UIManager.Instance.UpdatePlayerInventory(owner.playerIndex);
            // }
        }

        /// <summary>
        /// Add points from question answer
        /// </summary>
        public void AddQuestionScore(QuestionData question, bool correct)
        {
            if (question == null) return;

            answeredQuestions.Add(question);

            if (correct)
            {
                correctAnswers++;
                int points = question.points;
                questionScore += points;
                totalScore += points;

                Debug.Log($"Player {owner.playerIndex} answered correctly! (+{points} pts)");
            }
            else
            {
                wrongAnswers++;
                Debug.Log($"Player {owner.playerIndex} answered wrong. No points.");
            }
        }

        /// <summary>
        /// Add or subtract points from fate card
        /// </summary>
        public void AddFatePoints(int points)
        {
            fateScore += points;
            totalScore += points;

            if (points > 0)
                Debug.Log($"Player {owner.playerIndex} gained {points} pts from fate!");
            else
                Debug.Log($"Player {owner.playerIndex} lost {-points} pts from fate!");
        }

        /// <summary>
        /// Check if player has collected a specific ligand
        /// </summary>
        public bool HasLigand(string ligandID)
        {
            return collectedLigands.Exists(l => l.ligandID == ligandID);
        }

        /// <summary>
        /// Count ligands of specific denticity
        /// </summary>
        public int CountLigandsByDenticity(int denticityValue)
        {
            return collectedLigands.FindAll(l => l.denticityValue == denticityValue).Count;
        }

        /// <summary>
        /// Get monodentate ligand count
        /// </summary>
        public int GetMonodentateCount()
        {
            return CountLigandsByDenticity(1);
        }

        /// <summary>
        /// Get bidentate ligand count
        /// </summary>
        public int GetBidentateCount()
        {
            return CountLigandsByDenticity(2);
        }

        /// <summary>
        /// Get win score (combination of all sources)
        /// </summary>
        public int GetWinScore()
        {
            return totalScore;
        }

        /// <summary>
        /// Get formatted inventory summary for UI
        /// </summary>
        public string GetInventorySummary()
        {
            return $"Score: {totalScore}\n" +
                   $"Ligands: {ligandsCollected}\n" +
                   $"Correct: {correctAnswers}\n" +
                   $"Wrong: {wrongAnswers}";
        }

        /// <summary>
        /// Reset inventory (for new game)
        /// </summary>
        public void Reset()
        {
            collectedLigands.Clear();
            answeredQuestions.Clear();
            totalScore = 0;
            ligandScore = 0;
            questionScore = 0;
            fateScore = 0;
            correctAnswers = 0;
            wrongAnswers = 0;
            ligandsCollected = 0;
            fateCardsTriggered = 0;
            hasShield = false;
            skipNextTurn = false;
        }
    }
}
