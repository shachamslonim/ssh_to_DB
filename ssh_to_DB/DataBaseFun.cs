using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssh_to_DB
{
    class DataBaseFun
    {

        public static bool drop_table(String table_name)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            //String tableName = table_name;
            String dropTableSQL = "drop table " + table_name;
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand dbCmd = new OleDbCommand();
            try
            {
                conn.Open();


                dbCmd.Connection = conn;
                dbCmd.CommandText = dropTableSQL;
                dbCmd.ExecuteNonQuery();
                dbCmd.Connection.Close();


                return true;
            }
            catch (OleDbException exp)
            { MessageBox.Show("Database Error:" + exp.Message.ToString()); }
            finally { if (conn.State == ConnectionState.Open) { conn.Close(); } }
            return false;
        }

        public static bool DELETE_table(String table_name)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            //String tableName = table_name;
            String dropTableSQL = "delete from " + table_name;
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand dbCmd = new OleDbCommand();
            try
            {
                conn.Open();


                dbCmd.Connection = conn;
                dbCmd.CommandText = dropTableSQL;
                dbCmd.ExecuteNonQuery();
                dbCmd.Connection.Close();


                return true;
            }
            catch (OleDbException exp)
            { MessageBox.Show("Database Error:" + exp.Message.ToString()); }
            finally { if (conn.State == ConnectionState.Open) { conn.Close(); } }
            return false;
        }



        public static List<string> select_list_GroupFun(string select_command)
        {
            List<string> retval = new List<string>();
            try
            {
                string query = select_command;
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";
              

                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();
               // string dTable

                DataSet ds = new DataSet();
                //fill the DataTable
                dAdapter.Fill(dTable);

                ///    dAdapter.Fill(dataTable);
                
                        foreach(DataRow dr in dTable.Rows)
                            retval.Add( (string)dr[0] );

                        return retval;



            }
            catch (Exception ex)
            {


                SshExeTest.WriteLogFile("1", "GroupFun() need to return 1 valus", "Error: selet . \n{0}" + ex.Message);
                return retval;

            }


        }

        public static List<int> select_list_int_GroupFun(string select_command, int number_Columns)
        {
            // work only on wen the select get ally one rows
            List<int> retval = new List<int>();
            try
            {
                string query = select_command;
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";


                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();
                // string dTable

                DataSet ds = new DataSet();
                //fill the DataTable
                dAdapter.Fill(dTable);
        
                ///    dAdapter.Fill(dataTable);             
                foreach (DataRow dr in dTable.Rows)
                {
                    for (int i = 0; i < number_Columns; i++)
                    retval.Add((int)dr[i]);
                }
                return retval;



            }
            catch (Exception ex)
            {


                SshExeTest.WriteLogFile("1", "GroupFun() need to return 1 valus", "Error: selet . \n{0}" + ex.Message);
                return retval;

            }


        }
        public static DateTime selectGroupFun(string select_command)
        {
            try
            {
                string query = select_command;
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";
                if (select_command == "MAX")
                    query = "SELECT MAX(date) FROM Activity where LOG <> \"rreasons.log\"";
                if (select_command == "MIN")
                    query = "SELECT MIN(date) FROM Activity where LOG <> \"rreasons.log\"";

                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();


                DataSet ds = new DataSet();
                //fill the DataTable
                dAdapter.Fill(dTable);

                ///    dAdapter.Fill(dataTable);
                /// for function current if not have date collect one day pass
                if (dTable.Rows[0].ItemArray[0]== DBNull.Value)
                    return Convert.ToDateTime(DateTime.Today.AddDays(-1));
                return Convert.ToDateTime(dTable.Rows[0].ItemArray[0]);



            }
            catch (Exception ex)
            {


                SshExeTest.WriteLogFile("1", "GroupFun() need to return 1 valus", "Error: selet . \n{0}" + ex.Message);
                return DateTime.ParseExact("0000-01-01 00:00:00", "yyyy-MM-dd HH:mm:tt", null);

            }






        }


        public static bool create_table(String table_name)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            String tableName = table_name;
            String createSQL = "CREATE TABLE " + tableName + "([date] DateTime, [command] text,[LOG] text, [RPA] text)";
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand dbCmd = new OleDbCommand();

            try
            {
                conn.Open();



             //   MessageBox.Show(createSQL);
                dbCmd.Connection = conn;
                dbCmd.CommandText = createSQL;
                dbCmd.ExecuteNonQuery();
                dbCmd.Connection.Close();


                return true;
            }
            catch (OleDbException exp) { MessageBox.Show("Database Error:" + exp.Message.ToString()); }
            finally { if (conn.State == ConnectionState.Open) { conn.Close(); } }
            return false;
        }
        public static bool insert_table(String tableName, String IP, String Type ,int Hours , int Minutes, int Seconds)
        {


            string connectionStringInsert = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            //String tableName = table_name;
            // String createSQL = "CREATE TABLE " + tableName + "([date] DateTime,[MyIdentityColumn] long, [Name] text)";
            OleDbConnection conn = new OleDbConnection(connectionStringInsert);
            OleDbCommand dbCmd = new OleDbCommand();

            try
            {


                string insertTable = "INSERT INTO " + tableName
                        + " ([IP],[Type], [Hours], [Minutes], [Seconds]) "
                        + "VALUES (@IP, @Type, @Hours, @Minutes, @Seconds )";

                OleDbCommand command = new OleDbCommand(insertTable, conn);



                command.Parameters.Add("@IP", OleDbType.VarWChar, 40).Value = IP;
                command.Parameters.Add("@Type", OleDbType.VarWChar, 40).Value = Type;
                command.Parameters.Add("@Hours", OleDbType.Integer, 40).Value = Hours;
                command.Parameters.Add("@Minutes", OleDbType.Integer, 40).Value = Minutes;
                command.Parameters.Add("@Seconds", OleDbType.Integer, 40).Value = Seconds;


                //OleDbType.Date;

                conn.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;

            }
            catch (OleDbException exp)
            {
                MessageBox.Show("Database Error:" + exp.Message.ToString());
                return false;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }

            }

        }

        public static bool insert_table(String tableName, DateTime timeActivity, String CommandActivity, String CommandFile, String CommandRPA)
        {


            string connectionStringInsert = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            //String tableName = table_name;
            // String createSQL = "CREATE TABLE " + tableName + "([date] DateTime,[MyIdentityColumn] long, [Name] text)";
            OleDbConnection conn = new OleDbConnection(connectionStringInsert);
            OleDbCommand dbCmd = new OleDbCommand();

            try
            {


                string insertTable = "INSERT INTO " + tableName
                        + " ([date], [command],[LOG],[RPA]) "
                        + "VALUES (@date, @command, @LOG , @RPA )";

                OleDbCommand command = new OleDbCommand(insertTable, conn);


                command.Parameters.Add("@date", OleDbType.Date, 40).Value = timeActivity;
                command.Parameters.Add("@command", OleDbType.VarWChar, 300).Value = CommandActivity;
                command.Parameters.Add("@LOG", OleDbType.VarWChar, 40).Value = CommandFile;
                command.Parameters.Add("@RPA", OleDbType.VarWChar, 40).Value = CommandRPA;



                conn.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;

            }
            catch (OleDbException exp)
            {
                MessageBox.Show("Database Error:" + exp.Message.ToString());
                return false;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }

            }

        }

        public static void insert_table(DataTable insetrt_Table)
        {


            string connectionStringInsert = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";
            OleDbConnection conn = new OleDbConnection(connectionStringInsert);
            OleDbCommand dbCmd = new OleDbCommand();




            //String tableName = table_name;
            // String createSQL = "CREATE TABLE " + tableName + "([date] DateTime,[MyIdentityColumn] long, [Name] text)";
  

            try
            { 
                conn.Open();
                for (int i = 0; i < insetrt_Table.Rows.Count; i++)
                {
                   
                    string insertTable = "INSERT INTO " + insetrt_Table.Rows[i].ItemArray[0]
                            + " ([date], [command],[LOG],[RPA]) "
                            + "VALUES (@date, @command, @LOG , @RPA )";

                    OleDbCommand command = new OleDbCommand(insertTable, conn);


                    command.Parameters.Add("@date", OleDbType.Date, 40).Value = insetrt_Table.Rows[i].ItemArray[1];
                    command.Parameters.Add("@command", OleDbType.VarWChar, 300).Value = insetrt_Table.Rows[i].ItemArray[2];
                    command.Parameters.Add("@LOG", OleDbType.VarWChar, 40).Value = insetrt_Table.Rows[i].ItemArray[3];
                    command.Parameters.Add("@RPA", OleDbType.VarWChar, 40).Value = insetrt_Table.Rows[i].ItemArray[4];




                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }  

            }
                 
            catch (OleDbException exp)
            {
                MessageBox.Show("Database Error:" + exp.Message.ToString());
               
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }

            }

        }

        public static bool insert_table(string tableName,  string name_command ,string sql_qeary )
        {


            string connectionStringInsert = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            //String tableName = table_name;
            // String createSQL = "CREATE TABLE " + tableName + "([date] DateTime,[MyIdentityColumn] long, [Name] text)";
            OleDbConnection conn = new OleDbConnection(connectionStringInsert);
            OleDbCommand dbCmd = new OleDbCommand();

            try
            {


                string insertTable = "INSERT INTO " + tableName
                        + " ([Name],[Query]) "
                        + "VALUES (@Name, @Query )";

                OleDbCommand command = new OleDbCommand(insertTable, conn);

                command.Parameters.Add("@Name", OleDbType.VarWChar, 60).Value = name_command;
                command.Parameters.Add("@Query", OleDbType.VarWChar, 300).Value = sql_qeary;
 



                conn.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;

            }
            catch (OleDbException exp)
            {
                MessageBox.Show("Database Error:" + exp.Message.ToString());
                return false;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }

            }

        }

        public static string select_DB(String table, String select_command)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";

            DataSet myDataSet = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(connectionString);

            }
            catch (Exception ex)
            {
                SshExeTest.WriteLogFile("1", "button1_Click()", "Error: Failed to create a database connection. \n{0}" + ex.Message);
                return "Failed_create_DB_Connection";

            }


            string sql = "SELECT * FROM switch";

            try
            {

                OleDbCommand myAccessCommand = new OleDbCommand(sql, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                myAccessConn.Open();
                myDataAdapter.Fill(myDataSet, "switch");

            }
            catch (Exception ex)
            {

                SshExeTest.WriteLogFile("1", "button1_Click()", "Error: Failed to retrieve the required data from the DB.\n{0}" + ex.Message);
                return "Failed_to_retrieve_data";
            }
            finally
            {
                myAccessConn.Close();
            }
            DataColumnCollection drc = myDataSet.Tables["switch"].Columns;
            DataRowCollection dra = myDataSet.Tables["switch"].Rows;


            foreach (DataRow dr in dra)
            {

                SshExeTest.WriteLogFile("1", "button1_Click()", "switch " + dr[0] + " is " + dr[1] + " is " + dr[2]);

            }

            return "select_complit";

        }






        public static DataTable select_DB(DateTime minVolue, DateTime maxVolue)
        {
            try
            {

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";

                string query = "SELECT * FROM Activity Where (date BETWEEN #"+ minVolue + "# AND #" + maxVolue + "#) order by date";

                //create an OleDbDataAdapter to execute the query
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

                //create a command builder
                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();


                DataSet ds = new DataSet();


                dAdapter.Fill(dTable);


                return dTable;
                // for (int i = 0; i < dTable.Rows.Count; i++)
                //{
                //     dataGridView1.Rows.Add(dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3]);
                //  }


            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return notSend;

            }


        }
        public static bool update_sql(string tableName,string Names , string quary)
        {
            try
            {

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";

                string query = "UPDATE [" + tableName + @"] SET [Query] = """+quary+@""" WHERE Name = '" + Names + "'";

                OleDbConnection conn = new OleDbConnection(connString);
                OleDbCommand updateCmd = new OleDbCommand();
            
                //the 
                updateCmd.CommandText = query;
                updateCmd.Parameters.AddWithValue("@Query", quary);
                updateCmd.Parameters.AddWithValue("@Name", Names);
                updateCmd.Connection = conn;
                conn.Open();
                updateCmd.ExecuteNonQuery();
      
                conn.Close();


                return true;

            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return false;

            }


        }

        public static bool update_sql(string tableName,string coloum_update, string Names, string quary)
        {
            try
            {

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";

                string query = "UPDATE [" + tableName + @"] SET [" + coloum_update +"] = " + quary + @""" WHERE Name = '" + Names + "'";

                OleDbConnection conn = new OleDbConnection(connString);
                OleDbCommand updateCmd = new OleDbCommand();

                //the 
                updateCmd.CommandText = query;
                updateCmd.Parameters.AddWithValue("@Query", quary);
                updateCmd.Parameters.AddWithValue("@Name", Names);
                updateCmd.Connection = conn;
                conn.Open();
                updateCmd.ExecuteNonQuery();

                conn.Close();


                return true;

            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return false;

            }


        }

        public static bool update_sql_bool(string tableName, string coloum_update, Boolean YesNo ,string id)
        {
            try
            {

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";

                string query = "UPDATE [" + tableName + @"] SET [" + coloum_update + "] = " + YesNo  + @" WHERE id = " + id + " ;";

                OleDbConnection conn = new OleDbConnection(connString);
                OleDbCommand updateCmd = new OleDbCommand();

                //the 
                updateCmd.CommandText = query;
                updateCmd.Parameters.AddWithValue("@Query", query);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.Connection = conn;
                conn.Open();
                updateCmd.ExecuteNonQuery();

                conn.Close();


                return true;

            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return false;

            }


        }

        public static bool updateTime(long ChangeHours, long ChangeMinatis, long Changesecand, string tableName, string whatUpDate = "kate_tasks.log")
        {
            try
            {

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";

                string query = "UPDATE [" + tableName + "] SET [date] = DateAdd(@times, @ChangeHours1 ,[date]) WHERE LOG = '" + whatUpDate + "'";

                OleDbConnection conn = new OleDbConnection(connString);
                OleDbCommand updateCmdSecond = new OleDbCommand();
                OleDbCommand updateCmdMinute = new OleDbCommand();
                OleDbCommand updateCmd = new OleDbCommand();
                conn.Open();
                //update hours
                updateCmd.CommandText = query;
                updateCmd.Parameters.AddWithValue("@times", 'h');
                updateCmd.Parameters.AddWithValue("@ChangeHours1", ChangeHours);
                updateCmd.Connection = conn;
                updateCmd.ExecuteNonQuery();
                //update secund
                updateCmdSecond.CommandText = query;
                updateCmdSecond.Parameters.AddWithValue("@times", 's');
                updateCmdSecond.Parameters.AddWithValue("@ChangeHours1", Changesecand);
                updateCmdSecond.Connection = conn;
                updateCmdSecond.ExecuteNonQuery();
                //update minute
                updateCmdMinute.CommandText = query;
                updateCmdMinute.Parameters.AddWithValue("@times", 'n');
                updateCmdMinute.Parameters.AddWithValue("@ChangeHours1", ChangeMinatis);
                updateCmdMinute.Connection = conn;
                updateCmdMinute.ExecuteNonQuery();
                conn.Close();
                ;

                return true;
                // for (int i = 0; i < dTable.Rows.Count; i++)
                //{
                //     dataGridView1.Rows.Add(dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3]);
                //  }


            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return false;

            }


        }


        public static DataTable select_all(string query)
        {

            DataTable dTable = new DataTable();
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";
            OleDbConnection conn = new OleDbConnection(connString);

            // MySqlConnection conn = new OleDbConnection(connString);

            try
            {


                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);
                //create a command builder
                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);


                dAdapter.Fill(dTable);




                // for (int i = 0; i < dTable.Rows.Count; i++)
                //{
                //     dataGridView1.Rows.Add(dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3]);
                //  }


            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "Sellect_all", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return notSend;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dTable;

        }

        public static DataTable select_all(string TableGet, string where_string = "RPA", string type_collect = "Long_Time")
        {
 

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(connString);
                DataTable dTable = new DataTable();
                try
                {
                    
                string query ="";
                if (where_string == "RPA")
                    query = "SELECT * FROM [" + TableGet + "]" + " where Type ='" + type_collect + "'";
                else
                    query = "SELECT * FROM [" + TableGet + "]" + " where Type_splitter ='" + where_string + "'";

                SshExeTest.WriteLogFile("1", "the query = "+ query , "Error: selet . \n{0}" + query);
                //create an OleDbDataAdapter to execute the query
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);
                //create a command builder
                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
               


                DataSet ds = new DataSet();

                dAdapter.Fill(dTable);


               
                // for (int i = 0; i < dTable.Rows.Count; i++)
                //{
                //     dataGridView1.Rows.Add(dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3]);
                //  }


            }
            catch (Exception ex)
            {

                DataTable notSend = new DataTable();
                SshExeTest.WriteLogFile("1", "Sellect_all", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";
                return notSend;

            }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return dTable;

        }
    }

    
}
