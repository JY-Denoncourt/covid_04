﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace wpf_demo_phonebook
{
    public class DbConnection
    {
        //----------------------------------------------------------------------------------Vatriables


        public SqlDataAdapter DataAdapter { get; set; } = new SqlDataAdapter();
        public static SqlConnection Connection { get; set; }


        //---------------------------------------------------------------------------------Constructeurs


        public DbConnection()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }


        //---------------------------------------------------------------------------------Fonctions

        #region -->Fonctions
        private SqlConnection open()
        {
            if (Connection.State == ConnectionState.Closed || 
                Connection.State == ConnectionState.Broken)
            {
                Connection.Open();
            }

            return Connection;
        }


        private void writeError(string _message)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            MethodBase methodBase = stackFrame.GetMethod();

            var msg = $"Error - {methodBase.Name} - {_message}";
            Console.WriteLine(msg);
            Debug.WriteLine (msg);
        }


        public DataTable ExecuteSelectQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable = null;
            DataSet ds = new DataSet();

            try
            {
                command.Connection = open();
                command.CommandText = _query;

                //******************************************
                //Modif test JYD le if en surplus
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                //*******************************************


                command.ExecuteNonQuery();
                DataAdapter.SelectCommand = command;
                DataAdapter.Fill(ds);
                dataTable = ds.Tables[0];

            } catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");    
            }
            finally
            {
                Connection.Close();
            }

            return dataTable;
        }


        public int ExecuteInsertQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            int result = -1;

            try
            {
                command.Connection = open();
                command.CommandText = _query;

                //******************************************
                //Modif test JYD le if en surplus
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                //*******************************************

                DataAdapter.InsertCommand = command;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }


        public int ExecuteUpdateQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            int result = 0;

            try
            {
                command.Connection = open();
                command.CommandText = _query;
                command.Parameters.AddRange(parameters);
                DataAdapter.UpdateCommand = command;
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }
        
        
        public int ExecuteDeleteQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            int result = 0;

            try
            {
                command.Connection = open();
                command.CommandText = _query;
                command.Parameters.AddRange(parameters);
                DataAdapter.DeleteCommand = command;
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }
       
        #endregion

    }
}
