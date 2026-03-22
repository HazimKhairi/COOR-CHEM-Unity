# COOR-CHEM Unity Adaptation Plan

## 📋 Overview
Adapting Unity Ludo Framework for COOR-CHEM chemistry education board game.

**Original Framework:** https://github.com/bhaambhu/Unity-Ludo-Framework
**Target:** Chemistry-themed Ludo with ligands, questions, and coordination compounds

---

## ✅ What We Keep (Solid Foundation)

### 1. **Core Game Mechanics** (90% reusable)
- ✅ Board movement system (52 waypoints)
- ✅ Turn-based manager
- ✅ Dice rolling with physics
- ✅ Token movement & collision detection
- ✅ Safe waypoint system
- ✅ "Kill" mechanic (send opponent back to base)
- ✅ Win condition (reach endpoint)
- ✅ Player token system (4 tokens per player)

### 2. **Technical Infrastructure**
- ✅ `GameManager` singleton pattern
- ✅ `MatchManager` for game state
- ✅ `PlayersManager` for player management
- ✅ `Dice` component with animation
- ✅ `PlayerToken` movement coroutines
- ✅ `Constants` centralized config
- ✅ DOTween for animations
- ✅ Object pooling system
- ✅ Audio system (SFX)

### 3. **UI System** (Bhambhoo UI)
- ✅ Main menu screens
- ✅ Smooth transitions
- ✅ Material design patterns
- ✅ Roboto & BebasNeue fonts

---

## 🔧 What We Modify

### 1. **Tile Types** (Add Chemistry Mechanics)
**Old:** Normal tiles only
**New:** Multiple tile types
```csharp
public enum TileType
{
    Normal,        // Standard movement
    Ligand,        // Collect ligand (SCH3-, Cl-, NH3, etc.)
    Question,      // Answer chemistry question
    Fate,          // Random event (skip turn, extra roll)
    Start,         // Starting position
    Home,          // End zone
    Prison         // Corner home zones (renamed from "Base")
}
```

### 2. **Board Tiles** → Add Chemistry Data
**File:** `Assets/Scripts/BoardTile.cs` (NEW)
```csharp
public class BoardTile : MonoBehaviour
{
    public int tileIndex;
    public TileType type;
    public LigandData ligandData;      // NEW: Chemistry ligand
    public QuestionData questionData;  // NEW: Question card
    public Transform piecePosition;
    public Material tileMaterial;

    // Visual feedback when player lands
    public void OnPlayerLanded(Player player)
    {
        switch(type)
        {
            case TileType.Ligand:
                ShowLigandPopup();
                break;
            case TileType.Question:
                ShowQuestionCard();
                break;
            case TileType.Fate:
                TriggerFateCard();
                break;
        }
    }
}
```

### 3. **ScriptableObjects for Data**
**Files to create:**

**`Assets/Scripts/Data/LigandData.cs`**
```csharp
[CreateAssetMenu(fileName = "NewLigand", menuName = "COOR-CHEM/Ligand")]
public class LigandData : ScriptableObject
{
    public string ligandName;        // "Thiocyanate"
    public string formula;           // "SCN-"
    public int charge;               // -1
    public Color ligandColor;        // Visual color
    public Sprite icon;              // 2D icon
    public int denticity;            // 1 (monodentate)
    public int denticityValue;       // For scoring
}
```

**`Assets/Scripts/Data/QuestionData.cs`**
```csharp
[CreateAssetMenu(fileName = "NewQuestion", menuName = "COOR-CHEM/Question")]
public class QuestionData : ScriptableObject
{
    public string questionText;
    public string[] answerOptions;   // 4 options
    public int correctAnswerIndex;   // 0-3
    public string explanation;       // Why correct
    public int pointsReward;         // Bonus points
}
```

### 4. **Player Inventory System** (NEW)
**File:** `Assets/Scripts/PlayerInventory.cs`
```csharp
public class PlayerInventory
{
    public Player owner;
    public List<LigandData> collectedLigands = new List<LigandData>();
    public int score = 0;
    public int correctAnswers = 0;

    public void AddLigand(LigandData ligand)
    {
        collectedLigands.Add(ligand);
        score += ligand.denticityValue;
        // Trigger UI update
        UIManager.Instance.UpdateInventory(this);
    }
}
```

### 5. **Modify PlayerToken Movement**
**File:** `Assets/Scripts/PlayerToken.cs` (MODIFY)
```csharp
// Add to MoveCoroutine after moving
IEnumerator MoveCoroutine(int diceResult, int currentWaypointIndex)
{
    // ... existing movement code ...

    // NEW: Check tile type after landing
    BoardTile currentTile = GetTileAtWaypoint(localWaypointIndex);
    if (currentTile != null)
    {
        yield return new WaitForSeconds(0.3f);
        currentTile.OnPlayerLanded(player);
    }

    // ... rest of code ...
}
```

---

## 🆕 What We Add

### 1. **Chemistry UI Panels**
**Files to create:**
- `Assets/Prefabs/UI/LigandCollectionPanel.prefab`
- `Assets/Prefabs/UI/QuestionCardPanel.prefab`
- `Assets/Prefabs/UI/PlayerInventoryHUD.prefab`
- `Assets/Prefabs/UI/FateCardPanel.prefab`

### 2. **Question Card Manager**
**File:** `Assets/Scripts/QuestionCardManager.cs`
```csharp
public class QuestionCardManager : MonoBehaviour
{
    public static QuestionCardManager Instance;
    public QuestionData[] allQuestions;

    public void ShowQuestion(Player player, QuestionData question)
    {
        // Pause game
        MatchManager.InputAllowed = false;

        // Show UI with 4 options
        UIManager.Instance.ShowQuestionPanel(question, OnAnswerSelected);
    }

    void OnAnswerSelected(int selectedIndex)
    {
        // Check if correct
        // Award points or penalty
        // Resume game
        MatchManager.Instance.NextTurn();
    }
}
```

### 3. **Ligand Collection Manager**
**File:** `Assets/Scripts/LigandManager.cs`
```csharp
public class LigandManager : MonoBehaviour
{
    public static LigandManager Instance;
    public LigandData[] allLigands;  // 20 ligands from data.js

    public void CollectLigand(Player player, LigandData ligand)
    {
        player.inventory.AddLigand(ligand);

        // Show popup animation
        ShowLigandCollectedPopup(ligand);

        // Play sound
        SanUtils.PlaySound(Constants.Instance.sfxLigandCollected);
    }
}
```

### 4. **Board Tile Generator (Editor Script)**
**File:** `Assets/Scripts/Editor/BoardTileGenerator.cs`
```csharp
#if UNITY_EDITOR
using UnityEditor;

public class BoardTileGenerator : EditorWindow
{
    [MenuItem("COOR-CHEM/Generate Board Tiles")]
    static void GenerateBoard()
    {
        // Create 52 waypoint tiles
        for (int i = 0; i < 52; i++)
        {
            GameObject tile = new GameObject($"Tile_{i}");
            BoardTile tileComponent = tile.AddComponent<BoardTile>();

            // Assign tile type based on index
            if (i % 8 == 0) // Every 8th tile
                tileComponent.type = TileType.Ligand;
            else if (i % 13 == 6) // Specific positions
                tileComponent.type = TileType.Question;
            else
                tileComponent.type = TileType.Normal;

            // Position tile
            tile.transform.position = CalculatePosition(i);

            // Visual setup
            SetupTileVisual(tile, tileComponent.type);
        }
    }
}
#endif
```

---

## 🎨 Visual Changes

### 1. **Board Background**
**Replace:** Default Ludo board
**With:** Chemistry-themed gradient (dark blue/purple like web version)
- Update `Assets/Textures/Board.png`
- Use dark theme: `#0f0c29`, `#302b63`, `#24243e`

### 2. **Player Colors**
**Change:**
- Red → Red tone `#e63946` (from web version)
- Blue → Keep existing
- Green → Keep existing
- Yellow → Keep existing

### 3. **Tile Visuals**
**Add colored tile backgrounds:**
- Normal: White with border
- Ligand: Purple glow
- Question: Orange glow
- Fate: Blue sparkle

### 4. **Logo**
**Import:** `/LOGO SMART GAME/4.png` → `Assets/Textures/Logo.png`

---

## 📦 Data Migration (from Web Version)

### 1. **Copy Game Data**
**From:** `coor-chem game/src/js/data.js`
**To:** Unity ScriptableObjects

**Process:**
1. Read `LIGANDS` array → Create 20 `LigandData` assets
2. Read `QUESTIONS` array → Create question assets
3. Store in `Assets/Resources/Ligands/` and `Assets/Resources/Questions/`

### 2. **Create ScriptableObject Assets**
```bash
# In Unity Editor
Assets > Create > COOR-CHEM > Ligand
# Create 20 ligands manually (SCH3-, Cl-, NH3, etc.)

Assets > Create > COOR-CHEM > Question
# Create question cards
```

---

## 🔄 Multiplayer Strategy

### Option A: Keep Photon PUN 2 (Recommended)
**Pros:**
- Already exists in framework (`NetworkManager.cs`)
- Real-time sync (better than PHP polling)
- 20 CCU free tier

**Implementation:**
1. Keep existing `NetworkManager.cs`
2. Add RPCs for:
   - `[PunRPC] void SyncLigandCollection(int playerIndex, int ligandID)`
   - `[PunRPC] void SyncQuestionAnswer(int playerIndex, bool correct)`
3. Room-based system (max 4 players per room)

### Option B: Migrate to Mirror (Alternative)
**Pros:**
- Open source, no CCU limits
- Self-hosted (use XAMPP server)

**Cons:**
- More setup required

---

## 📝 Implementation Checklist

### Phase 1: Setup & Data
- [x] Clone Unity Ludo Framework
- [ ] Create ScriptableObject scripts (`LigandData`, `QuestionData`)
- [ ] Import 20 ligands from `data.js`
- [ ] Import questions from `data.js`
- [ ] Import logo and textures

### Phase 2: Core Mechanics
- [ ] Create `BoardTile.cs` with tile types
- [ ] Modify `PlayerToken.cs` to check tile types
- [ ] Create `PlayerInventory.cs`
- [ ] Create `LigandManager.cs`
- [ ] Create `QuestionCardManager.cs`

### Phase 3: UI
- [ ] Design `LigandCollectionPanel.prefab`
- [ ] Design `QuestionCardPanel.prefab`
- [ ] Design `PlayerInventoryHUD.prefab`
- [ ] Design `FateCardPanel.prefab`
- [ ] Update main menu with COOR-CHEM branding

### Phase 4: Board Setup
- [ ] Create `BoardTileGenerator.cs` (Editor script)
- [ ] Generate 52 tiles with correct types
- [ ] Position tiles in Ludo pattern
- [ ] Apply visual materials

### Phase 5: Multiplayer
- [ ] Test existing Photon setup
- [ ] Add RPCs for ligand collection
- [ ] Add RPCs for question answers
- [ ] Test 4-player room

### Phase 6: Polish
- [ ] Sound effects (ligand collect, question correct/wrong)
- [ ] Particle effects (ligand glow, question sparkle)
- [ ] Win condition (most ligands + correct answers)
- [ ] Tutorial/How to Play scene

---

## 🚀 Next Steps

1. **Hazim confirms plan** ✅
2. **Start Phase 1:** Create ScriptableObjects
3. **Migrate data** from `data.js` to Unity
4. **Build core mechanics** (Phase 2)
5. **Test gameplay** with placeholder UI
6. **Polish UI** (Phase 3-4)
7. **Multiplayer testing** (Phase 5)
8. **Final polish** (Phase 6)

---

## 📂 Final Unity Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   ├── GameBoard.unity           # Modified from MainScene.unity
│   └── Tutorial.unity            # NEW
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs        # KEEP (modified)
│   │   ├── MatchManager.cs       # KEEP (modified)
│   │   ├── Constants.cs          # KEEP (add new constants)
│   │   └── SanUtils.cs          # KEEP
│   ├── Gameplay/
│   │   ├── Dice.cs              # KEEP
│   │   ├── PlayerToken.cs        # MODIFY
│   │   ├── BoardTile.cs         # NEW
│   │   ├── LigandManager.cs     # NEW
│   │   └── QuestionCardManager.cs # NEW
│   ├── Players/
│   │   ├── PlayersManager.cs    # KEEP (modify)
│   │   ├── Player.cs            # KEEP (add inventory)
│   │   └── PlayerInventory.cs   # NEW
│   ├── Data/
│   │   ├── LigandData.cs        # NEW
│   │   └── QuestionData.cs      # NEW
│   ├── UI/
│   │   ├── UIManager.cs         # KEEP (extend)
│   │   ├── LigandPanel.cs       # NEW
│   │   └── QuestionPanel.cs     # NEW
│   ├── Networking/
│   │   └── NetworkManager.cs    # KEEP (add RPCs)
│   └── Editor/
│       └── BoardTileGenerator.cs # NEW
├── Prefabs/
│   ├── Tiles/
│   │   ├── NormalTile.prefab    # NEW
│   │   ├── LigandTile.prefab    # NEW
│   │   └── QuestionTile.prefab  # NEW
│   ├── UI/
│   │   ├── LigandPanel.prefab
│   │   ├── QuestionPanel.prefab
│   │   └── InventoryHUD.prefab
│   └── PlayerToken.prefab       # KEEP
├── Resources/
│   ├── Ligands/                 # NEW: 20 ligand assets
│   ├── Questions/               # NEW: Question assets
│   └── DiceSides/               # KEEP
├── Textures/
│   ├── Logo.png                 # NEW
│   ├── Board.png                # MODIFY
│   └── TileBackgrounds/         # NEW
└── Sounds/
    ├── LigandCollect.wav        # NEW
    ├── QuestionCorrect.wav      # NEW
    └── QuestionWrong.wav        # NEW
```

---

**Ready to proceed, Hazim?** 🚀
