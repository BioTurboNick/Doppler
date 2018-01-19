using System;

namespace Storm
{
    public class Localization
    {
        public float X { get; set; }

        public float XWithDriftCorrection { get; set; }

        public float XWithDriftAndChromaticAberrationCorrection { get; set; }

        public float Y { get; set; }

        public float YWithDriftCorrection { get; set; }

        public float YWithDriftAndChromaticAberrationCorrection { get; set; }

        public float Z { get; set; }

        public float ZWithDriftAndOffsetCorrection { get; set; }

        public float ZWithDriftOffsetAndChromaticAberrationCorrection { get; set; }

        public float PeakHeight { get; set; }

        public float PeakArea { get; set; }

        public float PeakWidth { get; set; }

        public float Phi { get; set; }

        public float AxialRatio { get; set; }

        public float Background { get; set; }

        public float Intensity { get; set; }

        public int Frame { get; set; }

        public int Length { get; set; }

        public int Index { get; set; }

        public int Link { get; set; }

        public int Photons { get; set; }

        public float LateralLocalizationAccuracy { get; set; }
    }
}
