using HegicMvc_Poc.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace HegicMvc_Poc.Controllers
{
    public class VehicleRepositry
    {
        /// <summary>
        /// Get Make List 
        /// </summary>
        /// <returns></returns>
        public List<NameValueData> GetMakeList()
        {
            List<NameValueData> makeDataList = new List<NameValueData>();
            using (OracleConnection con = new OracleConnection(OracleConnectionHelper.ConnectionStringMaster))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TW_GET_MAKE_LIST";
                    cmd.BindByName = true;
                    cmd.Parameters.Add(OracleConnectionHelper.CreateParam(null, "REF_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NameValueData makedetail = new NameValueData();
                            makedetail.Name = reader.GetString(reader.GetOrdinal("NAME"));
                            makedetail.Value = reader.GetInt32(reader.GetOrdinal("VALUE"));
                            makedetail.OriginalName = reader.GetString(reader.GetOrdinal("MakeName"));
                            makeDataList.Add(makedetail);
                        }
                    }
                }

            }
            return makeDataList;
        }

        /// <summary>
        /// Get Model Variant Detail.
        /// </summary>
        /// <returns></returns>
        public List<ModelVariant> GetModelVariantList()
        {
            List<ModelVariant> modelDataList = new List<ModelVariant>();
            using (OracleConnection con = new OracleConnection(OracleConnectionHelper.ConnectionStringMaster))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TW_GET_MODEL_VARIANT_LIST";
                    cmd.BindByName = true;
                    cmd.Parameters.Add(OracleConnectionHelper.CreateParam(0, "P_MODEL_VARIANT", OracleDbType.Int32, ParameterDirection.Input));
                    cmd.Parameters.Add(OracleConnectionHelper.CreateParam(null, "REF_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelVariant modelDetail = new ModelVariant();
                            modelDetail.MakeId = reader.GetInt32(reader.GetOrdinal("MAKE_ID"));
                            modelDetail.ModelId = reader.GetInt32(reader.GetOrdinal("MODEL_ID"));
                            modelDetail.ModelName = reader.GetString(reader.GetOrdinal("MODEL_NAME"));
                            modelDetail.VariantName = reader.GetString(reader.GetOrdinal("VARIANT_NAME"));
                            modelDetail.FuelType = reader.GetString(reader.GetOrdinal("FUEL_TYPE"));
                            modelDetail.ComprehensiveRestricted = reader.GetInt32(reader.GetOrdinal("COMPREHENSIVE_RESTRICTED"));
                            modelDetail.LiabilityRestricted = reader.GetInt32(reader.GetOrdinal("LIABILITY_RESTRICTED"));
                            modelDetail.ModelDisplaySequenceNo = reader.GetInt32(reader.GetOrdinal("MODEL_DISPLAY_SEQUENCE_NO"));
                            modelDetail.VariantDisplaySequenceNo = reader.GetInt32(reader.GetOrdinal("VARIANT_DISPLAY_SEQUENCE_NO"));
                            if (!reader.IsDBNull(reader.GetOrdinal("MODEL_IMAGE_URL")))
                                modelDetail.ModelImageUrl = reader.GetString(reader.GetOrdinal("MODEL_IMAGE_URL"));
                            if (!reader.IsDBNull(reader.GetOrdinal("SEATING_CAPACITY")))
                                modelDetail.SetingCapacity = reader.GetInt32(reader.GetOrdinal("SEATING_CAPACITY"));
                            if (!reader.IsDBNull(reader.GetOrdinal("FAST_LANE_ID")))
                                modelDetail.FastLaneId = reader.GetDecimal(reader.GetOrdinal("FAST_LANE_ID"));
                            if (!reader.IsDBNull(reader.GetOrdinal("IS_NEGATIVE_MODEL")))
                                modelDetail.IsNegativeModel = reader.GetInt16(reader.GetOrdinal("IS_NEGATIVE_MODEL"));
                            modelDataList.Add(modelDetail);
                        }
                    }
                }
            }
            return modelDataList;
        }
    }
}
