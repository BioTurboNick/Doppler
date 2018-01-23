using Doppler.Controls;
using Doppler.ViewModels;
using Microsoft.Graphics.Canvas.UI.Xaml;
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

namespace Doppler.Views
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

        Dictionary<int, ChannelViewModel> channels = new Dictionary<int, ChannelViewModel>();
        Dictionary<ChannelViewModel, int> channelIds = new Dictionary<ChannelViewModel, int>();

        double zoomFactor = 1;
        

        Dictionary<int, CanvasControl> channelCanvases = new Dictionary<int, CanvasControl>();

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            channels.Clear();
            channelCanvases.Clear();
            canvas.Children.Clear();
            controlPanel.Children.Clear();

            var picker = new FileOpenPicker
            {
                FileTypeFilter = { ".bin" },
                ViewMode = PickerViewMode.List
            };

            var file = await picker.PickSingleFileAsync();

            var localizations = await new MoleculeListReader().ReadMoleculeList(await file.OpenStreamForReadAsync());

            int minCategory = localizations.Min((l) => l.Category);
            int maxCategory = localizations.Max((l) => l.Category);

            for (int i = minCategory; i <= maxCategory; i++)
            {
                var color = i == 1 ? Colors.Red : Colors.Green;

                var channel = new ChannelViewModel(localizations.Where((l) => l.Category == i));
                channel.Color = color;
                channel.PropertyChanged += Channel_PropertyChanged;
                channels.Add(i, channel);
                channelIds.Add(channel, i);
                controlPanel.Children.Add(new ChannelControlPanel(channel));

                var channelCanvas = new CanvasControl { ClearColor = Colors.Transparent, Tag = i };
                channelCanvas.Draw += CanvasControl_Draw;
                channelCanvases.Add(i, channelCanvas);
                canvas.Children.Add(channelCanvas);
            }
        }

        void Channel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int channelId = channelIds[(ChannelViewModel)sender];
            channelCanvases[channelId].Invalidate();
        }

        void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            int channelId = (int)sender.Tag;

            var channel = channels[channelId];

            if (!channel.IsVisible)
                return;

            var color = channel.Color;
            color.A = 128;

            double size = Math.Min(sender.ActualHeight, sender.ActualWidth);

            foreach (var localization in channel.Localizations)
            {
                if (localization.PeakHeight < channel.MinPeakHeightThreshold ||
                    localization.PeakHeight > channel.MaxPeakHeightThreshold ||
                    localization.Frame < channel.MinFrameThreshold ||
                    localization.Frame > channel.MaxFrameThreshold)
                    continue;

                double x = channel.IsUsingDriftCorrection ? localization.XWithDriftCorrection * size / 256 * zoomFactor : localization.X * size / 256 * zoomFactor;
                double y = channel.IsUsingDriftCorrection ? localization.YWithDriftCorrection * size / 256 * zoomFactor : localization.Y * size / 256 * zoomFactor;

                args.DrawingSession.FillCircle(new Vector2((float)x, (float)y), 1, color);
            }
        }
    }
}
