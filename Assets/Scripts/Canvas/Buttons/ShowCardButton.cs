using UnityEngine;
using UnityEngine.UI;
using VFG.Core;
using VFG.Models;

namespace VFG.Canvas
{
    public class ShowCardButton : BaseButton
    {
        public Sprite showCard;
        public Sprite locked;

        public void SetButtonProperties(int pState)
        {
            Image image = gameObject.GetComponent<Image>();

            switch (pState)
            {
                case 0:
                    image.sprite = locked;
                    GetComponent<Button>().interactable = false;
                    image.color = colorSecondaryButtonDisabledBlack;
                    break;
                case 1:
                    image.sprite = showCard;
                    image.color = colorSecondaryButtonDisabledBlack;
                    GetComponent<Button>().interactable = false;
                    isUnlocked = false;
                    break;
                case 2:
                    image.sprite = showCard;
                    image.color = Color.white;
                    GetComponent<Button>().interactable = true;
                    isUnlocked = true;
                    break;
            }
        }

        public override void SendAction(TypeOfAction action)
        {
            GameState.currentObjectiveName = transform.parent.name;
            Objective objective = new Objective();
            objective = GameState.aquariums[GameState.currentAquarium].Objectives.Find(r => r.Id == GameState.currentObjectiveName);
            GameState.currentObjective = GameState.aquariums[GameState.currentAquarium].Objectives.IndexOf(objective);

            base.SendAction(action);
        }
    }
}