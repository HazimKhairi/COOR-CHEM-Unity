#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace com.bhambhoo.fairludo
{
    /// <summary>
    /// Editor tool to auto-generate COOR-CHEM board tiles
    /// </summary>
    public class BoardTileGenerator : EditorWindow
    {
        private int totalTiles = 52;
        private float tileSize = 1f;
        private bool createTileObjects = true;
        private bool assignTileTypes = true;
        private bool loadChemistryData = true;

        [MenuItem("COOR-CHEM/Generate Board Tiles")]
        static void ShowWindow()
        {
            BoardTileGenerator window = GetWindow<BoardTileGenerator>("Board Tile Generator");
            window.minSize = new Vector2(400, 300);
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("COOR-CHEM Board Tile Generator", EditorStyles.boldLabel);
            GUILayout.Space(10);

            totalTiles = EditorGUILayout.IntField("Total Tiles", totalTiles);
            tileSize = EditorGUILayout.FloatField("Tile Size", tileSize);

            GUILayout.Space(10);

            createTileObjects = EditorGUILayout.Toggle("Create Tile GameObjects", createTileObjects);
            assignTileTypes = EditorGUILayout.Toggle("Assign Tile Types", assignTileTypes);
            loadChemistryData = EditorGUILayout.Toggle("Load Chemistry Data", loadChemistryData);

            GUILayout.Space(20);

            if (GUILayout.Button("Generate Board", GUILayout.Height(40)))
            {
                GenerateBoard();
            }

            GUILayout.Space(10);

            EditorGUILayout.HelpBox(
                "This will create " + totalTiles + " board tiles in a Ludo pattern.\n\n" +
                "Tile Types:\n" +
                "- Every 8th tile = Ligand (purple)\n" +
                "- Index 6, 19, 32, 45 = Question (orange)\n" +
                "- Index 13, 26, 39 = Fate (blue)\n" +
                "- Others = Normal (white)",
                MessageType.Info
            );
        }

        void GenerateBoard()
        {
            Debug.Log("Starting board generation...");

            // Find or create parent object
            GameObject boardParent = GameObject.Find("BoardTiles");
            if (boardParent == null)
            {
                boardParent = new GameObject("BoardTiles");
                Undo.RegisterCreatedObjectUndo(boardParent, "Create Board Tiles Parent");
            }

            // Load chemistry data
            LigandData[] ligands = null;
            QuestionData[] questions = null;
            FateCardData[] fateCards = null;

            if (loadChemistryData)
            {
                ligands = Resources.LoadAll<LigandData>("Ligands");
                questions = Resources.LoadAll<QuestionData>("Questions");
                fateCards = Resources.LoadAll<FateCardData>("FateCards");

                Debug.Log($"Loaded: {ligands.Length} ligands, {questions.Length} questions, {fateCards.Length} fate cards");
            }

            // Create tiles
            for (int i = 0; i < totalTiles; i++)
            {
                GameObject tileObj = new GameObject($"Tile_{i:D2}");
                tileObj.transform.parent = boardParent.transform;

                // Position calculation (you'll adjust this to match your board layout)
                Vector3 position = CalculateTilePosition(i);
                tileObj.transform.position = position;

                // Add BoardTile component
                BoardTile tile = tileObj.AddComponent<BoardTile>();
                tile.tileIndex = i;

                // Assign tile type based on index
                if (assignTileTypes)
                {
                    tile.type = DetermineTileType(i);
                }

                // Assign chemistry data
                if (loadChemistryData)
                {
                    switch (tile.type)
                    {
                        case TileType.Ligand:
                            if (ligands != null && ligands.Length > 0)
                                tile.ligandData = ligands[Random.Range(0, ligands.Length)];
                            break;

                        case TileType.Question:
                            if (questions != null && questions.Length > 0)
                                tile.questionData = questions[Random.Range(0, questions.Length)];
                            break;

                        case TileType.Fate:
                            if (fateCards != null && fateCards.Length > 0)
                                tile.fateCardData = fateCards[Random.Range(0, fateCards.Length)];
                            break;
                    }
                }

                // Add visual components
                AddTileVisuals(tileObj, tile);

                // Create piece position marker
                GameObject piecePos = new GameObject("PiecePosition");
                piecePos.transform.parent = tileObj.transform;
                piecePos.transform.localPosition = Vector3.up * 0.1f;
                tile.piecePosition = piecePos.transform;

                Undo.RegisterCreatedObjectUndo(tileObj, "Create Board Tile");
            }

            Debug.Log($"✅ Generated {totalTiles} board tiles!");

            // Select parent in hierarchy
            Selection.activeGameObject = boardParent;
        }

        /// <summary>
        /// Determine tile type based on index
        /// </summary>
        TileType DetermineTileType(int index)
        {
            // Start tiles
            if (index == 0) return TileType.Start;

            // Home tiles (last 5 tiles)
            if (index >= totalTiles - 5) return TileType.Home;

            // Ligand tiles (every 8th tile)
            if (index % 8 == 0) return TileType.Ligand;

            // Question tiles (specific positions)
            if (index == 6 || index == 19 || index == 32 || index == 45)
                return TileType.Question;

            // Fate tiles (specific positions)
            if (index == 13 || index == 26 || index == 39)
                return TileType.Fate;

            // Default to normal
            return TileType.Normal;
        }

        /// <summary>
        /// Calculate tile position in a Ludo board pattern
        /// </summary>
        Vector3 CalculateTilePosition(int index)
        {
            // Ludo board has 52 tiles in a cross pattern
            // This is a simplified calculation - adjust to match your board layout

            float radius = 5f;
            float angleStep = 360f / 52f;
            float angle = angleStep * index * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(x, 0, z);
        }

        /// <summary>
        /// Add sprite renderer and visuals to tile
        /// </summary>
        void AddTileVisuals(GameObject tileObj, BoardTile tile)
        {
            // Add SpriteRenderer
            SpriteRenderer renderer = tileObj.AddComponent<SpriteRenderer>();

            // Create a simple square sprite
            renderer.sprite = CreateSquareSprite();

            // Set color based on type
            Color tileColor = Color.white;
            switch (tile.type)
            {
                case TileType.Normal:
                    tileColor = Color.white;
                    break;
                case TileType.Ligand:
                    tileColor = new Color(0.7f, 0.5f, 0.9f); // Purple
                    break;
                case TileType.Question:
                    tileColor = new Color(1f, 0.7f, 0.3f); // Orange
                    break;
                case TileType.Fate:
                    tileColor = new Color(0.3f, 0.7f, 1f); // Blue
                    break;
                case TileType.Start:
                    tileColor = new Color(0.3f, 0.85f, 0.3f); // Green
                    break;
                case TileType.Home:
                    tileColor = new Color(1f, 0.84f, 0f); // Gold
                    break;
            }

            renderer.color = tileColor;

            tile.tileRenderer = renderer;
        }

        /// <summary>
        /// Create a simple square sprite
        /// </summary>
        Sprite CreateSquareSprite()
        {
            Texture2D tex = new Texture2D(64, 64);
            Color[] pixels = new Color[64 * 64];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.white;
            }

            tex.SetPixels(pixels);
            tex.Apply();

            return Sprite.Create(tex, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64f);
        }
    }
}
#endif
