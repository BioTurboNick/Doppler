using Storm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Doppler.ViewModels
{
    public class ChannelViewModel :
        ViewModel
    {
        string _Name;

        Color _Color;
        List<Localization> _Localizations;
        double _MinPeakHeightThreshold;
        double _MaxPeakHeightThreshold;
        double _MinFrameThreshold;
        double _MaxFrameThreshold;
        bool _IsVisible;
        bool _IsUsingDriftCorrection;


        public ChannelViewModel(IEnumerable<Localization> localizations = null)
        {
            Localizations = localizations?.ToList();
            Name = localizations.FirstOrDefault()?.Category.ToString() ?? "No data";

            if (localizations is null)
                return;

            MinFrameThreshold = MinFrame;
            MaxFrameThreshold = MaxFrame;
            MinPeakHeightThreshold = MinPeakHeight;
            MaxPeakHeightThreshold = MaxPeakHeight;
            IsVisible = true;
            IsUsingDriftCorrection = true;
        }


        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        public Color Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        public List<Localization> Localizations
        {
            get => _Localizations;
            set => Set(ref _Localizations, value);
        }

        public double MinPeakHeight => _Localizations.Min((l) => l.PeakHeight);

        public double MaxPeakHeight => _Localizations.Max((l) => l.PeakHeight);

        public double MinFrame => _Localizations.Min((l) => l.Frame);

        public double MaxFrame => _Localizations.Max((l) => l.Frame);

        public double MinPeakHeightThreshold
        {
            get => _MinPeakHeightThreshold;
            set => Set(ref _MinPeakHeightThreshold, value);
        }

        public double MaxPeakHeightThreshold
        {
            get => _MaxPeakHeightThreshold;
            set => Set(ref _MaxPeakHeightThreshold, value);
        }

        public double MinFrameThreshold
        {
            get => _MinFrameThreshold;
            set => Set(ref _MinFrameThreshold, value);
        }

        public double MaxFrameThreshold
        {
            get => _MaxFrameThreshold;
            set => Set(ref _MaxFrameThreshold, value);
        }

        public bool IsVisible
        {
            get => _IsVisible;
            set => Set(ref _IsVisible, value);
        }

        public bool IsUsingDriftCorrection
        {
            get => _IsUsingDriftCorrection;
            set => Set(ref _IsUsingDriftCorrection, value);
        }
    }
}
