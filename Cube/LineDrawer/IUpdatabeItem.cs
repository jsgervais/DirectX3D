using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct3D11;

namespace LineDrawer
{
    public interface IUpdatableItem
    {
        /// <summary>
        /// Anything that moves or have to change state before being rendered
        /// </summary> 
        void Update(Timer time);
    }
}