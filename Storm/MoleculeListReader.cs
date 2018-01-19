using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Storm
{
    public class MoleculeListReader
    {
        public async Task<List<Localization>> ReadMoleculeList(Stream stream)
        {
            return await Task.Run(() =>
            {
                List<Localization> localizations;
                
                using (var reader = new BinaryReader(stream))
                {
                    byte[] buffer = new byte[4];

                    stream.Seek(69, SeekOrigin.Begin); // Skip 69-byte header
                    int numMolecules = reader.ReadInt32();

                    localizations = new List<Localization>(numMolecules);

                    for (int i = 0; i < numMolecules; i++)
                    {
                        var localization = new Localization
                        {
                            X = reader.ReadSingle(),
                            Y = reader.ReadSingle(),
                            XWithDriftCorrection = reader.ReadSingle(),
                            YWithDriftCorrection = reader.ReadSingle(),
                            PeakHeight = reader.ReadSingle(),
                            PeakArea = reader.ReadSingle(),
                            PeakWidth = reader.ReadSingle(),
                            Phi = reader.ReadSingle(),
                            AxialRatio = reader.ReadSingle(),
                            Background = reader.ReadSingle(),
                            Intensity = reader.ReadSingle(),
                        };
                        
                        stream.Seek(8, SeekOrigin.Current);

                        localization.Frame = reader.ReadInt32();
                        localization.Length = reader.ReadInt32();
                        localization.Link = reader.ReadInt32();
                        localization.Z = reader.ReadSingle();
                        localization.ZWithDriftAndOffsetCorrection = reader.ReadSingle();

                        localizations.Add(localization);
                    }

                    return localizations;
                }
            });
        }
    }
}
