namespace BonziClone
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            settings_trackbar_animationinterval = new TrackBar();
            settings_label_animInterval = new Label();
            settings_listbox_animations = new ListBox();
            settings_label_animList = new Label();
            ((System.ComponentModel.ISupportInitialize)settings_trackbar_animationinterval).BeginInit();
            SuspendLayout();
            // 
            // settings_trackbar_animationinterval
            // 
            settings_trackbar_animationinterval.LargeChange = 10;
            settings_trackbar_animationinterval.Location = new Point(12, 27);
            settings_trackbar_animationinterval.Maximum = 500;
            settings_trackbar_animationinterval.Minimum = 10;
            settings_trackbar_animationinterval.Name = "settings_trackbar_animationinterval";
            settings_trackbar_animationinterval.Size = new Size(277, 45);
            settings_trackbar_animationinterval.SmallChange = 10;
            settings_trackbar_animationinterval.TabIndex = 0;
            settings_trackbar_animationinterval.TickFrequency = 10;
            settings_trackbar_animationinterval.Value = 100;
            settings_trackbar_animationinterval.Scroll += settings_trackbar_animationinterval_Scroll;
            // 
            // settings_label_animInterval
            // 
            settings_label_animInterval.AutoSize = true;
            settings_label_animInterval.Location = new Point(12, 9);
            settings_label_animInterval.Name = "settings_label_animInterval";
            settings_label_animInterval.Size = new Size(98, 15);
            settings_label_animInterval.TabIndex = 1;
            settings_label_animInterval.Text = "Animation Speed";
            // 
            // settings_listbox_animations
            // 
            settings_listbox_animations.FormattingEnabled = true;
            settings_listbox_animations.ItemHeight = 15;
            settings_listbox_animations.Location = new Point(12, 93);
            settings_listbox_animations.Name = "settings_listbox_animations";
            settings_listbox_animations.Size = new Size(277, 154);
            settings_listbox_animations.TabIndex = 2;
            // 
            // settings_label_animList
            // 
            settings_label_animList.AutoSize = true;
            settings_label_animList.Location = new Point(12, 75);
            settings_label_animList.Name = "settings_label_animList";
            settings_label_animList.Size = new Size(84, 15);
            settings_label_animList.TabIndex = 3;
            settings_label_animList.Text = "Animation List";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(299, 259);
            Controls.Add(settings_label_animList);
            Controls.Add(settings_listbox_animations);
            Controls.Add(settings_label_animInterval);
            Controls.Add(settings_trackbar_animationinterval);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            Text = "BonziClone Settings";
            FormClosing += Settings_FormClosing;
            Load += Settings_Load;
            ((System.ComponentModel.ISupportInitialize)settings_trackbar_animationinterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar settings_trackbar_animationinterval;
        private Label settings_label_animInterval;
        private ListBox settings_listbox_animations;
        private Label settings_label_animList;
    }
}