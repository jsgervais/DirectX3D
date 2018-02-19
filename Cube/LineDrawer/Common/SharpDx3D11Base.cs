using System;
using System.Windows.Forms;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Device = SharpDX.Direct3D11.Device;

namespace LineDrawer.Common
{
    /// <summary>
    /// Root class for Directx11 3D types of App (Under SharpDX) 
    /// </summary>
    public class SharpDx3D11Base : BaseSharpDXApp, IDisposable
    {
        protected Device _device;
        protected SwapChain _swapChain;
        protected Texture2D _backBuffer;
        protected RenderTargetView _backBufferView;


        private Matrix _view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix _proj = Matrix.Identity;
        private DepthStencilView _depthView;
        private Texture2D _depthBuffer;
        public Matrix WorldViewMatrix { get; set; }

        /// <summary>
        /// Returns the device
        /// </summary>
        public Device Device
        {
            get
            {
                return _device;
            }
        }

        /// <summary>
        /// Returns the backbuffer used by the SwapChain
        /// </summary>
        public Texture2D BackBuffer
        {
            get
            {
                return _backBuffer;
            }
        }

        /// <summary>
        /// Returns the render target view on the backbuffer used by the SwapChain.
        /// </summary>
        public RenderTargetView BackBufferView
        {
            get
            {
                return _backBufferView;
            }
        }

        protected override void Initialize(DisplayWindowConfiguration displayWindowConfiguration)
        {
            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = 
                    new ModeDescription(displayWindowConfiguration.Width, 
                                        displayWindowConfiguration.Height,
                                        new Rational(60, 1), 
                                        Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
            // Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new [] { FeatureLevel.Level_10_0 }, desc, out _device, out _swapChain);
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out _device, out _swapChain);
            // Ignore all windows events
            Factory factory = _swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(DisplayHandle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            _backBufferView = new RenderTargetView(_device, _backBuffer);


            _depthBuffer = new Texture2D(Device, new Texture2DDescription()
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = _form.ClientSize.Width,
                Height = _form.ClientSize.Height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            });

            // Create the depth buffer view
            _depthView = new DepthStencilView(Device, _depthBuffer);

        }




        protected override void BeginDraw()
        {
            base.BeginDraw();

            // Clear views
            Device.ImmediateContext.ClearDepthStencilView(_depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
            Device.ImmediateContext.ClearRenderTargetView(_backBufferView, Color.Black);

            Device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, Config.Width, Config.Height));
            Device.ImmediateContext.OutputMerger.SetTargets(_backBufferView);



            float time = (float)clock.ElapsedTime / 1000.0f;
            var viewProj = Matrix.Multiply(_view, _proj);

            // Update WorldViewProj Matrix
            WorldViewMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
            WorldViewMatrix.Transpose();
        }


        protected override void EndDraw()
        {

            base.EndDraw();
            _swapChain.Present(Config.WaitVerticalBlanking?1:0, PresentFlags.None);
        }


        protected override void OnRender()
        {
            
        }


        protected override void Dispose(bool disposeManagedResources)
        {
            // Release all resources
 
         
            _depthBuffer.Dispose();
            _depthView.Dispose();
            _backBufferView.Dispose();
            _backBuffer.Dispose();
            Device.ImmediateContext.ClearState();
            Device.ImmediateContext.Flush();
            Device.ImmediateContext.Dispose();
            _device.Dispose();
            _swapChain.Dispose(); 
        }
           
    }
    
}