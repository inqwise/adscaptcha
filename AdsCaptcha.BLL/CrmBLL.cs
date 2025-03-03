using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.DAL;

namespace AdsCaptcha.BLL
{
    public enum CrmStatus
    {
        New = 19001,
        Open = 19002,
        Closed = 19003,
        Duplicated = 19004,
        Trash = 19005
    }

    public static class CrmBLL
    {
        #region Public Methods

        /// <summary>
        /// Get request.
        /// </summary>
        /// <param name="requestId">Request id.</param>
        /// <returns>Returns requested request.</returns>
        public static TC_REQUEST GetRequest(int requestId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                if (IsExist(requestId))
                {
                    // Return request.
                    return dataContext.TC_REQUESTs.SingleOrDefault(r => r.Message_Id == requestId);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get requests list.
        /// </summary>
        /// <returns>Returns all requests.</returns>
        public static List<TC_REQUEST> GetWebsitesList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return requests list.
                return dataContext.TC_REQUESTs.ToList();
            }
        }

        /// <summary>
        /// Add new request.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="email">User email.</param>
        /// <param name="phone">User phone.</param>
        /// <param name="subjectId">Request subject id.</param> 
        /// <param name="message">Request message body.</param>
        /// <returns>Returns new request's id.</returns>
        public static int Add(string name, string email, string phone, int subjectId, string message)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create request.
                TC_REQUEST request = new TC_REQUEST();
                request.Name = name;
                request.Email = email;
                request.Phone = phone;
                request.Subject_Id = subjectId;
                request.Message = message;
                request.Status_Id = (int)CrmStatus.New;
                request.Sent_Date = DateTime.Today;

                // Add website.
                dataContext.TC_REQUESTs.InsertOnSubmit(request);

                // Save changes.
                dataContext.SubmitChanges();

                // Get new request id.
                int requestId = request.Message_Id;

                // TODO: Adds new request flag to administrator.

                return requestId;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Check if a request exists.
        /// </summary>
        /// <param name="requestId">Request id.</param>
        /// <returns>Returns whether request exists or not.</returns>
        private static bool IsExist(int requestId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TC_REQUESTs.Where(r => r.Message_Id == requestId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Change specific request's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="requestId">Request id.</param>
        /// <param name="statusId">New status id.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int requestId, int statusId)
        {
            // Get request.
            TC_REQUEST request = new TC_REQUEST();
            request = GetRequest(requestId);

            // Check if request exists.
            if (request == null)
            {
                // TO DO: Handle request not exists
            }
            else
            {
                // Change status.
                request.Status_Id = statusId;
            }
        }

        #endregion Private Methods
    }
}
