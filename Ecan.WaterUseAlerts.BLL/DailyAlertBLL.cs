using Ecan.WaterUseAlerts.DAL;
using Ecan.WaterUseAlerts.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Ecan.WaterUseAlerts.BLL
{
    public class DailyAlertBLL
    {
        public List<DailyAlertEntity> SearchDailyAlerts(DailyAlertSearchCriteria searchCriteria)
        {
            List<DailyAlertEntity> list = new DailyAlertsDAL().SearchDailyAlerts(searchCriteria);
            if (list != null && list.Count > 0)
            {
                return list.OrderByDescending(a => a.SortIndex).ThenByDescending(a => a.MaxUseNumeric).ToList();
            }
            return list;
        }

        public List<DailyAlertEntity> GetRmoaAlerts(List<DailyAlertEntity> alerts)
        {
            List<DailyAlertEntity> rmoaAlerts = new List<DailyAlertEntity>();
            if (alerts != null || alerts.Count > 1)
            {
                // first sort it in consent order
                alerts = alerts.OrderBy(a => a.ConsentID).ThenByDescending(a => a.SortIndex).ThenByDescending(a => a.RunDate).ToList();
                string consentID = string.Empty;
                foreach (DailyAlertEntity dae in alerts)
                {
                    if (!dae.ConsentID.Equals(consentID))
                    {
                        consentID = dae.ConsentID;
                        if (dae.IsForRmoa())
                            rmoaAlerts.Add(dae);
                    }
                }

                int displayLimit = 0;
                string displayLimitText = ConfigurationManager.AppSettings["RmoaDisplayLimit"];
                if (!string.IsNullOrEmpty(displayLimitText))
                    displayLimit = int.Parse(displayLimitText);
                if (displayLimit == 0)
                    rmoaAlerts = rmoaAlerts.OrderByDescending(a => a.SortIndex).ThenByDescending(a => a.MaxUseNumeric).ToList();
                else
                    rmoaAlerts = rmoaAlerts.OrderByDescending(a => a.SortIndex).ThenByDescending(a => a.MaxUseNumeric).Take(displayLimit).ToList();
            }
            return rmoaAlerts;
        }

        public DailyAlertEntity UpdateForRmoa(DailyAlertEntity alert)
        {
            if (alert.Status.Equals("Not Set"))
                throw new Exception("Status cannot be set to 'Not Set'");
            return new DailyAlertsDAL().UpdateForRmoa(alert);
        }

        public DailyAlertEntity CreateFromCSV(DailyAlertEntity alert)
        {
            // First check to see if record already exists
            DailyAlertSearchCriteria searchCriteria = new DailyAlertSearchCriteria();
            searchCriteria.ConsentID = alert.ConsentID;
            searchCriteria.Type = alert.Type;
            searchCriteria.ActivityNumber = alert.ActivityNumber;
            searchCriteria.StartDate = alert.RunDate.Date;
            searchCriteria.EndDate = alert.RunDate.Date;
            searchCriteria.IgnoreAll = "es";

            List<DailyAlertEntity> alerts = SearchDailyAlerts(searchCriteria);

            if (alerts == null || alerts.Count == 0)
                alert = new DailyAlertsDAL().CreateFromCSV(alert);
            else
                Log.Warn("Alert already exists for ConsentID:[" + alert.ConsentID + "] Type:[" + alert.Type + "] Activity Number:[" + alert.ActivityNumber + "] RunDate:[" + alert.RunDate.ToShortDateString() + "]");
            return alert;
        }


        public List<DailyAlertEntity> SetIgnoreFlag(List<DailyAlertEntity> alerts)
        {
            List<DailyAlertEntity> returnAlerts = new List<DailyAlertEntity>();
            List<ConsentIgnoreEntity> ignoreList = GetCurrentIgnoreList();
            if (alerts != null && alerts.Count > 0)
            {
                foreach(DailyAlertEntity dae in alerts)
                {
                    DailyAlertEntity toAdd = dae;
                    if (ignoreList.Exists(e => e.ConsentID.Equals(toAdd.ConsentID)))
                        toAdd.Ignore = 1;
                    else
                        toAdd.Ignore = 0;
                    returnAlerts.Add(toAdd);
                }
            }
            return returnAlerts;
        }

        public List<LookupEntity> GetDailyAlertsStatus()
        {
            return new DailyAlertsDAL().GetDailyAlertsStatus();
        }

        public List<string> GetConsentIDs()
        {
            List<string> consents = new DailyAlertsDAL().GetConsentIDs();
            if (consents != null && consents.Count > 0)
            {
                consents.Add(string.Empty);
                consents.Sort();
            }
            return consents;
        }

        public List<ConsentIgnoreEntity> GetIgnoreList()
        {
            return new DailyAlertsDAL().GetIgnoreList();
        }

        public List<ConsentIgnoreEntity> GetCurrentIgnoreList()
        {
            List<ConsentIgnoreEntity> currentList = new List<ConsentIgnoreEntity>();
            List<ConsentIgnoreEntity> list = GetIgnoreList();
            if (list != null && list.Count > 0)
            {
                foreach (ConsentIgnoreEntity cil in list)
                {
                    if (cil.StartDate.Date <= DateTime.Now.Date && (cil.EndDate == null || ((DateTime) cil.EndDate).Date >= DateTime.Now.Date))
                        currentList.Add(cil);
                }
            }
            return currentList;
        }

        public List<LookupEntity> GetDailyAlertsIgnoreReason()
        {
            return new DailyAlertsDAL().GetDailyAlertsIgnoreReason();
        }

        public List<AlertStatsEntity> GetStatsForDateRange(AlertStatsCriteria criteria)
        {
            return new DailyAlertsDAL().GetStatsForDateRange(criteria);
        }

    }
}
