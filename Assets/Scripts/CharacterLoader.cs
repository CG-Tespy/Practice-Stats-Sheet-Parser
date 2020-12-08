using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This takes a CSV and creates/updates Character Scriptable Objects based on its contents.
/// You can even tell it where to create them, in a path relative to the Assets folder.
/// Though in actuality, it's the editor that does the actual work
/// </summary>
[CreateAssetMenu(fileName = "NewCharacterLoader", menuName = "RPG/Developer/CharacterLoader")]
public class CharacterLoader : ScriptableObject
{
    public TextAsset characterSpreadsheet;
    [Tooltip("Relative to the Asset folder's path. This is where the Characters will be loaded.")]
    public string folderPath = "Resources/Characters";
    
}
