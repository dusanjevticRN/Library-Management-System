﻿using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace LibraryManagementSystem
{
    public partial class SignInStage : Form
    {
        private static SignInStage instance;
        private static String fileName = "accounts.txt";
        private static String fileName2 = "books.txt";
        private static String fileName3 = "filteredBooks.txt";
        private static String fileName4 = "borrowedBooks.txt";
        private static String fileName5 = "faqs.txt";
        private SignInStage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adjustLblLibrarySystem();
            adjustTxtBoxPassword();
            adjustLinkLblSignUp();
            checkFiles();
        }

        private void checkFiles()
        {
            if(!File.Exists(fileName) && !File.Exists(fileName2) && !File.Exists(fileName3) && !File.Exists(fileName4) && !File.Exists(fileName5))
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.WriteLine("FirstName,LastName,Gender,Email,Password");
                sw.Close();

                sw = new StreamWriter(fileName2);
                sw.WriteLine("Name,Author,Publisher,Genre,Quantity,Availability,Price");
                sw.Close();

                sw = new StreamWriter(fileName3);
                sw.WriteLine("Name,Author,Publisher,Genre,Quantity,Availability,Price");
                sw.Close();

                sw = new StreamWriter(fileName4);
                sw.WriteLine("Borrower contact,Book name,Author,Genre,Borrow date,Return date");
                sw.Close();

                sw = new StreamWriter(fileName5);
                sw.WriteLine("Question,Answer");
                sw.Close();

            }
        }

        private void searchTxtFile(String email, String password)
        {
            try
            {
                String[] lines = File.ReadAllLines(fileName);
                Boolean noEmailFound = true;
                Boolean noPasswordFound = true;

                foreach(String line in lines)
                {
                    String[] elements = line.Split(',');
                    String name = elements[0];
                    String emailToCheck = elements[3];
                    String passwordToCheck = elements[4];

                    if(emailToCheck.Equals(email))
                    {
                        noEmailFound = false;

                        if(passwordToCheck.Equals(password))
                        {
                            MessageBox.Show("You have been signed in successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            noPasswordFound = false;

                            MainStage mainStage = MainStage.getInstance();
                            this.Hide();
                            mainStage.Show();
                            mainStage.setLblWelcome(name);

                            break;
                        }
                    }   
                }
                
                if(noEmailFound)
                    MessageBox.Show("No account found with the specified email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if(noPasswordFound)
                    MessageBox.Show("Invalid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void adjustLblLibrarySystem()
        {
            this.lblLibrarySystem.Parent = pBoxBackgroundSignIn;
            this.lblLibrarySystem.BackColor = Color.Transparent;
        }

        private void adjustTxtBoxPassword()
        {
            this.txtBoxPassword.PasswordChar = '*';
        }

        private void adjustLinkLblSignUp()
        {
            this.linkLblSignUp.LinkBehavior = LinkBehavior.NeverUnderline;
        }

        private void cBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(cBoxShowPassword.Checked)
                this.txtBoxPassword.PasswordChar = '\0';

            else
                this.txtBoxPassword.PasswordChar = '*';
        }

        private void linkLblSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpStage signUpStage = new SignUpStage();
            this.Hide();
            signUpStage.Show();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            String email = txtBoxEmail.Text;
            String password = txtBoxPassword.Text;
            searchTxtFile(email, password);

        }

        public static SignInStage getInstance()
        {
            if (instance == null)
                instance = new SignInStage();

            return instance;
        }

        private void SignInStage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
