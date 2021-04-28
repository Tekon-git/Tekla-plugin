using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace Frame_Wpf
{

    public class CreatePlate
    {
        public CreatePlate(TSG.Point point)
        {
            TSM.Part bPlate = CreateConturePlate(point);
            bPlate.Insert();
           
        }
        ContourPlate CreateConturePlate(TSG.Point point)
        {
            double x = 300;
            double y = 300;

            TSG.Point point1 = new TSG.Point(point.X - x/2, point.Y - y/2, point.Z);
            TSG.Point point2 = new TSG.Point(point.X + x / 2, point.Y - y / 2, point.Z);
            TSG.Point point3 = new TSG.Point(point.X + x / 2, point.Y + y / 2, point.Z);
            TSG.Point point4 = new TSG.Point(point.X - x / 2, point.Y + y / 2, point.Z);

            ContourPlate CP = new ContourPlate();
            ContourPoint conturePoint1 = new ContourPoint(point1, null);
            ContourPoint conturePoint2 = new ContourPoint(point2, null);
            ContourPoint conturePoint3 = new ContourPoint(point3, null);
            ContourPoint conturePoint4 = new ContourPoint(point4, null);
            
            CP.AddContourPoint(conturePoint1);
            CP.AddContourPoint(conturePoint2);
            CP.AddContourPoint(conturePoint3);
            CP.AddContourPoint(conturePoint4);
            CP.Finish = "FOO";
            CP.Profile.ProfileString = "PL10";
            CP.Material.MaterialString = "S235";

            return CP;
          
        }

      
    }

}
