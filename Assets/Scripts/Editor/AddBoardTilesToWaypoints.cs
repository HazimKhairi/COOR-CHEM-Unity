using UnityEngine;
using UnityEditor;
using com.bhambhoo.fairludo;

/// <summary>
/// COOR-CHEM: Add BoardTile components to existing waypoints
/// Menu: COOR-CHEM → Add Chemistry To Waypoints
/// </summary>
public class AddBoardTilesToWaypoints : EditorWindow
{
    [MenuItem("COOR-CHEM/Add Chemistry To Waypoints")]
    static void AddChemistryToWaypoints()
    {
        if (!EditorUtility.DisplayDialog("Add Chemistry To Waypoints",
            "This will:\n" +
            "• Find all waypoints in scene\n" +
            "• Add BoardTile component to each\n" +
            "• Assign tile types (Ligand/Question/Fate)\n" +
            "• Load chemistry data from Resources\n\n" +
            "Continue?", "Add Chemistry", "Cancel"))
        {
            return;
        }

        int waypointsProcessed = 0;
        int tilesAdded = 0;

        // Find parent object containing waypoints
        GameObject waypointsParent = GameObject.Find("Board Waypoints - Unique Names");

        if (waypointsParent == null)
        {
            EditorUtility.DisplayDialog("Error",
                "Could not find 'Board Waypoints - Unique Names' in scene!\n\n" +
                "Make sure MainScene.unity is open.", "OK");
            return;
        }

        // Load chemistry data
        LigandData[] allLigands = Resources.LoadAll<LigandData>("Ligands");
        QuestionData[] allQuestions = Resources.LoadAll<QuestionData>("Questions");
        FateCardData[] allFateCards = Resources.LoadAll<FateCardData>("FateCards");

        Debug.Log($"Loaded: {allLigands.Length} ligands, {allQuestions.Length} questions, {allFateCards.Length} fate cards");

        // Process each waypoint child
        for (int i = 0; i < waypointsParent.transform.childCount; i++)
        {
            Transform waypoint = waypointsParent.transform.GetChild(i);
            waypointsProcessed++;

            // Check if already has BoardTile
            BoardTile existingTile = waypoint.GetComponent<BoardTile>();
            if (existingTile != null)
            {
                Debug.Log($"Waypoint {i} already has BoardTile - skipping");
                continue;
            }

            // Add BoardTile component
            BoardTile tile = waypoint.gameObject.AddComponent<BoardTile>();
            tile.tileIndex = i;

            // Assign tile type based on position
            TileType tileType = DetermineTileType(i);
            tile.type = tileType;

            // Assign chemistry data
            switch (tileType)
            {
                case TileType.Ligand:
                    if (allLigands.Length > 0)
                    {
                        // Cycle through ligands
                        int ligandIndex = (i / 4) % allLigands.Length;
                        tile.ligandData = allLigands[ligandIndex];
                    }
                    break;

                case TileType.Question:
                    if (allQuestions.Length > 0)
                    {
                        // Cycle through questions
                        int questionIndex = (i / 8) % allQuestions.Length;
                        tile.questionData = allQuestions[questionIndex];
                    }
                    break;

                case TileType.Fate:
                    if (allFateCards.Length > 0)
                    {
                        // Cycle through fate cards
                        int fateIndex = (i / 6) % allFateCards.Length;
                        tile.fateCardData = allFateCards[fateIndex];
                    }
                    break;
            }

            tilesAdded++;

            // Mark scene as dirty
            EditorUtility.SetDirty(waypoint.gameObject);
        }

        EditorUtility.DisplayDialog("Success!",
            $"✅ Processed {waypointsProcessed} waypoints\n" +
            $"✅ Added {tilesAdded} BoardTile components\n\n" +
            "Chemistry system integrated with Ludo board!\n\n" +
            "Press Play to test!", "OK");

        Debug.Log($"✅ Added BoardTile to {tilesAdded} waypoints!");
    }

    /// <summary>
    /// Determine tile type based on waypoint index
    /// Strategy: Spread chemistry tiles evenly around the board
    /// </summary>
    static TileType DetermineTileType(int index)
    {
        // First tile = Start
        if (index == 0)
            return TileType.Start;

        // Last 4 tiles = Home
        if (index >= 48)
            return TileType.Home;

        // Every 4th tile = Ligand (chemistry!)
        if (index % 4 == 0)
            return TileType.Ligand;

        // Specific positions = Question
        if (index == 6 || index == 12 || index == 19 || index == 25 ||
            index == 32 || index == 38 || index == 45)
            return TileType.Question;

        // Every 6th tile = Fate
        if (index % 6 == 0)
            return TileType.Fate;

        // Default = Normal
        return TileType.Normal;
    }
}
