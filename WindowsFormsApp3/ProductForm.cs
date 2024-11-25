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
    public partial class ProductForm : Form
    {
        CDBConnect dBCon = new CDBConnect();

        // Constructor: Initializes the ProductForm and its components
        public ProductForm()
        {
            InitializeComponent();
        }

        // Event handler for Category button click. Opens the CategoryForm and hides the current form.
        private void button_category_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        // Event handler for form load. Retrieves categories and product table data when the form is loaded.
        private void ProductForm_Load(object sender, EventArgs e)
        {
            getCategory();
            getTable();
        }

        // Retrieves all categories from the database and populates the category ComboBox and search ComboBox.
        private void getCategory()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox_category.DataSource = table;
            comboBox_category.ValueMember = "CatName";
            comboBox_search.DataSource = table;
            comboBox_search.ValueMember = "CatName";
        }

        // Retrieves all products from the database and binds the result to the data grid view.
        private void getTable()
        {
            string selectQuery = "SELECT * FROM Product";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        // Clears all input fields (textboxes and combo boxes).
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_price.Clear();
            TextBox_qty.Clear();
            comboBox_category.SelectedIndex = 0;
        }

        // Event handler for Add button click. Adds a new product to the database with the data from input fields.
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Product VALUES(" + TextBox_id.Text + ",'" + TextBox_name.Text + "'," + TextBox_price.Text + "," + TextBox_qty.Text + ",'" + comboBox_category.Text + "')";
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

        // Event handler for Update button click. Updates an existing product based on the ID.
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_price.Text == "" || TextBox_qty.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Product SET ProdName='" + TextBox_name.Text + "',ProdPrice=" + TextBox_price.Text + ",ProdQty=" + TextBox_qty.Text + ",ProdCat='" + comboBox_category.Text + "' WHERE ProdID=" + TextBox_id.Text;
                    SqlCommand command = new SqlCommand(updateQuery, dBCon.Getcon());
                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Event handler for when a row in the product data grid is clicked. Populates input fields with selected product data.
        private void dataGridView_product_Click_1(object sender, EventArgs e)
        {
            TextBox_id.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_price.Text = dataGridView_product.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_qty.Text = dataGridView_product.SelectedRows[0].Cells[3].Value.ToString();
            comboBox_category.SelectedValue = dataGridView_product.SelectedRows[0].Cells[4].Value.ToString();
        }

        // Event handler for Delete button click. Deletes the product based on the product ID from input fields.
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
                    string deleteQuery = "DELETE FROM Product WHERE ProdId=" + TextBox_id.Text;
                    SqlCommand command = new SqlCommand(deleteQuery, dBCon.Getcon());
                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Event handler for Refresh button click. Refreshes the product table.
        private void button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        // Event handler for category selection change in search ComboBox. Filters the product table by the selected category.
        private void comboBox_search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM Product WHERE ProCat='" + comboBox_search.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        // Event handler for MouseEnter on exit label. Changes label color to red.
        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        // Event handler for MouseLeave on exit label. Resets label color to dark slate gray.
        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkSlateGray;
        }

        // Event handler for MouseEnter on logout label. Changes label color to red.
        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        // Event handler for MouseLeave on logout label. Resets label color to dark slate gray.
        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.DarkSlateGray;
        }

        // Event handler for logout label click. Navigates to the LoginForm.
        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        // Event handler for exit label click. Closes the application.
        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Event handler for Seller button click. Navigates to the SellerForm.
        private void button_seller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }
    }
}
