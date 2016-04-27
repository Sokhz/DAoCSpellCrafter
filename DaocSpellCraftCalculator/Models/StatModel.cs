using GalaSoft.MvvmLight;
using System;

namespace DaocSpellCraftCalculator.Models
{
    public class StatModel : ViewModelBase
    {

        public StatModel()
        {
            this.IsImportant = true;
        }

        private ElementModel _Stat;
        public ElementModel Stat
        {
            get { return _Stat; }
            set { if (_Stat != value) { _Stat = value; RaisePropertyChanged("Stat"); } }
        }


        private bool _IsImportant;
        public bool IsImportant
        {
            get { return _IsImportant; }
            set { if (_IsImportant != value) { _IsImportant = value; RaisePropertyChanged("IsImportant"); RaisePropertyChanged("Diff"); } }
        }

        private int _NormalDecap;
        public int NormalDecap
        {
            get { return _NormalDecap; }
            set { if (_NormalDecap != value) { _NormalDecap = value; RaisePropertyChanged("NormalDecap"); RaisePropertyChanged("Resume"); } }
        }

        private int _MaxNormalDecap;
        public int MaxNormalDecap
        {
            get { return _MaxNormalDecap; }
            set { if (_MaxNormalDecap != value) { _MaxNormalDecap = value; RaisePropertyChanged("MaxNormalDecap"); RaisePropertyChanged("Resume"); } }
        }

        private int _DiffNormalDecap;
        public int DiffNormalDecap
        {
            get { return _DiffNormalDecap; }
            set { if (_DiffNormalDecap != value) { _DiffNormalDecap = value; RaisePropertyChanged("DiffNormalDecap"); RaisePropertyChanged("Resume"); } }
        }

        private int _MythicalDecap;
        public int MythicalDecap
        {
            get { return _MythicalDecap; }
            set { if (_MythicalDecap != value) { _MythicalDecap = value; RaisePropertyChanged("MythicalDecap"); RaisePropertyChanged("Resume"); } }
        }

        private int _MaxMythicalDecap;
        public int MaxMythicalDecap
        {
            get { return _MaxMythicalDecap; }
            set { if (_MaxMythicalDecap != value) { _MaxMythicalDecap = value; RaisePropertyChanged("MaxMythicalDecap"); RaisePropertyChanged("Resume"); } }
        }

        private int _DiffMythicalDecap;
        public int DiffMythicalDecap
        {
            get { return _DiffMythicalDecap; }
            set { if (_DiffMythicalDecap != value) { _DiffMythicalDecap = value; RaisePropertyChanged("DiffMythicalDecap"); } }
        }

        private int _MaxValue;
        public int MaxValue
        {
            get { return _MaxValue; }
            set { if (_MaxValue != value) { _MaxValue = value; RaisePropertyChanged("MaxValue"); } }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; RaisePropertyChanged("Value"); } }
        }

        private int _Diff;
        public int Diff
        {
            get { return _Diff; }
            set { if (_Diff != value) { _Diff = value; RaisePropertyChanged("Diff"); } }
        }

        public string Resume
        {
            get
            {
                return this.GetResume();
            }
        }

        public string Representation
        {
            get { return this.GetRepresentation(); }
        }



        private string GetResume()
        {
            if (this.MaxNormalDecap != 0 || this.MaxMythicalDecap != 0)
            {
                var retour = "Normal decap : " + this.NormalDecap + "/" + this.MaxNormalDecap;
                if (this.MaxMythicalDecap != 0)
                    retour += Environment.NewLine + "Mythical decap : " + this.MythicalDecap + "/" + this.MaxMythicalDecap;
                return retour;
            }
            else
                return null;
        }


        private string GetRepresentation()
        {
            var retour = this.Stat.Full + " : ";
            retour += this.Value + "/" + this.MaxValue;
            return retour;
        }

    }
}
