# Quick Fix - Unity Compilation Errors

## Problem
- Menu "COOR-CHEM" tidak muncul
- Folders kosong (Ligands, Questions, FateCards)
- Error: "All compiler errors have to be fixed before you can enter playmode!"

## Solution Steps

### 1. Open Console Window
**Keyboard:** `Cmd + Shift + C` (Mac) or `Ctrl + Shift + C` (Windows)

**Or click:** Bottom tab "Console"

```
┌────────────────────────────────────┐
│ Project │ Console ◄─ Click here    │
└────────────────────────────────────┘
```

### 2. Look for RED error messages

Console will show:
```
❌ Assets/Scripts/Editor/ChemistryDataGenerator.cs(123,45): error CS0246: The type or namespace name 'X' could not be found
```

### 3. Screenshot the errors and share

Take screenshot of Console window showing ALL red errors.

---

## Common Fixes (Try These First)

### Fix 1: Missing UnityEditor namespace

**If error says:** `The type or namespace name 'UnityEditor' could not be found`

**Solution:** Check if file has:
```csharp
using UnityEditor;  // At top of file
```

### Fix 2: Script not in Editor folder

**If error says:** `EditorWindow` or `MenuItem` not found

**Check:** File MUST be in `Assets/Scripts/Editor/` folder

**Current location:**
```
Assets/Scripts/Editor/ChemistryDataGenerator.cs ✅ (correct)
```

### Fix 3: DifficultyLevel enum not found

**If error says:** `The type or namespace name 'DifficultyLevel' could not be found`

**Check:** `QuestionData.cs` must define enum:
```csharp
public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}
```

### Fix 4: FateEffect enum not found

**If error says:** `The type or namespace name 'FateEffect' could not be found`

**Check:** `FateCardData.cs` must define enum:
```csharp
public enum FateEffect
{
    Move,
    Points,
    Ligand,
    SkipTurn,
    ExtraRoll,
    Swap,
    Shield
}
```

---

## Manual Verification

### Check if these files exist:

1. ✅ `Assets/Scripts/Data/LigandData.cs`
2. ✅ `Assets/Scripts/Data/QuestionData.cs`
3. ✅ `Assets/Scripts/Data/FateCardData.cs`
4. ✅ `Assets/Scripts/Editor/ChemistryDataGenerator.cs`

### Check if enums are defined:

**In QuestionData.cs:**
```csharp
public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}
```

**In FateCardData.cs:**
```csharp
public enum FateEffect
{
    Move,
    Points,
    Ligand,
    SkipTurn,
    ExtraRoll,
    Swap,
    Shield
}
```

---

## After Fixing Errors

1. **Console should be clear** (no red errors)
2. **Menu appears:** Top menu bar → `COOR-CHEM`
3. **Click:** `COOR-CHEM` → `Generate All Chemistry Data`
4. **Click:** `Generate` button
5. **Wait 5-10 seconds**
6. **Check folders:** Should have 41 files now

---

## Still Not Working?

**Try Force Reimport:**
1. Unity menu → `Assets` → `Reimport All`
2. Wait for reimport to complete
3. Check Console again

**Or try:**
1. Close Unity
2. Delete `Library/` folder in project
3. Reopen Unity (will rebuild everything)
