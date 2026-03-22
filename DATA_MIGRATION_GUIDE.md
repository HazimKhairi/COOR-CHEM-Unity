# COOR-CHEM Data Migration Guide

This guide shows how to create all chemistry data assets in Unity Editor from the web version's `data.js`.

---

## 📁 Step 1: Create Folder Structure

In Unity Project window:

```
Assets/
├── Resources/
│   ├── Ligands/        # Create this folder
│   ├── Questions/      # Create this folder
│   └── FateCards/      # Create this folder
└── Scripts/
    └── Data/           # Already created (LigandData.cs, QuestionData.cs, FateCardData.cs)
```

**Actions:**
1. Right-click `Assets/Resources/` → Create → Folder → Name: `Ligands`
2. Right-click `Assets/Resources/` → Create → Folder → Name: `Questions`
3. Right-click `Assets/Resources/` → Create → Folder → Name: `FateCards`

---

## 🧪 Step 2: Create 13 Ligand Assets

### How to Create One Ligand:
1. Right-click `Assets/Resources/Ligands/`
2. Create → COOR-CHEM → Ligand
3. Name it with the ligand ID (e.g., `H2O`, `NH3`, `en`)
4. Fill in the Inspector fields

### 13 Ligands to Create:

#### 1. **H2O** (Water)
```
Ligand ID: H2O
Ligand Name: Water
Formula: H₂O
Charge: 0
Ligand Color: RGB(244, 67, 54) → #f44336 red
Color Name: Red
Denticity: monodentate
Denticity Value: 1
```

#### 2. **NH3** (Ammonia)
```
Ligand ID: NH3
Ligand Name: Ammonia
Formula: NH₃
Charge: 0
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: monodentate
Denticity Value: 1
```

#### 3. **py** (Pyridine)
```
Ligand ID: py
Ligand Name: Pyridine
Formula: py
Charge: 0
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: monodentate
Denticity Value: 1
```

#### 4. **PPh3** (Triphenylphosphine)
```
Ligand ID: PPh3
Ligand Name: Triphenylphosphine
Formula: PPh₃
Charge: 0
Ligand Color: RGB(255, 214, 67) → #ffd643 orange
Color Name: Orange
Denticity: monodentate
Denticity Value: 1
```

#### 5. **CN** (Cyanide)
```
Ligand ID: CN
Ligand Name: Cyanide
Formula: CN⁻
Charge: -1
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: monodentate
Denticity Value: 1
```

#### 6. **O2** (Oxide)
```
Ligand ID: O2
Ligand Name: Oxide
Formula: O²⁻
Charge: -2
Ligand Color: RGB(244, 67, 54) → #f44336 red
Color Name: Red
Denticity: monodentate
Denticity Value: 1
```

#### 7. **Cl** (Chloride)
```
Ligand ID: Cl
Ligand Name: Chloride
Formula: Cl⁻
Charge: -1
Ligand Color: RGB(76, 175, 80) → #4CAF50 green
Color Name: Green
Denticity: monodentate
Denticity Value: 1
```

#### 8. **ox** (Oxalate)
```
Ligand ID: ox
Ligand Name: Oxalate
Formula: ox
Charge: -2
Ligand Color: RGB(244, 67, 54) → #f44336 red
Color Name: Red
Denticity: bidentate
Denticity Value: 2
```

#### 9. **acac** (Acetylacetonate)
```
Ligand ID: acac
Ligand Name: Acetylacetonate
Formula: acac
Charge: -1
Ligand Color: RGB(244, 67, 54) → #f44336 red
Color Name: Red
Denticity: bidentate
Denticity Value: 2
```

#### 10. **CO3** (Carbonate)
```
Ligand ID: CO3
Ligand Name: Carbonate
Formula: CO₃²⁻
Charge: -2
Ligand Color: RGB(244, 67, 54) → #f44336 red
Color Name: Red
Denticity: bidentate
Denticity Value: 2
```

#### 11. **phen** (1,10-Phenanthroline)
```
Ligand ID: phen
Ligand Name: 1,10-Phenanthroline
Formula: phen
Charge: 0
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: bidentate
Denticity Value: 2
```

#### 12. **bipy** (2,2'-Bipyridine)
```
Ligand ID: bipy
Ligand Name: 2,2'-Bipyridine
Formula: bipy
Charge: 0
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: bidentate
Denticity Value: 2
```

#### 13. **en** (Ethylenediamine)
```
Ligand ID: en
Ligand Name: Ethylenediamine
Formula: en
Charge: 0
Ligand Color: RGB(33, 150, 243) → #2196F3 blue
Color Name: Blue
Denticity: bidentate
Denticity Value: 2
```

---

## ❓ Step 3: Create 18 Question Card Assets

### How to Create One Question:
1. Right-click `Assets/Resources/Questions/`
2. Create → COOR-CHEM → Question Card
3. Name it with question ID (e.g., `q1`, `q2`)
4. Fill in Inspector fields

### 18 Questions to Create:

#### **EASY Questions (3 points each)**

#### q1
```
Question ID: q1
Difficulty: Easy
Points: 3
Question Text: What type of ligand is NH₃?
Answer Options:
  [0]: Monodentate
  [1]: Bidentate
  [2]: Polydentate
  [3]: Ambidentate
Correct Answer Index: 0
Hint: NH₃ donates through one nitrogen atom.
```

#### q2
```
Question ID: q2
Difficulty: Easy
Points: 3
Question Text: What is the charge of the chloride ligand (Cl⁻)?
Answer Options:
  [0]: 0
  [1]: -1
  [2]: -2
  [3]: +1
Correct Answer Index: 1
Hint: Chloride is a halide ion.
```

#### q3
```
Question ID: q3
Difficulty: Easy
Points: 3
Question Text: Which ligand is also known as "en"?
Answer Options:
  [0]: Ethanol
  [1]: Ethylenediamine
  [2]: Ethyne
  [3]: Ethanolamine
Correct Answer Index: 1
Hint: It has two -NH₂ groups.
```

#### q4
```
Question ID: q4
Difficulty: Easy
Points: 3
Question Text: How many donor atoms does a monodentate ligand have?
Answer Options:
  [0]: 1
  [1]: 2
  [2]: 3
  [3]: 4
Correct Answer Index: 0
Hint: Mono means one.
```

#### q5
```
Question ID: q5
Difficulty: Easy
Points: 3
Question Text: What is the coordination number in [Co(NH₃)₆]³⁺?
Answer Options:
  [0]: 3
  [1]: 4
  [2]: 5
  [3]: 6
Correct Answer Index: 3
Hint: Count the donor atoms from all ligands.
```

#### q6
```
Question ID: q6
Difficulty: Easy
Points: 3
Question Text: Which color sphere represents H₂O in the indicator table?
Answer Options:
  [0]: Blue
  [1]: Green
  [2]: Red
  [3]: Orange
Correct Answer Index: 2
Hint: Check the indicator table.
```

#### **MEDIUM Questions (4 points each)**

#### q7
```
Question ID: q7
Difficulty: Medium
Points: 4
Question Text: What is the geometry of a complex with coordination number 6?
Answer Options:
  [0]: Tetrahedral
  [1]: Square planar
  [2]: Octahedral
  [3]: Trigonal planar
Correct Answer Index: 2
Hint: Think of 6 ligands arranged symmetrically around a central atom.
```

#### q8
```
Question ID: q8
Difficulty: Medium
Points: 4
Question Text: Ethylenediamine (en) is a bidentate ligand. What is its denticity value?
Answer Options:
  [0]: 1
  [1]: 2
  [2]: 3
  [3]: 4
Correct Answer Index: 1
Hint: Bidentate means two donor atoms.
```

#### q9
```
Question ID: q9
Difficulty: Medium
Points: 4
Question Text: A complex [CoCl₂(en)₂]⁺ has which type of charge?
Answer Options:
  [0]: Neutral
  [1]: Cation
  [2]: Anion
Correct Answer Index: 1
Hint: Calculate: Co charge + Cl charges + en charges.
```

#### q10
```
Question ID: q10
Difficulty: Medium
Points: 4
Question Text: What is the coordination number of [PtCl₄]²⁻?
Answer Options:
  [0]: 2
  [1]: 4
  [2]: 6
  [3]: 8
Correct Answer Index: 1
Hint: Each Cl is monodentate.
```

#### q11
```
Question ID: q11
Difficulty: Medium
Points: 4
Question Text: Which geometry is associated with coordination number 4 and a d⁸ metal?
Answer Options:
  [0]: Tetrahedral
  [1]: Square planar
  [2]: Octahedral
  [3]: Trigonal bipyramidal
Correct Answer Index: 1
Hint: Think of Pt(II) complexes.
```

#### q12
```
Question ID: q12
Difficulty: Medium
Points: 4
Question Text: What is the total charge contribution of 2 oxalate (ox) ligands?
Answer Options:
  [0]: -2
  [1]: -4
  [2]: 0
  [3]: -1
Correct Answer Index: 1
Hint: Each oxalate has charge -2.
```

#### **HARD Questions (5 points each)**

#### q13
```
Question ID: q13
Difficulty: Hard
Points: 5
Question Text: In [Fe(CN)₆]⁴⁻, what is the oxidation state of Fe?
Answer Options:
  [0]: +2
  [1]: +3
  [2]: +4
  [3]: 0
Correct Answer Index: 0
Hint: CN⁻ has -1 charge each. Overall charge is -4.
```

#### q14
```
Question ID: q14
Difficulty: Hard
Points: 5
Question Text: Which complex would exhibit a trigonal bipyramidal geometry?
Answer Options:
  [0]: [Ni(CN)₄]²⁻
  [1]: [Fe(CO)₅]
  [2]: [Co(NH₃)₆]³⁺
  [3]: [PtCl₄]²⁻
Correct Answer Index: 1
Hint: Count 5 ligands around the metal.
```

#### q15
```
Question ID: q15
Difficulty: Hard
Points: 5
Question Text: In [Co(en)₂Cl₂]⁺, what is the coordination number?
Answer Options:
  [0]: 4
  [1]: 5
  [2]: 6
  [3]: 8
Correct Answer Index: 2
Hint: en is bidentate (2 donor atoms each), Cl is monodentate.
```

#### q16
```
Question ID: q16
Difficulty: Hard
Points: 5
Question Text: Which of these is NOT a possible geometry for coordination number 5?
Answer Options:
  [0]: Trigonal bipyramidal
  [1]: Square pyramidal
  [2]: Octahedral
  [3]: Pentagonal planar
Correct Answer Index: 2
Hint: Octahedral requires 6 positions.
```

#### q17
```
Question ID: q17
Difficulty: Hard
Points: 5
Question Text: What is the charge of the complex [Cr(ox)₃]³⁻?
Answer Options:
  [0]: -3
  [1]: -6
  [2]: 0
  [3]: +3
Correct Answer Index: 0
Hint: Cr³⁺ + 3(ox²⁻) = +3 + (-6) = -3.
```

#### q18
```
Question ID: q18
Difficulty: Hard
Points: 5
Question Text: A neutral complex has a metal with +3 charge and 3 identical ligands. What must each ligand's charge be?
Answer Options:
  [0]: -1
  [1]: -2
  [2]: 0
  [3]: +1
Correct Answer Index: 0
Hint: For neutral: metal charge + total ligand charge = 0.
```

---

## 🎴 Step 4: Create 10 Fate Card Assets

### How to Create One Fate Card:
1. Right-click `Assets/Resources/FateCards/`
2. Create → COOR-CHEM → Fate Card
3. Name it with fate ID (e.g., `f1`, `f2`)
4. Fill in Inspector fields

### 10 Fate Cards to Create:

#### f1
```
Fate ID: f1
Description: Lucky break! Move forward 3 spaces.
Effect Type: Move
Value: 3
Card Color: RGB(76, 175, 80) → #4CAF50 green (positive)
```

#### f2
```
Fate ID: f2
Description: Oops! Move back 2 spaces.
Effect Type: Move
Value: -2
Card Color: RGB(244, 67, 54) → #f44336 red (negative)
```

#### f3
```
Fate ID: f3
Description: Bonus! Earn 5 extra points.
Effect Type: Points
Value: 5
Card Color: RGB(76, 175, 80) → #4CAF50 green (positive)
```

#### f4
```
Fate ID: f4
Description: Bad luck! Lose 3 points.
Effect Type: Points
Value: -3
Card Color: RGB(244, 67, 54) → #f44336 red (negative)
```

#### f5
```
Fate ID: f5
Description: Free ligand! Collect a random ligand card.
Effect Type: Ligand
Value: 1
Card Color: RGB(103, 58, 183) → #673AB7 purple (gift)
```

#### f6
```
Fate ID: f6
Description: Skip a turn! You lose your next turn.
Effect Type: SkipTurn
Value: 1
Card Color: RGB(244, 67, 54) → #f44336 red (negative)
```

#### f7
```
Fate ID: f7
Description: Double roll! Roll the dice again.
Effect Type: ExtraRoll
Value: 1
Card Color: RGB(76, 175, 80) → #4CAF50 green (positive)
```

#### f8
```
Fate ID: f8
Description: Swap! Exchange positions with the nearest player.
Effect Type: Swap
Value: 1
Card Color: RGB(255, 152, 0) → #FF9800 orange (neutral/tricksy)
```

#### f9
```
Fate ID: f9
Description: Shield! You are immune to the next fate card.
Effect Type: Shield
Value: 1
Card Color: RGB(33, 150, 243) → #2196F3 blue (protective)
```

#### f10
```
Fate ID: f10
Description: Jackpot! Earn 8 bonus points.
Effect Type: Points
Value: 8
Card Color: RGB(255, 193, 7) → #FFC107 gold (jackpot!)
```

---

## ✅ Verification Checklist

After creating all assets, verify:

- [ ] 13 ligand assets in `Assets/Resources/Ligands/`
- [ ] 18 question assets in `Assets/Resources/Questions/`
- [ ] 10 fate card assets in `Assets/Resources/FateCards/`
- [ ] All assets have correct colors (use Color picker in Inspector)
- [ ] All Unicode symbols display correctly (₂, ₃, ⁻, ²⁻, ³⁺)
- [ ] No missing fields in Inspector

---

## 🚀 Next Steps

Once all data assets are created:

1. **Phase 2:** Create game managers (LigandManager, QuestionCardManager)
2. **Phase 3:** Modify PlayerToken to interact with tiles
3. **Phase 4:** Build UI panels
4. **Phase 5:** Test gameplay loop

**Total Assets to Create:**
- ✅ 3 ScriptableObject scripts (done)
- 📝 13 Ligands
- 📝 18 Questions
- 📝 10 Fate Cards

**Estimated Time:** 30-45 minutes for manual asset creation

---

## 💡 Tips

### Converting Hex Colors to Unity RGB:
Unity uses 0-1 range. Convert hex like this:
- `#f44336` → R: 244/255 = 0.957, G: 67/255 = 0.263, B: 54/255 = 0.212
- Or use online converter: https://www.rapidtables.com/convert/color/hex-to-rgb.html

### Unicode Symbols in Unity:
Unity supports Unicode! Just copy-paste from this document:
- Subscript: ₂ ₃ ₄
- Superscript: ⁺ ⁻ ² ³ ⁴

### Batch Editing:
Select multiple assets → Lock Inspector → Edit common fields together!

---

**Ready to start creating assets in Unity Editor!** 🎮
