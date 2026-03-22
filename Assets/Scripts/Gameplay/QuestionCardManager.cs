using UnityEngine;
using System.Collections.Generic;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Manages chemistry question cards, quiz UI, and scoring
    /// </summary>
    public class QuestionCardManager : MonoBehaviour
    {
        public static QuestionCardManager Instance;

        [Header("Question Data")]
        [Tooltip("All available questions (load from Resources/Questions)")]
        public QuestionData[] allQuestions;

        [Header("Question Pool")]
        private List<QuestionData> easyQuestions = new List<QuestionData>();
        private List<QuestionData> mediumQuestions = new List<QuestionData>();
        private List<QuestionData> hardQuestions = new List<QuestionData>();

        [Header("Audio")]
        public AudioClip sfxCorrectAnswer;
        public AudioClip sfxWrongAnswer;

        private Player currentPlayer;
        private QuestionData currentQuestion;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadQuestions();
            OrganizeQuestionsByDifficulty();
        }

        /// <summary>
        /// Load all question assets from Resources/Questions
        /// </summary>
        private void LoadQuestions()
        {
            allQuestions = Resources.LoadAll<QuestionData>("Questions");
            Debug.Log($"Loaded {allQuestions.Length} questions");

            if (allQuestions.Length == 0)
            {
                Debug.LogWarning("No questions found in Resources/Questions! Create question assets first.");
            }
        }

        /// <summary>
        /// Organize questions by difficulty level
        /// </summary>
        private void OrganizeQuestionsByDifficulty()
        {
            easyQuestions.Clear();
            mediumQuestions.Clear();
            hardQuestions.Clear();

            foreach (QuestionData q in allQuestions)
            {
                switch (q.difficulty)
                {
                    case DifficultyLevel.Easy:
                        easyQuestions.Add(q);
                        break;
                    case DifficultyLevel.Medium:
                        mediumQuestions.Add(q);
                        break;
                    case DifficultyLevel.Hard:
                        hardQuestions.Add(q);
                        break;
                }
            }

            Debug.Log($"Questions organized: {easyQuestions.Count} easy, {mediumQuestions.Count} medium, {hardQuestions.Count} hard");
        }

        /// <summary>
        /// Show question to player (pause game, show UI)
        /// </summary>
        public void ShowQuestion(Player player, QuestionData question)
        {
            if (player == null || question == null) return;

            currentPlayer = player;
            currentQuestion = question;

            Debug.Log($"📝 Question for {player.name}: {question.questionText}");

            // Pause game input
            MatchManager.InputAllowed = false;

            // Show question UI panel
            // TODO: Show UI popup with 4 answer options
            // if (UIManager.Instance != null)
            // {
            //     UIManager.Instance.ShowQuestionPanel(question, OnAnswerSelected);
            // }

            // TEMP: Auto-answer for testing (remove in production)
            // StartCoroutine(AutoAnswerForTesting());
        }

        /// <summary>
        /// Called when player selects an answer
        /// </summary>
        public void OnAnswerSelected(int selectedIndex)
        {
            if (currentQuestion == null || currentPlayer == null) return;

            bool isCorrect = currentQuestion.IsCorrect(selectedIndex);

            if (isCorrect)
            {
                // Correct answer
                if (sfxCorrectAnswer)
                    SanUtils.PlaySound(sfxCorrectAnswer);

                Debug.Log($"✅ Correct! +{currentQuestion.points} points");
                currentPlayer.inventory.AddQuestionScore(currentQuestion, true);
            }
            else
            {
                // Wrong answer
                if (sfxWrongAnswer)
                    SanUtils.PlaySound(sfxWrongAnswer);

                Debug.Log($"❌ Wrong! Correct answer: {currentQuestion.GetCorrectAnswer()}");
                currentPlayer.inventory.AddQuestionScore(currentQuestion, false);
            }

            // Show result UI with hint/explanation
            ShowQuestionResult(isCorrect);

            // Resume game after 2 seconds
            Invoke(nameof(ResumeGame), 2f);
        }

        /// <summary>
        /// Show question result UI
        /// </summary>
        private void ShowQuestionResult(bool correct)
        {
            string result = correct ? "✅ CORRECT!" : "❌ WRONG!";
            Debug.Log($"{result}\nHint: {currentQuestion.hint}");

            // TODO: Show result popup
            // if (UIManager.Instance != null)
            // {
            //     UIManager.Instance.ShowQuestionResult(correct, currentQuestion);
            // }
        }

        /// <summary>
        /// Resume game after question
        /// </summary>
        private void ResumeGame()
        {
            MatchManager.InputAllowed = true;
            MatchManager.Instance.NextTurn();
        }

        /// <summary>
        /// Get random question by difficulty
        /// </summary>
        public QuestionData GetRandomQuestion(DifficultyLevel difficulty)
        {
            List<QuestionData> pool = null;

            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    pool = easyQuestions;
                    break;
                case DifficultyLevel.Medium:
                    pool = mediumQuestions;
                    break;
                case DifficultyLevel.Hard:
                    pool = hardQuestions;
                    break;
            }

            if (pool == null || pool.Count == 0)
            {
                Debug.LogWarning($"No {difficulty} questions available!");
                return null;
            }

            int randomIndex = Random.Range(0, pool.Count);
            return pool[randomIndex];
        }

        /// <summary>
        /// Get random question (any difficulty)
        /// </summary>
        public QuestionData GetRandomQuestion()
        {
            if (allQuestions.Length == 0) return null;

            int randomIndex = Random.Range(0, allQuestions.Length);
            return allQuestions[randomIndex];
        }
    }
}
