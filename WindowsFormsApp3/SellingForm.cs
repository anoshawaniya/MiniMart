using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace WindowsFormsApp3
{
    /// <summary>
    /// The SellingForm class handles the UI and functionality for managing the selling process in the application.
    /// </summary>
    public partial class SellingForm : Form
    {
        CDBConnect dBCon = new CDBConnect(); // Database connection object
        DGVPrinter printer = new DGVPrinter(); // Printer object for printing data

        /// <summary>
        /// Initializes the components of the form.
        /// </summary>
        public SellingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fetches product categories from the database and populates the category dropdown.
        /// </summary>
        private void getCategory()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox_category.DataSource = table;
            comboBox_category.ValueMember = "CatName";
        }

        /// <summary>
        /// Fetches product details (name and price) from the database and populates the product table.
        /// </summary>
        private void getTable()
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        /// <summary>
        /// Fetches sales records (bills) from the database and populates the sell list table.
        /// </summary>
        private void getSellTable()
        {
            string selectQuery = "SELECT * FROM Bill";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_sellList.DataSource = table;
        }

        /// <summary>
        /// Event handler for form load. Initializes the form with current date, seller's name, and data from the database.
        /// </summary>
        private void SellingForm_Load(object sender, EventArgs e)
        {
            label_date.Text = DateTime.Today.ToShortDateString();
            label_seller.Text = LoginForm.sellerName;
            getTable();
            getCategory();
            getSellTable();
        }

        /// <summary>
        /// Event handler for when a product is clicked in the product data grid view. Fills the product details in the corresponding textboxes.
        /// </summary>
        private void dataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_name.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_price.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
        }

        int grandTotal = 0, n = 0; // Track the grand total and order number

        /// <summary>
        /// Event handler for adding an order. Inserts the order details into the Bill table in the database.
        /// </summary>
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Bill VALUES(" + TextBox_id.Text + ",'" + label_seller.Text + "','" + label_date.Text + "'," + grandTotal.ToString() + ")";
                SqlCommand command = new SqlCommand(insertQuery, dBCon.Getcon());
                dBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully", "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dBCon.CloseCon();
                getSellTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Event handler for printing the sell list. Prints the data from the sell list data grid view.
        /// </summary>
        private void button_print_Click(object sender, EventArgs e)
        {
            // Print the sell list using DGVPrinterHelper for PDF file generation
            printer.Title = "Smart Choice Sell Lists";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_sellList);
        }

        /// <summary>
        /// Event handler for logging out. Navigates to the login form and hides the current form.
        /// </summary>
        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm Form1 = new LoginForm();
            Form1.Show();
            this.Hide();
        }

        /// <summary>
        /// Event handler for exiting the application. Closes the application.
        /// </summary>
        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Event handler for mouse hover on the exit label. Changes the label color to red when hovered.
        /// </summary>
        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        /// <summary>
        /// Event handler for mouse leave from the exit label. Resets the label color to dark slate gray when the mouse leaves.
        /// </summary>
        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkSlateGray;
        }

        /// <summary>
        /// Event handler for mouse hover on the logout label. Changes the label color to red when hovered.
        /// </summary>
        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        /// <summary>
        /// Event handler for mouse leave from the logout label. Resets the label color to dark slate gray when the mouse leaves.
        /// </summary>
        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.DarkSlateGray;
        }

        /// <summary>
        /// Event handler for refreshing the product table. Fetches the latest product data from the database.
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        /// <summary>
        /// Event handler for category selection change. Updates the product table to display products of the selected category.
        /// </summary>
        private void comboBox_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product WHERE ProCat='" + comboBox_category.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuery, dBCon.Getcon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        /// <summary>
        /// Event handler for adding an order item. Adds the selected product to the order list and updates the grand total.
        /// </summary>
        private void button_addOrder_Click(object sender, EventArgs e)
        {
            if (TextBox_name.Text == "" || TextBox_qty.Text == "")
            {
                MessageBox.Show("Missing Information", "Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int Total = Convert.ToInt32(TextBox_price.Text) * Convert.ToInt32(TextBox_qty.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView_order);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = TextBox_name.Text;
                addRow.Cells[2].Value = TextBox_price.Text;
                addRow.Cells[3].Value = TextBox_qty.Text;
                addRow.Cells[4].Value = Total;
                dataGridView_order.Rows.Add(addRow);
                grandTotal += Total;
                label_amount.Text = grandTotal + " Rs";
            }
        }
    }
}
