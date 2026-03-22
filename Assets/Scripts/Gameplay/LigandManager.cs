using UnityEngine;
using System.Collections.Generic;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Manages ligand collection, distribution, and UI display
    /// </summary>
    public class LigandManager : MonoBehaviour
    {
        public static LigandManager Instance;

        [Header("Ligand Data")]
        [Tooltip("All available ligands (load from Resources/Ligands)")]
        public LigandData[] allLigands;

        [Header("Audio")]
        public AudioClip sfxLigandCollected;

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
            // Load all ligands from Resources folder
            LoadLigands();
        }

        /// <summary>
        /// Load all ligand assets from Resources/Ligands
        /// </summary>
        private void LoadLigands()
        {
            allLigands = Resources.LoadAll<LigandData>("Ligands");
            Debug.Log($"Loaded {allLigands.Length} ligands");

            if (allLigands.Length == 0)
            {
                Debug.LogWarning("No ligands found in Resources/Ligands! Create ligand assets first.");
            }
        }

        /// <summary>
        /// Player collects a ligand
        /// </summary>
        public void CollectLigand(Player player, LigandData ligand)
        {
            if (player == null || ligand == null) return;

            // Add to player's inventory
            player.inventory.AddLigand(ligand);

            // Play collection sound
            if (sfxLigandCollected)
                SanUtils.PlaySound(sfxLigandCollected);

            // Show collection UI popup
            ShowLigandCollectedPopup(player, ligand);

            // Network sync (if multiplayer)
            if (NetworkManager.Instance != null)
            {
                // NetworkManager.Instance.SyncLigandCollection(player.playerIndex, ligand.ligandID);
            }
        }

        /// <summary>
        /// Show ligand collection popup UI
        /// </summary>
        private void ShowLigandCollectedPopup(Player player, LigandData ligand)
        {
            Debug.Log($"🎁 {player.name} collected {ligand.ligandName}!");

            // TODO: Show UI popup panel
            // if (UIManager.Instance != null)
            // {
            //     UIManager.Instance.ShowLigandPopup(ligand);
            // }
        }

        /// <summary>
        /// Get a random ligand (for fate cards)
        /// </summary>
        public LigandData GetRandomLigand()
        {
            if (allLigands.Length == 0)
            {
                Debug.LogError("No ligands available!");
                return null;
            }

            int randomIndex = Random.Range(0, allLigands.Length);
            return allLigands[randomIndex];
        }

        /// <summary>
        /// Get ligand by ID
        /// </summary>
        public LigandData GetLigandByID(string ligandID)
        {
            foreach (LigandData ligand in allLigands)
            {
                if (ligand.ligandID == ligandID)
                    return ligand;
            }

            Debug.LogWarning($"Ligand '{ligandID}' not found!");
            return null;
        }

        /// <summary>
        /// Get all monodentate ligands
        /// </summary>
        public List<LigandData> GetMonodentateLigands()
        {
            List<LigandData> result = new List<LigandData>();
            foreach (LigandData ligand in allLigands)
            {
                if (ligand.denticityValue == 1)
                    result.Add(ligand);
            }
            return result;
        }

        /// <summary>
        /// Get all bidentate ligands
        /// </summary>
        public List<LigandData> GetBidentateLigands()
        {
            List<LigandData> result = new List<LigandData>();
            foreach (LigandData ligand in allLigands)
            {
                if (ligand.denticityValue == 2)
                    result.Add(ligand);
            }
            return result;
        }
    }
}
