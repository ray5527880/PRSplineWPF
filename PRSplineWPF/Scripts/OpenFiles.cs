using GSF.COMTRADE;
using BF_FW;
using BF_FW.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRSplineWPF.Scripts
{
    public class OpenFiles
    {
        public int[] FFTIndex;
        public void OpenFile(string FilePaht, string FileName)
        {
            //var _MainWindowData = new MainWindowData();

            string strFile = FilePaht;
            string strRarPath = string.Empty;
            string strFileName = FileName;
            string strXmlFile = this.GetType().Assembly.Location;
            string strfilePath = strXmlFile = strXmlFile.Replace("PRView.dll", "CompressFile\\");
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
                //_MainWindowData.PrimaryData = new List<double[]>();
                //_MainWindowData.SecondaryData = new List<double[]>();
               // _MainWindowData.PerUnitData = new List<double[]>();
               // LoadDataFile.GetDatData(Parsers, ref _MainWindowData.PrimaryData, ref _MainWindowData.SecondaryData, ref _MainWindowData.PerUnitData);

                List<int> _fftIndex = new List<int>();
                for (int i = 0; i < Parsers.Schema.TotalAnalogChannels; i++)
                {
                    if (Parsers.Schema.AnalogChannels[i].Units == "V" || Parsers.Schema.AnalogChannels[i].Units == "A")
                        _fftIndex.Add(i);
                }
                FFTIndex = _fftIndex.ToArray();
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
                //_MainWindowData.AnalogNames = _analogData.ToArray();
                //_MainWindowData.DigitalNames = _DigitalData.ToArray();
            }
            catch (ApplicationException message)
            {
                throw;
            }
          //  return _MainWindowData;
        }
    }
    
}
