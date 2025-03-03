using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.BLL
{    
    public static class AdminBLL
    {
        #region Public Methods

        /// <summary>
        /// Get admin by id.
        /// </summary>
        /// <param name="adminId">Admin's id to look for.</param>
        /// <returns>Requested admin.</returns>
        public static TM_ADMIN GetAdmin(int adminId)
        {
            // Check if admin exists.
            if (IsExist(adminId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return adminId.
                    return dataContext.TM_ADMINs.SingleOrDefault(a => a.Admin_Id == adminId);
                }
            }
            else
            {
                // Admin does not exist.
                return null;                
            }
        }

        /// <summary>
        /// Get admin by email.
        /// </summary>
        /// <param name="email">Admin's email to look for.</param>
        /// <returns>Requested admin.</returns>
        public static TM_ADMIN GetAdmin(string email)
        {
            // Check if admin exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return admin.
                    return dataContext.TM_ADMINs.SingleOrDefault(a => a.Email == email);
                }
            }
            else
            {
                // Admin does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get all admins list.
        /// </summary>
        /// <returns>Returns all admins list.</returns>
        public static List<TM_ADMIN> GetAdminList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TM_ADMINs.ToList<TM_ADMIN>();
            }
        }

        /// <summary>
        /// Get admin id by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Requested admin id.</returns>
        public static int GetAdminIdByEmail(string email)
        {
            // Check if admin exists.
            if (IsExist(email))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return admin id.
                    return dataContext.TM_ADMINs.SingleOrDefault(a => a.Email == email).Admin_Id;
                }
            }
            else
            {
                // TO DO: Handle admin not exists
                return 0;
            }
        }

        /// <summary>
        /// Check if admin already exists.
        /// </summary>
        /// <param name="adminId">Admin id.</param>
        /// <param name="email">Email.</param>
        /// <returns>Returns whether admin exists or not.</returns>
        public static bool IsDuplicateAdmin(int adminId, string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TM_ADMINs.Where(item =>
                                                    item.Admin_Id != adminId &&
                                                    item.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Add new admin.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="name">Name.</param>
        /// <returns>Returns admin id.</returns>
        public static int Add(string email, string password, string name)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create admin.
                TM_ADMIN admin = new TM_ADMIN();
                admin.Email = email;
                admin.Password = General.GenerateMD5(password);
                admin.Name = name;
                admin.Add_Date = DateTime.Today;

                // Add admin.
                dataContext.TM_ADMINs.InsertOnSubmit(admin);

                // Save changes.
                dataContext.SubmitChanges();

                // Return admin id.
                return admin.Admin_Id;
            }
        }

        /// <summary>
        /// Update admin.
        /// </summary>
        /// <param name="adminId">Admin id.</param>
        /// <param name="email">Email.</param>
        /// <param name="name">Name.</param>
        public static void Update(int adminId, string email, string name)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                TM_ADMIN admin = new TM_ADMIN();

                // Get admin details.
                admin = dataContext.TM_ADMINs.SingleOrDefault(item => item.Admin_Id == adminId);

                // Update admin details.
                admin.Email = email;
                admin.Name = name;

                // Save changes.
                dataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Delete admin.
        /// </summary>
        /// <param name="adminId">Admin's id for deletion.</param>
        public static void Delete(int adminId)
        {
            // Get admin.
            TM_ADMIN admin = GetAdmin(adminId);

            // Check if admin exists.
            if (admin != null)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Attach admin.
                    dataContext.TM_ADMINs.Attach(admin);

                    // Delete admin.
                    dataContext.TM_ADMINs.DeleteOnSubmit(admin);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
            else
            {
                // TODO: Handle admin not exists
            }
        }                        

        /// <summary>
        /// Check if password match to admin's password by email.
        /// </summary>
        /// <param name="email">Admin's email.</param>
        /// <param name="password">Password to check if matched.</param>
        /// <returns>Returns whether password match.</returns>
        public static bool CheckPassword(string email, string password)
        {
            // Check if admin exists.
            if (IsExist(email) == true)
            {
                // Encrypt password.
                password = General.GenerateMD5(password);

                // Checks if passoword match.
                if (GetPassword(email) == password)
                {
                    using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                    {
                        // TO DO: Write login to DB
                        return true;
                    }
                }
                else
                {
                    // Password does not match.
                    return false;
                }
            }
            else
            {
                // Admin does not exist.
                return false;
            }
        }

        /// <summary>
        /// Change admin's password.
        /// </summary>
        /// <param name="email">Admin's email.</param>
        /// <param name="password">New password.</param>
        /// <returns>Returns whether password changed.</returns>
        public static bool ChangePassword(string email, string password)
        {
            // Check if admin exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Encrypt password.
                    password = General.GenerateMD5(password);

                    // Change admin's password.
                    dataContext.TM_ADMINs.SingleOrDefault(a => a.Email == email).Password = password;

                    // Sava changes.
                    dataContext.SubmitChanges();

                    return true;
                }
            }
            else
            {
                // Admin does not exist.
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks if admin exist by id.
        /// </summary>
        /// <param name="adminId">Admin's id to look for.</param>
        /// <returns>Returns whether admin exists or not.</returns>
        private static bool IsExist(int adminId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TM_ADMINs.Where(a => a.Admin_Id == adminId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if admin exist by email.
        /// </summary>
        /// <param name="email">Admin's email to look for.</param>
        /// <returns>Returns whether admin exists or not.</returns>
        private static bool IsExist(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TM_ADMINs.Where(a => a.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Returns admin's password by email (encrypted from DB).
        /// </summary>
        /// <param name="email">Admin's email to look for.</param>
        /// <returns>Requested user's encrypted password.</returns>
        private static string GetPassword(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TM_ADMINs.Single(a => a.Email == email).Password;
            }
        }

        #endregion Private Methods       
    }
}
