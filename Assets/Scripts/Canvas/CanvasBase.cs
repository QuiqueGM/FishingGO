using UnityEngine;
using VFG.Models;

namespace VFG.Canvas
{
    public class CanvasBase : MonoBehaviour , IMenusScreen
    {
        public virtual void Initialize()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.SetActive(false);
        }

        public virtual void Populate()
        {
            gameObject.SetActive(true);
        }

		public virtual void ShowCard(GameObject card) { }

        public virtual void Show()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }

        public virtual void ShowPopUp(GameObject card)
		{
			card.GetComponent<IMenusScreen>().Initialize ();
			card.GetComponent<IMenusScreen> ().Populate ();
			card.GetComponent<IMenusScreen>().Show();
		}

        public string GetSize(Collectable c)
        {
            if (c.Features.CommonSize != c.Features.MaxSize)
                return string.Format("{0} - {1} cm", c.Features.CommonSize, c.Features.MaxSize);
            else
                return string.Format("{0} cm", c.Features.CommonSize);
        }

        public string GetDepth(Collectable c)
        {
            if (c.Features.MinDepth != c.Features.MaxDepth)
                return string.Format("{0} - {1} m", c.Features.MinDepth, c.Features.MaxDepth);
            else
                return string.Format("{0} m", c.Features.MinDepth);
        }
    }
}
