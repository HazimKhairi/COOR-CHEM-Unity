using UnityEngine;
using System.Collections;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Represents a single tile on the COOR-CHEM board
    /// Handles tile types, chemistry data, and player interactions
    /// </summary>
    public class BoardTile : MonoBehaviour
    {
        [Header("Tile Properties")]
        [Tooltip("Index in the board path (0-51)")]
        public int tileIndex;

        [Tooltip("Type of this tile")]
        public TileType type = TileType.Normal;

        [Header("Chemistry Data")]
        [Tooltip("Ligand data if this is a Ligand tile")]
        public LigandData ligandData;

        [Tooltip("Question data if this is a Question tile")]
        public QuestionData questionData;

        [Tooltip("Fate card data if this is a Fate tile")]
        public FateCardData fateCardData;

        [Header("Visual Components")]
        [Tooltip("Transform where player token stands")]
        public Transform piecePosition;

        [Tooltip("SpriteRenderer for tile visual")]
        public SpriteRenderer tileRenderer;

        [Tooltip("Particle effect for special tiles")]
        public ParticleSystem tileEffect;

        private Color originalColor;

        private void Start()
        {
            if (tileRenderer)
                originalColor = tileRenderer.color;

            SetupTileVisual();
        }

        /// <summary>
        /// Setup visual appearance based on tile type
        /// </summary>
        private void SetupTileVisual()
        {
            if (!tileRenderer) return;

            switch (type)
            {
                case TileType.Normal:
                    tileRenderer.color = Color.white;
                    break;
                case TileType.Ligand:
                    tileRenderer.color = new Color(0.7f, 0.5f, 0.9f); // Purple
                    if (tileEffect) tileEffect.Play();
                    break;
                case TileType.Question:
                    tileRenderer.color = new Color(1f, 0.7f, 0.3f); // Orange
                    if (tileEffect) tileEffect.Play();
                    break;
                case TileType.Fate:
                    tileRenderer.color = new Color(0.3f, 0.7f, 1f); // Blue
                    if (tileEffect) tileEffect.Play();
                    break;
                case TileType.Start:
                    tileRenderer.color = new Color(0.3f, 0.85f, 0.3f); // Green
                    break;
                case TileType.Home:
                    tileRenderer.color = new Color(1f, 0.84f, 0f); // Gold
                    break;
            }
        }

        /// <summary>
        /// Called when a player lands on this tile
        /// </summary>
        public void OnPlayerLanded(Player player)
        {
            Debug.Log($"Player {player.playerIndex} landed on {type} tile at index {tileIndex}");

            // Visual feedback
            StartCoroutine(TileHighlightEffect());

            // Handle tile type specific logic
            switch (type)
            {
                case TileType.Ligand:
                    if (ligandData != null)
                    {
                        LigandManager.Instance?.CollectLigand(player, ligandData);
                    }
                    break;

                case TileType.Question:
                    if (questionData != null)
                    {
                        QuestionCardManager.Instance?.ShowQuestion(player, questionData);
                    }
                    break;

                case TileType.Fate:
                    if (fateCardData != null)
                    {
                        FateCardManager.Instance?.TriggerFateCard(player, fateCardData);
                    }
                    break;

                case TileType.Home:
                    // Player reached home
                    OnPlayerReachedHome(player);
                    break;
            }
        }

        /// <summary>
        /// Visual highlight effect when player lands
        /// </summary>
        private IEnumerator TileHighlightEffect()
        {
            if (!tileRenderer) yield break;

            Color highlightColor = Color.yellow;
            float duration = 0.5f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                tileRenderer.color = Color.Lerp(highlightColor, originalColor, t);
                yield return null;
            }

            tileRenderer.color = originalColor;
        }

        /// <summary>
        /// Handle player reaching home
        /// </summary>
        private void OnPlayerReachedHome(Player player)
        {
            Debug.Log($"Player {player.playerIndex} reached HOME!");
            // TODO: Add win sound when Constants.sfxWin is available
            // SanUtils.PlaySound(Constants.Instance.sfxWin);

            // Check if this player has won
            MatchManager.Instance?.CheckWinCondition(player);
        }

        /// <summary>
        /// Get the waypoint transform for player positioning
        /// </summary>
        public Transform GetPiecePosition()
        {
            return piecePosition ? piecePosition : transform;
        }

        private void OnMouseDown()
        {
            // Debug: Click tile to see info
            Debug.Log($"Tile {tileIndex}: {type}");
            if (ligandData) Debug.Log($"  Ligand: {ligandData.ligandName}");
            if (questionData) Debug.Log($"  Question: {questionData.questionText}");
            if (fateCardData) Debug.Log($"  Fate: {fateCardData.description}");
        }
    }

    /// <summary>
    /// Types of tiles in COOR-CHEM board
    /// </summary>
    public enum TileType
    {
        Normal,     // Standard movement tile
        Ligand,     // Collect chemistry ligand
        Question,   // Answer chemistry question
        Fate,       // Random fate card effect
        Start,      // Starting position
        Home        // End zone (win condition)
    }
}
