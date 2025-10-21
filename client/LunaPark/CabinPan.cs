using CitizenFX.Core;

namespace Client.net.LunaPark
{
    internal class CabinPan
    {
        public int Index { get; set; }
        public int NPlayer { get; set; }
        public Prop Entity { get; set; }
        public float Gradient { get; set; }

        public CabinPan(int index) : this()
        {
            Index = index;
        }

        public CabinPan()
        {
            NPlayer = 0;
            Entity = null;
            Gradient = 0f;
        }
    }
}
