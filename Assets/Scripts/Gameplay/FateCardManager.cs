using UnityEngine;
using System.Collections;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Manages fate card effects and random events
    /// </summary>
    public class FateCardManager : MonoBehaviour
    {
        public static FateCardManager Instance;

        [Header("Fate Card Data")]
        [Tooltip("All available fate cards (load from Resources/FateCards)")]
        public FateCardData[] allFateCards;

        [Header("Audio")]
        public AudioClip sfxFatePositive;
        public AudioClip sfxFateNegative;

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
            LoadFateCards();
        }

        /// <summary>
        /// Load all fate card assets from Resources/FateCards
        /// </summary>
        private void LoadFateCards()
        {
            allFateCards = Resources.LoadAll<FateCardData>("FateCards");
            Debug.Log($"Loaded {allFateCards.Length} fate cards");

            if (allFateCards.Length == 0)
            {
                Debug.LogWarning("No fate cards found in Resources/FateCards! Create fate card assets first.");
            }
        }

        /// <summary>
        /// Trigger a fate card effect
        /// </summary>
        public void TriggerFateCard(Player player, FateCardData fateCard)
        {
            if (player == null || fateCard == null) return;

            // Check shield immunity
            if (player.inventory.hasShield)
            {
                Debug.Log($"🛡️ {player.name} is protected by shield! Fate card blocked.");
                player.inventory.hasShield = false;
                ShowFateCardUI(player, fateCard, true);
                return;
            }

            Debug.Log($"🎴 {player.name} triggered fate: {fateCard.description}");
            player.inventory.fateCardsTriggered++;

            // Show fate card UI
            ShowFateCardUI(player, fateCard, false);

            // Apply effect
            StartCoroutine(ApplyFateEffect(player, fateCard));
        }

        /// <summary>
        /// Apply fate card effect to player
        /// </summary>
        private IEnumerator ApplyFateEffect(Player player, FateCardData fateCard)
        {
            yield return new WaitForSeconds(1f);

            switch (fateCard.effectType)
            {
                case FateEffect.Move:
                    ApplyMoveEffect(player, fateCard.value);
                    break;

                case FateEffect.Points:
                    ApplyPointsEffect(player, fateCard.value);
                    break;

                case FateEffect.Ligand:
                    ApplyLigandEffect(player);
                    break;

                case FateEffect.SkipTurn:
                    ApplySkipTurnEffect(player);
                    break;

                case FateEffect.ExtraRoll:
                    ApplyExtraRollEffect(player);
                    break;

                case FateEffect.Swap:
                    ApplySwapEffect(player);
                    break;

                case FateEffect.Shield:
                    ApplyShieldEffect(player);
                    break;
            }

            yield return new WaitForSeconds(1.5f);

            // Resume game
            MatchManager.Instance.NextTurn();
        }

        /// <summary>
        /// Move effect: Move forward/backward X spaces
        /// </summary>
        private void ApplyMoveEffect(Player player, int spaces)
        {
            Debug.Log($"Moving {player.name} {spaces} spaces");

            // TODO: Move player's active token
            // Find the most advanced token
            PlayerToken tokenToMove = GetMostAdvancedToken(player);
            if (tokenToMove != null && spaces != 0)
            {
                tokenToMove.Move(Mathf.Abs(spaces));
            }

            PlayFateSound(spaces > 0);
        }

        /// <summary>
        /// Points effect: Gain/lose points
        /// </summary>
        private void ApplyPointsEffect(Player player, int points)
        {
            player.inventory.AddFatePoints(points);
            PlayFateSound(points > 0);
        }

        /// <summary>
        /// Ligand effect: Collect random ligand
        /// </summary>
        private void ApplyLigandEffect(Player player)
        {
            LigandData randomLigand = LigandManager.Instance?.GetRandomLigand();
            if (randomLigand != null)
            {
                LigandManager.Instance.CollectLigand(player, randomLigand);
            }
            PlayFateSound(true);
        }

        /// <summary>
        /// Skip turn effect
        /// </summary>
        private void ApplySkipTurnEffect(Player player)
        {
            player.inventory.skipNextTurn = true;
            Debug.Log($"{player.name} will skip their next turn!");
            PlayFateSound(false);
        }

        /// <summary>
        /// Extra roll effect
        /// </summary>
        private void ApplyExtraRollEffect(Player player)
        {
            MatchManager.Instance.diceRollsRemaining++;
            Debug.Log($"{player.name} gets an extra dice roll!");
            PlayFateSound(true);
        }

        /// <summary>
        /// Swap effect: Exchange position with nearest player
        /// </summary>
        private void ApplySwapEffect(Player player)
        {
            Debug.Log($"{player.name} swaps position with nearest player!");
            // TODO: Implement swap logic
            PlayFateSound(false);
        }

        /// <summary>
        /// Shield effect: Immunity to next fate card
        /// </summary>
        private void ApplyShieldEffect(Player player)
        {
            player.inventory.hasShield = true;
            Debug.Log($"{player.name} gained shield protection!");
            PlayFateSound(true);
        }

        /// <summary>
        /// Get player's most advanced token
        /// </summary>
        private PlayerToken GetMostAdvancedToken(Player player)
        {
            PlayerToken mostAdvanced = null;
            int maxIndex = -1;

            foreach (PlayerToken token in player.playerTokens)
            {
                if (token.localWaypointIndex > maxIndex)
                {
                    maxIndex = token.localWaypointIndex;
                    mostAdvanced = token;
                }
            }

            return mostAdvanced;
        }

        /// <summary>
        /// Show fate card UI popup
        /// </summary>
        private void ShowFateCardUI(Player player, FateCardData fateCard, bool blocked)
        {
            string message = blocked ? "🛡️ BLOCKED by shield!" : fateCard.GetFormattedDescription();
            Debug.Log($"Fate Card: {message}");

            // TODO: Show UI popup
            // if (UIManager.Instance != null)
            // {
            //     UIManager.Instance.ShowFateCardPopup(fateCard, blocked);
            // }
        }

        /// <summary>
        /// Play fate sound based on positive/negative
        /// </summary>
        private void PlayFateSound(bool positive)
        {
            AudioClip clip = positive ? sfxFatePositive : sfxFateNegative;
            if (clip)
                SanUtils.PlaySound(clip);
        }

        /// <summary>
        /// Get random fate card
        /// </summary>
        public FateCardData GetRandomFateCard()
        {
            if (allFateCards.Length == 0)
            {
                Debug.LogError("No fate cards available!");
                return null;
            }

            int randomIndex = Random.Range(0, allFateCards.Length);
            return allFateCards[randomIndex];
        }
    }
}
