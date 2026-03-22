using UnityEngine;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// ScriptableObject for storing fate card data
    /// Based on COOR-CHEM Smart Board game fate cards
    /// </summary>
    [CreateAssetMenu(fileName = "NewFateCard", menuName = "COOR-CHEM/Fate Card", order = 3)]
    public class FateCardData : ScriptableObject
    {
        [Header("Fate Card Info")]
        [Tooltip("Unique identifier (e.g., 'f1', 'f2')")]
        public string fateID;

        [TextArea(2, 3)]
        [Tooltip("The fate card text description")]
        public string description;

        [Tooltip("Type of fate effect")]
        public FateEffect effectType;

        [Tooltip("Numeric value for the effect (e.g., +3 spaces, -2 points, etc.)")]
        public int value;

        [Header("Visual Assets")]
        [Tooltip("Icon sprite for this fate card")]
        public Sprite icon;

        [Tooltip("Color for the fate card background")]
        public Color cardColor = new Color(0.56f, 0.27f, 0.68f); // Purple default

        /// <summary>
        /// Get formatted description with icon placeholder
        /// </summary>
        public string GetFormattedDescription()
        {
            string prefix = "";
            switch (effectType)
            {
                case FateEffect.Move:
                    prefix = value > 0 ? "🎯 " : "⚠️ ";
                    break;
                case FateEffect.Points:
                    prefix = value > 0 ? "⭐ " : "💔 ";
                    break;
                case FateEffect.Ligand:
                    prefix = "🎁 ";
                    break;
                case FateEffect.SkipTurn:
                    prefix = "⏸️ ";
                    break;
                case FateEffect.ExtraRoll:
                    prefix = "🎲 ";
                    break;
                case FateEffect.Swap:
                    prefix = "🔄 ";
                    break;
                case FateEffect.Shield:
                    prefix = "🛡️ ";
                    break;
            }
            return prefix + description;
        }

        /// <summary>
        /// Get color based on effect type (positive/negative)
        /// </summary>
        public Color GetEffectColor()
        {
            bool isPositive = false;

            switch (effectType)
            {
                case FateEffect.Move:
                    isPositive = value > 0;
                    break;
                case FateEffect.Points:
                    isPositive = value > 0;
                    break;
                case FateEffect.Ligand:
                case FateEffect.ExtraRoll:
                case FateEffect.Shield:
                    isPositive = true;
                    break;
                case FateEffect.SkipTurn:
                case FateEffect.Swap:
                    isPositive = false;
                    break;
            }

            return isPositive
                ? new Color(0.3f, 0.69f, 0.31f)  // Green #4CAF50
                : new Color(0.96f, 0.26f, 0.21f); // Red #f44336
        }
    }

    public enum FateEffect
    {
        Move,        // Move forward/backward X spaces
        Points,      // Gain/lose X points
        Ligand,      // Collect random ligand
        SkipTurn,    // Skip next turn
        ExtraRoll,   // Roll dice again
        Swap,        // Swap position with another player
        Shield       // Immunity to next fate card
    }
}
