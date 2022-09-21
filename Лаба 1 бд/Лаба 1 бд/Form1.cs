using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using SD = System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Лаба_1_бд
{
    public partial class Form1 : Form
    {
        int i = 0;
        private MySqlDataAdapter mySqlDataAdapter;
        public Form1()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;user=root;database=mdb;password=Asus;";
        public SD.DataSet DS;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        public void TestDB()
        {
            // строка подключения к БД
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";//primer- название бд, root - свой рут 
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = "SELECT * FROM Listofapplicants;";// название таблицы
           
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            textBox1.Text = "";
            while (reader.Read())
            {
                textBox1.Text += reader[0].ToString() + " " + reader[1].ToString() + "\r\n";
                // элементы массива [] - это значения столбцов из запроса SELECT
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //PoiskData1();
            try
            {
                // строка подключения к БД
                string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
                // создаём объект для подключения к БД
                MySqlConnection conn = new MySqlConnection(connStr);
                // запрос
                string sql = "SELECT * FROM  exampoints;";
                // объект для чтения ответа сервера
                mySqlDataAdapter = new MySqlDataAdapter(sql, conn);
                int rowsGot = -1;
                DataSet DS = new DataSet();//List <Table>
                int g;
               /* for (int f = 0; f < dataGridView1.RowCount - 1; f++)
                {
                    g = dataGridView1.RowCount - 2;
                    if (f == g)
                    {
                        rtb.AppendText(dataGridView1[0, f].Value.ToString());
                    }
                    else
                    {
                        rtb.AppendText(dataGridView1[0, f].Value.ToString() + ", ");
                    }
                }*/
                rowsGot = await mySqlDataAdapter.FillAsync(DS);
                dataGridView1.DataSource = DS.Tables[0];
                if (rowsGot != -1)
                {
                    textBoxLog.Text += "Rows got from select: " + rowsGot.ToString() + "\r\n";
                }
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                textBoxLog.Text += ex.Message + "\r\n";
            }
           /* try
            {
                connStr.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }*/
        }
        private void insertData()
        {
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "INSERT INTO exampoints (Maths, Russian, Social, ID)" + textBox2.Text;
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные добавлены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /*  private void insertData_Delite()
          {
              string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
              using (MySqlConnection con = new MySqlConnection(connStr))
              {
                  try
                  {
                      string sql = "DELETE FROM settype (settype, ID)" + comboBox1.Text;
                      MySqlCommand cmd = new MySqlCommand(sql, con);
                      con.Open();
                      cmd.ExecuteNonQuery();
                      MessageBox.Show("Данные добавлены!");
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message);
                  }
              }
          }*/
        private void DeleteRow(string ID)
        {
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "DELETE FROM exampoints " + "WHERE ID = @id";
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@ID", ID);

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Продукт удален.");
                }
            }
        }
        /*private void UpdateRow(string ID)
        {
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "UPDATE settype SET ID = " + textBox3.Text + "WHERE ID =" + textBox4.Text;// @ID WHERE ID = @ID, textBox3, textBox4)";
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                  //  cmd.Parameters.AddWithValue("@ID", textBox4);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Продукт изменен.");
                }
            }
        }*/
        private void UpdateRow(string textBox3, string textBox4)
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "localhost";
            mysqlCSB.Database = "mdb";
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "Asus";
            string queryString = string.Format("UPDATE exampoints SET Maths = '{0}' WHERE ID = '{1}'", textBox3, textBox4);
            try 
            {
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = mysqlCSB.ConnectionString;
                    con.Open();
                    using (MySqlCommand com = new MySqlCommand(queryString, con))
                    {
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PoiskData1()
        {
            try
            {
                mycon = new MySqlConnection(connect);
                mycon.Open();
                //MessageBox.Show("DB CONNECT");
                string scrpt = "SHOW COLUMNS FROM exampoints";
                MySqlDataAdapter msql_dt = new MySqlDataAdapter(scrpt, mycon);
                SD.DataTable table = new SD.DataTable();
                msql_dt.Fill(table);
                dataGridView1.DataSource = table;
                int g;
                for (int f = 0; f < dataGridView1.RowCount - 1; f++)
                {
                    g = dataGridView1.RowCount - 2;
                    if (f == g)
                    {
                        rtb.AppendText(dataGridView1[0, f].Value.ToString());
                    }
                    else
                    {
                        rtb.AppendText(dataGridView1[0, f].Value.ToString() + ", ");
                    }
                }
                mycon.Close();
            }
            catch
            {
                //MessageBox.Show("Connection lost");
            }
        }
        private void PoiskData()
        {
            if (rtb.Text != "")
            {
              //string script = "Select * from exampoints WHERE CONCAT Maths like'%" + textBox5.Text + "%'";
            //string script = "Select * from exampoints WHERE CONCAT(" + dataGridView1.Text + ") like'%" + textBox5.Text + "%'";
            string script = "Select * from exampoints WHERE CONCAT(" + rtb.Text + ") like'%" + textBox5.Text + "%'";
            mycon = new MySqlConnection(connect);
                mycon.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(script, connect);
                SD.DataTable table = new SD.DataTable();
                ms_data.Fill(table);
                dataGridView1.DataSource = table;
                //dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Aqua;
                mycon.Close();

             }

           /* else
            {
                MessageBox.Show("ERROR");
            }*/

        }
        private void Sortirovka()
        {

            string script = "SELECT * FROM exampoints ORDER BY " + textBox6.Text + " ASC;";
            mycon = new MySqlConnection(connect);
            mycon.Open();
            MySqlDataAdapter ms_data = new MySqlDataAdapter(script, connect);
            SD.DataTable table = new SD.DataTable();
            ms_data.Fill(table);
            dataGridView1.DataSource = table;
            //dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Aqua;
            mycon.Close();
        }
        private void Procedura1()
        {
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "procedure1";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные добавлены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Procedura2()
        {
            string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "procedure2";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные добавлены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            insertData();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(comboBox1.Text))
            {
                DeleteRow(comboBox1.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
                UpdateRow(textBox3.Text, textBox4.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (i < 1)
            {
                PoiskData1();
            }
            i++;
            PoiskData();    
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Aqua;



        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sortirovka();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Procedura1();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Procedura2();
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {

        }


        private async void button11_Click(object sender, EventArgs e)
        {
            //PoiskData1();
            try
            {
                // строка подключения к БД
                string connStr = "server=localhost;user=root;database=mdb;password=Asus;";
                // создаём объект для подключения к БД
                MySqlConnection conn = new MySqlConnection(connStr);
                // запрос
                string sql = "SELECT * FROM  exampoints;";
                // объект для чтения ответа сервера
                mySqlDataAdapter = new MySqlDataAdapter(sql, conn);
                int rowsGot = -1;
                DataSet DS = new DataSet();//List <Table>
                int g;
                /* for (int f = 0; f < dataGridView1.RowCount - 1; f++)
                 {
                     g = dataGridView1.RowCount - 2;
                     if (f == g)
                     {
                         rtb.AppendText(dataGridView1[0, f].Value.ToString());
                     }
                     else
                     {
                         rtb.AppendText(dataGridView1[0, f].Value.ToString() + ", ");
                     }
                 }*/
                rowsGot = await mySqlDataAdapter.FillAsync(DS);
                dataGridView1.DataSource = DS.Tables[0];
                if (rowsGot != -1)
                {
                    textBoxLog.Text += "Rows got from select: " + rowsGot.ToString() + "\r\n";
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                textBoxLog.Text += ex.Message + "\r\n";
            }
            /* try
             {
                 connStr.Close();
             }
             catch (MySqlException ex)
             {
                 MessageBox.Show(ex.Message);
                 return;
             }*/
        }
    }
}
