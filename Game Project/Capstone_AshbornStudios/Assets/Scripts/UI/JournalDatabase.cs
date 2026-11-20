using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

namespace Journal
{
    //Data in each journal entry
    [System.Serializable]
    public class JournalDataEntry
    {
        public int gemNumber;
        public int gemID;
        public string gemName;
        public string gemDescription;
        public string gemAbilityDescription;

    }

    //The json holding the data
    [System.Serializable]
    public class JournalDataJSON
    {
        public JournalDataEntry[] journalPages;
    }

    //Stores and retrieves data from JSON
    public class JournalDatabase : MonoBehaviour
    {
        public TextAsset jsonData;

        private JournalDataJSON journalData;
        
        void Start()
        {
            string json = jsonData.text;
            journalData = JsonUtility.FromJson<JournalDataJSON>(json);
        }

        public int getGemNumber(int gemID)
        {
            for (int i = 0; i < journalData.journalPages.Length; i++)
            {
                if (journalData.journalPages[i].gemID == gemID)
                {
                    return journalData.journalPages[i].gemID;
                }
            }
            return 000000000;
        }

        public int getGemID(int gemNumber)
        {
            for (int i = 0; i < journalData.journalPages.Length; i++)
            {
                if (journalData.journalPages[i].gemNumber == gemNumber)
                {
                    return journalData.journalPages[i].gemID;
                }
            }
            return 000000000;
        }

        public string getGemName(int gemNumber)
        {
            for(int i = 0; i < journalData.journalPages.Length; i++)
            {
                if(journalData.journalPages[i].gemNumber == gemNumber)
                {
                    return journalData.journalPages[i].gemName;
                }
            }
            return "[GEM NAME NOT FOUND]";
        }

        public string getGemDescription(int gemNumber)
        {
            for (int i = 0; i < journalData.journalPages.Length; i++)
            {
                if (journalData.journalPages[i].gemNumber == gemNumber)
                {
                    return journalData.journalPages[i].gemDescription;
                }
            }
            return "[GEM DESCRIPTION NOT FOUND]";
        }

        public string getGemAbilityDescription(int gemNumber)
        {
            for (int i = 0; i < journalData.journalPages.Length; i++)
            {
                if (journalData.journalPages[i].gemNumber == gemNumber)
                {
                    return journalData.journalPages[i].gemAbilityDescription;
                }
            }
            return "[GEM ABILITY NOT FOUND]";
        }
    }


}



