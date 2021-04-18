// <copyright file="MainForm.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

namespace Limitpad
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using PublicDomain;
    using System.Xml.Serialization;
    using System.IO;
    using System.Diagnostics;

    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The count regex.
        /// </summary>
        Regex countRegex = new Regex(@"[\S]+", RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        private Icon associatedIcon = null;

        /// <summary>
        /// The settings data.
        /// </summary>
        private SettingsData settingsData = null;

        /// <summary>
        /// The settings data path.
        /// </summary>
        private string settingsDataPath = $"{Application.ProductName}-SettingsData.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Limitpad.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();

            /* Settings data */

            // Check for settings file
            if (!File.Exists(this.settingsDataPath))
            {
                // Create new settings file
                this.SaveSettingsFile(this.settingsDataPath, new SettingsData());
            }

            // Load settings from disk
            this.settingsData = this.LoadSettingsFile(this.settingsDataPath);

            // Set save on exit
            this.rememberTextToolStripMenuItem.Checked = this.settingsData.RememberText;

            // TODO Check if must load text [Multiple pads]
            if (this.settingsData.RememberText && this.settingsData.padTextList.Count > 0)
            {
                // Load pad
                this.limitRichTextBox.Text = this.settingsData.padTextList[0];
            }
        }

        /// <summary>
        /// Handles the limit rich text box text changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnLimitRichTextBoxTextChanged(object sender, EventArgs e)
        {
            // Colorize limit
            this.ColorizeLimit();

            // Update labels
            this.characterCountToolStripStatusLabel.Text = this.limitRichTextBox.Text.Length.ToString();
            this.wordCountToolStripStatusLabel.Text = this.CountByRegex(this.limitRichTextBox.Text).ToString();
        }

        /// <summary>
        /// Colorizes the limit.
        /// </summary>
        private void ColorizeLimit()
        {
            // TODO Suspend layout [Can be handled via WM_PAINT]
            this.limitRichTextBox.SuspendLayout();

            // Set caret position
            var selectionStart = this.limitRichTextBox.SelectionStart;

            // Text
            this.formatRichTextBox.Clear();
            this.formatRichTextBox.ForeColor = this.prevColorDialog.Color;
            this.formatRichTextBox.AppendText(this.limitRichTextBox.Text);

            // Set selection start
            try
            {
                // Process characters
                if (this.charactersRadioButton.Checked)
                {
                    // Change color
                    this.formatRichTextBox.SelectionStart = (int)this.limitNumericUpDown.Value;
                    this.formatRichTextBox.SelectionLength = this.formatRichTextBox.TextLength - (int)this.limitNumericUpDown.Value;
                    this.formatRichTextBox.SelectionColor = this.postColorDialog.Color;
                }
                else // Process words
                {
                    // Get word position list
                    var wordPositionList = this.GetWordPositionList(this.formatRichTextBox.Text);

                    // Set word selection start
                    var wordSelectionStart = wordPositionList[(int)this.limitNumericUpDown.Value - 1];

                    // Change color
                    this.formatRichTextBox.SelectionStart = wordSelectionStart;
                    this.formatRichTextBox.SelectionLength = this.formatRichTextBox.TextLength - wordSelectionStart;
                    this.formatRichTextBox.SelectionColor = this.postColorDialog.Color;
                }
            }

            catch
            {
                // Let it fall through
            }

            // Set rtf
            this.limitRichTextBox.Rtf = this.formatRichTextBox.Rtf;

            // Set cursor
            this.limitRichTextBox.SelectionStart = selectionStart;
            this.limitRichTextBox.SelectionLength = 0;

            // Resume layout
            this.limitRichTextBox.ResumeLayout();
        }

        /// <summary>
        /// Counts the by regex.
        /// </summary>
        /// <returns>The by regex.</returns>
        /// <param name="inputString">Input string.</param>
        private int CountByRegex(string inputString)
        {
            // Set match collection
            var matchCollectioncollection = this.countRegex.Matches(inputString);

            // Return the resulting count
            return matchCollectioncollection.Count;
        }

        /// <summary>
        /// Gets the word position list.
        /// </summary>
        /// <returns>The word position list.</returns>
        /// <param name="inputString">Input string.</param>
        public List<int> GetWordPositionList(string inputString)
        {
            // The position list 
            var positionList = new List<int>();

            // The count
            var count = 0;

            // Iterate input string
            for (var index = 1; index < inputString.Length; index++)
            {
                // Evaluate white space
                if (char.IsWhiteSpace(inputString[index - 1]) == true)
                {
                    // Evaluate digit or punctuation
                    if (char.IsLetterOrDigit(inputString[index]) == true || char.IsPunctuation(inputString[index]))
                    {
                        // Raise count
                        count++;

                        // Add word position
                        positionList.Add(index - 1);
                    }
                }
            }

            // Raise by input string length
            if (inputString.Length > 2)
            {
                count++;
            }

            // The populated position list
            return positionList;
        }


        /// <summary>
        /// Handles the post button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPostButtonClick(object sender, EventArgs e)
        {
            // Show post color dialog
            DialogResult dialogResult = postColorDialog.ShowDialog();

            // Check the user clicked OK
            if (dialogResult == DialogResult.OK)
            {
                // Set post button back color
                this.postButton.ForeColor = postColorDialog.Color;
            }
        }

        /// <summary>
        /// Handles the previous button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPrevButtonClick(object sender, EventArgs e)
        {
            // Show prev color dialog
            DialogResult dialogResult = prevColorDialog.ShowDialog();

            // Check the user clicked OK
            if (dialogResult == DialogResult.OK)
            {
                // Set prev button back color
                this.prevButton.ForeColor = prevColorDialog.Color;
            }
        }

        /// <summary>
        /// Handles the main tab control selected index changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the more releases public domain giftcom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMoreReleasesPublicDomainGiftcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open current website
            Process.Start("https://publicdomaingift.com");
        }

        /// <summary>
        /// Handles the original thread donation codercom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the source code githubcom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub repository
            Process.Start("https://github.com/publicdomain/limitpad");
        }

        /// <summary>
        /// Handles the about tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the cut tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the copy tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCopyToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the paste tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPasteToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the select all tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSelectAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the save tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the save as tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the exit tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close program
            this.Close();
        }

        /// <summary>
        /// Handles the new tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Clear pad text
            this.limitRichTextBox.Clear();

            // Focus
            this.limitRichTextBox.Focus();
        }

        /// <summary>
        /// Handles the open tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Handles the options tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set clicked item
            var clickedItem = (ToolStripMenuItem)e.ClickedItem;

            // Toggle checked
            clickedItem.Checked = !clickedItem.Checked;
        }

        /// <summary>
        /// Handles the limit numeric up down value changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnLimitNumericUpDownValueChanged(object sender, EventArgs e)
        {
            // Colorize pad
            this.ColorizeLimit();
        }

        /// <summary>
        /// Loads the settings file.
        /// </summary>
        /// <returns>The settings file.</returns>
        /// <param name="settingsFilePath">Settings file path.</param>
        private SettingsData LoadSettingsFile(string settingsFilePath)
        {
            // Use file stream
            using (FileStream fileStream = File.OpenRead(settingsFilePath))
            {
                // Set xml serialzer
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                // Return populated settings data
                return xmlSerializer.Deserialize(fileStream) as SettingsData;
            }
        }

        /// <summary>
        /// Saves the settings file.
        /// </summary>
        /// <param name="settingsFilePath">Settings file path.</param>
        /// <param name="settingsDataParam">Settings data parameter.</param>
        private void SaveSettingsFile(string settingsFilePath, SettingsData settingsDataParam)
        {
            try
            {
                // Use stream writer
                using (StreamWriter streamWriter = new StreamWriter(settingsFilePath, false))
                {
                    // Set xml serialzer
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                    // Serialize settings data
                    xmlSerializer.Serialize(streamWriter, settingsDataParam);
                }
            }
            catch (Exception exception)
            {
                // Advise user
                MessageBox.Show($"Error saving settings file.{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{exception.Message}", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the main form form closing event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            /* Setiings data */

            // Save on exit
            this.settingsData.RememberText = this.rememberTextToolStripMenuItem.Checked;

            // Clear pad text list
            this.settingsData.padTextList.Clear();

            // Process list for pads' text
            if (this.rememberTextToolStripMenuItem.Checked)
            {
                // TODO Save pad(s) [Multiple pads]
                this.settingsData.padTextList.Add(this.limitRichTextBox.Text);
            }

            // Save settings data to disk
            this.SaveSettingsFile(this.settingsDataPath, this.settingsData);
        }
    }
}
