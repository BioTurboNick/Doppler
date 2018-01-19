using Storm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Doppler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        List<Localization> localizations;

        void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            if (localizations == null)
                return;

            double size = Math.Min(sender.ActualHeight, sender.ActualWidth);

            foreach(var localization in localizations)
            {
                args.DrawingSession.FillCircle(new Vector2((float)(localization.XWithDriftCorrection * size / 256), (float)(localization.YWithDriftCorrection * size / 256)), 1, Colors.Red);
            }
            
        }

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                FileTypeFilter = { ".bin" },
                ViewMode = PickerViewMode.List
            };

            var file = await picker.PickSingleFileAsync();

            localizations = await new MoleculeListReader().ReadMoleculeList(await file.OpenStreamForReadAsync());

            canvas.Invalidate();
        }
    }
}
