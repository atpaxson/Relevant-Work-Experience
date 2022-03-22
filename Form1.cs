using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//This is for connecting using SQL;
using System.Data.SqlClient;

namespace C_Sharp_Assignment_NEW_4283

{
    public partial class Form1 : Form
    {
        ////////////////////////////////////////////////////////the string below is what connects me to the Walton back-end system
        string connectionstring = "Data Source = essql1._____; Initial Catalog = _____; User ID = _____; Password = _____";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader datareader;
        //////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select a character
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT CharacterName FROM Character";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_ChooseFighter.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void cmb_ChooseFighter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////using the above, this translates all of the variables from the character table
            try
            {

                if (cmb_ChooseFighter.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT CharacterID, PlayerName, Race, Class, Strength, Dexterity, Intelligence, HitPoints FROM Character WHERE CharacterName = '" + cmb_ChooseFighter.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_CharID.Text = datareader[0].ToString();
                        txt_Player.Text = datareader[1].ToString();
                        txt_Race.Text = datareader[2].ToString();
                        txt_Class.Text = datareader[3].ToString();
                        txt_Strength.Text = datareader[4].ToString();
                        txt_Dex.Text = datareader[5].ToString();
                        txt_Intel.Text = datareader[6].ToString();
                        txt_HP.Text = datareader[7].ToString();
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    lst_Equip.Items.Clear();
                    connection.Open();
                    sql = "SELECT EquipmentName FROM Equipment";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        lst_Equip.Items.Add(datareader[0].ToString());
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    var sql2 = "SELECT * FROM Inventory WHERE CharacterID = '" + txt_CharID.Text + "'";
                    var da = new SqlDataAdapter(sql2, connection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dgv_CharInventory.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("You must choose a character.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void lst_Equip_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////////////pay close attention to the indexing - i.e. [0], [1], etc.
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT EquipmentID, Type, Size, Rarity FROM Equipment WHERE EquipmentName = '" + lst_Equip.SelectedItem.ToString() + "'";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    txt_EquipID.Text = datareader[0].ToString();
                    txt_Type.Text = datareader[1].ToString();
                    txt_Size.Text = datareader[2].ToString();
                    txt_Rare.Text = datareader[3].ToString();

                }

                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void dgv_CharInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //////////
            try
            {
                txt_Acquire.Text = dgv_CharInventory.CurrentRow.Cells[3].Value.ToString();
                txt_Loss.Text = dgv_CharInventory.CurrentRow.Cells[4].Value.ToString();
                txt_Status.Text = dgv_CharInventory.CurrentRow.Cells[5].Value.ToString();
                txt_Slot.Text = dgv_CharInventory.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_Insert1_Click(object sender, EventArgs e)
        {
            //////////the @ signs below represent scalar variables that allow us to interact with the UI
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Character VALUES (@name, @playername, @race, @class, @strength, @dexterity, @intelligence, @HP)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", txt_CharName.Text);
                command.Parameters.AddWithValue("@playername", txt_Player.Text);
                command.Parameters.AddWithValue("@race", txt_Race.Text);
                command.Parameters.AddWithValue("@class", txt_Class.Text);
                command.Parameters.AddWithValue("@strength", txt_Strength.Text);
                command.Parameters.AddWithValue("@dexterity", txt_Dex.Text);
                command.Parameters.AddWithValue("@intelligence", txt_Intel.Text);
                command.Parameters.AddWithValue("@HP", txt_HP.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have created " + answer + " character.");

                connection.Close();
                command.Dispose();
            } catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_Update1_Click(object sender, EventArgs e)
        {
            /////
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "UPDATE Character SET CharacterName = @name, PlayerName = @playername, Race = @race, Class = @class, Strength = @strength, Dexterity = @dexterity, Intelligence = @intelligence, HitPoints = @HP WHERE CharacterID = @CID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", cmb_ChooseFighter.SelectedItem.ToString());
                command.Parameters.AddWithValue("@playername", txt_Player.Text);
                command.Parameters.AddWithValue("@race", txt_Race.Text);
                command.Parameters.AddWithValue("@class", txt_Class.Text);
                command.Parameters.AddWithValue("@strength", txt_Strength.Text);
                command.Parameters.AddWithValue("@dexterity", txt_Dex.Text);
                command.Parameters.AddWithValue("@intelligence", txt_Intel.Text);
                command.Parameters.AddWithValue("@HP", txt_HP.Text);
                command.Parameters.AddWithValue("@CID", txt_CharID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have modified " + answer + " character.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            /////
        }

        private void btn_Delete1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Character WHERE CharacterID = @CID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@CID", txt_CharID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " character.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Insert3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Equipment VALUES (@Ename, @Etype, @Esize, @Erarity)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Ename", txt_NewEquip.Text);
                command.Parameters.AddWithValue("@Etype", txt_Type.Text);
                command.Parameters.AddWithValue("@Esize", txt_Size.Text);
                command.Parameters.AddWithValue("@Erarity", txt_Rare.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have created " + answer + " equipment.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Update3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "UPDATE Equipment SET EquipmentName = @Ename, Type = @Etype, Size = @Esize, Rarity = @Erarity WHERE EquipmentID = @EID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Ename", lst_Equip.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Etype", txt_Type.Text);
                command.Parameters.AddWithValue("@Esize", txt_Size.Text);
                command.Parameters.AddWithValue("@Erarity", txt_Rare.Text);
                command.Parameters.AddWithValue("@EID", txt_EquipID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have modified " + answer + " equipment.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Delete3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Equipment WHERE EquipmentID = @EID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@EID", txt_EquipID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " equipment.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Insert2_Click(object sender, EventArgs e)
        {
            ///// this is the insert button for inventory
            ///// in order to insert into inventory, you must first choose both a character and a piece of equipment
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Inventory VALUES (@CID, @EID, @Adate, @Ldate, @Status, @Slot)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@CID", txt_CharID.Text);
                command.Parameters.AddWithValue("@EID", txt_EquipID.Text);
                command.Parameters.AddWithValue("@Adate", txt_Acquire.Text);
                command.Parameters.AddWithValue("@Ldate", txt_Loss.Text);
                command.Parameters.AddWithValue("@Status", txt_Status.Text);
                command.Parameters.AddWithValue("@Slot", txt_Slot.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have entered " + answer + " item into your inventory.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Update2_Click(object sender, EventArgs e)
        {
            ///// this is the inventory update button
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "UPDATE Inventory SET CharacterID = @CID, EquipmentID = @EID, AcquireDate = @Adate, LossDate = @Ldate, Status = @Status, Slot = @Slot WHERE InventoryID = @InvID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@CID", txt_CharID.Text);
                command.Parameters.AddWithValue("@EID", txt_EquipID.Text);
                command.Parameters.AddWithValue("@Adate", txt_Acquire.Text);
                command.Parameters.AddWithValue("@Ldate", txt_Loss.Text);
                command.Parameters.AddWithValue("@Status", txt_Status.Text);
                command.Parameters.AddWithValue("@Slot", txt_Slot.Text);
                command.Parameters.AddWithValue("@InvID", dgv_CharInventory.CurrentRow.Cells[0].Value.ToString());

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have modified " + answer + " item in your inventory.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Delete2_Click(object sender, EventArgs e)
        {
            /////this is the inventory delete button
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Inventory WHERE InventoryID = @InvID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@InvID", dgv_CharInventory.CurrentRow.Cells[0].Value.ToString());

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " item in your inventory.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            /////this is the refresh button
            //how to clear a text box
            try
            {
                txt_Strength.Text = "";
                txt_Dex.Text = "";
                txt_Intel.Text = "";
                txt_HP.Text = "";
                txt_CharID.Text = "";
                txt_Player.Text = "";
                txt_Race.Text = "";
                txt_Class.Text = "";
                txt_CharName.Text = "";
                txt_Acquire.Text = "";
                txt_Status.Text = "";
                txt_Loss.Text = "";
                txt_Slot.Text = "";
                txt_EquipID.Text = "";
                txt_Type.Text = "";
                txt_Size.Text = "";
                txt_Rare.Text = "";
                txt_NewEquip.Text = "";

                //how to clear a listbox
                lst_Equip.Items.Clear();

                //how to clear a combobox
                cmb_ChooseFighter.SelectedIndex = -1;

                //how to clear a datagrid
                dgv_CharInventory.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }
    }
}
