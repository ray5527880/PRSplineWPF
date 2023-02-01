using GSF.COMTRADE;
using BF_FW;
using BF_FW.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRSplineWPF.Model;


namespace PRSplineWPF.Scripts
{
    public class OpenFiles
    {
        public int[] FFTIndex;
        public void OpenFile(string FilePaht, string FileName,ref RalayPRData Raleydata)
        {
            //var _MainWindowData = new MainWindowData();
            if (Directory.Exists("./CompressFile"))
            {
                Directory.Delete("./CompressFile", true);
            }
            string strFile = FilePaht;
            string strRarPath = string.Empty;
            string strFileName = FileName;
            string strXmlFile = this.GetType().Assembly.Location;
            string strfilePath = strXmlFile = strXmlFile.Replace("PRSplineWPF.exe", "CompressFile\\");
            if (strFile.IndexOf(".cfg") > 0 || strFile.IndexOf(".CFG") > 0)
            {
                if (File.Exists(strFile.Replace(".cfg", ".dat")) || File.Exists(strFile.Replace(".CFG", ".DAT")))
                {
                    Directory.CreateDirectory(strfilePath);
                    File.Copy(strFile, (strfilePath + strFileName), true);
                    File.Copy(strFile.Replace(".cfg", ".dat"), strfilePath + strFileName.Replace(".cfg", ".dat"), true);
                    File.Copy(strFile.Replace(".CFG", ".DAT"), strfilePath + strFileName.Replace(".CFG", ".DAT"), true);
                }
            }
            else
            {
                while (strFile.IndexOf(@"\") > -1)
                {
                    strRarPath += strFile.Substring(0, strFile.IndexOf(@"\") + 1);
                    strFile = strFile.Substring(strFile.IndexOf(@"\") + 1);
                }
                var mCompressWinRAR = new CompressWinRAR();
                mCompressWinRAR.UnCompressRar(strfilePath, strRarPath, strFile);
            }
            var Parsers = new Parser();
            try
            {
                foreach (var item in Directory.GetFiles(strfilePath, "*.cfg"))
                {
                    LoadDataFile.GetCFGData(item, ref Parsers);
                    break;
                }
                foreach (var item in Directory.GetFiles(strfilePath, "*.CFG"))
                {
                    LoadDataFile.GetCFGData(item, ref Parsers);
                    break;
                }
                Raleydata.PrimaryData = new List<double[]>();
                Raleydata.SecondaryData = new List<double[]>();
                Raleydata.PerUnitData = new List<double[]>();
                Raleydata.timeData = new List<double>();
               LoadDataFile.GetDatData(Parsers, ref Raleydata.PrimaryData, ref Raleydata.SecondaryData, ref Raleydata.PerUnitData);

                List<int> _fftIndex = new List<int>();
                for (int i = 0; i < Parsers.Schema.TotalAnalogChannels; i++)
                {
                    if (Parsers.Schema.AnalogChannels[i].Units == "V" || Parsers.Schema.AnalogChannels[i].Units == "A")
                        _fftIndex.Add(i);
                }
                FFTIndex = _fftIndex.ToArray();

                GetFFTData(FFTIndex, Parsers, ref Raleydata.PrimaryData);
                GetFFTData(FFTIndex, Parsers, ref Raleydata.SecondaryData);
                GetFFTData(FFTIndex, Parsers, ref Raleydata.PerUnitData);
                // var _mFFTData = new FFTData();
                // var mfft = new FFTCal(_fftIndex.ToArray(), Parsers);
                // try
                // {
                //     _mFFTData = mfft.GetFFTData(Raleydata.PrimaryData);
                // }
                // catch (Exception ex)
                // {
                //     //.Show(ex.Message);
                // }

                //for (int i = 0; i < Parsers.Schema.TotalSamples; i++)
                // {                    
                //     var _double = new List<double>();
                //     int _index = 0;

                //     foreach (var items in Raleydata.PrimaryData[i])
                //     {
                //         if (_index < Parsers.Schema.TotalChannels + 2)
                //         {
                //             _double.Add(items);
                //         }
                //         _index++;
                //     }
                //     foreach(var items in _mFFTData.arrFFTData[i].Value)
                //     {
                //         _double.Add(items);
                //     }
                //     Raleydata.PrimaryData[i] = _double.ToArray();
                // }

                var _analogData = new List<string>();
                var _DigitalData = new List<string>();
                for (int i = 0; i < Parsers.Schema.TotalAnalogChannels; i++)
                {
                    _analogData.Add(Parsers.Schema.AnalogChannels[i].Name);
                }
                for (int i = 0; i < Parsers.Schema.TotalDigitalChannels; i++)
                {
                    _DigitalData.Add(Parsers.Schema.DigitalChannels[i].Name);
                }
                for (int i = 0; i < FFTIndex.Length; i++)
                {
                    _analogData.Add(Parsers.Schema.AnalogChannels[i].Name + "_FFT");
                }
                Raleydata.AnalogNames = _analogData.ToArray();
                Raleydata.DigitalNames = _DigitalData.ToArray();
            }
            catch (ApplicationException message)
            {
                throw;
            }
            Raleydata.Parsers= Parsers;
          //  return _MainWindowData;
        }
        private void GetFFTData(int [] _fftIndex, Parser Parsers,ref List<double[]>doubleList)
        {
            var _mFFTData = new FFTData();
            var mfft = new FFTCal(_fftIndex.ToArray(), Parsers);
            try
            {
                _mFFTData = mfft.GetFFTData(doubleList);
            }
            catch (Exception ex)
            {
                //.Show(ex.Message);
            }

            for (int i = 0; i < Parsers.Schema.TotalSamples; i++)
            {
                var _double = new List<double>();
                int _index = 0;

                foreach (var items in doubleList[i])
                {
                    if (_index < Parsers.Schema.TotalChannels + 2)
                    {
                        _double.Add(items);
                    }
                    _index++;
                }
                foreach (var items in _mFFTData.arrFFTData[i].Value)
                {
                    _double.Add(items);
                }
                doubleList[i] = _double.ToArray();
            }
        }
    }
    
}
