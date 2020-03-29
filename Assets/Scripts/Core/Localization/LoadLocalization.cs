using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VFG.Core.Localization
{
    public delegate void ChangeLanguageEvent();

    public class LoadLocalization : MonoBehaviour
    {
        public event ChangeLanguageEvent ChangeLanguageEvent;

        const int FIRST_ELEMENT = 1;
        const int LAST_ELEMENT = 2;
		const int NUM_LANGUAGES = 11;

        const string PATH_LANGUAGES = "Localization/Languages";
        const string PATH_GENERIC_KEYS = "Localization/GenericKeys";
        const string PATH_PLACES = "Localization/Places";
        const string PATH_OBJECTIVES = "Localization/Objectives";
        const string PATH_DESCRIPTION = "Localization/Description";

        private TextAsset languagesLocalizationText;
        private TextAsset genericKeysLocalizationText;
        private TextAsset placesLocalizationText;
        private TextAsset objectivesLocalizationText;
        private TextAsset descriptionsLocalizationText;

        string[,] languagesGrid;
        string[,] genericKeysGrid;
        string[,] placesGrid;
        string[,] objectivesGrid;
        string[,] descriptionsGrid;

        private string currentLanguage;
        private int indexOfLanguage;
        private List<string> languages;
        private Dictionary<string, string> keys = new Dictionary<string, string>();

        public static LoadLocalization Instance;

        void Awake()
        {
            Instance = this;

            languagesLocalizationText = Resources.Load(PATH_LANGUAGES) as TextAsset;
            genericKeysLocalizationText = Resources.Load(PATH_GENERIC_KEYS) as TextAsset;
            placesLocalizationText = Resources.Load(PATH_PLACES) as TextAsset;
            objectivesLocalizationText = Resources.Load(PATH_OBJECTIVES) as TextAsset;
            descriptionsLocalizationText = Resources.Load(PATH_DESCRIPTION) as TextAsset;
        }

        public void Init()
        {
            languagesGrid = SplitCsvGrid(languagesLocalizationText.text);
            genericKeysGrid = SplitCsvGrid(genericKeysLocalizationText.text);
            placesGrid = SplitCsvGrid(placesLocalizationText.text);
            objectivesGrid = SplitCsvGrid(objectivesLocalizationText.text);
            descriptionsGrid = SplitCsvGrid(descriptionsLocalizationText.text);

			if (GameState.FirstTimeIstUsed == (int)GameState.Toggle.On) 
			{
				currentLanguage = GetSystemLanguage (Application.systemLanguage);
				GameState.Language = currentLanguage;
				Debug.Log ("FIRST USE: " + currentLanguage);
			}
            else
			{
                currentLanguage = GameState.Language;
				Debug.Log ("SECOND USE: " + currentLanguage);
			}

            indexOfLanguage = languages.IndexOf(currentLanguage.ToString()) - FIRST_ELEMENT;
            ChangeLanguage();
        }

        private string GetSystemLanguage(SystemLanguage systemLanguage)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.English:
                case SystemLanguage.Spanish:
                case SystemLanguage.Catalan:
                case SystemLanguage.Portuguese:
                case SystemLanguage.Italian:
                case SystemLanguage.French:
                case SystemLanguage.German:
                case SystemLanguage.Russian:
				case SystemLanguage.ChineseTraditional:
                case SystemLanguage.Korean:
                case SystemLanguage.Japanese: return Application.systemLanguage.ToString();

                default: return SystemLanguage.English.ToString();
            }
        }

		public string GetCurrentLanguage
		{
			get { return currentLanguage; }
			set { currentLanguage = value; }
		}

        private string[,] SplitCsvGrid(string csvText)
        {
            string[] textParsed = csvText.Split("\n"[0]);
            GetLanguageList(textParsed);

            int width = 0;

            for (int i = 0; i < textParsed.Length; i++)
            {
                string[] row = SplitCsvLine(textParsed[i]);
                width = Mathf.Max(width, row.Length);
            }

            string[,] outputGrid = new string[width + FIRST_ELEMENT, textParsed.Length + FIRST_ELEMENT];

            for (int y = 0; y < textParsed.Length; y++)
            {
                string[] row = SplitCsvLine(textParsed[y]);
                for (int x = 0; x < row.Length; x++)
                {
                    outputGrid[x, y] = row[x];
                    outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
                }
            }

            return outputGrid;
        }

        private string[] SplitCsvLine(string line)
        {
            return 
            (
                from Match m in Regex.Matches(line, @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", RegexOptions.ExplicitCapture)
                select m.Groups[1].Value
            )
            .ToArray();
        }

        private void GetLanguageList(string[] lang)
        {
            if (languages == null)
            {
                languages = new List<string>();
                languages = SplitCsvLine(lang[0]).ToList();

				if (languages.Count > NUM_LANGUAGES+FIRST_ELEMENT)
					languages.Remove(languages.Last());
            }
        }

        public void ChangeLanguage()
        {
            indexOfLanguage++;
            if(indexOfLanguage == languages.Count) indexOfLanguage = FIRST_ELEMENT;
			GetCurrentLanguage = languages[indexOfLanguage];

            keys.Clear();

            AddKeysToDictionary(indexOfLanguage, languagesGrid);
            AddKeysToDictionary(indexOfLanguage, placesGrid);
            AddKeysToDictionary(indexOfLanguage, objectivesGrid);
            AddKeysToDictionary(indexOfLanguage, genericKeysGrid);
            AddKeysToDictionary(indexOfLanguage, descriptionsGrid);

            if (ChangeLanguageEvent != null)
                ChangeLanguageEvent();
        }


        private void AddKeysToDictionary(int indexOfLanguage, string[,] grid)
        {
            for (int n = 1; n < grid.GetUpperBound(1); n++)
                keys.Add(grid[0, n], grid[indexOfLanguage, n]);
        }

        public string GetKey(string key)
        {
            try
            {
                return keys[key];
            }
            catch
            {
                Debug.LogError(string.Format("The given key <color=red>{0}</color> was not present in the dictionary.", key));
                return null;
            }
        }
    }
}