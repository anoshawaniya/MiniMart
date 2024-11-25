using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    /// <summary>
    /// Represents the CategoryForm used to manage categories in the application.
    /// </summary>
    public partial class CategoryForm : Form
    {
        /// <summary>
        /// Database connection object for interacting with the database.
        /// </summary>
        CDBConnect dBCon = new CDBConnect();

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryForm"/> class.
        /// </summary>
        public CategoryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieves and populates the category table in the DataGridView.
        /// </summary>
        private void getTable()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_category.DataSource = table;
        }

        /// <summary>
        /// Handles the click event to add a new category to the database.
        /// </summary>
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Category VALUES(" + TextBox_id.Text + ",'" + TextBox_name.Text + "','" + TextBox_description.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, dBCon.Getcon());
                dBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Loads the form and initializes the category table.
        /// </summary>
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        /// <summary>
        /// Handles the click event to update an existing category in the database.
        /// </summary>
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_description.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Category SET CatName='" + TextBox_name.Text + "',CatDes='" + TextBox_description.Text + "'WHERE CatId=" + TextBox_id.Text + " ";
                    SqlCommand command = new SqlCommand(updateQuery, dBCon.Getcon());
                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void DataGridView_category_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_category.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_category.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_description.Text = DataGridView_category.SelectedRows[0].Cells[2].Value.ToString();
        }

        /// <summary>
        /// Clears the text boxes for ID, Name, and Description fields.
        /// </summary>
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_description.Clear();
        }

        /// <summary>
        /// Handles the click event to delete a category from the database.
        /// </summary>
        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string deleteQuery = "DELETE FROM Category WHERE CatId=" + TextBox_id.Text + "";
                SqlCommand command = new SqlCommand(deleteQuery, dBCon.Getcon());
                dBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Exits the application when the exit label is clicked.
        /// </summary>
        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        /// Changes the logout label's color when the mouse hovers over it.
        /// </summary>
        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        /// <summary>
        /// Restores the logout label's color when the mouse leaves it.
        /// </summary>
        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.DarkSlateGray;
        }

        /// <summary>
        /// Logs the user out and redirects to the LoginForm when the logout label is clicked.
        /// </summary>
        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm Form1 = new LoginForm();
            Form1.Show();
            this.Hide();
        }

        /// <summary>
        /// Navigates to the ProductForm when the product button is clicked.
        /// </summary>
        private void button_product_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
            this.Hide();
        }

        /// <summary>
        /// Navigates to the SellerForm when the seller button is clicked.
        /// </summary>
        private void button_seller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }
    }
}
