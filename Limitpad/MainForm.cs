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
        /// Initializes a new instance of the <see cref="T:Limitpad.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();
        }

        /// <summary>
        /// Ons the limit rich text box text changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnLimitRichTextBoxTextChanged(object sender, EventArgs e)
        {
            // TODO Add code
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
        /// Ons the post button click.
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
        /// Ons the previous button click.
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
        /// Ons the main tab control selected index changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the more releases public domain giftcom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMoreReleasesPublicDomainGiftcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the original thread donation codercom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the source code githubcom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the about tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the cut tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the copy tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCopyToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the paste tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPasteToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the select all tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSelectAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the save tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the save as tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the exit tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the new tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the open tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Add code
        }

        /// <summary>
        /// Ons the options tool strip menu item drop down item clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Add code
        }
    }
}
