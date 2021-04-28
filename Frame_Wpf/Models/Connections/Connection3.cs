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
    public class ConnectionFactory3
    {
        public Connection GetDefaultConnection3()
        {
            var connection = new TSM.Connection
            {
                Name = "Apex haunch",
                Number = 106,
            };

            connection.LoadAttributesFromFile("standard");
            connection.UpVector = new TSG.Vector(0, 0, 1000);
            connection.PositionType = Tekla.Structures.PositionTypeEnum.COLLISION_PLANE;
            connection.AutoDirectionType = Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_GLOBAL_Z;

            return connection;
        }
    }
}
