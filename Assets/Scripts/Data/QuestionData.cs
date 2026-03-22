using UnityEngine;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// ScriptableObject for storing chemistry question card data
    /// Based on COOR-CHEM Smart Board game questions
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "COOR-CHEM/Question Card", order = 2)]
    public class QuestionData : ScriptableObject
    {
        [Header("Question Info")]
        [Tooltip("Unique identifier (e.g., 'q1', 'q2')")]
        public string questionID;

        [Tooltip("Difficulty level")]
        public DifficultyLevel difficulty = DifficultyLevel.Easy;

        [Tooltip("Points awarded for correct answer")]
        [Range(1, 10)]
        public int points = 3;

        [Header("Question Content")]
        [TextArea(3, 5)]
        [Tooltip("The question text")]
        public string questionText;

        [Tooltip("Four answer options (A, B, C, D)")]
        public string[] answerOptions = new string[4];

        [Tooltip("Index of correct answer (0-3)")]
        [Range(0, 3)]
        public int correctAnswerIndex = 0;

        [TextArea(2, 4)]
        [Tooltip("Hint or explanation for the answer")]
        public string hint;

        /// <summary>
        /// Check if selected answer is correct
        /// </summary>
        public bool IsCorrect(int selectedIndex)
        {
            return selectedIndex == correctAnswerIndex;
        }

        /// <summary>
        /// Get the correct answer text
        /// </summary>
        public string GetCorrectAnswer()
        {
            if (correctAnswerIndex >= 0 && correctAnswerIndex < answerOptions.Length)
                return answerOptions[correctAnswerIndex];
            return "";
        }

        /// <summary>
        /// Get color based on difficulty
        /// </summary>
        public Color GetDifficultyColor()
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return new Color(0.3f, 0.69f, 0.31f); // #4CAF50 green
                case DifficultyLevel.Medium:
                    return new Color(1f, 0.6f, 0f); // #FF9800 orange
                case DifficultyLevel.Hard:
                    return new Color(0.96f, 0.26f, 0.21f); // #f44336 red
                default:
                    return Color.white;
            }
        }

        /// <summary>
        /// Get points based on difficulty (auto-assign)
        /// </summary>
        public int GetPoints()
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return 3;
                case DifficultyLevel.Medium:
                    return 4;
                case DifficultyLevel.Hard:
                    return 5;
                default:
                    return 3;
            }
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}
