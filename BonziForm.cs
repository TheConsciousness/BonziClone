using BonziClone;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BonziClone
{
    public class BonziForm : Form
    {
        private Settings settingsForm = new Settings();
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private ContextMenuStrip rightClickMenu;
        private PictureBox spriteBox;
        private System.Windows.Forms.Timer animationTimer;
        private Image? spriteSheet;
        private int frameIndex = 0;
        private int frameWidth = 202;
        private int frameHeight = 162;
        private int animationInterval = 100;
        private int totalFrames;

        private bool isDragging = false;
        private Point lastCursorPos;

        private bool loopAnimation = true;
        private string basePath = "..\\..\\..\\BonziSpriteSheet\\"; 

        public enum BonziAnimations
        {
            BONZI_IDLE,
            BONZI_WAITING,
            BONZI_BACKFLIP,
            BONZI_BOWING,
            BONZI_CHEST_BEATING,
            BONZI_GLOBE_SPIN,
            BONZI_SCRATCHING_HEAD,
            BONZI_SUNGLASSES,
            BONZI_SURF_AWAY,
            BONZI_SURF_UP,
            BONZI_WAVING
        }

        public BonziForm()
        {
            // Setup Right-Click animation menu
            rightClickMenu = new ContextMenuStrip();
            foreach (BonziAnimations animation in Enum.GetValues(typeof(BonziAnimations)))
            {
                rightClickMenu.Items.Add(animation.ToString());
            }
            rightClickMenu.ItemClicked += RightClickMenu_ItemClicked;

            // Setup NotifyIcon
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Reset Position", null, ResetPosition);
            trayMenu.Items.Add("Settings", null, OpenSettings);
            trayMenu.Items.Add("Exit", null, OnExit);
            trayIcon = new NotifyIcon()
            {
                Text = "Bonzi Clone",
                Icon = SystemIcons.Information,
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            spriteBox = new PictureBox()
            {
                Size = new Size(frameWidth, frameHeight),
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };
            spriteBox.MouseDown += BonziMouseDown;
            spriteBox.MouseMove += DragBonzi;
            spriteBox.MouseUp += StopDragging;
            Controls.Add(spriteBox);

            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Location = new Point(100, 0);
            TransparencyKey = Color.Magenta;
            BackColor = Color.Magenta;
            Size = new Size(frameWidth, frameHeight);

            // Setup animation timer
            animationTimer = new System.Windows.Forms.Timer { Interval = animationInterval };
            animationTimer.Tick += AnimateSprite;
            animationTimer.Start();

            AnimateBonzi(BonziAnimations.BONZI_WAVING, true);
        }

        private void RightClickMenu_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            if (Enum.TryParse(e.ClickedItem?.ToString(), out BonziAnimations animation))
            {
                AnimateBonzi(animation, true);
            }
        }

        public void AnimateBonzi(BonziAnimations animation, bool loop)
        {
            // Build file path
            string filePath = $"{basePath}{animation.ToString().ToLower()}\\spritesheet.png";

            try
            {
                // Load new sprite sheet
                spriteSheet = Image.FromFile(filePath);
                totalFrames = spriteSheet.Width / frameWidth; // Calculate frames dynamically
                frameIndex = 0;
                loopAnimation = loop;

                // Restart animation
                animationTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading animation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AnimateSprite(object? sender, EventArgs e)
        {
            if (spriteSheet == null) return;

            // Get current frame
            Rectangle sourceRect = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
            Bitmap frame = new Bitmap(frameWidth, frameHeight);
            using (Graphics g = Graphics.FromImage(frame))
            {
                g.DrawImage(spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight), sourceRect, GraphicsUnit.Pixel);
            }

            spriteBox.Image = frame;

            // Advance frame index
            frameIndex++;

            // Handle looping or stopping animation
            if (frameIndex >= totalFrames)
            {
                if (loopAnimation)
                    frameIndex = 0; // Loop animation
                else
                    animationTimer.Stop(); // Stop if looping is disabled
            }
        }
        private void OnExit(object? sender, EventArgs e)
        {
            animationTimer.Stop();
            trayIcon.Visible = false;
            Application.Exit();
        }
        private void BonziMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightClickMenu.Show(this, e.Location);
            }
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = e.Location;
            }
        }
        private void DragBonzi(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int dx = e.X - lastCursorPos.X;
                int dy = e.Y - lastCursorPos.Y;
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }
        private void StopDragging(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        private void ResetPosition(object? sender, EventArgs e)
        {
            isDragging = false;
            Location = new Point(200, 200);
        }
        private void OpenSettings(object? sender, EventArgs e)
        {
            string previousAnimation = Properties.Settings.Default.current_animation;
            TopMost = false;
            settingsForm.ShowDialog();
            if (Properties.Settings.Default.current_animation != previousAnimation)
            {
                BonziAnimations animation;
                if (Enum.TryParse(Properties.Settings.Default.current_animation, out animation))
                {
                    AnimateBonzi(animation, true);
                }
                else
                {
                    Console.WriteLine("Invalid animation name!");
                }
            }
            TopMost = true;
        }
    }
}