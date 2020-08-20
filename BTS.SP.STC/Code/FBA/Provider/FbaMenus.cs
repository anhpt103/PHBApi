using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using Microsoft.SharePoint;
using System.Web.Security;
using Microsoft.SharePoint.Utilities;
using System.Linq;
using Microsoft.SharePoint.Administration.Claims;

namespace BTS.SP.STC.Code.FBA.Provider
{
    public class FbaMenus
    {
        public static string connString = ConfigurationManager.ConnectionStrings["FBADB"].ToString();
        public static string AppName = FbaMembers.BaseMembershipProvider().ApplicationName;
        public static DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "select * from aspnet_Menus where ApplicationName='" + AppName + "'";

                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetOne(string praMenuId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "select * from aspnet_Menus where MenuId='" + praMenuId + "' and ApplicationName='" + AppName + "'";
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    conn.Close();
                    da.Dispose();
                }
                catch (SqlException sqlEx)
                {
                }
                finally
                {
                    conn.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void UpdateOne(string MenuIdParam, string MenuIdParentParam, string MenuNameParam, string PatheParam, string DescriptionParam, string StatusParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" UPDATE aspnet_Menus SET MenuIdParent = @MenuIdParentParam, MenuName = @MenuNameParam, Description = @DescriptionParam, Status = @StatusParam WHERE MenuId = '" + MenuIdParam + "'", conn);
           
            #region Parameters
            /* Parameters */
            sqlCmd.Parameters.Add("@MenuIdParentParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@MenuIdParentParam"].Value = MenuIdParentParam;

            sqlCmd.Parameters.Add("@MenuNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@MenuNameParam"].Value = MenuNameParam;

            sqlCmd.Parameters.Add("@DescriptionParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@DescriptionParam"].Value = DescriptionParam;

            sqlCmd.Parameters.Add("@StatusParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@StatusParam"].Value = StatusParam;
            #endregion

            #region Try/Catch/Finally
            /* Try/Catch/Finally */
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
            }
            finally
            {
                conn.Close();
            }
            #endregion
        }
        public static void CreateOne(string MenuIdParam, string MenuIdParentParam, string MenuNameParam,string PatheParam, string DescriptionParam, string StatusParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" INSERT INTO aspnet_Menus(ApplicationName,MenuId,MenuIdParent,MenuName,Path,Description,Status) VALUES(@ApplicationNameParam,@MenuIdParam,@MenuIdParentParam,@MenuNameParam,@PatheParam, @DescriptionParam, @StatusParam)", conn);

            #region Parameters
            /* Parameters */
            sqlCmd.Parameters.Add("@ApplicationNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@ApplicationNameParam"].Value = AppName;

            sqlCmd.Parameters.Add("@MenuIdParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@MenuIdParam"].Value = MenuIdParam;

            sqlCmd.Parameters.Add("@MenuIdParentParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@MenuIdParentParam"].Value = MenuIdParentParam;

            sqlCmd.Parameters.Add("@MenuNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@MenuNameParam"].Value = MenuNameParam;

            sqlCmd.Parameters.Add("@PatheParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@PatheParam"].Value = PatheParam;

            sqlCmd.Parameters.Add("@DescriptionParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@DescriptionParam"].Value = DescriptionParam;

            sqlCmd.Parameters.Add("@StatusParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@StatusParam"].Value = StatusParam;
            #endregion

            #region Try/Catch/Finally
            /* Try/Catch/Finally */
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
            }
            finally
            {
                conn.Close();
            }
            #endregion
        }
        public static void DeleteOne(string MenuIdParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" DELETE aspnet_Menus WHERE MenuId = '" + MenuIdParam + "' AND ApplicationName='" + AppName + "'", conn);

            #region Try/Catch/Finally
            /* Try/Catch/Finally */
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
            }
            finally
            {
                conn.Close();
            }
            #endregion
        }
    }
}
