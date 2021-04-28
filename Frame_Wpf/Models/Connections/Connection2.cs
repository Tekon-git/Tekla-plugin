using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSG =Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace Frame_Wpf
{
 
    public class ConnectionFactory2
    {
        public Connection GetDefaultConnection2()
        {
            var connection = new TSM.Connection
            {
                Name = "Cranked Beam",
                Number = 41,
            };

            connection.LoadAttributesFromFile("standard");
            connection.UpVector = new TSG.Vector(0,1000,0);
            connection.AutoDirectionType = Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_DIAGONAL;
            connection.PositionType = Tekla.Structures.PositionTypeEnum.COLLISION_PLANE;

            return connection;
        }
    }
}
