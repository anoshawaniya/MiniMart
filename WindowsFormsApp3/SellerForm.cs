using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    /// <summary>
    /// Represents the SellerForm used to manage seller data in the application.
    /// </summary>
    public partial class SellerForm : Form
    {
        /// <summary>
        /// Database connection object for interacting with the database.
        /// </summary>
        CDBConnect dBCon = new CDBConnect();

        /// <summary>
        /// Initializes a new instance of the <see cref="SellerForm"/> class.
        /// </summary>
        public SellerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieves and populates the seller table in the DataGridView.
        /// </summary>
        private void getTable()
        {
            string selectQuery = "SELECT * FROM Seller";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_Seller.DataSource = table;
        }

        /// <summary>
        /// Clears the text boxes for Seller information.
        /// </summary>
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_age.Clear();
            TextBox_phone.Clear();
            TextBox_pass.Clear();
        }

        /// <summary>
        /// Handles the click event to add a new seller to the database.
        /// </summary>
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Seller VALUES(" + TextBox_id.Text + ",'" + TextBox_name.Text + "','" + TextBox_age.Text + "','" + TextBox_phone.Text + "','" + TextBox_pass.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, dBCon.Getcon());
                dBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dBCon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Loads the form and initializes the seller table.
        /// </summary>
        private void SellerForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        /// <summary>
        /// Handles the click event to update an existing seller's details in the database.
        /// </summary>
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_age.Text == "" || TextBox_phone.Text == "" || TextBox_pass.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Seller SET SellerName='" + TextBox_name.Text + "',SellerAge='" + TextBox_age.Text + "',SellerPhone='" + TextBox_phone.Text + "',SellerPass='" + TextBox_pass.Text + "'WHERE SellerId=" + TextBox_id.Text + "";
                    SqlCommand command = new SqlCommand(updateQuery, dBCon.Getcon());
                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Update Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dBCon.CloseCon();
                    getTable();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Populates the text boxes with selected row data from the DataGridView.
        /// </summary>
        private void dataGridView_Seller_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = dataGridView_Seller.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = dataGridView_Seller.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_age.Text = dataGridView_Seller.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_phone.Text = dataGridView_Seller.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_pass.Text = dataGridView_Seller.SelectedRows[0].Cells[4].Value.ToString();
        }

        /// <summary>
        /// Handles the click event to delete a seller from the database.
        /// </summary>
        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if ((MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        string deleteQuery = "DELETE FROM Seller WHERE SellerId=" + TextBox_id.Text + "";
                        SqlCommand command = new SqlCommand(deleteQuery, dBCon.Getcon());
                        dBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Seller Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dBCon.CloseCon();
                        getTable();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Changes the logout label's color when the mouse hovers over it.
        /// </summary>
        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        /// <summary>
        /// Restores the logout label's color when the mouse leaves it.
        /// </summary>
        private void label_logout_DragLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.DarkSlateGray;
        }

        /// <summary>
        /// Changes the exit label's color when the mouse hovers over it.
        /// </summary>
        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        /// <summary>
        /// Restores the exit label's color when the mouse leaves it.
        /// </summary>
        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkSlateGray;
        }

        /// <summary>
        /// Exits the application when the exit label is clicked.
        /// </summary>
        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Logs the user out and redirects to the LoginForm when the logout label is clicked.
        /// </summary>
        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        /// <summary>
        /// Navigates to the ProductForm when the product button is clicked.
        /// </summary>
        private void button_seller_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Navigates to the CategoryForm when the category button is clicked.
        /// </summary>
        private void button_category_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }
    }
}
