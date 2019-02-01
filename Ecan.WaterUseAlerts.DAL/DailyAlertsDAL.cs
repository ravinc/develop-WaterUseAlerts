using Ecan.WaterUseAlerts.Global;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Ecan.WaterUseAlerts.DAL
{
    public class DailyAlertsDAL : BaseDAL
    {
        public List<DailyAlertEntity> SearchDailyAlerts(DailyAlertSearchCriteria searchCriteria)
        {
            List<DailyAlertEntity> alertsList = new List<DailyAlertEntity>();

            string sqlCommand = "dbo.pSearchDailyAlerts";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    AddSearchParametersToCommand(db, cmd, searchCriteria);
                    using (IDataReader dataReader = db.ExecuteReader(cmd))
                    {
                        while (dataReader.Read())
                        {
                            alertsList.Add(PopulateAlertFromDataReader(dataReader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Search Daily Alerts failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return alertsList;
        }

        public DailyAlertEntity UpdateForRmoa(DailyAlertEntity alert)
        {
            string sqlCommand = "dbo.pUpdateDailyAlerts";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    AddUpdateRmoaParametersToCommand(db, cmd, alert);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Update Daily Alert failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return alert;
        }


        public DailyAlertEntity CreateFromCSV(DailyAlertEntity alert)
        {
            string sqlCommand = "dbo.pCreateDailyAlerts";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    AddCreateCSVParametersToCommand(db, cmd, alert);
                    db.AddOutParameter(cmd, "@AlertID", DbType.Int32, 4);
                    db.ExecuteNonQuery(cmd);
                    alert.ID = int.Parse(db.GetParameterValue(cmd, "@AlertID").ToString());

                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Create Daily Alert failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return alert;
        }

        public List<string> GetConsentIDs()
        {
            List<string> consents = new List<string>();

            string sqlCommand = "dbo.pGetConsentIDs";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    using (IDataReader dataReader = db.ExecuteReader(cmd))
                    {
                        while (dataReader.Read())
                        {
                            consents.Add(GetString("ConsentID", dataReader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Get Consent IDs failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return consents;
        }


        public List<ConsentIgnoreEntity> GetIgnoreList()
        {
            List<ConsentIgnoreEntity> ignoreList = new List<ConsentIgnoreEntity>();

            string sqlCommand = "dbo.pGetIgnoreList";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    using (IDataReader dataReader = db.ExecuteReader(cmd))
                    {
                        while (dataReader.Read())
                        {
                            ignoreList.Add(PopulateConsentIgnoreFromDataReader(dataReader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Get Consent to be ignored list failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return ignoreList;
        }

        public List<AlertStatsEntity> GetStatsForDateRange(AlertStatsCriteria criteria)
        {
            List<AlertStatsEntity> stats = new List<AlertStatsEntity>();

            string sqlCommand = "dbo.pDailyAlertStats";

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    AddSearchParametersToCommand(db, cmd, criteria);
                    using (IDataReader dataReader = db.ExecuteReader(cmd))
                    {
                        while (dataReader.Read())
                        {
                            stats.Add(PopulateAlertStatsFromDataReader(dataReader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Alerts Stats retrieval failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return stats;
        }

        public List<LookupEntity> GetDailyAlertsStatus()
        {
            return GetLookupList("dbo.pGetDailyAlertsStatus");
        }

        public List<LookupEntity> GetDailyAlertsIgnoreReason()
        {
            return GetLookupList("dbo.pGetDailyAlertsIgnoreReason");
        }

        private List<LookupEntity> GetLookupList(string storedProcedure)
        {
            List<LookupEntity> itemList = new List<LookupEntity>();

            string sqlCommand = storedProcedure;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("HilltopDatabase");

                using (DbCommand cmd = db.GetStoredProcCommand(sqlCommand))
                {
                    // AddSearchParametersToCommand(db, cmd, searchCriteria);
                    using (IDataReader dataReader = db.ExecuteReader(cmd))
                    {
                        while (dataReader.Read())
                        {
                            itemList.Add(PopulateLookupItemFromDataReader(dataReader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessages = string.Empty;
                ExceptionHelper.GetInnerExceptions(ex, out exceptionMessages);

                string message = "Stored Procedure [" + storedProcedure  + "] failed at the data access layer"
                    + Environment.NewLine + exceptionMessages;
                throw new Exception(message);
            }
            return itemList;
        }

        #region private Methods

        private void AddSearchParametersToCommand(Database db, DbCommand cmd, AlertStatsCriteria criteria)
        {

            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, criteria.StartDate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, criteria.EndDate);
        }

        private void AddSearchParametersToCommand(Database db, DbCommand cmd, DailyAlertSearchCriteria searchCriteria)
        {
            AddStringValueAsParameter(db, cmd, "@IncludeAll", searchCriteria.IgnoreAll);

            if (searchCriteria.StartDate != null && searchCriteria.StartDate.HasValue)
            {
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, searchCriteria.StartDate.Value);
            }
            if (searchCriteria.EndDate != null && searchCriteria.EndDate.HasValue)
            {
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, searchCriteria.EndDate.Value);
            }

            AddStringValueAsParameter(db, cmd, "@ConsentID", searchCriteria.ConsentID);

            AddStringValueAsParameter(db, cmd, "@Username", searchCriteria.Username);

            AddStringValueAsParameter(db, cmd, "@Type", searchCriteria.Type);

            AddStringValueAsParameter(db, cmd, "@ActivityNumber", searchCriteria.ActivityNumber);

        }

        private void AddUpdateRmoaParametersToCommand(Database db, DbCommand cmd, DailyAlertEntity alert)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, alert.ID);
            db.AddInParameter(cmd, "@Ignore", DbType.Int32, alert.Ignore);
            db.AddInParameter(cmd, "@Status", DbType.String, alert.Status);
            db.AddInParameter(cmd, "@Comment", DbType.String, alert.Comment);
            db.AddInParameter(cmd, "@Username", DbType.String, alert.Username);
        }

        private void AddCreateCSVParametersToCommand(Database db, DbCommand cmd, DailyAlertEntity alert)
        {
            db.AddInParameter(cmd, "@ServiceProvider", DbType.String, alert.ServiceProvider);
            db.AddInParameter(cmd, "@ConsentID", DbType.String, alert.ConsentID);
            db.AddInParameter(cmd, "@ActivityNumber", DbType.String, alert.ActivityNumber);
            db.AddInParameter(cmd, "@Meters", DbType.String, alert.Meters);
            db.AddInParameter(cmd, "@Type", DbType.String, alert.Type);
            db.AddInParameter(cmd, "@Limit", DbType.String, alert.Limit);
            db.AddInParameter(cmd, "@MaxUse", DbType.String, alert.MaxUse);
            db.AddInParameter(cmd, "@PercentOverLimit", DbType.String, alert.PercentOverLimit);
            db.AddInParameter(cmd, "@DateofHighestOverage", DbType.DateTime, alert.MaxAvDate);
            db.AddInParameter(cmd, "@DateofEarliestNoncompliance", DbType.DateTime, alert.FirstNCDate);
            db.AddInParameter(cmd, "@DateofLatestNoncompliance", DbType.DateTime, alert.LastNCDate);
            db.AddInParameter(cmd, "@Ignore", DbType.Int32, alert.Ignore);
            db.AddInParameter(cmd, "@Rundate", DbType.Date, alert.RunDate);
        }

        private DailyAlertEntity PopulateAlertFromDataReader(IDataReader dataReader)
        {
            DailyAlertEntity alert = new DailyAlertEntity();

            alert.ID = GetInt("ID", dataReader);
            alert.ServiceProvider = GetString("ServiceProvider", dataReader);
            alert.ConsentID = GetString("ConsentID", dataReader);
            alert.ActivityNumber = GetString("ActivityNumber", dataReader);
            alert.WaterUseType = GetString("Type", dataReader);
            alert.Meters = GetString("Meters", dataReader);
            alert.Type = GetString("Type", dataReader);
            alert.Limit = GetString("Limit", dataReader);
            alert.MaxUse = GetString("MaxUse", dataReader);
            alert.Comment = GetString("Comment", dataReader);

            alert.Ignore = GetIntValue("Ignore", dataReader);

            alert.IgnoreReason = GetString("IgnoreReason", dataReader);
            alert.Status = GetString("Status", dataReader);
            alert.Username = GetString("Username", dataReader);
            alert.PercentOverLimit = GetString("PercentOverLimit", dataReader);
            alert.MaxAvDate = GetDateTime("MaxAvDate", dataReader);
            alert.RunDate = GetDateTime("RunDate", dataReader);
            alert.LastModifiedDate = GetDateTime("LastModifiedDate", dataReader);
            alert.FirstNCDate = GetNullableDateTime("FirstNCDate", dataReader);
            alert.LastNCDate = GetNullableDateTime("LastNCDate", dataReader);

            return alert;
        }

        private ConsentIgnoreEntity PopulateConsentIgnoreFromDataReader(IDataReader dataReader)
        {
            ConsentIgnoreEntity item = new ConsentIgnoreEntity();

            item.ID = GetInt("ID", dataReader);
            item.ConsentID = GetString("ConsentID", dataReader);
            item.Comment = GetString("Comment", dataReader);
            item.IgnoreReason = GetString("Reason", dataReader);
            item.Username = GetString("CreatedBy", dataReader);
            item.StartDate = GetDateTime("StartDate", dataReader);
            item.EndDate = GetNullableDateTime("EndDate", dataReader);
            return item;
        }

        private LookupEntity PopulateLookupItemFromDataReader(IDataReader dataReader)
        {
            LookupEntity item = new LookupEntity();

            item.ID = GetInt("ID", dataReader);
            item.Description = GetString("Description", dataReader);
            return item;
        }

        private AlertStatsEntity PopulateAlertStatsFromDataReader(IDataReader dataReader)
        {
            AlertStatsEntity stats = new AlertStatsEntity();

            stats.Count = GetInt("Count", dataReader);
            stats.StatsType = GetString("StatsType", dataReader);
            return stats;
        }

        private int GetIntValue(string fieldName, IDataReader dataReader)
        {
            int? intVal = GetNullableInt(fieldName, dataReader);
            if (intVal != null && intVal.HasValue && intVal.Value == 1)
                return 1;
            else
                return 0;
        }

        private void AddStringValueAsParameter(Database db, DbCommand cmd, string param, string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                db.AddInParameter(cmd, param, DbType.String, val);
            }
        }

        #endregion

    }
}
