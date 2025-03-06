using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonziClone
{
    public partial class Settings : Form
    {
        private string basePath = "..\\..\\..\\BonziSpriteSheet\\"; // Base directory for sprite sheets

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            settings_trackbar_animationinterval.Value = Properties.Settings.Default.animation_interval;
            populateListBox();

            string currentAnimation = Properties.Settings.Default.current_animation;
            int index = settings_listbox_animations.FindStringExact(currentAnimation);
            if (index != ListBox.NoMatches)
            {
                settings_listbox_animations.SelectedIndex = index;
            }
        }

        private void settings_trackbar_animationinterval_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.animation_interval = settings_trackbar_animationinterval.Value;
            Properties.Settings.Default.Save();
        }

        private void populateListBox()
        {
            String[] directories = Directory.GetDirectories(basePath);
            foreach (String directory in directories)
            {
                var sections = directory.Split('\\');
                var folderName = sections[sections.Length - 1];
                settings_listbox_animations.Items.Add(folderName.ToUpper());
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settings_listbox_animations.SelectedItem != null)
            {
                Properties.Settings.Default.current_animation = settings_listbox_animations.SelectedItem?.ToString();
            }
        }
    }
}
