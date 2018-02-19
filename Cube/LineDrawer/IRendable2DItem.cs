using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct3D11;

namespace LineDrawer
{

    public interface IRenderableItem
    {
 

        /// <summary>
        /// Each items knows how to render itself properly (i.e Drawline, DrawCircule, DrawBitmap, DrawGeometry etc)
        /// </summary>
        /// <param name="device"></param>
        void Render(Device device, Matrix worldViewMatrix);
    }

    public interface IRenderable
    {


        /// <summary>
        /// Each items knows how to render itself properly (i.e Drawline, DrawCircule, DrawBitmap, DrawGeometry etc)
        /// </summary>
        /// <param name="device"></param>
        void Render(Device device, Matrix worldViewMatrix);
    }
}