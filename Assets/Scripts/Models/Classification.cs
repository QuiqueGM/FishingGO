namespace VFG.Models
{
    public class Classification
    {
        private string _kingdom;
        private string _phylum;
        private string _class;
        private string _order;
        private string _family;
        private string _genus;
        private string _specie;
        private string _stage;

        public string Kingdom
        {
            get { return _kingdom; }
            set { _kingdom = value; }
        }

        public string Phylum
        {
            get { return _phylum; }
            set { _phylum = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        public string Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public string Family
        {
            get { return _family; }
            set { _family = value; }
        }

        public string Genus
        {
            get { return _genus; }
            set { _genus = value; }
        }

        public string Specie
        {
            get { return _specie; }
            set { _specie = value; }
        }

        public string StageSexVar
        {
            get { return _stage; }
            set { _stage = value; }
        }

        public Classification()
        {
        }

        public Classification(string pKingdom, string pPhylum, string pClass, string pOrder, string pFamily, string pGenus, string pSpecie, string pStageSexVariation)
        {
            this._kingdom = pKingdom;
            this._phylum = pPhylum;
            this._class = pClass;
            this._order = pOrder;
            this._family = pFamily;
            this._genus = pGenus;
            this._specie = pSpecie;
            this._stage = pStageSexVariation;
        }
    }
}