using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Character_Creator
{
    public class CharacterBasic
    {
        public bool Set = false;
        public bool NameSet = false;
        public bool GenderSet = false;
        public bool RaceSet = false;
        public bool CClassSet = false;
        public bool BackgroundSet = false;
        public bool AlignmentSet = false;
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string SubRace { get; set; }
        public string Draconic { get; set; }
        public string CClass { get; set; }
        public string SubClass { get; set; }
        public string Background { get; set; }
        public string Alignment { get; set; }
        public int Level { get; set; }
        public int Speed { get; set; }

        public CharacterBasic(string name = "", string gender = "", string race = "", string subRace = "", string draconic = "", string cClass = "", string subClass = "", string background = "", string alignment = "", int level = 1, int speed = 0)
        {
            Name = name;
            Gender = gender;
            Race = race;
            SubRace = subRace;
            Draconic = draconic;
            CClass = cClass;
            SubClass = subClass;
            Background = background;
            Alignment = alignment;
            Level = level;
            Speed = speed;
        }
        public string SetGender()
        {
            Console.Clear();
            string[] genderOptions = { "Male ", "Female", "I am a robot. Beep boop. "};
            string[] genderSelection = { "Male", "Female", "Unspecified"};
            while (Gender == "")
            {
                Gender = Builder.Selection("Choose your character's gender: ", genderOptions, genderSelection);
            }
            return Gender;
        }
        public string SetAlignment()
        {
            string[] alignments = { "Lawful Good", "Neutral Good", "Chaotic Good", "Lawful Neutral", "True Neutral", "Chaotic Neutral", "Lawful Evil", "Neutral Evil", "Chaotic Evil", "What are you even talking about??" };
            while (Alignment == "")
            {
                Alignment = Builder.Selection("What is your character's alignment?     \r\nLawful Good     Neutral Good     Chaotic Good    " +
                                                                                           "\r\nLawful Neutral  True Neutral     Chaotic Neutral " +
                                                                                           "\r\nLawful Evil     Neutral Evil     Chaotic Evil    \r\n", alignments, alignments);
            }
            
            if (Alignment == "What are you even talking about??") //Secondary menu to help users if they don't understand character alignment
            {
                Alignment = AlignmentHelp();
            }
            return Alignment;
        }
        public static string AlignmentHelp() //Provides some help for new players who don't fully understand alignment. This application is not designed to be a tutorial, however this functionality is included as a courtesy.
        {
            string choice1 = "";
            string choice2 = "";
            string[] alignment1 = { "Lawful", "Neutral", "Chaotic" };
            string[] alignment2 = { "Good", "Neutral", "Evil" };

            string[] options1 = { "Yes, absolutely", "In between; It depends on the situation", "No, nobody tells my character what to do." };
            string[] options2 = { "Yes. Absolutely", "In between; It depends on the situation; There's no such thing as good or evil; etc.", "No, my character is a villain." };

            while (choice1 == "")
            {
                choice1 = Builder.Selection("Does your character obey laws, rules, norms, etc.?", options1, alignment1);
            }
            while (choice2 == "")
            {
                choice2 = Builder.Selection("Is your character morally good? ", options2, alignment2);
            }

            string combine = choice1 + " " + choice2;
            if (combine == "Neutral Neutral")
            {
                combine = "True Neutral";
            }
            return combine;
        }
    }

    public class Health
    {
        public int HitPoints;
        public int HitPointsConstant;
        public int ArmorClass;
        public int Initiative;
        public string HitDice;

        public bool HitPointsSet = false;
        public bool HitPointsConstantSet = false;
        public bool ArmorClassSet = false;
        public bool InitiativeSet = false;
        public bool HitDiceSet = false;
        public Health(int hitPoints = 0, int hitPointsConstant = 0, int armorClass = 0, string hitDice = "", int initiative = -5)
        {
            HitPoints = hitPoints;
            HitPointsConstant = hitPointsConstant;
            ArmorClass = armorClass;
            HitDice = hitDice;
            Initiative = initiative;
        }
        public static int SetArmorClass(Stats abilityModifiers, Equipment equipment)    //Armor class is determined by armor, shield, and/or Dexterity modifiers
        {
            //Armor and shield technically only affect AC if they are being used.
            //This application assumes that the player will be using said equipment upon selection. This is a design feature, as the intended scope is for quick-start setup, not ongoing character updating.
            
            int armor = 0;
            int dexBonus = abilityModifiers.Dexterity;
            int shield = 0;
            int leatherArmor = 11 + dexBonus;
            int scaleMail = 13 + dexBonus;
            int chainMail = 16;
            if (scaleMail > 15)
            {
                scaleMail = 15;
            }
            string[] armorSets = { "Leather Armor", "Scale Mail", "Chain Mail" };
            int[] armorValues = { leatherArmor, scaleMail, chainMail };
            for (int i = 0; i < armorSets.Length; i++)
            {
                if (equipment.Bag.Contains(armorSets[i]))
                {
                    armor = armorValues[i];
                }
            }
            if (equipment.Bag.Contains("Shield"))
            {
                shield = 2;
            }

            int armorClassVariables = armor + shield;
            return armorClassVariables;
        }
    }
}
