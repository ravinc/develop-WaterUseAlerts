using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Ecan.WaterUseAlerts.Global;
using Ecan.WaterUseAlerts.BLL;
using System.Data;
using System.Web;
using System.Linq;

namespace Ecan.WaterUseAlerts.Web
{
    public partial class Alerts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                currTabNo.Text = "0";

                try
                {
                    // populate the Alerts Grid
                    RefreshAllAlertsTab();
                    RefreshStatsTab();
                    RefreshRmoaTab();
                }
                catch (Exception ex)
                {
                    errorMessages.Text = ex.Message;
                    currTabNo.Text = "0";
                }

            }
        }


        // RMOA Tab grid
        protected void gvRmoaAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvRmoaAlerts.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlRmoaStatus");
                if (string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    ddlStatus.DataTextField = "Description";
                    ddlStatus.DataValueField = "Description";
                    ddlStatus.DataSource = new DailyAlertBLL().GetDailyAlertsStatus();
                    ddlStatus.DataBind();
                    string selectedStatus = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                    if (!string.IsNullOrEmpty(selectedStatus))
                        ddlStatus.Items.FindByValue(selectedStatus).Selected = true;
                }
                DailyAlertEntity alertToUpdate = (DailyAlertEntity)e.Row.DataItem;
                txtID.Text = alertToUpdate.ID.ToString();
            }
        }

        protected void gvRmoaAlerts_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            gvRmoaAlerts.EditIndex = e.NewEditIndex;
            gvRmoaAlerts.DataBind();
        }

        protected void gvRmoaAlerts_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            gvRmoaAlerts.EditIndex = -1;
            gvRmoaAlerts.DataBind();
            RefreshRmoaTab();
        }

        protected void gvRmoaAlerts_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //DailyAlertEntity alertToUpdate = gvRmoaAlerts.Rows[e.RowIndex].DataItem as DailyAlertEntity;
            DailyAlertEntity alertToUpdate = new DailyAlertEntity();

            alertToUpdate.ID = int.Parse(txtID.Text);

            TextBox txtComments = (TextBox)gvRmoaAlerts.Rows[e.RowIndex].FindControl("txtRmoaComments");
            alertToUpdate.Comment = txtComments.Text;

            DropDownList ddlStatus = (DropDownList)gvRmoaAlerts.Rows[e.RowIndex].FindControl("ddlRmoaStatus");
            alertToUpdate.Status = ddlStatus.Text;

            alertToUpdate.Username = HttpContext.Current.User.Identity.Name;

            try
            {
                new DailyAlertBLL().UpdateForRmoa(alertToUpdate);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                errorMessages.CssClass = "error";
                errorMessages.Text = ex.Message;
                currTabNo.Text = "0";
            }
        }

        protected void gvRmoaAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        // All Alerts Tab grid
        protected void gvAllAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //  && gvRmoaAlerts.EditIndex ==e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                }
            }
        }

        protected void gvAllAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CommandName = e.CommandName;
            int PaySequenceNo = int.Parse(e.CommandArgument.ToString());

            try
            {
                switch (CommandName)
                {
                    case "TimesheetJournal":

                        //PayglobalTimesheetHandler timesheets = new PayglobalTimesheetHandler(PaySequenceNo);
                        //timesheets.ProcessMessage();

                        //DisplayHandlerMessages(timesheets.messages);
                        break;

                    case "HolidayAccrualJournal":
                        infoMessages.Text = string.Format("INFO - Holiday Accrual Journals has not yet been implemented.");
                        break;

                    case "ClosePGJobs":
                        //PayglobalCostCentreCloseHandler costcentre = new PayglobalCostCentreCloseHandler();
                        //costcentre.ProcessMessage();

                        //DisplayHandlerMessages(costcentre.messages);
                        break;
                }

            }
            catch (Exception ex)
            {
                errorMessages.Text = ex.Message;
            }

            currTabNo.Text = "0";
        }


        protected void gvRmoaAlerts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvAllAlerts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRefreshRmoa_Click(object sender, EventArgs e)
        {
            RefreshRmoaTab();
        }

        protected void btnRefreshAll_Click(object sender, EventArgs e)
        {
            RefreshAllAlertsTab();
        }

        protected void btnRefreshStats_Click(object sender, EventArgs e)
        {
            RefreshStatsTab();
        }

        protected void txtRmoaStartDate_OnTextChanged(object sender, EventArgs e)
        {
            if (gvRmoaAlerts.EditIndex == -1)
                RefreshRmoaTab();
        }

        protected void cbxMyAlerts_OnCheckedChanged(object sender, EventArgs e)
        {
            if (gvRmoaAlerts.EditIndex == -1)
                RefreshRmoaTab();
        }

        protected void cbxIncludeIgnored_OnCheckedChanged(object sender, EventArgs e)
        {
            // Force the user to click on the Refresh Button
            //RefreshAllAlertsTab();
        }

        protected void cbkMyAllAlerts_OnCheckedChanged(object sender, EventArgs e)
        {
            // Force the user to click on the Refresh Button
            //RefreshAllAlertsTab();
        }

        protected override void OnPreRender(EventArgs e)
        {
            // force GridViews to use <thead> & <tbody> for use with jquery tablesorter

            //if ((gvRmoaAlerts.ShowHeader && gvRmoaAlerts.Rows.Count > 0)
            //    || (gvRmoaAlerts.ShowHeaderWhenEmpty)
            //    )
            //    gvRmoaAlerts.HeaderRow.TableSection = TableRowSection.TableHeader;

            if ((gvAllAlerts.ShowHeader && gvAllAlerts.Rows.Count > 0)
                || (gvAllAlerts.ShowHeaderWhenEmpty)
                )
                gvAllAlerts.HeaderRow.TableSection = TableRowSection.TableHeader;

            base.OnPreRender(e);
        }

        private void DisplayHandlerMessages(MessagesEntity messages)
        {
            if (messages == null)
                return;
            infoMessages.Text = "";
            errorMessages.Text = "";

            foreach (string msg in messages.Info)
            {
                infoMessages.Text += string.Format("INFO - {0}<br/>", msg);
            }

            if (messages.Debug.Count > 0) { infoMessages.Text += "<br/>"; };
            foreach (string msg in messages.Debug)
            {
                infoMessages.Text += string.Format("DEBUG - {0}<br/>", msg);
            }

            if (messages.Warn.Count > 0) { infoMessages.Text += "<br/>"; };
            foreach (string msg in messages.Warn)
            {
                infoMessages.Text += string.Format("WARN - {0}<br/>", msg);
            }

            foreach (string msg in messages.Error)
            {
                errorMessages.Text += string.Format("ERROR - {0}<br/>", msg);
            }
        }
        private void RefreshAllAlertsTab()
        {
            int displayLimit = 0;
            string displayLimitText = ConfigurationManager.AppSettings["AllAlertsDisplayLimit"];
            if (!string.IsNullOrEmpty(displayLimitText))
                displayLimit = int.Parse(displayLimitText);

            List<DailyAlertEntity> alerts = new DailyAlertBLL().SearchDailyAlerts(GetAllAlertsSearchCriteria());
            if (displayLimit == 0)
                gvAllAlerts.DataSource = alerts;
            else
                gvAllAlerts.DataSource = alerts.Take(displayLimit).ToList();

            gvAllAlerts.DataBind();

            ddlAllConsents.DataSource = new DailyAlertBLL().GetConsentIDs();
            ddlAllConsents.DataBind();

            currTabNo.Text = "1";
        }

        private void RefreshStatsTab()
        {
            List<AlertStatsEntity> stats = new DailyAlertBLL().GetStatsForDateRange(GetAlertStatsCriteria());
            gvStatistics.DataSource = stats;
            gvStatistics.DataBind();

            currTabNo.Text = "2";
        }

        private void RefreshRmoaTab()
        {
            List<DailyAlertEntity> alerts = new DailyAlertBLL().SearchDailyAlerts(GetRmoaAlertsSearchCriteria());
            gvRmoaAlerts.DataSource = new DailyAlertBLL().GetRmoaAlerts(alerts);
            gvRmoaAlerts.DataBind();
            currTabNo.Text = "0";
        }

        private DailyAlertSearchCriteria GetAllAlertsSearchCriteria()
        {
            DailyAlertSearchCriteria searchCriteria = new DailyAlertSearchCriteria();
            if (!string.IsNullOrEmpty(txtStartDate.Text))
                searchCriteria.StartDate = DateTime.Parse(txtStartDate.Text);
            else
            {
                searchCriteria.StartDate = DateTime.Today.AddDays(-7 - (int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                txtStartDate.Text = string.Format("{0:dd/M/yyyy}", searchCriteria.StartDate);
                txtRmoaStartDate.Text = String.Format("{0:dd/M/yyyy}", searchCriteria.StartDate);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
                searchCriteria.EndDate = DateTime.Parse(txtEndDate.Text);
            else
            {
                searchCriteria.EndDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);
                txtEndDate.Text = String.Format("{0:dd/M/yyyy}", searchCriteria.EndDate);
            }
            if (cbxIncludeIgnored.Checked)
                searchCriteria.IgnoreAll = "Yes";
            if (cbkMyAllAlerts.Checked)
                searchCriteria.Username = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrEmpty(ddlAllConsents.Text))
                searchCriteria.ConsentID = ddlAllConsents.Text;
            return searchCriteria;
        }

        private DailyAlertSearchCriteria GetRmoaAlertsSearchCriteria()
        {
            DateTime startDate = GetStartDate(txtRmoaStartDate.Text);
            DailyAlertSearchCriteria searchCriteria = new DailyAlertSearchCriteria();
            searchCriteria.StartDate = startDate;
            if (string.IsNullOrEmpty(txtRmoaStartDate.Text))
                txtRmoaStartDate.Text = String.Format("{0:dd/M/yyyy}", startDate);
            else if (!startDate.Equals(DateTime.Parse(txtRmoaStartDate.Text)))
                    txtRmoaStartDate.Text = String.Format("{0:dd/M/yyyy}", startDate);
            searchCriteria.EndDate = ((DateTime)searchCriteria.StartDate).AddDays(6);
            if (cbxMyAlerts.Checked)
                searchCriteria.Username = HttpContext.Current.User.Identity.Name;
            return searchCriteria;
        }

        private AlertStatsCriteria GetAlertStatsCriteria()
        {
            DateTime startDate = GetStartDate(txtStatsStartDate.Text);
            AlertStatsCriteria criteria = new AlertStatsCriteria();
            criteria.StartDate = startDate;
            if (string.IsNullOrEmpty(txtStatsStartDate.Text))
                txtStatsStartDate.Text = String.Format("{0:dd/M/yyyy}", startDate);
            else if (!startDate.Equals(DateTime.Parse(txtStatsStartDate.Text)))
                txtStatsStartDate.Text = String.Format("{0:dd/M/yyyy}", startDate);
            criteria.EndDate = ((DateTime)criteria.StartDate).AddDays(6);
            return criteria;
        }

        private DateTime GetStartDate(string dateValue)
        {
            DateTime startDate = DateTime.Now.Date;
            if (!string.IsNullOrEmpty(dateValue))
            {
                startDate = DateTime.Parse(dateValue);
                if (startDate.DayOfWeek.Equals(DayOfWeek.Sunday))
                    startDate = startDate.AddDays(-1);
                startDate = startDate.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            }
            else
            {
                startDate = startDate.AddDays(-7 - (int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            }
            return startDate;
        }

    }
}