using Ecan.WaterUseAlerts.BLL;
using Ecan.WaterUseAlerts.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecan.WaterUseAlerts.LoadFile
{
    public class Program
    {
        static void Main(string[] args)
        {
            DateTime runDate = DateTime.Now.Date;
            if (args.Length > 0)
                runDate = DateTime.Parse(args[0]);
            try
            {
                StartProcess(runDate);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        private static void StartProcess(DateTime runDate)
        {
            int count = 0;
            Log.Info(Environment.NewLine
                + "Alert load started Rundate:[" + runDate.ToShortDateString() + "] ...");
            List<DailyAlertEntity> alerts = new AlertFileHandler(runDate).GetFileAlerts();
            if (alerts != null && alerts.Count > 0)
            {
                count = alerts.Count;
                alerts = new DailyAlertBLL().SetIgnoreFlag(alerts);
                foreach (DailyAlertEntity dae in alerts)
                {
                    DailyAlertEntity newDae = new DailyAlertBLL().CreateFromCSV(dae);
                }
            }
            Log.Info(Environment.NewLine
                + "Alert load Completed for Rundate:[" + runDate.ToShortDateString() + "]. Count of Alerts Loaded=" + count.ToString() + Environment.NewLine);
        }
    }
}
