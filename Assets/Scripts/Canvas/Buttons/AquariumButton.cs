using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Models;
using VFG.Core;
using VFG.Core.Audio;

namespace VFG.Canvas
{
    public class AquariumButton : BaseButton
    {
        public Sprite unlocked;
        public Sprite locked;

        public void SetButtonProperties(Sprite pBackground, bool pIsUnlocked)
        {
            isUnlocked = pIsUnlocked;
            GetComponent<Image>().sprite = pBackground;
            GetComponent<Image>().color = isUnlocked ? Color.white : colorMainButtonDisabled;
            GetComponent<Button>().interactable = isUnlocked;
        }

        public void SetSecondaryButton(bool pIsUnlocked)
        {
            isUnlocked = pIsUnlocked;
            GetComponent<Button>().interactable = false; //Fake!!
            GetComponent<Image>().sprite = isUnlocked ? unlocked : locked;
            GetComponent<Image>().color = isUnlocked ? Color.white : colorSecondaryButtonDisabledBlack;
        }

        public override void SendAction(TypeOfAction action)
        {
            GameState.currentAquariumName = transform.parent.name;
            Aquarium aquarium = new Aquarium();
            aquarium = GameState.aquariums.Find(r => r.Id == GameState.currentAquariumName);
            GameState.currentAquarium = GameState.aquariums.IndexOf(aquarium);
            //GameState.sceneToLoad = action == TypeOfAction.Objectives ? GameState.currentAquariumName : string.Format("User{0}", GameState.currentAquariumName);
            base.SendAction(action);
        }
    }
}