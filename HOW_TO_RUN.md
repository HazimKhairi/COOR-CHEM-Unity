# 🎮 How to Run - COOR-CHEM Unity

## Step 1: Open Project in Unity Hub

1. **Unity Hub akan auto-open** (atau click Unity Hub icon)
2. **Find project:** COOR-CHEM-Unity
3. **Click nama project** untuk buka

```
┌─────────────────────────────────────────┐
│ Unity Hub                                │
├─────────────────────────────────────────┤
│ Projects                                 │
│                                          │
│ ┌────────────────────────────────────┐  │
│ │ COOR-CHEM-Unity                    │◄─ Click here!
│ │ Unity 6000.4.0f1                   │  │
│ │ /Applications/XAMPP/...            │  │
│ └────────────────────────────────────┘  │
│                                          │
└─────────────────────────────────────────┘
```

**First time:** Tunggu 2-3 minit Unity load project

---

## Step 2: Tunggu Import Complete

**Location:** Bottom-right corner Unity Editor

```
Unity Editor
┌────────────────────────────────────────────────┐
│                                                 │
│  Scene View                    Game View        │
│                                                 │
│  ┌─────────────────────────────────────────┐  │
│  │                                          │  │
│  │         (Your 3D Scene Here)            │  │
│  │                                          │  │
│  └─────────────────────────────────────────┘  │
│                                                 │
│  Console | Project | Inspector                 │
│                                                 │
└───────────────────────────────[Importing...]───┘
                                      ↑
                          Wait until this disappears
```

**Tengok:** Progress bar hilang = import done

---

## Step 3: Run Auto-Generator

### A. Find Menu

**Top menu bar:**

```
File  Edit  Assets  GameObject  Component  Window  COOR-CHEM  Help
                                                       ↑
                                                  Click here!
```

### B. Click Menu Item

```
COOR-CHEM ▼
├─ Generate Board Tiles
├─ Generate All Chemistry Data  ◄─ CLICK THIS!
└─ (Other tools...)
```

### C. Confirmation Dialog

```
┌─────────────────────────────────────────┐
│  Generate Chemistry Data               │
│                                         │
│  This will create 41 chemistry data    │
│  assets:                                │
│  • 13 Ligands                          │
│  • 18 Questions                        │
│  • 10 Fate Cards                       │
│                                         │
│  Continue?                              │
│                                         │
│     [Cancel]        [Generate] ◄─ Click!│
└─────────────────────────────────────────┘
```

### D. Wait 5-10 Seconds

```
Unity Editor
┌────────────────────────────────────────────────┐
│                                                 │
│  Creating assets...                            │
│  [████████████████░░] 80%                      │
│                                                 │
└────────────────────────────────────────────────┘
```

### E. Success!

```
┌─────────────────────────────────────────┐
│  Success!                              │
│                                         │
│  ✅ Generated 41 chemistry data        │
│  assets!                                │
│                                         │
│  Check:                                 │
│  • Resources/Ligands/ (13)            │
│  • Resources/Questions/ (18)          │
│  • Resources/FateCards/ (10)          │
│                                         │
│              [OK] ◄─ Click              │
└─────────────────────────────────────────┘
```

---

## Step 4: Verify Assets Created

**Project window** (bottom-left):

```
Assets ▼
├─ Scenes/
├─ Scripts/
├─ Textures/
└─ Resources/ ◄─ Expand this
    ├─ Ligands/ (13 files) ✅
    │   ├─ h2o.asset
    │   ├─ nh3.asset
    │   ├─ en.asset
    │   └─ ...
    ├─ Questions/ (18 files) ✅
    │   ├─ q1.asset
    │   ├─ q2.asset
    │   └─ ...
    └─ FateCards/ (10 files) ✅
        ├─ f1.asset
        ├─ f2.asset
        └─ ...
```

**Total:** 41 files created! ✅

---

## Step 5: Test Game (Play Mode)

### A. Click Play Button

**Top-center toolbar:**

```
┌────────────────────────────────────────┐
│  ◄   ▶   ||   ▸     [●○○]            │
│       ↑                                │
│   Play button - Click here!            │
└────────────────────────────────────────┘
```

### B. Watch Console

**Console window** (bottom):

```
Console
├─ Loaded 13 ligands from Resources/Ligands ✅
├─ Loaded 18 questions from Resources/Questions ✅
├─ Loaded 10 fate cards from Resources/FateCards ✅
└─ Game ready!
```

**If errors:** Screenshot console and share with me

### C. Test Gameplay

1. **Roll dice** (press button or Enter key)
2. **Token moves**
3. **Watch Console** for tile interactions:
   ```
   🎁 Player 1 collected ligand: Water (H₂O)
   📝 Player 1 landed on question tile
   🎴 Player 2 triggered fate card: Move Forward 3
   ```

### D. Stop Game

**Click Play button again** (same button, now turns blue when playing)

```
▶  ← Gray = Not playing
▶  ← Blue = Playing (click to stop)
```

---

## ⌨️ Keyboard Shortcuts

| Action | Mac | Windows |
|--------|-----|---------|
| **Play/Stop** | Cmd + P | Ctrl + P |
| **Pause** | Cmd + Shift + P | Ctrl + Shift + P |
| **Console** | Cmd + Shift + C | Ctrl + Shift + C |
| **Project** | Cmd + 5 | Ctrl + 5 |
| **Inspector** | Cmd + 3 | Ctrl + 3 |

---

## 🐛 Troubleshooting

### Menu "COOR-CHEM" Tak Nampak

**Solution:**
1. Check **Console** for compilation errors (red messages)
2. Fix errors first
3. Unity → Assets → Refresh (Cmd+R / Ctrl+R)

### "No ligands found" Error

**Solution:**
1. Check `Assets/Resources/Ligands/` ada files ke?
2. If kosong, run generator lagi
3. Menu: COOR-CHEM → Generate All Chemistry Data

### Game Tak Jalan

**Solution:**
1. Ensure managers ada dalam scene:
   - LigandManager
   - QuestionCardManager
   - FateCardManager
2. Check Console for missing component errors

### Import Progress Stuck

**Solution:**
1. Close Unity
2. Delete `Library/` folder
3. Reopen project (will reimport everything)

---

## 📸 Visual Reference

When everything works, you'll see:

**Project Structure:**
```
✅ 41 assets in Resources/
✅ All .meta files generated
✅ No red errors in Console
```

**Game Playing:**
```
✅ Dice rolls
✅ Tokens move
✅ Tile interactions trigger
✅ Console shows chemistry events
```

---

## Next Steps After Generation

1. ✅ **Test gameplay** (Play mode)
2. ✅ **Setup scene managers** (if needed)
3. ✅ **Build UI panels** (optional)
4. ✅ **Add audio clips** (optional)
5. ✅ **Polish & deploy**

---

**Need help?** Screenshot apa yang Hazim nampak and share!
