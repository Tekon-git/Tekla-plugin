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

namespace Frame_Wpf
{
   public class PluginData
   {
        [StructuresField("parameterh1")]
        public double H1;
        [StructuresField("parameterh2")]
        public double H2;
        [StructuresField("material")]
        public string material;
        [StructuresField("class")]
        public string clas;
        public const string PluginName = "Frame_Wpf";
    }
    [Plugin("Frame_Wpf")]
    [PluginUserInterface("Frame_Wpf.MainWindow")]
    public class Frame_Wpf : PluginBase
    {
        private double _H1 = 0.0;
        private double _H2 = 0.0;
        private string _material = string.Empty;
        private string _clas = string.Empty;
        private Model _Model;
        private PluginData _Data;

        public static readonly string DefaultMaterial = "S235";
        public static readonly string DefaultClas = "1";
        public static readonly double DefaultH1 = 3000.0;
        public static readonly double DefaultH2 = 2000.0;

        private Model Model { get => _Model; set => _Model = value; }
        private PluginData Data { get => _Data; set => _Data = value; }

        public Frame_Wpf(PluginData data)
        {
            Model = new Model();
            Data = data;
        }

        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> PointList = new List<InputDefinition>();
            Picker Picker = new Picker();
            ArrayList PickedPoints = Picker.PickPoints(Picker.PickPointEnum.PICK_TWO_POINTS);

            PointList.Add(new InputDefinition(PickedPoints));

            return PointList;

        }


        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                GetValueFromDialog();

                ArrayList points = (ArrayList)Input[0].GetInput();
                TSG.Point p1 = points[0] as TSG.Point;
                TSG.Point p2 = points[1] as TSG.Point;
                List<TSG.Point> pointsList = new List<TSG.Point>();
                pointsList.Add(p1);
                pointsList.Add(p2);
                TSG.Vector vectorX = new TSG.Vector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
                double _lengthFrame = vectorX.GetLength();
                vectorX.Normalize();
                TSG.Vector vectorZ = new TSG.Vector(0, 0, 1);
                TSG.Vector vectorY = vectorZ.Cross(vectorX);
                TSM.TransformationPlane currentTp = Model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                TSM.TransformationPlane tpNew = new TransformationPlane(p1, vectorX, vectorY);
                Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(tpNew);
                var objects = CreateFrame(_H1, _H2, _lengthFrame);
                Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTp);             

                var connection2 = new ConnectionFactory2().GetDefaultConnection2();
                for (int c = 0; c < objects.Count; c += 2)
                {
                    InstertConnection(new ConnectionFactory2().GetDefaultConnection2(), objects[c].beam, objects[c+1].beam);
                }

                List<Beam> beams = new List<Beam>();
                foreach (var item in objects)
                {
                    if (item.BeamType == BeamType.BEAM )
                    {
                        beams.Add(item);
                    }
                }

                var connection3 = new ConnectionFactory3().GetDefaultConnection3();
                for (int c = 0; c < beams.Count; c +=2)
                {
                    InstertConnection(new ConnectionFactory3().GetDefaultConnection3(), beams[c].beam, beams[c + 1].beam);
                }


                foreach (var p in pointsList)
                {
                    var bp = new CreatePlate(p);
                }


                Model.CommitChanges();


            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.ToString());
            }

            return true;

        }

        public List<Beam> CreateFrame(double h1, double h2, double l)
        {
            Frame f = new Frame(l, h1 , h2);
            List<Beam> objects = new List<Beam>();

            for (int c = 0; c < f.Columns.Count; c +=2)
            {

                Beam column = new Beam(f.Columns[c], f.Columns[c + 1], _material, _clas)
                {
                    BeamType = BeamType.COLUMN
                };
                column.Insert();
                objects.Add(column);
                Beam beam = new Beam(f.Beams[c], f.Beams[c + 1], _material, _clas)
                {
                    BeamType = BeamType.BEAM
                };
                beam.Insert();
                objects.Add(beam);
            }

            return objects;
        }

        private void InstertConnection(Connection connection, TSM.Beam primary, TSM.Beam secondary)
        {
            connection.SetPrimaryObject(primary);
            connection.SetSecondaryObject(secondary);

            if (!connection.Insert())
            {
                Console.WriteLine("Connection inssert failed");
            }

        }

        public void GetValueFromDialog()
        {
            _H1 = Data.H1;
            if (IsDefaultValue(_H1))
                _H1 = DefaultH1;
            _H2 = Data.H2;
            if (IsDefaultValue(_H2))
                _H2 = DefaultH2;
            _material = Data.material;
            if (IsDefaultValue(_material))
                _material = DefaultMaterial;
            _clas = Data.clas;
            if (IsDefaultValue(_clas))
                _clas = DefaultClas;
        }

        
    }
    
}
