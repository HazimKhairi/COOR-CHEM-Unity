using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// COOR-CHEM: Auto-generate all chemistry data assets (41 total)
/// Menu: COOR-CHEM → Generate All Chemistry Data
/// </summary>
public class ChemistryDataGenerator : EditorWindow
{
    [MenuItem("COOR-CHEM/Generate All Chemistry Data")]
    static void GenerateAllData()
    {
        if (!EditorUtility.DisplayDialog("Generate Chemistry Data",
            "This will create 41 chemistry data assets:\n" +
            "• 13 Ligands\n" +
            "• 18 Questions\n" +
            "• 10 Fate Cards\n\n" +
            "Continue?", "Generate", "Cancel"))
        {
            return;
        }

        CreateFolders();
        GenerateLigands();
        GenerateQuestions();
        GenerateFateCards();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success!",
            "✅ Generated 41 chemistry data assets!\n\n" +
            "Check:\n" +
            "• Resources/Ligands/ (13)\n" +
            "• Resources/Questions/ (18)\n" +
            "• Resources/FateCards/ (10)",
            "OK");

        Debug.Log("✅ COOR-CHEM: All chemistry data generated successfully!");
    }

    static void CreateFolders()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/Ligands"))
            AssetDatabase.CreateFolder("Assets/Resources", "Ligands");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/Questions"))
            AssetDatabase.CreateFolder("Assets/Resources", "Questions");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/FateCards"))
            AssetDatabase.CreateFolder("Assets/Resources", "FateCards");
    }

    #region Ligand Generation

    static void GenerateLigands()
    {
        CreateLigand("h2o", "Water", "H₂O", 0, new Color(0.4f, 0.7f, 1f), "Light Blue", "monodentate", 1);
        CreateLigand("nh3", "Ammonia", "NH₃", 0, new Color(0.6f, 0.8f, 1f), "Sky Blue", "monodentate", 1);
        CreateLigand("cl", "Chloride", "Cl⁻", -1, new Color(0.5f, 1f, 0.5f), "Light Green", "monodentate", 1);
        CreateLigand("cn", "Cyanide", "CN⁻", -1, new Color(1f, 1f, 0.6f), "Light Yellow", "monodentate", 1);
        CreateLigand("co", "Carbon Monoxide", "CO", 0, new Color(0.7f, 0.7f, 0.7f), "Gray", "monodentate", 1);
        CreateLigand("py", "Pyridine", "C₅H₅N", 0, new Color(0.9f, 0.7f, 1f), "Lavender", "monodentate", 1);
        CreateLigand("en", "Ethylenediamine", "en", 0, new Color(1f, 0.8f, 0.4f), "Orange", "bidentate", 2);
        CreateLigand("ox", "Oxalate", "C₂O₄²⁻", -2, new Color(1f, 0.6f, 0.6f), "Pink", "bidentate", 2);
        CreateLigand("acac", "Acetylacetonate", "acac⁻", -1, new Color(1f, 0.9f, 0.4f), "Golden", "bidentate", 2);
        CreateLigand("bipy", "Bipyridine", "bipy", 0, new Color(0.8f, 0.5f, 1f), "Purple", "bidentate", 2);
        CreateLigand("phen", "Phenanthroline", "phen", 0, new Color(1f, 0.5f, 0.8f), "Magenta", "bidentate", 2);
        CreateLigand("dien", "Diethylenetriamine", "dien", 0, new Color(0.4f, 0.9f, 0.7f), "Mint", "tridentate", 3);
        CreateLigand("edta", "EDTA", "EDTA⁴⁻", -4, new Color(0.3f, 0.5f, 0.8f), "Deep Blue", "hexadentate", 6);

        Debug.Log("✅ Generated 13 ligands");
    }

    static void CreateLigand(string id, string name, string formula, int charge, Color color, string colorName, string denticity, int denticityValue)
    {
        LigandData ligand = ScriptableObject.CreateInstance<LigandData>();
        ligand.ligandID = id;
        ligand.ligandName = name;
        ligand.formula = formula;
        ligand.charge = charge;
        ligand.ligandColor = color;
        ligand.colorName = colorName;
        ligand.denticity = denticity;
        ligand.denticityValue = denticityValue;

        string path = $"Assets/Resources/Ligands/{id}.asset";
        AssetDatabase.CreateAsset(ligand, path);
    }

    #endregion

    #region Question Generation

    static void GenerateQuestions()
    {
        // Easy Questions (6)
        CreateQuestion("q1", DifficultyLevel.Easy, 3,
            "What is a ligand in coordination chemistry?",
            new string[] {
                "A molecule or ion that donates electrons to a metal",
                "A metal ion that accepts electrons",
                "A covalent bond between metals",
                "A type of ionic compound"
            },
            0,
            "Think about electron donation in coordination compounds");

        CreateQuestion("q2", DifficultyLevel.Easy, 3,
            "Which of these is a monodentate ligand?",
            new string[] {
                "H₂O",
                "Ethylenediamine (en)",
                "EDTA",
                "Oxalate"
            },
            0,
            "Mono = one binding site");

        CreateQuestion("q3", DifficultyLevel.Easy, 3,
            "What does 'bidentate' mean?",
            new string[] {
                "Has two donor atoms",
                "Has one donor atom",
                "Has three donor atoms",
                "Has no donor atoms"
            },
            0,
            "Bi = two");

        CreateQuestion("q4", DifficultyLevel.Easy, 3,
            "What is the charge of CN⁻ ligand?",
            new string[] {
                "-1",
                "0",
                "+1",
                "-2"
            },
            0,
            "Look at the superscript");

        CreateQuestion("q5", DifficultyLevel.Easy, 3,
            "Which ligand is hexadentate?",
            new string[] {
                "EDTA",
                "Water",
                "Ammonia",
                "Chloride"
            },
            0,
            "Hexa = six binding sites");

        CreateQuestion("q6", DifficultyLevel.Easy, 3,
            "What color is the ammonia (NH₃) ligand?",
            new string[] {
                "Sky Blue",
                "Red",
                "Green",
                "Yellow"
            },
            0,
            "Check the ligand card");

        // Medium Questions (6)
        CreateQuestion("q7", DifficultyLevel.Medium, 4,
            "What is the denticity of ethylenediamine (en)?",
            new string[] {
                "2",
                "1",
                "3",
                "4"
            },
            0,
            "It's a bidentate ligand");

        CreateQuestion("q8", DifficultyLevel.Medium, 4,
            "Which ligand has the highest denticity?",
            new string[] {
                "EDTA (6)",
                "en (2)",
                "dien (3)",
                "H₂O (1)"
            },
            0,
            "Hexadentate is the highest");

        CreateQuestion("q9", DifficultyLevel.Medium, 4,
            "What is the coordination number in [Co(NH₃)₆]³⁺?",
            new string[] {
                "6",
                "3",
                "9",
                "12"
            },
            0,
            "Count the donor atoms around Co");

        CreateQuestion("q10", DifficultyLevel.Medium, 4,
            "Which is a tridentate ligand?",
            new string[] {
                "dien",
                "en",
                "H₂O",
                "EDTA"
            },
            0,
            "Tri = three donor atoms");

        CreateQuestion("q11", DifficultyLevel.Medium, 4,
            "What is the charge of EDTA ligand?",
            new string[] {
                "-4",
                "-1",
                "0",
                "-2"
            },
            0,
            "It's a highly charged anion");

        CreateQuestion("q12", DifficultyLevel.Medium, 4,
            "In [Fe(en)₃]²⁺, how many ligands are present?",
            new string[] {
                "3",
                "6",
                "2",
                "9"
            },
            0,
            "The subscript shows the count");

        // Hard Questions (6)
        CreateQuestion("q13", DifficultyLevel.Hard, 5,
            "What is the coordination number if 2 EDTA ligands bind to one metal?",
            new string[] {
                "12",
                "6",
                "8",
                "4"
            },
            0,
            "EDTA is hexadentate, 6×2=12");

        CreateQuestion("q14", DifficultyLevel.Hard, 5,
            "Which combination gives coordination number 6?",
            new string[] {
                "3 bidentate ligands",
                "2 tridentate ligands",
                "1 hexadentate ligand",
                "All of the above"
            },
            3,
            "Multiple ways to reach 6");

        CreateQuestion("q15", DifficultyLevel.Hard, 5,
            "What is the total charge in [Co(NH₃)₄Cl₂]⁺?",
            new string[] {
                "+1",
                "+3",
                "0",
                "+2"
            },
            0,
            "NH₃=0, Cl=-1, total must equal +1");

        CreateQuestion("q16", DifficultyLevel.Hard, 5,
            "If a complex has 4 en ligands, what is the coordination number?",
            new string[] {
                "8",
                "4",
                "6",
                "12"
            },
            0,
            "en is bidentate, 4×2=8");

        CreateQuestion("q17", DifficultyLevel.Hard, 5,
            "Which ligand forms the most stable chelate complex?",
            new string[] {
                "EDTA (hexadentate)",
                "H₂O (monodentate)",
                "en (bidentate)",
                "dien (tridentate)"
            },
            0,
            "Higher denticity = more stable");

        CreateQuestion("q18", DifficultyLevel.Hard, 5,
            "In [Cr(H₂O)₄Cl₂]⁺, what is the oxidation state of Cr?",
            new string[] {
                "+3",
                "+2",
                "+4",
                "+1"
            },
            0,
            "H₂O=0, Cl=-1, complex=+1");

        Debug.Log("✅ Generated 18 questions");
    }

    static void CreateQuestion(string id, DifficultyLevel difficulty, int points, string questionText, string[] options, int correctIndex, string hint)
    {
        QuestionData question = ScriptableObject.CreateInstance<QuestionData>();
        question.questionID = id;
        question.difficulty = difficulty;
        question.points = points;
        question.questionText = questionText;
        question.answerOptions = options;
        question.correctAnswerIndex = correctIndex;
        question.hint = hint;

        string path = $"Assets/Resources/Questions/{id}.asset";
        AssetDatabase.CreateAsset(question, path);
    }

    #endregion

    #region Fate Card Generation

    static void GenerateFateCards()
    {
        CreateFateCard("f1", "Move Forward 3 Spaces", FateEffect.Move, 3);
        CreateFateCard("f2", "Move Backward 2 Spaces", FateEffect.Move, -2);
        CreateFateCard("f3", "Gain 5 Points", FateEffect.Points, 5);
        CreateFateCard("f4", "Lose 3 Points", FateEffect.Points, -3);
        CreateFateCard("f5", "Free Random Ligand", FateEffect.Ligand, 0);
        CreateFateCard("f6", "Skip Next Turn", FateEffect.SkipTurn, 0);
        CreateFateCard("f7", "Extra Dice Roll", FateEffect.ExtraRoll, 0);
        CreateFateCard("f8", "Swap Position with Another Player", FateEffect.Swap, 0);
        CreateFateCard("f9", "Shield (Immune to Negative Effects)", FateEffect.Shield, 0);
        CreateFateCard("f10", "Steal 2 Points from Another Player", FateEffect.Points, 2);

        Debug.Log("✅ Generated 10 fate cards");
    }

    static void CreateFateCard(string id, string description, FateEffect effect, int value)
    {
        FateCardData fateCard = ScriptableObject.CreateInstance<FateCardData>();
        fateCard.fateID = id;
        fateCard.description = description;
        fateCard.effectType = effect;
        fateCard.value = value;

        string path = $"Assets/Resources/FateCards/{id}.asset";
        AssetDatabase.CreateAsset(fateCard, path);
    }

    #endregion
}
