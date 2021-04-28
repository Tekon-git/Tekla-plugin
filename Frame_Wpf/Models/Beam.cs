using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace Frame_Wpf
{
    public class Beam
    {
        public TSM.Beam beam { get; } = new TSM.Beam();

        public BeamType BeamType { get; set; }

        public Beam() : this(new Tekla.Structures.Geometry3d.Point(0,0,0), new Tekla.Structures.Geometry3d.Point(12000, 0, 0))
        {

        }

        public Beam (Point p1, Point p2)
        {
            beam.Name = "Tekon";
            beam.Material.MaterialString = "S235JR";
            beam.Profile.ProfileString = "IPE300";
            beam.StartPoint = p1;
            beam.EndPoint = p2;
            beam.Class = "99";
            beam.AssemblyNumber.Prefix = "NIPAssembly";
            beam.AssemblyNumber.StartNumber = 1;
            beam.PartNumber.Prefix = "NIPPart";
            beam.PartNumber.StartNumber = 1;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.TOP;
           
        }

        public Beam(Point p1, Point p2, string material) : this(p1,p2)
        {
            beam.Material.MaterialString = material;
        }
        public Beam(Point p1, Point p2, string material, string clas) : this(p1, p2)
        {
            beam.Material.MaterialString = material;
            beam.Class = clas;
        }
        public void Insert()
        {
            beam.Insert();
        }
        public void Update()
        {
            beam.Modify();
        }

    }

    public enum BeamType
    {
        COLUMN,
        BEAM
    }
}
