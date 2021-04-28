using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace Frame_Wpf
{
    public class MainWindowViewModel : BaseViewModel
    {
        private double H1 = 3000.0;
        private double H2 = 2000.0;
        private string material = "S235JR";
        private string clas ="1";

        [StructuresDialog("parameterh1", typeof(TD.Double))]
        public double H11 
        {
            get => H1;
            set
            {
                 H1 = value;
                OnPropertyChanged("H11");
            }
        }
        [StructuresDialog("parameterh2", typeof(TD.Double))]
        public double H22
        {
            get => H2;
            set
            {
                H2 = value;
                OnPropertyChanged("H22");
            }
        }
        [StructuresDialog("material", typeof(TD.String))]
        public string Material
        {
            get => material;
            set
            {
                material = value;
                OnPropertyChanged("Material");
            }
        }
        [StructuresDialog("class", typeof(TD.String))]
        public string Clas
        {
            get => clas;
            set
            {
                clas = value;
                OnPropertyChanged("Clas");
            }
        }

        
    }
}
