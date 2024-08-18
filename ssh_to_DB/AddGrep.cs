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
    public partial class AddGrep : Form
    {
        public AddGrep()
        {
            InitializeComponent();
            Show_Tables("select * from add_Grep ");


        }
        private void Show_Tables(string query)
        {
            DataTable dTable = DataBaseFun.select_all(query);

            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if ((bool)dTable.Rows[i].ItemArray[4] == false)
                {
                    dataGridView1.Rows.Add(false, dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3], dTable.Rows[i].ItemArray[5]);
                }
                else
                {
                    dataGridView2.Rows.Add(false, dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3], dTable.Rows[i].ItemArray[5]);
                }
            }
        }

        private void Show_Tables_View1(string query)
        {
            DataTable dTable = DataBaseFun.select_all(query);

            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if ((bool)dTable.Rows[i].ItemArray[4] == false)
                {
                    dataGridView1.Rows.Add(false, dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3], dTable.Rows[i].ItemArray[5]);
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)     
        {
            if ((0 < dataGridView1.Rows.Count) && (0 < dataGridView1.Columns.Count))
            {
                for (int i = dataGridView1.Rows.Count - 1; -1 < i; i--)
                {
                    var row = dataGridView1.Rows[i];
                    if ((row.Cells[0].Value != null) && (row.Cells[0].Value != DBNull.Value))
                    {
                        bool isChecked = (bool)row.Cells[0].Value;
                        if (isChecked)
                        {
                     dataGridView2.Rows.Add(false, dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(),dataGridView1.Rows[i].Cells[3].Value.ToString(),dataGridView1.Rows[i].Cells[4].Value.ToString(),dataGridView1.Rows[i].Cells[5].Value.ToString());
                     ////////////////   DataBaseFun.update_sql();
                        DataBaseFun.update_sql_bool("add_Grep", "add", true, dataGridView1.Rows[i].Cells[5].Value.ToString());
                   

                            dataGridView1.Rows.Remove(row);
                        }
                    }
                }
            }




            
        }

        private void AddGrep_Load(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if ((0 < dataGridView2.Rows.Count) && (0 < dataGridView2.Columns.Count))
            {
                for (int i = dataGridView2.Rows.Count - 1; -1 < i; i--)
                {
                    var row = dataGridView2.Rows[i];
                    if ((row.Cells[0].Value != null) && (row.Cells[0].Value != DBNull.Value))
                    {
                        bool isChecked = (bool)row.Cells[0].Value;
                        if (isChecked)
                        {
                            dataGridView1.Rows.Add(false, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString());
                            ////////////////   DataBaseFun.update_sql();
                            DataBaseFun.update_sql_bool("add_Grep", "add", false, dataGridView2.Rows[i].Cells[5].Value.ToString());


                            dataGridView2.Rows.Remove(row);
                        }
                    }
                }
            }

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((bool)dataGridView1.SelectedRows[0].Cells[0].Value == false)
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = true;
            }
            else
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = false;

            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((bool)dataGridView2.SelectedRows[0].Cells[0].Value == false)
            {
                dataGridView2.SelectedRows[0].Cells[0].Value = true;
            }
            else
            {
                dataGridView2.SelectedRows[0].Cells[0].Value = false;

            }

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filter_open_option(object sender, MouseEventArgs e)
        {
            checkListBoxFilter.Size = new System.Drawing.Size(120, 190);
            
        }

        private void checkListBoxFilterLeave(object sender, EventArgs e)
        {
            checkListBoxFilter.Size = new System.Drawing.Size(120, 20);
            string all_sort = "";
            foreach (string sort_by in checkListBoxFilter.CheckedItems)
            {
                all_sort = all_sort + ","+ sort_by;
            }
           // MessageBox.Show(all_sort);

        }

        private void filter_open_option(object sender, EventArgs e)
        {
            checkListBoxFilter.Size = new System.Drawing.Size(120, 190);

        }

        private void checkListBoxFilterItemcheck(object sender, ItemCheckEventArgs e)
        {
            
            string query="";
            Boolean firstRow = true;
            dataGridView1.Rows.Clear();
           // dataGridView1.ClearSelection();
            string all_sort = checkListBoxFilter.SelectedItem.ToString();
            all_sort = "'%"+all_sort+"%'";
            if ( e.CurrentValue.ToString()== "Unchecked")
            {

          
                foreach (string sort_by in checkListBoxFilter.CheckedItems)
                {
                    all_sort = all_sort + "Or Catgory like '%" + sort_by+"%'";
                }


            }
            else
            {
                all_sort = "";
                foreach (string sort_by in checkListBoxFilter.CheckedItems)
                {
                    if (sort_by != checkListBoxFilter.SelectedItem.ToString())
                    {
                        if (firstRow == true)
                        {
                            all_sort = "'%" + sort_by + "%'";
                            firstRow = false;
                        }
                        else
                        {
                            all_sort = all_sort + "Or Catgory like '%" + sort_by + "%'";
                        }
                    }
                }

            }
   //         select * from add_Grep where Catgory like '*issues*'

            if (all_sort != "")
                query = "select * from add_Grep where Catgory like " + all_sort;
            else
                query = "select * from add_Grep";
            Show_Tables_View1(query);


        }
    
          //  string dd = e.CurrentValue.ToString();
          //  MessageBox.Show(e.NewValue.ToString());
        
    }
}
