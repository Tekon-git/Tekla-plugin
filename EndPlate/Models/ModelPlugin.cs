using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using System.Collections;
using System.Windows;
using TSM = Tekla.Structures.Model;
using Tekla.Structures;

namespace EndPlate
{
    public class PluginData
    {
        [StructuresField("plateName")]
        public string plateName;
        [StructuresField("plateThickness")]
        public double plateThickness;
     

        #region General Tab

        [StructuresField("ac_root")]
        public int AutoConnection;
        [StructuresField("ad_root")]
        public int AutoDefaults;
        [StructuresField("group_no")]
        public int Class;
        [StructuresField("joint_code")]
        public string ConnectionCode;
        [StructuresField("OBJECT_LOCKED")]
        public int Locked;
        [StructuresField("zang2")]
        public double RotationAngleX;
        [StructuresField("zang1")]
        public double RotationAngleY;
        [StructuresField("zsuunta")]
        public int UpDirection;

        #endregion General Tab

    }

    [Plugin("EndPlate")]
    [PluginUserInterface("EndPlate.MainWindow")]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(AutoDirectionTypeEnum.AUTODIR_GLOBAL_Z)]
    [PositionType(PositionTypeEnum.BOX_PLANE)]
    public class EndPlate : ConnectionBase
    {
        private string _plateName = string.Empty;
        public static readonly string DefaultPlateName = "Tekon";

        private double _plateThickness = 0.0;
        public static readonly double DefaultPlateThickness = 10.0;

       


        private  TSM.Model _model;
        private  PluginData _data;

        private Model Model { get => _model; set => _model = value; }
        private PluginData Data { get => _data; set => _data = value; }

        public EndPlate(PluginData data)
        {
            Model = new TSM.Model();
            Data = data;
        }
        public override bool Run()
        {
            try
            {
                GetValueFromDialog();

                TSM.Beam primaryBeam = Model.SelectModelObject(Primary) as TSM.Beam;
                TSM.Beam secondaryBeam = Model.SelectModelObject(Secondaries[0]) as TSM.Beam;

               

                if (primaryBeam != null && secondaryBeam != null)
                {
                    

                    Plate createPlate = new Plate(secondaryBeam, primaryBeam, _plateThickness, _plateName);
                    FitBeam(secondaryBeam, _plateThickness);

                    if (createPlate != null && secondaryBeam != null)
                    {
                        WeldBeamToPlate(secondaryBeam, createPlate.bPlate); 
                    }

                    BoltBeamtoPlate(primaryBeam, createPlate.bPlate);

                }

            }
            catch (Exception)
            {

                MessageBox.Show(Tekla.Structures.Dialog.UIControls.LocalizeForm.Localization.GetText("albl_Invalid_input_parts"));
            }

            return true;
        }

        private void FitBeam(TSM.Beam secondaryBeam, double plateThickness)
        {
            TSM.Fitting fitting = new TSM.Fitting();

            fitting.Father = secondaryBeam;

            TSM.Plane fitPlane = new TSM.Plane();
            fitPlane.Origin = new TSG.Point(plateThickness, 0, 0);
            fitPlane.AxisX = new TSG.Vector(0, 1000, 0);
            fitPlane.AxisY = new TSG.Vector(0, 0, 1000);

            fitting.Plane = fitPlane;

            fitting.Insert();
        }

        private void WeldBeamToPlate(TSM.Beam secondaryBeam, TSM.ContourPlate createPlate)
        {
            TSM.Weld weld = new TSM.Weld();

            weld.MainObject = secondaryBeam;
            weld.SecondaryObject = createPlate;
            weld.ConnectAssemblies = true;
            weld.SizeAbove = 5;
            weld.Insert();
        }

        private void BoltBeamtoPlate(TSM.Beam beam, TSM.ContourPlate plate)
        {
            double plateLength = 0.0;
            plate.GetReportProperty("LENGTH", ref plateLength);
            

            BoltArray B = new BoltArray();

            B.PartToBeBolted = beam;
            B.PartToBoltTo = plate;

            B.FirstPosition = new TSG.Point(0, plateLength / 2, 0);
            B.SecondPosition = new TSG.Point(0, -plateLength / 2, 0);

            B.BoltSize = 16;
            B.Tolerance = 3.00;
            B.BoltStandard = "7990";
            B.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            B.CutLength = 105;

            B.Length = 100;
            B.ExtraLength = 0;
            B.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

            B.Position.Depth = Position.DepthEnum.MIDDLE;
            B.StartPointOffset.Dx = 30.0;
            B.Position.Plane = Position.PlaneEnum.MIDDLE;
            B.Position.Rotation = Position.RotationEnum.TOP;

            B.Bolt = true;
            B.Washer1 = true;
            B.Washer2 = true;
            B.Washer3 = true;
            B.Nut1 = true;
            B.Nut2 = false;

            B.Hole1 = true;
            B.Hole2 = true;
            B.Hole3 = true;
            B.Hole4 = true;
            B.Hole5 = true;

            B.AddBoltDistX(100);
            B.AddBoltDistY(70);

            B.Insert();
        }

        public void GetValueFromDialog()
        {
            _plateName = Data.plateName;
            if (IsDefaultValue(_plateName))
                _plateName = DefaultPlateName;

            _plateThickness = Data.plateThickness;
            if (IsDefaultValue(_plateThickness))
                _plateThickness = DefaultPlateThickness;

           

        }

    }
}
