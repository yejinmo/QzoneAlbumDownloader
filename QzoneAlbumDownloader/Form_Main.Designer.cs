namespace QzoneAlbumDownloader
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TabControl_Main = new MaterialSkin.Controls.MaterialTabControl();
            this.TabPage_Detect = new System.Windows.Forms.TabPage();
            this.Panel_Detect = new System.Windows.Forms.Panel();
            this.Label_DetectName = new System.Windows.Forms.Label();
            this.Label_Detect_Tip = new System.Windows.Forms.Label();
            this.Button_Login = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Cancel = new MaterialSkin.Controls.MaterialFlatButton();
            this.Text_Detect_Number = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Button_Detect_Enter = new MaterialSkin.Controls.MaterialFlatButton();
            this.ProcessBar_Detect = new MaterialSkin.Controls.MaterialProcessBar();
            this.TabPage_Album = new System.Windows.Forms.TabPage();
            this.FlowLayoutPanel_Album = new System.Windows.Forms.FlowLayoutPanel();
            this.TipTool = new System.Windows.Forms.ToolTip(this.components);
            this.Label_Header_UserName = new System.Windows.Forms.Label();
            this.Timer_GetUserHeadIMG = new System.Windows.Forms.Timer(this.components);
            this.Panel_Header = new System.Windows.Forms.Panel();
            this.Button_Header_Back = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Header_UserIMG = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Header_Config = new MaterialSkin.Controls.MaterialFlatButton();
            this.Button_Detect_HeadIMG = new MaterialSkin.Controls.MaterialFlatButton();
            this.TabControl_Main.SuspendLayout();
            this.TabPage_Detect.SuspendLayout();
            this.Panel_Detect.SuspendLayout();
            this.TabPage_Album.SuspendLayout();
            this.Panel_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl_Main
            // 
            this.TabControl_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl_Main.Controls.Add(this.TabPage_Detect);
            this.TabControl_Main.Controls.Add(this.TabPage_Album);
            this.TabControl_Main.Depth = 0;
            this.TabControl_Main.Location = new System.Drawing.Point(0, 61);
            this.TabControl_Main.Margin = new System.Windows.Forms.Padding(2);
            this.TabControl_Main.MouseState = MaterialSkin.MouseState.HOVER;
            this.TabControl_Main.Name = "TabControl_Main";
            this.TabControl_Main.SelectedIndex = 0;
            this.TabControl_Main.Size = new System.Drawing.Size(1132, 682);
            this.TabControl_Main.TabIndex = 0;
            this.TabControl_Main.SelectedIndexChanged += new System.EventHandler(this.TabControl_Main_SelectedIndexChanged);
            // 
            // TabPage_Detect
            // 
            this.TabPage_Detect.AutoScroll = true;
            this.TabPage_Detect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.TabPage_Detect.Controls.Add(this.Panel_Detect);
            this.TabPage_Detect.ForeColor = System.Drawing.Color.White;
            this.TabPage_Detect.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Detect.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage_Detect.Name = "TabPage_Detect";
            this.TabPage_Detect.Padding = new System.Windows.Forms.Padding(2);
            this.TabPage_Detect.Size = new System.Drawing.Size(1124, 652);
            this.TabPage_Detect.TabIndex = 0;
            this.TabPage_Detect.Text = "TabPage_Detect";
            // 
            // Panel_Detect
            // 
            this.Panel_Detect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Panel_Detect.Controls.Add(this.Button_Detect_HeadIMG);
            this.Panel_Detect.Controls.Add(this.Label_DetectName);
            this.Panel_Detect.Controls.Add(this.Label_Detect_Tip);
            this.Panel_Detect.Controls.Add(this.Button_Login);
            this.Panel_Detect.Controls.Add(this.Button_Cancel);
            this.Panel_Detect.Controls.Add(this.Text_Detect_Number);
            this.Panel_Detect.Controls.Add(this.Button_Detect_Enter);
            this.Panel_Detect.Controls.Add(this.ProcessBar_Detect);
            this.Panel_Detect.Location = new System.Drawing.Point(125, 135);
            this.Panel_Detect.Name = "Panel_Detect";
            this.Panel_Detect.Size = new System.Drawing.Size(857, 311);
            this.Panel_Detect.TabIndex = 2;
            // 
            // Label_DetectName
            // 
            this.Label_DetectName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_DetectName.Location = new System.Drawing.Point(11, 116);
            this.Label_DetectName.Name = "Label_DetectName";
            this.Label_DetectName.Size = new System.Drawing.Size(839, 21);
            this.Label_DetectName.TabIndex = 8;
            this.Label_DetectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Detect_Tip
            // 
            this.Label_Detect_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Detect_Tip.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.Label_Detect_Tip.ForeColor = System.Drawing.Color.White;
            this.Label_Detect_Tip.Location = new System.Drawing.Point(3, 197);
            this.Label_Detect_Tip.Name = "Label_Detect_Tip";
            this.Label_Detect_Tip.Size = new System.Drawing.Size(850, 46);
            this.Label_Detect_Tip.TabIndex = 6;
            this.Label_Detect_Tip.Text = "Label_Detect_Tip";
            this.Label_Detect_Tip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Detect_Tip.Visible = false;
            // 
            // Button_Login
            // 
            this.Button_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Button_Login.Depth = 0;
            this.Button_Login.DrawHoverMode = true;
            this.Button_Login.DrawImageMode = false;
            this.Button_Login.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.Button_Login.ForeColor = System.Drawing.Color.White;
            this.Button_Login.Image = null;
            this.Button_Login.Location = new System.Drawing.Point(433, 257);
            this.Button_Login.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Login.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Primary = false;
            this.Button_Login.Size = new System.Drawing.Size(417, 45);
            this.Button_Login.TabIndex = 6;
            this.Button_Login.Text = "登录";
            this.Button_Login.UseVisualStyleBackColor = false;
            this.Button_Login.Visible = false;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Button_Cancel.Depth = 0;
            this.Button_Cancel.DrawHoverMode = true;
            this.Button_Cancel.DrawImageMode = false;
            this.Button_Cancel.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.Button_Cancel.ForeColor = System.Drawing.Color.White;
            this.Button_Cancel.Image = null;
            this.Button_Cancel.Location = new System.Drawing.Point(8, 257);
            this.Button_Cancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Cancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Primary = false;
            this.Button_Cancel.Size = new System.Drawing.Size(417, 45);
            this.Button_Cancel.TabIndex = 5;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = false;
            this.Button_Cancel.Visible = false;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Text_Detect_Number
            // 
            this.Text_Detect_Number.Depth = 0;
            this.Text_Detect_Number.ErrorModeColor = System.Drawing.Color.Red;
            this.Text_Detect_Number.ErrorModeString = "";
            this.Text_Detect_Number.FollowLabel = null;
            this.Text_Detect_Number.FollowLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Text_Detect_Number.FollowLabeloldColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Text_Detect_Number.ForeColor = System.Drawing.Color.White;
            this.Text_Detect_Number.Hint = "目标QQ号";
            this.Text_Detect_Number.IsErrorMode = false;
            this.Text_Detect_Number.Location = new System.Drawing.Point(8, 146);
            this.Text_Detect_Number.MaxLength = 32767;
            this.Text_Detect_Number.MouseState = MaterialSkin.MouseState.HOVER;
            this.Text_Detect_Number.Name = "Text_Detect_Number";
            this.Text_Detect_Number.PasswordChar = '\0';
            this.Text_Detect_Number.ReadOlay = false;
            this.Text_Detect_Number.SelectedText = "";
            this.Text_Detect_Number.SelectionLength = 0;
            this.Text_Detect_Number.SelectionStart = 0;
            this.Text_Detect_Number.Size = new System.Drawing.Size(703, 37);
            this.Text_Detect_Number.TabIndex = 3;
            this.Text_Detect_Number.TabStop = false;
            this.Text_Detect_Number.Text = "985189148";
            this.Text_Detect_Number.UseSystemPasswordChar = false;
            this.Text_Detect_Number.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Detect_Number_KeyDown);
            this.Text_Detect_Number.TextChanged += new System.EventHandler(this.Text_Detect_Number_TextChanged);
            // 
            // Button_Detect_Enter
            // 
            this.Button_Detect_Enter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Button_Detect_Enter.Depth = 0;
            this.Button_Detect_Enter.DrawHoverMode = true;
            this.Button_Detect_Enter.DrawImageMode = false;
            this.Button_Detect_Enter.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.Button_Detect_Enter.ForeColor = System.Drawing.Color.White;
            this.Button_Detect_Enter.Image = null;
            this.Button_Detect_Enter.Location = new System.Drawing.Point(718, 140);
            this.Button_Detect_Enter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Detect_Enter.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Detect_Enter.Name = "Button_Detect_Enter";
            this.Button_Detect_Enter.Primary = false;
            this.Button_Detect_Enter.Size = new System.Drawing.Size(132, 45);
            this.Button_Detect_Enter.TabIndex = 2;
            this.Button_Detect_Enter.Text = "访问";
            this.Button_Detect_Enter.UseVisualStyleBackColor = false;
            this.Button_Detect_Enter.Click += new System.EventHandler(this.Button_Detect_Enter_Click);
            // 
            // ProcessBar_Detect
            // 
            this.ProcessBar_Detect.Depth = 0;
            this.ProcessBar_Detect.Interval = 5;
            this.ProcessBar_Detect.LengthValue = 300;
            this.ProcessBar_Detect.Location = new System.Drawing.Point(8, 187);
            this.ProcessBar_Detect.MouseState = MaterialSkin.MouseState.HOVER;
            this.ProcessBar_Detect.Name = "ProcessBar_Detect";
            this.ProcessBar_Detect.Processing = true;
            this.ProcessBar_Detect.Size = new System.Drawing.Size(842, 5);
            this.ProcessBar_Detect.StepValue = 7;
            this.ProcessBar_Detect.TabIndex = 1;
            this.ProcessBar_Detect.Visible = false;
            // 
            // TabPage_Album
            // 
            this.TabPage_Album.AutoScroll = true;
            this.TabPage_Album.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.TabPage_Album.Controls.Add(this.FlowLayoutPanel_Album);
            this.TabPage_Album.ForeColor = System.Drawing.Color.White;
            this.TabPage_Album.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Album.Name = "TabPage_Album";
            this.TabPage_Album.Padding = new System.Windows.Forms.Padding(20);
            this.TabPage_Album.Size = new System.Drawing.Size(1124, 652);
            this.TabPage_Album.TabIndex = 1;
            this.TabPage_Album.Text = "TabPage_Album";
            this.TabPage_Album.Resize += new System.EventHandler(this.TabPage_Album_Resize);
            // 
            // FlowLayoutPanel_Album
            // 
            this.FlowLayoutPanel_Album.AutoScroll = true;
            this.FlowLayoutPanel_Album.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel_Album.Location = new System.Drawing.Point(20, 20);
            this.FlowLayoutPanel_Album.Name = "FlowLayoutPanel_Album";
            this.FlowLayoutPanel_Album.Size = new System.Drawing.Size(1084, 616);
            this.FlowLayoutPanel_Album.TabIndex = 0;
            // 
            // Label_Header_UserName
            // 
            this.Label_Header_UserName.AutoSize = true;
            this.Label_Header_UserName.BackColor = System.Drawing.Color.Transparent;
            this.Label_Header_UserName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Label_Header_UserName.ForeColor = System.Drawing.Color.White;
            this.Label_Header_UserName.Location = new System.Drawing.Point(86, 13);
            this.Label_Header_UserName.Name = "Label_Header_UserName";
            this.Label_Header_UserName.Size = new System.Drawing.Size(69, 25);
            this.Label_Header_UserName.TabIndex = 10;
            this.Label_Header_UserName.Text = "请登录";
            this.TipTool.SetToolTip(this.Label_Header_UserName, "点击左侧头像登录");
            // 
            // Timer_GetUserHeadIMG
            // 
            this.Timer_GetUserHeadIMG.Enabled = true;
            this.Timer_GetUserHeadIMG.Interval = 5;
            this.Timer_GetUserHeadIMG.Tick += new System.EventHandler(this.Timer_GetUserHeadIMG_Tick);
            // 
            // Panel_Header
            // 
            this.Panel_Header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Header.Controls.Add(this.Button_Header_Back);
            this.Panel_Header.Controls.Add(this.Label_Header_UserName);
            this.Panel_Header.Controls.Add(this.Button_Header_UserIMG);
            this.Panel_Header.Controls.Add(this.Button_Header_Config);
            this.Panel_Header.Location = new System.Drawing.Point(4, 9);
            this.Panel_Header.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.Panel_Header.Name = "Panel_Header";
            this.Panel_Header.Size = new System.Drawing.Size(1124, 50);
            this.Panel_Header.TabIndex = 1;
            // 
            // Button_Header_Back
            // 
            this.Button_Header_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Header_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Header_Back.Depth = 0;
            this.Button_Header_Back.DrawHoverMode = false;
            this.Button_Header_Back.DrawImageMode = true;
            this.Button_Header_Back.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_back_white_50px;
            this.Button_Header_Back.Location = new System.Drawing.Point(986, 0);
            this.Button_Header_Back.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Header_Back.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Header_Back.Name = "Button_Header_Back";
            this.Button_Header_Back.Primary = false;
            this.Button_Header_Back.Size = new System.Drawing.Size(50, 50);
            this.Button_Header_Back.TabIndex = 11;
            this.Button_Header_Back.Text = "后退";
            this.TipTool.SetToolTip(this.Button_Header_Back, "设置");
            this.Button_Header_Back.UseVisualStyleBackColor = false;
            // 
            // Button_Header_UserIMG
            // 
            this.Button_Header_UserIMG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Header_UserIMG.Depth = 0;
            this.Button_Header_UserIMG.DrawHoverMode = false;
            this.Button_Header_UserIMG.DrawImageMode = true;
            this.Button_Header_UserIMG.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_account_box_white_100px;
            this.Button_Header_UserIMG.Location = new System.Drawing.Point(29, 0);
            this.Button_Header_UserIMG.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Header_UserIMG.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Header_UserIMG.Name = "Button_Header_UserIMG";
            this.Button_Header_UserIMG.Primary = false;
            this.Button_Header_UserIMG.Size = new System.Drawing.Size(50, 50);
            this.Button_Header_UserIMG.TabIndex = 9;
            this.Button_Header_UserIMG.Text = "设置";
            this.TipTool.SetToolTip(this.Button_Header_UserIMG, "点击登录");
            this.Button_Header_UserIMG.UseVisualStyleBackColor = false;
            this.Button_Header_UserIMG.Click += new System.EventHandler(this.Button_Header_UserIMG_Click);
            // 
            // Button_Header_Config
            // 
            this.Button_Header_Config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Header_Config.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Header_Config.Depth = 0;
            this.Button_Header_Config.DrawHoverMode = false;
            this.Button_Header_Config.DrawImageMode = true;
            this.Button_Header_Config.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_settings_black_50px;
            this.Button_Header_Config.Location = new System.Drawing.Point(1044, 0);
            this.Button_Header_Config.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Header_Config.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Header_Config.Name = "Button_Header_Config";
            this.Button_Header_Config.Primary = false;
            this.Button_Header_Config.Size = new System.Drawing.Size(50, 50);
            this.Button_Header_Config.TabIndex = 3;
            this.Button_Header_Config.Text = "设置";
            this.TipTool.SetToolTip(this.Button_Header_Config, "设置");
            this.Button_Header_Config.UseVisualStyleBackColor = false;
            // 
            // Button_Detect_HeadIMG
            // 
            this.Button_Detect_HeadIMG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Button_Detect_HeadIMG.Depth = 0;
            this.Button_Detect_HeadIMG.DrawHoverMode = false;
            this.Button_Detect_HeadIMG.DrawImageMode = true;
            this.Button_Detect_HeadIMG.Image = global::QzoneAlbumDownloader.Properties.Resources.ic_account_box_white_100px;
            this.Button_Detect_HeadIMG.Location = new System.Drawing.Point(377, 8);
            this.Button_Detect_HeadIMG.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Detect_HeadIMG.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Detect_HeadIMG.Name = "Button_Detect_HeadIMG";
            this.Button_Detect_HeadIMG.Primary = false;
            this.Button_Detect_HeadIMG.Size = new System.Drawing.Size(100, 100);
            this.Button_Detect_HeadIMG.TabIndex = 10;
            this.Button_Detect_HeadIMG.Text = "设置";
            this.Button_Detect_HeadIMG.UseVisualStyleBackColor = false;
            this.Button_Detect_HeadIMG.Click += new System.EventHandler(this.Button_Detect_HeadIMG_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(1132, 743);
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.TabControl_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QzoneAlbumDownloader";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.TabControl_Main.ResumeLayout(false);
            this.TabPage_Detect.ResumeLayout(false);
            this.Panel_Detect.ResumeLayout(false);
            this.TabPage_Album.ResumeLayout(false);
            this.Panel_Header.ResumeLayout(false);
            this.Panel_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl TabControl_Main;
        private System.Windows.Forms.TabPage TabPage_Detect;
        private System.Windows.Forms.Panel Panel_Detect;
        private MaterialSkin.Controls.MaterialFlatButton Button_Detect_Enter;
        private MaterialSkin.Controls.MaterialProcessBar ProcessBar_Detect;
        private MaterialSkin.Controls.MaterialSingleLineTextField Text_Detect_Number;
        private MaterialSkin.Controls.MaterialFlatButton Button_Login;
        private MaterialSkin.Controls.MaterialFlatButton Button_Cancel;
        private System.Windows.Forms.TabPage TabPage_Album;
        private System.Windows.Forms.ToolTip TipTool;
        private System.Windows.Forms.Label Label_Detect_Tip;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Album;
        private System.Windows.Forms.Timer Timer_GetUserHeadIMG;
        private System.Windows.Forms.Label Label_DetectName;
        private System.Windows.Forms.Panel Panel_Header;
        private MaterialSkin.Controls.MaterialFlatButton Button_Header_Config;
        private MaterialSkin.Controls.MaterialFlatButton Button_Header_UserIMG;
        private System.Windows.Forms.Label Label_Header_UserName;
        private MaterialSkin.Controls.MaterialFlatButton Button_Detect_HeadIMG;
        private MaterialSkin.Controls.MaterialFlatButton Button_Header_Back;
    }
}

