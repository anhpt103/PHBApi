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
    public class FBAUnit
    {
        public static string connString = ConfigurationManager.ConnectionStrings["FBADB"].ToString();
        public static string AppName = FbaMembers.BaseMembershipProvider().ApplicationName;
        public static DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "select * from aspnet_Unit where ApplicationName='" + AppName + "'";

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
        public static DataTable GetOne(string praUnitId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "select * from aspnet_Unit where UnitId='" + praUnitId + "' and ApplicationName='" + AppName + "'";
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

        public static void UpdateOne(string UnitIdParam, string UnitIdParentParam, string UnitNameParam, string PhoneParam, string EmailParam, string AddressParam, string DescriptionParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" UPDATE aspnet_Unit SET UnitIdParent = @UnitIdParentParam, UnitName = @UnitNameParam,Phone=@PhoneParam,Email=@EmailParam,Address=@AddressParam, Description = @DescriptionParam WHERE UnitId = '" + UnitIdParam + "'", conn);

            #region Parameters
            /* Parameters */

            sqlCmd.Parameters.Add("@UnitIdParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitIdParam"].Value = UnitIdParam;

            sqlCmd.Parameters.Add("@UnitIdParentParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitIdParentParam"].Value = UnitIdParentParam;

            sqlCmd.Parameters.Add("@UnitNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitNameParam"].Value = UnitNameParam;

            sqlCmd.Parameters.Add("@PhoneParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@PhoneParam"].Value = PhoneParam;

            sqlCmd.Parameters.Add("@EmailParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@EmailParam"].Value = EmailParam;

            sqlCmd.Parameters.Add("@AddressParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@AddressParam"].Value = AddressParam;

            sqlCmd.Parameters.Add("@DescriptionParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@DescriptionParam"].Value = DescriptionParam;
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
        public static void CreateOne(string UnitIdParam, string UnitIdParentParam, string UnitNameParam, string PhoneParam, string EmailParam, string AddressParam, string DescriptionParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" INSERT INTO aspnet_Unit(ApplicationName,UnitId,UnitIdParent,UnitName,Phone,Email,Address,Description) VALUES(@ApplicationNameParam,@UnitIdParam,@UnitIdParentParam,@UnitNameParam,@PhoneParam,@EmailParam,@AddressParam, @DescriptionParam)", conn);

            #region Parameters
            /* Parameters */
            sqlCmd.Parameters.Add("@ApplicationNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@ApplicationNameParam"].Value = AppName;

            sqlCmd.Parameters.Add("@UnitIdParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitIdParam"].Value = UnitIdParam;

            sqlCmd.Parameters.Add("@UnitIdParentParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitIdParentParam"].Value = UnitIdParentParam;

            sqlCmd.Parameters.Add("@UnitNameParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@UnitNameParam"].Value = UnitNameParam;

            sqlCmd.Parameters.Add("@PhoneParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@PhoneParam"].Value = PhoneParam;

            sqlCmd.Parameters.Add("@EmailParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@EmailParam"].Value = EmailParam;

            sqlCmd.Parameters.Add("@AddressParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@AddressParam"].Value = AddressParam;

            sqlCmd.Parameters.Add("@DescriptionParam", SqlDbType.NVarChar);
            sqlCmd.Parameters["@DescriptionParam"].Value = DescriptionParam;

           
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
        public static void DeleteOne(string UnitIdParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(" DELETE aspnet_Unit WHERE UnitId = '" + UnitIdParam + "' AND ApplicationName='" + AppName + "'", conn);

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
