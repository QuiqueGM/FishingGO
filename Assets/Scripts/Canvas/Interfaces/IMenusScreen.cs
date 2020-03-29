using UnityEngine;
using VFG.Canvas;

public interface IMenusScreen
{
    void Initialize();
    void Populate();
	void ShowCard(GameObject card);
	void ShowPopUp (GameObject card);
	void Show();
}
