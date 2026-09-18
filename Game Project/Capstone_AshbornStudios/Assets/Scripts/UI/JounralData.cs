using UnityEngine;
using UnityEngine.Serialization;

namespace Journal
{
    //Data in each journal entry
    [System.Serializable]
    public class JournalDataEntry
    {
        public int gemNumber;
        public string gemName;
        public string gemDescription;
        public string gemAbilityDescription;

    }

    //The json holding the data
    [System.Serializable]
    public class JournalDataJSON
    {
        public JournalDataEntry[] journalPage;
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

        public string getGemName(int gemNumber)
        {
            for(int i = 0; i < journalData.journalPage.Length; i++)
            {
                if(journalData.journalPage[i].gemNumber == gemNumber)
                {
                    return journalData.journalPage[i].gemName;
                }
            }
            return "[GEM NAME NOT FOUND]";
        }

        public string getGemDescription(int gemNumber)
        {
            for (int i = 0; i < journalData.journalPage.Length; i++)
            {
                if (journalData.journalPage[i].gemNumber == gemNumber)
                {
                    return journalData.journalPage[i].gemDescription;
                }
            }
            return "[GEM DESCRIPTION NOT FOUND]";
        }

        public string getGemAbilityDescription(int gemNumber)
        {
            for (int i = 0; i < journalData.journalPage.Length; i++)
            {
                if (journalData.journalPage[i].gemNumber == gemNumber)
                {
                    return journalData.journalPage[i].gemAbilityDescription;
                }
            }
            return "[GEM ABILITY NOT FOUND]";
        }
    }


}



