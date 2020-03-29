using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VFG.Core;
using VFG.Models;
using System.Linq;
using UnityEngine.UI;
using VFG.Core.Localization;

namespace VFG.Canvas
{
    public class CardsMenu : CanvasBase
    {
        const int CELLSIZE_LOWRES = 360;
        const int CELLSIZE_BIGRES = 350;

        public enum Collection
        {
            All,
            Mine,
            Missing
        }

        public enum Phylum
        {
            All,
            Chordata,
            Echinoderms,
            Crustaceans,
            Molluscs,
            Cnidarians
        }

        public enum OrderBy
        {
            ScientificName,
            CommonName,
            Family,
            Genus,
            Size,
            Depth
        }

        public Image IMG_Cards;
        public Sprite[] cards;
        [Space]
        public Image IMG_Phylum;
        public Sprite[] phylums;

        private GameObject _cards;
        private TMP_Text _TXT_CardsCollection;
        private GameObject _popupPhylum;
        private GameObject _popupOrderBy;
        private TMP_Text _TXT_CardsPhylum;
        private TMP_Text _BTN_OrderBy;
        private string _filterName = string.Empty;

        private Collection collection = Collection.All;
        private Phylum phylum = Phylum.All;
        private OrderBy orderBy = OrderBy.ScientificName;

        private List<Collectable> objectives = new List<Collectable>();
        private List<Collectable> allKeysCollection = new List<Collectable>();
        private List<Collectable> allKeysPhylum = new List<Collectable>();
        private List<Collectable> allKeys = new List<Collectable>();
        private List<string> finalList = new List<string>();
        private Vector3 initButtonsPosition;

        public override void Initialize()
        {
            _cards = transform.Find("Cards/Buttons").gameObject;
            _TXT_CardsCollection = transform.Find("Collections/TXT_CardsCollection").GetComponent<TMP_Text>();
            _popupPhylum = transform.Find("Phylum/PopupPhylum").gameObject;
            _TXT_CardsPhylum = transform.Find("Phylum/TXT_Phylum").GetComponent<TMP_Text>();
            _popupOrderBy = transform.Find("OrderBy/PopupOrderBy").gameObject;
            _BTN_OrderBy = transform.Find("OrderBy/BTN_OrderBy").GetComponent<TMP_Text>();
            _TXT_CardsPhylum.text = LoadLocalization.Instance.GetKey("#ALL#");
            initButtonsPosition = _cards.transform.position;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.SetActive(false);
            _popupPhylum.SetActive(false);
            _popupOrderBy.SetActive(false);

            SetResolution();
            InitObjectivesLists();
        }

        private void SetResolution()
        {
            float screenResolution = (float)Screen.width / (float)Screen.height;
            _cards.GetComponent<GridLayoutGroup>().cellSize = (screenResolution > 2) ? new Vector2(CELLSIZE_BIGRES, CELLSIZE_BIGRES) : new Vector2(CELLSIZE_LOWRES, CELLSIZE_LOWRES);
        }

        public void Populate(List<Objective> listResult)
        {
            int numCards = GameState.objectives.Count;
            int numChilds = _cards.transform.childCount;

            if (numCards > numChilds)
                Debug.Log("<color=red>[VFG Error...]</color> There are more cards than buttons. Please, increase the number of buttons from the hierarchy.");

            if (numCards < numChilds)
                Debug.Log("<color=red>[VFG Error...]</color> There are more buttons than objectives. Please, delete the number of buttons from the hierarchy.");

            for (int n = 0; n < numChilds; n++)
            {
                if (n < numCards)
                {
                    string id = listResult[n].Id; 

                    _cards.transform.GetChild(n).GetComponent<CardButton>().Initialize();
                    _cards.transform.GetChild(n).GetComponent<CardButton>().FillButton
                    (
                        id,
                        GameState.collectables.Find(it => it.Id == id).Color,
                        _filterName == "" ? string.Empty : LoadLocalization.Instance.GetKey(_filterName),
                        _filterName == "" ? string.Empty : GetFilterResult(_filterName, id),
                        GameState.collectables.Find(i => i.Id == id).ScientificName,
                        GameState.spritesObjectives.Find(i => i.name == id),
                        (GameState.State)(GameState.objectivesState[id])
                    );
                }
            }
        }

        private string GetFilterResult(string filter, string id)
        {
            switch (filter)
            {
                case GameState.FAMILY: return GameState.collectables.Find(i => i.Id == id).Classification.Family;
                case GameState.GENUS: return GameState.collectables.Find(i => i.Id == id).Classification.Genus;
                case GameState.SIZE: return string.Format("{0} cm", GameState.collectables.Find(i => i.Id == id).Features.MaxSize.ToString());
                case GameState.DEPTH: return string.Format("{0} m", GameState.collectables.Find(i => i.Id == id).Features.MaxDepth.ToString());
                default: return string.Empty;
            }
        }

        public override void Populate()
        {
            ShowCards();
        }

        private void InitObjectivesLists()
        {
            List<string> l = GameState.objectives.Select(i => i.Id).ToList();

            foreach (Collectable c in GameState.collectables)
            {
                if (l.Contains(c.Id)) objectives.Add(c);
            }

            allKeysCollection = allKeysPhylum = objectives;
        }

        public void ShowCards()
        {
            OrderCards(orderBy);
            Populate(GameState.objectives);

            for (int n = 0; n < _cards.transform.childCount; n++)
                _cards.transform.GetChild(n).gameObject.SetActive(finalList.Contains(_cards.transform.GetChild(n).name));

            for (int n = 0; n < finalList.Count; n++)
            {
                _cards.transform.Find(finalList[n]).transform.SetAsLastSibling();
            }

            _cards.GetComponent<RectTransform>().position = initButtonsPosition;
        }

        public void OrderCards(OrderBy order)
        {
            List<Collectable> allKeys = allKeysCollection.Intersect(allKeysPhylum).ToList();

            switch (order)
            {
                case OrderBy.ScientificName:
                    finalList = allKeys.OrderBy(n => n.Id).Select(i => i.Id).ToList();
                    _filterName = string.Empty;
                    break;

                case OrderBy.Family:
                    finalList = allKeys.OrderBy(n => n.Classification.Family).Select(i => i.Id).ToList();
                    _filterName = GameState.FAMILY;
                    break;

                case OrderBy.Genus:
                    finalList = allKeys.OrderBy(n => n.Classification.Genus).Select(i => i.Id).ToList();
                    _filterName = GameState.GENUS;
                    break;

                case OrderBy.Size:
                    finalList = allKeys.OrderBy(n => n.Features.MaxSize).Select(i => i.Id).ToList();
                    _filterName = GameState.SIZE;
                    break;

                case OrderBy.Depth:
                    finalList = allKeys.OrderBy(n => n.Features.MaxDepth).Select(i => i.Id).ToList();
                    _filterName = GameState.DEPTH;
                    break;
            }
        }

        #region COLLECTION

        public void ChangeCollection(bool left)
        {
            collection = left ? collection + 1 : collection - 1;
            if (collection < Collection.All) collection = Collection.Missing;
            if (collection > Collection.Missing) collection = Collection.All;

            IMG_Cards.sprite = cards[(int)collection];
            _TXT_CardsCollection.GetComponent<TMP_Text>().text = LoadLocalization.Instance.GetKey(GetTextDifficulty(collection));
            _TXT_CardsCollection.GetComponent<LocalizeText>().SetNewInitKey(GetTextDifficulty(collection));
            _popupPhylum.SetActive(false);
            _popupOrderBy.SetActive(false);

            allKeysCollection = ShowCollection(collection);
            ShowCards();
        }

        private string GetTextDifficulty(Collection collection)
        {
            switch (collection)
            {
                case Collection.All: return "#ALL#";
                case Collection.Mine: return "#MINE#";
                case Collection.Missing: return "#MISSING#";
                default: return "#ALL#";
            }
        }

        private List<Collectable> ShowCollection(Collection collection)
        {
            switch (collection)
            {
                case Collection.All:
                    return objectives;

                case Collection.Mine:
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.State == GameState.State.Solved)
                        select item
                    );

                case Collection.Missing:
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.State != GameState.State.Solved)
                        select item
                    );
                default: return objectives;
            }
        }

        #endregion

        #region PHYLUM

        public void ShowPopupPhylums()
        {
            _popupPhylum.SetActive(true);
            _popupOrderBy.SetActive(false);
        }

        public void ChangePhylum(string phylum)
        {
            this.phylum = GetPhylum(phylum);
            IMG_Phylum.sprite = phylums[(int)GetPhylum(phylum)];
            _TXT_CardsPhylum.text = phylum == "All" ? LoadLocalization.Instance.GetKey("#ALL#") : phylum;
            _popupPhylum.SetActive(false);

            allKeysPhylum = ShowPhylum(phylum);
            ShowCards();
        }

        private Phylum GetPhylum(string phylum)
        {
            switch (phylum)
            {
                case "All": return Phylum.All;
                case "Echinoderms": return Phylum.Echinoderms;
                case "Chordata": return Phylum.Chordata;
                case "Cnidarians": return Phylum.Cnidarians;
                case "Molluscs": return Phylum.Molluscs;
                case "Crustaceans": return Phylum.Crustaceans;
                default: return Phylum.All;
            }
        }

        private List<Collectable> ShowPhylum(string phylum)
        {
            switch (phylum)
            {
                case "All":
                    return objectives;

                case "Echinoderms":
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.TypeOfItem == TypeOfItem.Urchin || item.TypeOfItem == TypeOfItem.Starfish)
                        select item
                    );

                case "Chordata":
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.TypeOfItem == TypeOfItem.Fish || item.TypeOfItem == TypeOfItem.Seahorse || item.TypeOfItem == TypeOfItem.MorayEel)
                        select item
                    );

                case "Cnidarians":
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.TypeOfItem == TypeOfItem.Coral || item.TypeOfItem == TypeOfItem.Anemone)
                        select item
                    );

                case "Molluscs":
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.TypeOfItem == TypeOfItem.Mollusc)
                        select item
                    );

                case "Crustaceans":
                    return new List<Collectable>
                    (
                        from item in objectives
                        where (item.TypeOfItem == TypeOfItem.Crustacean)
                        select item
                    );
                default: return objectives;
            }
        }

        #endregion

        #region ORDER_BY

        public void ShowPopupOrderBy()
        {
            _popupOrderBy.SetActive(true);
            _popupPhylum.SetActive(false);
        }

        public void ChangeOrderBy(string order)
        {
            orderBy = GetOrderBy(order);
            _BTN_OrderBy.text = LoadLocalization.Instance.GetKey(order);
            _popupOrderBy.SetActive(false);

            ShowCards();
        }

        private OrderBy GetOrderBy(string order)
        {
            switch (order)
            {
                case "#SCIENTIFIC_NAME#": return OrderBy.ScientificName;
                case "#COMMON_NAME#": return OrderBy.CommonName;
                case "#FAMILY#": return OrderBy.Family;
                case "#GENUS#": return OrderBy.Genus;
                case "#SIZE#": return OrderBy.Size;
                case "#DEPTH#": return OrderBy.Depth;
                default: return OrderBy.ScientificName;
            }
        }

        #endregion
    }
}