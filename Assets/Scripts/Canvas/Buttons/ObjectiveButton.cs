using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Models;
using VFG.Core;
using VFG.Core.Audio;

namespace VFG.Canvas
{
    public class ObjectiveButton : BaseButton
    {
        public void SetButtonProperties(Sprite pBackground, int pState)
        {
            Image image = gameObject.transform.Find("IMG_Objective").GetComponent<Image>();
            image.sprite = pBackground;

            switch (pState)
            {
                case 0:
                    image.color = colorSecondaryButtonDisabledBlack;
                    GetComponent<Button>().interactable = false;
                    isUnlocked = false;
                    break;
                case 1:
                case 2:
                    image.color = Color.white;
                    GetComponent<Button>().interactable = true;
                    isUnlocked = true;
                    break;
            }
        }

        public override void SendAction(TypeOfARAction action)
        {
            GameState.currentObjectiveName = transform.parent.name;
            Objective objective = new Objective();
            objective = GameState.aquariums[GameState.currentAquarium].Objectives.Find(r => r.Id == GameState.currentObjectiveName);
            GameState.currentObjective = GameState.aquariums[GameState.currentAquarium].Objectives.IndexOf(objective);
            AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_INGAME, 1, 1);

            base.SendAction(action);
        }
    }
}