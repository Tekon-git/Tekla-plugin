using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;


namespace EndPlate
{
    public class Plate
    {
        public TSM.ContourPlate bPlate { get; set; } = new ContourPlate();

        public Plate(TSM.Beam secondaryBeam, TSM.Beam primaryBeam, double plateThickness, string plateName)
        {
          
            this.bPlate = CreateConturePlate(secondaryBeam, primaryBeam, plateThickness, plateName) as TSM.ContourPlate;
            this.bPlate.Insert();

        }
        ContourPlate CreateConturePlate(TSM.Beam secondaryBeam, TSM.Beam primaryBeam, double plateThickness, string plateName)
        {
            double secondaryWidth = 0.0;
            double secondaryHeight = 0.0;
            

            secondaryBeam.GetReportProperty("WIDTH", ref secondaryWidth);
            secondaryBeam.GetReportProperty("HEIGHT", ref secondaryHeight);
            

            //TSG.Point p1 = new TSG.Point(secondaryWidth / 2, 0, secondaryHeight / 2);
            //TSG.Point p2 = new TSG.Point(secondaryWidth / 2, 0, -secondaryHeight / 2);
            //TSG.Point p3 = new TSG.Point(-secondaryWidth / 2, 0, -secondaryHeight / 2);
            //TSG.Point p4 = new TSG.Point(-secondaryWidth / 2, 0, secondaryHeight / 2);

            TSG.Point p1 = new TSG.Point(0, secondaryHeight / 2  , secondaryWidth / 2);
            TSG.Point p2 = new TSG.Point(0, -secondaryHeight / 2 , secondaryWidth / 2);
            TSG.Point p3 = new TSG.Point(0, -secondaryHeight / 2, -secondaryWidth / 2);
            TSG.Point p4 = new TSG.Point(0, secondaryHeight / 2, -secondaryWidth / 2);

            ContourPlate CP = new ContourPlate();
            ContourPoint conturePoint1 = new ContourPoint(p1, null);
            ContourPoint conturePoint2 = new ContourPoint(p2, null);
            ContourPoint conturePoint3 = new ContourPoint(p3, null);
            ContourPoint conturePoint4 = new ContourPoint(p4, null);

            CP.AddContourPoint(conturePoint1);
            CP.AddContourPoint(conturePoint2);
            CP.AddContourPoint(conturePoint3);
            CP.AddContourPoint(conturePoint4);
            CP.Name = plateName;
            CP.Finish = "xxx";
            CP.Profile.ProfileString = "PL" + plateThickness;
            CP.Material.MaterialString = "S235";
            CP.Position.Depth = Position.DepthEnum.FRONT;

            


            return CP;

        }

       
    }
}
