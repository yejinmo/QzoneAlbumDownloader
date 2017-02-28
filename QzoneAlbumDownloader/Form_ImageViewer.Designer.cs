namespace QzoneAlbumDownloader
{
    partial class Form_ImageViewer
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
            this.components = new System.ComponentModel.Container();
            this.Panel_Control = new System.Windows.Forms.Panel();
            this.Button_Control_Save = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Control_ZoomIn = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Control_ZoomOut = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Control_Rotate = new MaterialSkin.Controls.MaterialFlatButton();
            this.Timer_Fade = new System.Windows.Forms.Timer(this.components);
            this.ProcessBar_LoadImage = new MaterialSkin.Controls.MaterialProcessBar();
            this.Button_Control_Close = new MaterialSkin.Controls.MaterialFlatButton();
            this.Timer_Panel = new System.Windows.Forms.Timer(this.components);
            this.Timer_GetCurPos = new System.Windows.Forms.Timer(this.components);
            this.Panel_Control.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Control
            // 
            this.Panel_Control.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Panel_Control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Panel_Control.Controls.Add(this.Button_Control_Save);
            this.Panel_Control.Controls.Add(this.Button_Control_ZoomIn);
            this.Panel_Control.Controls.Add(this.Button_Control_ZoomOut);
            this.Panel_Control.Controls.Add(this.Button_Control_Rotate);
            this.Panel_Control.Location = new System.Drawing.Point(302, 563);
            this.Panel_Control.Name = "Panel_Control";
            this.Panel_Control.Size = new System.Drawing.Size(298, 50);
            this.Panel_Control.TabIndex = 13;
            // 
            // Button_Control_Save
            // 
            this.Button_Control_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Control_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Control_Save.Depth = 0;
            this.Button_Control_Save.DrawHoverMode = false;
            this.Button_Control_Save.DrawImageMode = true;
            this.Button_Control_Save.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_save_black_48dp;
            this.Button_Control_Save.Location = new System.Drawing.Point(200, 0);
            this.Button_Control_Save.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Control_Save.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Control_Save.Name = "Button_Control_Save";
            this.Button_Control_Save.Primary = false;
            this.Button_Control_Save.Size = new System.Drawing.Size(50, 50);
            this.Button_Control_Save.TabIndex = 15;
            this.Button_Control_Save.Text = "后退";
            this.Button_Control_Save.UseVisualStyleBackColor = false;
            // 
            // Button_Control_ZoomIn
            // 
            this.Button_Control_ZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Control_ZoomIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Control_ZoomIn.Depth = 0;
            this.Button_Control_ZoomIn.DrawHoverMode = false;
            this.Button_Control_ZoomIn.DrawImageMode = true;
            this.Button_Control_ZoomIn.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_zoom_in_black_48dp;
            this.Button_Control_ZoomIn.Location = new System.Drawing.Point(150, 0);
            this.Button_Control_ZoomIn.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Control_ZoomIn.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Control_ZoomIn.Name = "Button_Control_ZoomIn";
            this.Button_Control_ZoomIn.Primary = false;
            this.Button_Control_ZoomIn.Size = new System.Drawing.Size(50, 50);
            this.Button_Control_ZoomIn.TabIndex = 14;
            this.Button_Control_ZoomIn.Text = "后退";
            this.Button_Control_ZoomIn.UseVisualStyleBackColor = false;
            // 
            // Button_Control_ZoomOut
            // 
            this.Button_Control_ZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Control_ZoomOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Control_ZoomOut.Depth = 0;
            this.Button_Control_ZoomOut.DrawHoverMode = false;
            this.Button_Control_ZoomOut.DrawImageMode = true;
            this.Button_Control_ZoomOut.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_zoom_out_black_48dp;
            this.Button_Control_ZoomOut.Location = new System.Drawing.Point(100, 0);
            this.Button_Control_ZoomOut.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Control_ZoomOut.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Control_ZoomOut.Name = "Button_Control_ZoomOut";
            this.Button_Control_ZoomOut.Primary = false;
            this.Button_Control_ZoomOut.Size = new System.Drawing.Size(50, 50);
            this.Button_Control_ZoomOut.TabIndex = 13;
            this.Button_Control_ZoomOut.Text = "后退";
            this.Button_Control_ZoomOut.UseVisualStyleBackColor = false;
            // 
            // Button_Control_Rotate
            // 
            this.Button_Control_Rotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Control_Rotate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Control_Rotate.Depth = 0;
            this.Button_Control_Rotate.DrawHoverMode = false;
            this.Button_Control_Rotate.DrawImageMode = true;
            this.Button_Control_Rotate.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_settings_backup_restore_black_48dp;
            this.Button_Control_Rotate.Location = new System.Drawing.Point(50, 0);
            this.Button_Control_Rotate.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Control_Rotate.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Control_Rotate.Name = "Button_Control_Rotate";
            this.Button_Control_Rotate.Primary = false;
            this.Button_Control_Rotate.Size = new System.Drawing.Size(50, 50);
            this.Button_Control_Rotate.TabIndex = 12;
            this.Button_Control_Rotate.Text = "后退";
            this.Button_Control_Rotate.UseVisualStyleBackColor = false;
            // 
            // Timer_Fade
            // 
            this.Timer_Fade.Interval = 10;
            this.Timer_Fade.Tick += new System.EventHandler(this.Timer_Fade_Tick);
            // 
            // ProcessBar_LoadImage
            // 
            this.ProcessBar_LoadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessBar_LoadImage.Depth = 0;
            this.ProcessBar_LoadImage.Interval = 10;
            this.ProcessBar_LoadImage.LengthValue = 300;
            this.ProcessBar_LoadImage.Location = new System.Drawing.Point(12, 296);
            this.ProcessBar_LoadImage.MouseState = MaterialSkin.MouseState.HOVER;
            this.ProcessBar_LoadImage.Name = "ProcessBar_LoadImage";
            this.ProcessBar_LoadImage.Processing = true;
            this.ProcessBar_LoadImage.Size = new System.Drawing.Size(878, 5);
            this.ProcessBar_LoadImage.StepValue = 7;
            this.ProcessBar_LoadImage.TabIndex = 17;
            // 
            // Button_Control_Close
            // 
            this.Button_Control_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Control_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Control_Close.Depth = 0;
            this.Button_Control_Close.DrawHoverMode = false;
            this.Button_Control_Close.DrawImageMode = true;
            this.Button_Control_Close.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_highlight_off_black_48dp;
            this.Button_Control_Close.Location = new System.Drawing.Point(843, 9);
            this.Button_Control_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Control_Close.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Control_Close.Name = "Button_Control_Close";
            this.Button_Control_Close.Primary = false;
            this.Button_Control_Close.Size = new System.Drawing.Size(50, 50);
            this.Button_Control_Close.TabIndex = 16;
            this.Button_Control_Close.Text = "后退";
            this.Button_Control_Close.UseVisualStyleBackColor = false;
            this.Button_Control_Close.Click += new System.EventHandler(this.Button_Control_Close_Click);
            // 
            // Timer_Panel
            // 
            this.Timer_Panel.Interval = 5;
            this.Timer_Panel.Tick += new System.EventHandler(this.Timer_Panel_Tick);
            // 
            // Timer_GetCurPos
            // 
            this.Timer_GetCurPos.Enabled = true;
            this.Timer_GetCurPos.Interval = 10;
            this.Timer_GetCurPos.Tick += new System.EventHandler(this.Timer_GetCurPos_Tick);
            // 
            // Form_ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(902, 613);
            this.Controls.Add(this.ProcessBar_LoadImage);
            this.Controls.Add(this.Button_Control_Close);
            this.Controls.Add(this.Panel_Control);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_ImageViewer";
            this.Text = "图片查看器";
            this.Load += new System.EventHandler(this.Form_ImageViewer_Load);
            this.Panel_Control.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton Button_Control_Rotate;
        private System.Windows.Forms.Panel Panel_Control;
        private MaterialSkin.Controls.MaterialFlatButton Button_Control_Save;
        private MaterialSkin.Controls.MaterialFlatButton Button_Control_ZoomIn;
        private MaterialSkin.Controls.MaterialFlatButton Button_Control_ZoomOut;
        private MaterialSkin.Controls.MaterialFlatButton Button_Control_Close;
        private System.Windows.Forms.Timer Timer_Fade;
        private MaterialSkin.Controls.MaterialProcessBar ProcessBar_LoadImage;
        private System.Windows.Forms.Timer Timer_Panel;
        private System.Windows.Forms.Timer Timer_GetCurPos;
    }
}