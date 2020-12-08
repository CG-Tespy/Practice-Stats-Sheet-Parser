using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(CharacterLoader))]
public class CharacterUpdaterEditor : Editor
{
    CharacterLoader updater;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        updater = target as CharacterLoader;
        ShowUpdateCharacterButton();
    }

    protected virtual void ShowUpdateCharacterButton()
    {
        if (GUILayout.Button("Create/Update"))
        {
            Update();
            
        }
    }

    public virtual void Update()
    {
        EnsureFolderPathExists();

        TextAsset characterSpreadsheet = updater.characterSpreadsheet;
        string[] linesArr = characterSpreadsheet.text.Split('\n');
        Queue<string> lines = new Queue<string>(linesArr);

        // The first line has only the headers. We want only the values
        lines.Dequeue();

        while (lines.Count > 1) // The last element will be an empty string, so we ignore it
        {
            string rawData = lines.Dequeue();
            UpdateCharacterWith(rawData);

        }
    }

    protected virtual void EnsureFolderPathExists()
    {
        string pathOnDisk = Path.Combine(Application.dataPath, updater.folderPath);
        characterFolderPath = "Assets\\" + updater.folderPath;

        if (!Directory.Exists(pathOnDisk))
        {
            Directory.CreateDirectory(pathOnDisk);
        }
    }

    string characterFolderPath = ""; // Set in the format the AssetDatabase expects

    protected virtual void UpdateCharacterWith(string rawData)
    {
        // Create a new Character with the data. If the character for it already exists,
        // we'll use the new one to update the old one.
        Character charInstance = Character.From(rawData);

        string pathToChar = characterFolderPath + "\\" + charInstance.DisplayName + ".asset";

        if (File.Exists(pathToChar))
        {
            Character alreadyThere = AssetDatabase.LoadAssetAtPath<Character>(pathToChar);
            alreadyThere.SetFrom(charInstance);
        }
        else
        {
            AssetDatabase.CreateAsset(charInstance, pathToChar);
        }

        AssetDatabase.SaveAssets();
    }
}
