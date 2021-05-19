using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace EndPlate
{
    public class MainWindowViewModel : BaseViewModel
    {


        /// <summary>
        /// General value.
        /// </summary>
        private int cbAc_root;

        /// <summary>
        /// General value.
        /// </summary>
        private int cbAd_root;

        /// <summary>
        /// General value.
        /// </summary>
        private int cbOBJECT_LOCKED;

        /// <summary>
        /// General value.
        /// </summary>
        private int cbZsuunta;

        /// <summary>
        /// General value.
        /// </summary>
        private int txtGroup_no;

        /// <summary>
        /// General value.
        /// </summary>
        private string txtJoint_code = string.Empty;

        /// <summary>
        /// General value.
        /// </summary>
        private double txtZang1;

        /// <summary>
        /// General value.
        /// </summary>
        private double txtZang2;

        public string plateName = "Tekon";

        [StructuresDialog("plateName", typeof(TD.String))]
        public string PlateName
        {
            get => plateName;
            set
            {
                plateName = value;
                OnPropertyChanged("PlateName");
            }
        }

        public double plateThickness = 10.0;

        [StructuresDialog("plateThickness", typeof(TD.Double))]
        public double PlateThickness
        {
            get => plateThickness;
            set
            {
                plateThickness = value;
                OnPropertyChanged("PlateThickness");
            }
        }

   

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("ac_root", typeof(TD.Integer))]
        public int ac_root
        {
            get
            {
                return this.cbAc_root;
            }

            set
            {
                this.cbAc_root = value;
                this.OnPropertyChanged("ac_root");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("ad_root", typeof(TD.Integer))]
        public int ad_root
        {
            get
            {
                return this.cbAd_root;
            }

            set
            {
                this.cbAd_root = value;
                this.OnPropertyChanged("ad_root");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("group_no", typeof(TD.Integer))]
        public int group_no
        {
            get
            {
                return this.txtGroup_no;
            }

            set
            {
                this.txtGroup_no = value;
                this.OnPropertyChanged("group_no");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("joint_code", typeof(TD.String))]
        public string joint_code
        {
            get
            {
                return this.txtJoint_code;
            }

            set
            {
                this.txtJoint_code = value;
                this.OnPropertyChanged("joint_code");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("OBJECT_LOCKED", typeof(TD.Integer))]
        public int OBJECT_LOCKED
        {
            get
            {
                return this.cbOBJECT_LOCKED;
            }

            set
            {
                this.cbOBJECT_LOCKED = value;
                this.OnPropertyChanged("OBJECT_LOCKED");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("zang1", typeof(TD.Double))]
        public double zang1
        {
            get
            {
                return this.txtZang1;
            }

            set
            {
                this.txtZang1 = value;
                this.OnPropertyChanged("zang1");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("zang2", typeof(TD.Double))]
        public double zang2
        {
            get
            {
                return this.txtZang2;
            }

            set
            {
                this.txtZang2 = value;
                this.OnPropertyChanged("zang2");
            }
        }

        /// <summary>
        /// Gets or sets sample text
        /// </summary>
        [StructuresDialog("zsuunta", typeof(TD.Integer))]
        public int zsuunta
        {
            get
            {
                return this.cbZsuunta;
            }

            set
            {
                this.cbZsuunta = value;
                this.OnPropertyChanged("zsuunta");
            }
        }
    }
}
