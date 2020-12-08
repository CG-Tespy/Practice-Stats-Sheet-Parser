using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Stats = Character.CombatStats;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    Character character;
    bool statsVisible;

    public override void OnInspectorGUI()
    {
        character = target as Character;

        ShowDisplayName();
        ShowClassName();
        ShowStats();
    }

    protected virtual void ShowDisplayName()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Display Name");
        character.DisplayName = GUILayout.TextField(character.DisplayName);
        EditorGUILayout.EndHorizontal();
    }

    protected virtual void ShowClassName()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Class Name");
        character.ClassName = GUILayout.TextField(character.ClassName);
        EditorGUILayout.EndHorizontal();
    }

    protected virtual void ShowStats()
    {
        EditorGUILayout.BeginVertical();
        statsVisible = EditorGUILayout.Foldout(statsVisible, "Options");
        if (statsVisible)
        {
            EditorGUI.indentLevel++;

            Stats stats = character.Stats;

            stats.health = EditorGUILayout.IntField("Health", stats.health);
            stats.mana = EditorGUILayout.IntField("Mana", stats.mana);

            stats.attack = EditorGUILayout.IntField("Attack", stats.attack);
            stats.defense = EditorGUILayout.IntField("Defense", stats.defense);

            stats.magicAttack = EditorGUILayout.IntField("Magic Attack", stats.magicAttack);
            stats.magicDefense = EditorGUILayout.IntField("Magic Defense", stats.magicDefense);

            stats.speed = EditorGUILayout.IntField("Speed", stats.speed);
            stats.luck = EditorGUILayout.IntField("Luck", stats.luck);

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndVertical();
    }
}
