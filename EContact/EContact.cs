﻿using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EContact_Load(object sender, EventArgs e)
        {
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            //Inserting Data into DAtabase uing the method we created in previous episode
            bool success = c.Insert(c);
            if (success == true)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                //Call the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to Add Contact
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";
            textBoxContactID.Text = "";
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            //Get the Data from textboxes
            c.ContactID = int.Parse(textBoxContactID.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;
            //Update DAta in Database
            bool success = c.Update(c);
            if (success == true)
            {
                //Updated Successfully
                MessageBox.Show("Contact has been successfully Updated.");
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Contact.Try Again.");
            }
        }
      

        

      
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        
        private void dgvContactList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //Get the Contact ID fromt eh Application
            c.ContactID = Convert.ToInt32(textBoxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact successfully deleted.");
                //Refresh Data GridView
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //CAll the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to dElte
                MessageBox.Show("Failed to Delete Dontact. Try Again.");
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }

        private void dgvContactList_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the DAta From Data Grid View and Load it to the textboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            //Get teh value from text box
            string keyword = textBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
