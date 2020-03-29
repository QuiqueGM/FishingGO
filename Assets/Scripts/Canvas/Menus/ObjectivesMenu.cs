using TMPro;
using UnityEngine;
using VFG.Core;
using VFG.Core.Localization;

namespace VFG.Canvas
{
    public class ObjectivesMenu : CanvasBase
    {
        private TMP_Text _txtAquarium;
        private GameObject _objectives;

        public override void Initialize()
        {
            _objectives = transform.Find("Objectives/Buttons").gameObject;
            _txtAquarium = transform.Find("IMG_Header/TXT_Aquarium").GetComponent<TMP_Text>();
            base.Initialize();
        }

        public override void Populate()
        {
            _txtAquarium.text = LoadLocalization.Instance.GetKey(GameState.currentAquariumName);

            int numObj = GameState.aquariums[GameState.currentAquarium].Objectives.Count;
            int numChilds = _objectives.transform.childCount;

            if (numObj > numChilds)
                Debug.Log("<color=red>[VFG Error...]</color> There are more objectives than buttons. Please, increase the number of buttons from the hierarchy.");

            for (int n = 0; n < numChilds; n++)
            {
                if (n < numObj)
                {
                    string id = GameState.aquariums[GameState.currentAquarium].Objectives[n].Id;

                    _objectives.transform.GetChild(n).gameObject.SetActive(true);
                    _objectives.transform.GetChild(n).GetComponent<PopulateObjectivesButtons>().Initialize();
                    _objectives.transform.GetChild(n).GetComponent<PopulateObjectivesButtons>().FillButton
                    (
                        id,
                        GameState.collectables.Find(it => it.Id == id).Color,
                        GameState.spritesObjectives.Find(it => it.name == id),
						(GameState.State)(GameState.objectivesState[id])
                    );
                }
                else
                    _objectives.transform.GetChild(n).gameObject.SetActive(false);
            }
        }
    }
}