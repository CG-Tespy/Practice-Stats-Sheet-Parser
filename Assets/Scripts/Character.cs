using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "RPG/Character")]
public class Character : ScriptableObject
{
    [System.Serializable]
    public class CombatStats
    {
        public int health, mana,
        attack, defense,
        magicAttack, magicDefense,
        speed, luck;

        /// <summary>
        /// The string is assumed to be a line from the CSV
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static CombatStats From(string str)
        {
            List<string> statStrings = new List<string>(str.Split(','));

            // The first two values are not numbers, so...
            int firstValuesNotNumbers = 2;
            statStrings.RemoveRange(0, firstValuesNotNumbers);

            List<int> statInts = GetStatNums(statStrings);
            return From(statInts);
        }

        protected static List<int> GetStatNums(IList<string> statStrings)
        {
            List<int> statInts = new List<int>();
            for (int i = 0; i < statStrings.Count; i++)
            {
                string currentStat = statStrings[i];
                statInts.Add(int.Parse(currentStat));
            }

            return statInts;
        }

        protected static CombatStats From(IList<int> statInts)
        {
            CombatStats newStats = new CombatStats();
            int statIndex = 0;
            newStats.health = statInts[statIndex];
            statIndex++;
            newStats.mana = statInts[statIndex];
            statIndex++;
            newStats.attack = statInts[statIndex];
            statIndex++;
            newStats.defense = statInts[statIndex];
            statIndex++;
            newStats.magicAttack = statInts[statIndex];
            statIndex++;
            newStats.magicDefense = statInts[statIndex];
            statIndex++;
            newStats.speed = statInts[statIndex];
            statIndex++;
            newStats.luck = statInts[statIndex];

            return newStats;
        }

        public virtual void SetFrom(CombatStats other)
        {
            this.health = other.health;
            this.mana = other.mana;
            this.attack = other.attack;
            this.defense = other.defense;
            this.magicAttack = other.magicAttack;
            this.magicDefense = other.magicDefense;
            this.speed = other.speed;
            this.luck = other.luck;
        }

    }

    public string DisplayName { get; set; } = "";
    public string ClassName { get; set; } = "";

    public CombatStats Stats { get; set; } = new CombatStats();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawData">Line of character data from the CSV</param>
    /// <returns></returns>
    public static Character From(string rawData)
    {
        string[] lineVals = rawData.Split(',');
        string displayName = lineVals[0];
        string className = lineVals[1];

        CombatStats combatStats = CombatStats.From(rawData);
        Character newChar = ScriptableObject.CreateInstance<Character>();
        newChar.DisplayName = displayName;
        newChar.ClassName = className;

        newChar.SetStats(combatStats);

        return newChar;
    }

    public virtual void SetFrom(Character other)
    {
        this.DisplayName = other.DisplayName;
        this.ClassName = other.ClassName;
        this.SetStats(other.Stats);
    }

    public virtual void SetStats(CombatStats stats)
    {
        this.Stats.SetFrom(stats);
    }

}
