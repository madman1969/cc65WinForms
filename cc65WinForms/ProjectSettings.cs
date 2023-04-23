using cc65Wrapper.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cc65WinForms
{
    public partial class ProjectSettings : Form
    {
        public ProjectSettings()
        {
            InitializeComponent();

            PopulateTargetPlatformComboBox();
        }

        private void PopulateTargetPlatformComboBox()
        {
            foreach (var value in Enum.GetValues(typeof(CC65ProjectTypes)))
            {
                comboBox1.Items.Add(value.ToString());
            }

            comboBox1.SelectedIndex = 0;
        }

        private void okButtom_Click(Object sender, EventArgs e)
        {
            CloseProjectSettings();
        }

        private void CloseProjectSettings()
        {
            this.Close();
        }
    }
}
