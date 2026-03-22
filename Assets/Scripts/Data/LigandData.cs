using UnityEngine;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// ScriptableObject for storing ligand data from Coordination Chemistry
    /// Based on COOR-CHEM Smart Board game data
    /// </summary>
    [CreateAssetMenu(fileName = "NewLigand", menuName = "COOR-CHEM/Ligand", order = 1)]
    public class LigandData : ScriptableObject
    {
        [Header("Basic Info")]
        [Tooltip("Unique identifier (e.g., 'H2O', 'NH3', 'en')")]
        public string ligandID;

        [Tooltip("Full chemical name (e.g., 'Water', 'Ammonia')")]
        public string ligandName;

        [Tooltip("Chemical formula with Unicode symbols (e.g., 'H₂O', 'NH₃')")]
        public string formula;

        [Header("Properties")]
        [Tooltip("Charge of the ligand (e.g., 0, -1, -2)")]
        public int charge;

        [Tooltip("Visual color for UI display")]
        public Color ligandColor = Color.white;

        [Tooltip("Color name for reference (e.g., 'Red', 'Blue', 'Green')")]
        public string colorName;

        [Header("Coordination Chemistry")]
        [Tooltip("Type of denticity (e.g., 'monodentate', 'bidentate')")]
        public string denticity;

        [Tooltip("Numeric denticity value (1 for monodentate, 2 for bidentate)")]
        [Range(1, 6)]
        public int denticityValue = 1;

        [Header("Visual Assets")]
        [Tooltip("2D sprite icon for ligand")]
        public Sprite icon;

        [Tooltip("3D model (optional, for future use)")]
        public GameObject model3D;

        /// <summary>
        /// Get a formatted display string for UI
        /// </summary>
        public string GetDisplayText()
        {
            string chargeStr = "";
            if (charge > 0)
                chargeStr = $" (+{charge})";
            else if (charge < 0)
                chargeStr = $" ({charge})";

            return $"{ligandName} ({formula}){chargeStr}";
        }

        /// <summary>
        /// Calculate score value based on denticity
        /// </summary>
        public int GetScoreValue()
        {
            return denticityValue;
        }
    }
}
