using Ecan.WaterUseAlerts.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecan.WaterUseAlerts.LoadFile
{
    public class AlertFileHandler
    {
        private DateTime RunDate;

        const string REPORT_END = "Report End";
        const int FIELD_COUNT = 11;
        const int INDEX_SERVP  = 0;
        const int INDEX_CONSNT = 1;
        const int INDEX_ACTNO  = 2;
        const int INDEX_METERS = 3;
        const int INDEX_TYPE   = 4;
        const int INDEX_LIMIT  = 5;
        const int INDEX_MAXUSE = 6;
        const int INDEX_PEROVR = 7;
        const int INDEX_HUSEDT = 8;
        const int INDEX_ENCDT  = 9;
        const int INDEX_LNCDT  = 10;

        public AlertFileHandler()
        {
            this.RunDate = DateTime.Now.Date;
        }

        public AlertFileHandler(DateTime runDate):base()
        {
            this.RunDate = runDate.Date;
        }

        /*
            Service Provider,ConsentID or Water Group,Activity Number,Meters,Type,Limit,Max Use,Percent Over Limit,Date of Highest Overage,Date of Earliest Non-compliance,Date of Latest Non-compliance
            WaterCheck,CRC991620.5,CRC991620.5-1,L35/0855-M1 + L35/0906-M1,Take Rate,90.0 l/s,91.9 l/s,2.1%,23-Jan-2019 11:30:00,23-Jan-2019 00:00:00,26-Jan-2019 09:15:00
            WaterCheck,CRC990501.2,CRC990501.2-1,M35/11240-M1 + M35/11241-M1 + M35/11242-M1,Take Rate,51.8 l/s,63.3 l/s,22.4%,25-Jan-2019 12:30:00,24-Jan-2019 11:15:00,26-Jan-2019 11:30:00
         * 
         * */
        public List<DailyAlertEntity> GetFileAlerts()
        {
            List<DailyAlertEntity> alerts = new List<DailyAlertEntity>();
            var fileStream = new FileStream(GetFileNameWithLocation(), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line = streamReader.ReadLine();  // Header line ignore
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line) && !line.Contains(REPORT_END))
                    {
                        var values = line.Split(',');
                        if (values.Length == FIELD_COUNT)
                        {
                            DailyAlertEntity alert = new DailyAlertEntity();
                            alert.ServiceProvider = values[INDEX_SERVP];
                            alert.ConsentID = values[INDEX_CONSNT];
                            alert.ActivityNumber = values[INDEX_ACTNO];
                            alert.Meters = values[INDEX_METERS];
                            alert.Type = values[INDEX_TYPE];
                            alert.Limit = values[INDEX_LIMIT];
                            alert.MaxUse = values[INDEX_MAXUSE];
                            alert.PercentOverLimit = values[INDEX_PEROVR];
                            alert.MaxAvDate = DateTime.Parse(values[INDEX_HUSEDT]);
                            alert.FirstNCDate = DateTime.Parse(values[INDEX_ENCDT]);
                            alert.LastNCDate = DateTime.Parse(values[INDEX_LNCDT]);
                            alert.RunDate = this.RunDate.Date;
                            alerts.Add(alert);
                        }
                    }
                }
            }

            return alerts;
        }

        private string GetFileNameWithLocation()
        {
            return ConfigurationManager.AppSettings["CsvFileLocation"]
                + "NonComp" + RunDate.Year.ToString() + '-'
                + RunDate.Month.ToString().PadLeft(2,'0') + '-'
                + RunDate.Day.ToString().PadLeft(2, '0') + ".csv";
        }
    }
}
