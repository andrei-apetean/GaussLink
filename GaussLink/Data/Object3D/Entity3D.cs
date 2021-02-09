
using System.Windows.Media.Media3D;

namespace GaussLink.Data.Object3D
{
    public class Entity3D
    {
        public Entity3DType Type { get; set; }
        public int AtomID { get; set; }
        public (int, int) BondID { get; set; }
        public Vector3D Position { get; set; }
        public Vector3D Offset { get; set; }
        public Vector3D Direction { get; set; }
        public Entity3D(int AtomID,Vector3D Position , Entity3DType Type)
        {
            this.AtomID = AtomID;
            this.Position = Position;
            this.Type = Type;
        }
        public Entity3D((int,int) BondID,Vector3D Position, Vector3D Direction, Entity3DType Type)
        {
            this.BondID = BondID;
            this.Position = Position;
            this.Direction = Direction;
            this.Type = Type;
        }
    }
}
