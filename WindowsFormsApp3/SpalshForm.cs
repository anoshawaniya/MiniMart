using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    /// <summary>
    /// The SpalshForm class represents the splash screen that is shown when the application starts.
    /// It includes a progress bar that fills up and then transitions to the login form.
    /// </summary>
    public partial class SpalshForm : Form
    {
        /// <summary>
        /// Initializes the components of the splash screen form.
        /// </summary>
        public SpalshForm()
        {
            InitializeComponent();
        }

        int startPoint = 0; // Variable to track the progress bar's value

        /// <summary>
        /// Event handler for the timer tick. This method updates the progress bar and transitions to the login form once the progress bar reaches 100%.
        /// </summary>
        /// <param name="sender">The source of the event (timer).</param>
        /// <param name="e">Event arguments.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 2; // Increment the progress bar value by 2
            myProgressBar.Value = startPoint; // Update the progress bar value

            // When progress bar reaches 100%, stop the timer and show the LoginForm
            if (myProgressBar.Value == 100)
            {
                myProgressBar.Value = 0; // Reset progress bar
                timer1.Stop(); // Stop the timer
                LoginForm Form1 = new LoginForm(); // Create an instance of the LoginForm
                this.Hide(); // Hide the splash form
                Form1.Show(); // Show the login form
            }
        }

        /// <summary>
        /// Event handler for the splash form's load event. Starts the timer when the form is loaded.
        /// </summary>
        /// <param name="sender">The source of the event (form).</param>
        /// <param name="e">Event arguments.</param>
        private void SpalshForm_Load(object sender, EventArgs e)
        {
            timer1.Start(); // Start the timer when the splash form is loaded
        }
    }
}
