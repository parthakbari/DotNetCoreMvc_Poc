using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HegicMvc_Poc.Controllers
{
    public class OracleConnectionHelper
    {

        /// <summary>
        /// Gets Portal master database connection string.
        /// </summary>
        public static string ConnectionStringMaster
        {
            get
            {
                return ConfigHelper.AppSetting("PORTAL_MASTER");
            }
        }


        /// <summary>
        /// Helps Closing Connection and Transaction
        /// </summary>
        /// <param name="con"></param>
        /// <param name="trans"></param>
        public static void CloseConnection(IDbConnection con, IDbTransaction trans = null)
        {
            if (con != null)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                if (con.State == ConnectionState.Open)
                {
                    try
                    {
                        con.Close();
                    }
                    catch (OracleException) { }
                    catch (SocketException) { }

                }
            }
        }
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static OracleParameter CreateParam(object paramValue, string paramName, OracleDbType type, ParameterDirection direction, int size)
        {
            using (OracleParameter param = new OracleParameter((paramName ?? string.Empty).Trim(), type))
            {
                param.Direction = direction;
                if (size > 0)
                {
                    param.Size = size;
                }

                if (direction == ParameterDirection.Input || direction == ParameterDirection.InputOutput)
                {
                    if (type == OracleDbType.Int32)
                    {
                        if (paramValue != null)
                        {
                            int i = 0;
                            bool success = int.TryParse(Convert.ToString(paramValue), out i);
                            if (!success)
                            {
                                i = 0;
                            }
                            param.Value = i;
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (type == OracleDbType.Int64)
                    {
                        if (paramValue != null)
                        {
                            long l = 0;
                            bool success = long.TryParse(Convert.ToString(paramValue), out l);
                            if (!success)
                            {
                                l = 0;
                            }
                            param.Value = l;
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (type == OracleDbType.Decimal)
                    {
                        if (paramValue != null)
                        {
                            decimal d = 0M;
                            bool success = decimal.TryParse(Convert.ToString(paramValue), out d);
                            if (!success)
                            {
                                d = 0M;
                            }
                            param.Value = d;
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (type == OracleDbType.Varchar2)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(paramValue)))
                        {
                            param.Value = Convert.ToString(paramValue).Trim();
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (type == OracleDbType.Date)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(paramValue)))
                        {
                            param.Value = paramValue;
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (type == OracleDbType.TimeStamp)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(paramValue)))
                        {
                            param.Value = paramValue;
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        param.Value = (paramValue != null ? paramValue : DBNull.Value);
                    }
                }

                return param;
            }
        }

        /// <summary>
        /// Creates System.Data.OracleClient.OracleParameter
        /// </summary>
        /// <param name="paramValue"></param>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static OracleParameter CreateParam(object paramValue, string paramName, OracleDbType type, ParameterDirection direction)
        {
            return CreateParam(paramValue, paramName, type, direction, 0);
        }

        /// <summary>
        /// Creates System.Data.OracleClient.OracleParameter
        /// </summary>
        /// <param name="paramValue"></param>
        /// <param name="paramName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static OracleParameter CreateParam(object paramValue, string paramName, ParameterDirection direction)
        {
            return CreateParam(paramValue, paramName, GetOracleType(paramValue), direction, 0);
        }

        /// <summary>
        /// Creates System.Data.OracleClient.OracleParameter of input direction
        /// </summary>
        /// <param name="paramValue"></param>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static OracleParameter CreateInParam(object paramValue, string paramName, OracleDbType type)
        {
            return CreateParam(paramValue, paramName, type, ParameterDirection.Input, 0);
        }

        /// <summary>
        /// Creates System.Data.OracleClient.OracleParameter of input direction
        /// </summary>
        /// <param name="paramValue"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static OracleParameter CreateInParam(object paramValue, string paramName)
        {
            return CreateParam(paramValue, paramName, GetOracleType(paramValue), ParameterDirection.Input, 0);
        }

        /// <summary>
        /// Gets equivalent OracleType of CLR type
        /// </summary>
        /// <param name="val">Parameter value</param>
        /// <returns></returns>
        private static OracleDbType GetOracleType(object val)
        {
            OracleDbType otype = OracleDbType.Varchar2;

            string type = string.Empty;

            if (val != null)
            {
                type = val.GetType().Name;
            }
            else
            {
                type = "String";
            }

            switch (type)
            {
                case "Int32":
                    otype = OracleDbType.Int32;
                    break;
                case "Int64":
                    otype = OracleDbType.Int64;
                    break;
                case "Decimal":
                    otype = OracleDbType.Decimal;
                    break;
                case "DateTime":
                    otype = OracleDbType.Date;
                    break;
                case "String":
                default:
                    otype = OracleDbType.Varchar2;
                    break;
            }

            return otype;
        }
    }
}
