using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssh_to_DB
{
    public partial class Form_filter : Form
    {
        public Form_filter()
        {
            InitializeComponent();
            dataGridView1.Rows[0].Cells[3].Value = "ADD";

            foreach (string goInto in DataBaseFun.select_list_GroupFun("SELECT Name FROM Filter_Query ")) 
            {
         
                comboBox_query_list.Items.Insert(0, goInto); 
            }
           // dataGridView1.Rows.Add("", "", "", "ADD") ;
        }

        private void Form_filter_Load(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("", "", "", "ADD");
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            show_query();
         

        }


   private string Create_string(string stringBefor , string OrAnd ,string coloums )
        {



            stringBefor = stringBefor + " " + OrAnd + " '%" + coloums + "%'";

       return stringBefor;

        }

   private void button_close_Click(object sender, EventArgs e)
   {
        int resultIndex = -1;
        resultIndex = comboBox_query_list.FindStringExact("last_Query",
                resultIndex);
       if (resultIndex != -1)
       {
       DataBaseFun.update_sql("Filter_Query", "last_Query", text_sql.Text);
           }
       else
       {
       
           DataBaseFun.insert_table("Filter_Query", "last_Query", text_sql.Text);
       }
       this.Close();
   }

   private void button_Save_Click(object sender, EventArgs e)
   {
       int resultIndex = -1; 
       resultIndex = comboBox_query_list.FindStringExact(comboBox_query_list.Text,
                resultIndex);
       if (resultIndex == -1)
       {
           DataBaseFun.insert_table("Filter_Query", comboBox_query_list.Text, text_sql.Text);
           comboBox_query_list.Items.Clear();
           foreach (string goInto in DataBaseFun.select_list_GroupFun("SELECT name FROM Filter_Query "))
           {

               comboBox_query_list.Items.Insert(0, goInto);
           }
           MessageBox.Show("The Query save ,with Name " + comboBox_query_list.Text);
       }
       else
       {
           DataBaseFun.update_sql("Filter_Query", comboBox_query_list.Text, text_sql.Text);
           MessageBox.Show("The Query update, with Name " + comboBox_query_list.Text);

         }
      
   }

   private void comboBox_query_list_SelectedIndexChanged(object sender, EventArgs e)
   {
       		ComboBox comboBox = (ComboBox) sender;

		// Save the selected employee's name, because we will remove 
		// the employee's name from the list. 
            string selectedName = (string)comboBox_query_list.SelectedItem;



        foreach (string goInto in DataBaseFun.select_list_GroupFun("SELECT Query FROM Filter_Query where name ='" + selectedName + "'"))
        {
            if (goInto !="")
             text_sql.Text= goInto;
        }


   }

        private void show_query()
   {
       string log = "";
       string commend = "";
       string RPA = "";
       string OR_ADD = "";
       string where_string = "";


       for (int row = 0; row < (dataGridView1.RowCount - 1); row++)
       {
           int check_first_line = 0;
           // read the first row . 

           for (int col = 0; col < dataGridView1.ColumnCount; col++)
           {
               if (dataGridView1.Rows[row].Cells[col].Value != null)
               {
                   switch (col)
                   {
                       case 0:
                           log = dataGridView1.Rows[row].Cells[col].Value.ToString();

                           break;
                       case 1:
                           commend = dataGridView1.Rows[row].Cells[col].Value.ToString();

                           break;
                       case 2:
                           RPA = dataGridView1.Rows[row].Cells[col].Value.ToString();

                           break;
                       case 3:
                           OR_ADD = dataGridView1.Rows[row].Cells[col].Value.ToString();
                           break;
                       default:
                           Console.WriteLine("Default case");
                           break;


                   }
                   // bouild the first line 






               }


           }
           /* if (countColumnUse != 6)
                saveOrNot = false;
            lines += Environment.NewLine;
            countColumnUse = 0;*/

           // bouild the first line 
           if (where_string == "")
           {
               if (log != "")
               {
                   where_string = where_string + "LOG = " + "'" + log + "'";
                   check_first_line = 1;

               }
               else
               {
                   if (commend != "")
                   {
                       where_string = where_string + "command like " + "'%" + commend + "%'";
                       check_first_line = 2;
                   }
                   else
                   {
                       if (RPA != "")
                       {
                           where_string = where_string + "RPA like " + "'%" + RPA + "%'";
                           check_first_line = 3;

                       }
                       else
                       {
                           check_first_line = 4;
                       }

                   }
               }


           }
           switch (check_first_line)
           {
               case 0:
                   if (OR_ADD == "OR")
                   {
                       if (log != "")
                       {
                           where_string = Create_string(where_string, "OR LOG =", log);
                       }
                       if (commend != "")
                       {
                           where_string = Create_string(where_string, "OR command like", commend);

                       }
                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "OR RPA like", RPA);
                       }
                   }
                   else
                   {
                       if (log != "")
                       {
                           where_string = Create_string(where_string, "AND LOG =", log);
                       }
                       if (commend != "")
                       {
                           where_string = Create_string(where_string, "AND command like", commend);

                       }
                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "AND RPA like", RPA);
                       }

                   }

                   break;
               case 1:

                   if (OR_ADD == "OR")
                   {

                       if (commend != "")
                       {
                           where_string = Create_string(where_string, "OR command like", commend);

                       }
                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "OR RPA like", RPA);
                       }
                   }
                   else
                   {

                       if (commend != "")
                       {
                           where_string = Create_string(where_string, "AND command like", commend);

                       }
                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "AND RPA like", RPA);
                       }

                   }

                   break;
               case 2:
                   if (OR_ADD == "OR")
                   {

                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "OR RPA like", RPA);
                       }
                   }
                   else
                   {


                       if (RPA != "")
                       {
                           where_string = Create_string(where_string, "AND RPA like", RPA);
                       }

                   }
                   break;
               case 3:

                   break;
               case 4:

                   break;

           }

       }

       text_sql.Text = "select * from Activity where " + where_string;
   }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            show_query();
        }



    }
}
