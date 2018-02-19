using System;
using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace LineDrawer.GUI
{
    class GUIButton : GUIBaseControl
    {
         
        public bool RoundedButton { get; set; }

        public GUIButton(int x, int y, int w, int h, string text, Action onClickDelegate)
        {
            PositionX = x;
            PositionY = y;
            Width = w;
            Height = h;
            Text = text;
            OnClickDelegate = onClickDelegate;

            TextFormat = new TextFormat(FactoryDWrite, "Calibri", 20) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            TextLayout = new TextLayout(FactoryDWrite, Text, TextFormat, Width, Height);
        }

        public override void Update(Timer time)
        { 
            //TODO manage clicked animation 

        }

        

        public override void Render(Device device, Matrix worldViewMatrix)
        {
            //DrawFilledRectangle(renderTarget, PositionX, PositionY, Width, Height, BackgroundColor );
           // DrawBorderBox(renderTarget, PositionX, PositionY, Width, Height, BorderColor);
            //DrawText(renderTarget, PositionX, PositionY, TextLayout, TextColor);
        }

        public override void DrawHover(Device device, Matrix worldViewMatrix)
        {
           // DrawFilledRectangle(renderTarget, PositionX, PositionY, Width, Height, BackgroundColorHover);
           // DrawBorderBox(renderTarget, PositionX, PositionY, Width, Height, BorderColorHover);
           // DrawText(renderTarget, PositionX, PositionY, TextLayout, TextColor);
        }
    }
}
