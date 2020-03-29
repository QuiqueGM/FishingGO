using System.Collections.Generic;

namespace VFG.Models
{
    public class Aquarium
    {
        private string _id;
        private bool _comingSoon;
        private int _numberOfSolvedObjectivesToUnlockNextAquarium;
        private List<Objective> _objectives;

        public string  Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool IsComingSoon
        {
            get { return _comingSoon; }
            set { _comingSoon = value; }
        }

        public int NumberOfSolvedObjectivesToUnlockNextAquarium
        {
            get { return _numberOfSolvedObjectivesToUnlockNextAquarium; }
            set { _numberOfSolvedObjectivesToUnlockNextAquarium = value; }
        }

        public List<Objective> Objectives
        {
            get { return _objectives; }
            set { _objectives = value; }
        }

        public Aquarium()
        {
        }

        public Aquarium(string pId, bool pIsComingSoon, int pNumOfSolvedObj, List<Objective> pObjectives)
        {
            this._id = pId;
            this._comingSoon = pIsComingSoon;
            this._numberOfSolvedObjectivesToUnlockNextAquarium = pNumOfSolvedObj;
            this._objectives = pObjectives;
        }
    }
}