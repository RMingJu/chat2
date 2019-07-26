﻿using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat
{
    public class PubClass
    {
        public DataTable dtReturn { get; set; }


        public String GetConnectionString() {
            String strCnn = "";
            strCnn = String.Format("Server=tcp:mjserver.database.windows.net,1433;Initial Catalog=MingJuWebDB;Persist Security Info=False;User ID={0};Password={1};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                                    ,"asd831012","Zxc3769722");
            return strCnn;
        }

        public bool InsertData(String userName,String phoneNumber,String job,String jobIntroduction, byte[] img) {
            
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            String strSQL = "";
            strSQL += " INSERT INTO [dbo].[ChatRoomUser] ([userName],[phoneNumber],[job],[jobIntroduction],[img]) ";
            strSQL += " VALUES(@userName,@phoneNumber,@job,@jobIntroduction,@img) ";

            using (cnn)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cnn))
                {
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@job", SqlDbType.NVarChar).Value = job;
                    cmd.Parameters.Add("@jobIntroduction", SqlDbType.NVarChar).Value = jobIntroduction;
                    cmd.Parameters.Add("@img", SqlDbType.Image);
                    if (img == null)
                        cmd.Parameters["@img"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@img"].Value = img;

                    cnn.Open();
                    int cc = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cnn.Close();

                    if (cc > 0)
                        return true;
                    else
                        return false;
                }
            }
        }



        public DataTable GetRegistrationData(String userName)
        {
            DataTable dtRegistration = new DataTable();
            if (userName == null) return dtRegistration;

            SqlConnection cnn = new SqlConnection(GetConnectionString());
            String strSQL = "";
            strSQL += " SELECT TOP 1 * FROM [dbo].[ChatRoomUser] WHERE userName = @userName ORDER BY entryDate DESC ";

            using (cnn)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cnn))
                {
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;


                    cnn.Open();
                    SqlDataReader mydr = cmd.ExecuteReader();
                    if (mydr.HasRows)
                    {
                        dtRegistration = new DataTable();
                        dtRegistration.Load(mydr);
                    }
                    mydr.Close();
                    cnn.Close();

                }
            }
            return dtRegistration;

        }


        public Boolean IsLoginSuccessed(String phoneNumber)
        {

            bool bln = false;

            DataTable dtTemp = new DataTable();

            if (String.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            SqlConnection cnn = new SqlConnection(GetConnectionString());
            String strSQL = "";
            strSQL += " SELECT [userName],[phoneNumber],[job],[jobIntroduction],[entryDate],[imgPath] FROM [dbo].[ChatRoomUser] where phoneNumber = @phoneNumber ";

            using (cnn)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cnn))
                {
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;


                    cnn.Open();
                    SqlDataReader mydr = cmd.ExecuteReader();
                    if (mydr.HasRows)
                    {
                        dtTemp = new DataTable();
                        dtTemp.Load(mydr);
                    }
                    mydr.Close();
                    cnn.Close();
                }
            }

            if (dtTemp != null)
            {
                if (dtTemp.Rows.Count > 0)
                {
                    dtReturn = dtTemp;
                    bln = true;
                }
            }



            return bln;
        }


        public DataTable GetUserInfoByPhoneNumber(String phoneNumber){
            DataTable dtTemp = new DataTable();

            SqlConnection cnn = new SqlConnection(GetConnectionString());
            String strSQL = "";
            strSQL += " SELECT [userName],[phoneNumber],[job],[jobIntroduction],[entryDate],[imgPath] FROM [dbo].[ChatRoomUser] where phoneNumber = @phoneNumber ";

            using (cnn)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cnn))
                {
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;


                    cnn.Open();
                    SqlDataReader mydr = cmd.ExecuteReader();
                    if (mydr.HasRows)
                    {
                        dtTemp = new DataTable();
                        dtTemp.Load(mydr);
                    }
                    mydr.Close();
                    cnn.Close();
                }   //using (cmd)
            }   //using (cnn)
            return dtTemp;
        }



        public DataTable GetQuestionByID(int questionID)
        {
            DataTable dtTemp = new DataTable();

            SqlConnection cnn = new SqlConnection(GetConnectionString());
            String strSQL = "";
            strSQL += " SELECT * FROM [dbo].[Question] WHERE id = @id  ";

            using (cnn)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cnn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = questionID;

                    cnn.Open();
                    SqlDataReader mydr = cmd.ExecuteReader();
                    if (mydr.HasRows)
                    {
                        dtTemp = new DataTable();
                        dtTemp.Load(mydr);
                    }
                    mydr.Close();
                    cnn.Close();
                }   //using (cmd)
            }   //using (cnn)
            return dtTemp;
        }

        public String ConvertToJsonString(DataTable dtData) {
            String strJson = JsonConvert.SerializeObject(dtData, Formatting.Indented);
            return strJson;
        }

    }
}