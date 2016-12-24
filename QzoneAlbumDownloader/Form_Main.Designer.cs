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
            this.TabControl_Main = new MaterialSkin.Controls.MaterialTabControl();
            this.TabPage_Detect = new System.Windows.Forms.TabPage();
            this.Panel_Detect = new System.Windows.Forms.Panel();
            this.Text_Detect_Number = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Button_Detect_Enter = new MaterialSkin.Controls.MaterialFlatButton();
            this.ProcessBar_Detect = new MaterialSkin.Controls.MaterialProcessBar();
            this.TabPage_Login = new System.Windows.Forms.TabPage();
            this.WebBrowser_Login = new System.Windows.Forms.WebBrowser();
            this.TabControl_Main.SuspendLayout();
            this.TabPage_Detect.SuspendLayout();
            this.Panel_Detect.SuspendLayout();
            this.TabPage_Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl_Main
            // 
            this.TabControl_Main.Controls.Add(this.TabPage_Detect);
            this.TabControl_Main.Controls.Add(this.TabPage_Login);
            this.TabControl_Main.Depth = 0;
            this.TabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl_Main.Location = new System.Drawing.Point(0, 0);
            this.TabControl_Main.Margin = new System.Windows.Forms.Padding(2);
            this.TabControl_Main.MouseState = MaterialSkin.MouseState.HOVER;
            this.TabControl_Main.Name = "TabControl_Main";
            this.TabControl_Main.SelectedIndex = 0;
            this.TabControl_Main.Size = new System.Drawing.Size(971, 664);
            this.TabControl_Main.TabIndex = 0;
            this.TabControl_Main.SelectedIndexChanged += new System.EventHandler(this.TabControl_Main_SelectedIndexChanged);
            // 
            // TabPage_Detect
            // 
            this.TabPage_Detect.BackColor = System.Drawing.Color.White;
            this.TabPage_Detect.Controls.Add(this.Panel_Detect);
            this.TabPage_Detect.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Detect.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage_Detect.Name = "TabPage_Detect";
            this.TabPage_Detect.Padding = new System.Windows.Forms.Padding(2);
            this.TabPage_Detect.Size = new System.Drawing.Size(963, 634);
            this.TabPage_Detect.TabIndex = 0;
            this.TabPage_Detect.Text = "TabPage_Detect";
            // 
            // Panel_Detect
            // 
            this.Panel_Detect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Panel_Detect.Controls.Add(this.Text_Detect_Number);
            this.Panel_Detect.Controls.Add(this.Button_Detect_Enter);
            this.Panel_Detect.Controls.Add(this.ProcessBar_Detect);
            this.Panel_Detect.Location = new System.Drawing.Point(52, 258);
            this.Panel_Detect.Name = "Panel_Detect";
            this.Panel_Detect.Size = new System.Drawing.Size(857, 142);
            this.Panel_Detect.TabIndex = 2;
            // 
            // Text_Detect_Number
            // 
            this.Text_Detect_Number.Depth = 0;
            this.Text_Detect_Number.ErrorModeColor = System.Drawing.Color.Red;
            this.Text_Detect_Number.ErrorModeString = "";
            this.Text_Detect_Number.FollowLabel = null;
            this.Text_Detect_Number.FollowLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Text_Detect_Number.FollowLabeloldColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Text_Detect_Number.ForeColor = System.Drawing.Color.Black;
            this.Text_Detect_Number.Hint = "目标QQ号";
            this.Text_Detect_Number.IsErrorMode = false;
            this.Text_Detect_Number.Location = new System.Drawing.Point(3, 33);
            this.Text_Detect_Number.MaxLength = 32767;
            this.Text_Detect_Number.MouseState = MaterialSkin.MouseState.HOVER;
            this.Text_Detect_Number.Name = "Text_Detect_Number";
            this.Text_Detect_Number.PasswordChar = '\0';
            this.Text_Detect_Number.ReadOlay = false;
            this.Text_Detect_Number.SelectedText = "";
            this.Text_Detect_Number.SelectionLength = 0;
            this.Text_Detect_Number.SelectionStart = 0;
            this.Text_Detect_Number.Size = new System.Drawing.Size(708, 37);
            this.Text_Detect_Number.TabIndex = 3;
            this.Text_Detect_Number.TabStop = false;
            this.Text_Detect_Number.UseSystemPasswordChar = false;
            this.Text_Detect_Number.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Detect_Number_KeyDown);
            // 
            // Button_Detect_Enter
            // 
            this.Button_Detect_Enter.Depth = 0;
            this.Button_Detect_Enter.Location = new System.Drawing.Point(718, 27);
            this.Button_Detect_Enter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Button_Detect_Enter.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Detect_Enter.Name = "Button_Detect_Enter";
            this.Button_Detect_Enter.Primary = false;
            this.Button_Detect_Enter.Size = new System.Drawing.Size(135, 45);
            this.Button_Detect_Enter.TabIndex = 2;
            this.Button_Detect_Enter.Text = "访问";
            this.Button_Detect_Enter.UseVisualStyleBackColor = true;
            this.Button_Detect_Enter.Click += new System.EventHandler(this.Button_Detect_Enter_Click);
            // 
            // ProcessBar_Detect
            // 
            this.ProcessBar_Detect.Depth = 0;
            this.ProcessBar_Detect.Interval = 5;
            this.ProcessBar_Detect.LengthValue = 300;
            this.ProcessBar_Detect.Location = new System.Drawing.Point(3, 76);
            this.ProcessBar_Detect.MouseState = MaterialSkin.MouseState.HOVER;
            this.ProcessBar_Detect.Name = "ProcessBar_Detect";
            this.ProcessBar_Detect.Processing = true;
            this.ProcessBar_Detect.Size = new System.Drawing.Size(850, 5);
            this.ProcessBar_Detect.StepValue = 7;
            this.ProcessBar_Detect.TabIndex = 1;
            this.ProcessBar_Detect.Visible = false;
            // 
            // TabPage_Login
            // 
            this.TabPage_Login.BackColor = System.Drawing.Color.White;
            this.TabPage_Login.Controls.Add(this.WebBrowser_Login);
            this.TabPage_Login.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Login.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage_Login.Name = "TabPage_Login";
            this.TabPage_Login.Padding = new System.Windows.Forms.Padding(2);
            this.TabPage_Login.Size = new System.Drawing.Size(963, 634);
            this.TabPage_Login.TabIndex = 1;
            this.TabPage_Login.Text = "TabPage_Login";
            // 
            // WebBrowser_Login
            // 
            this.WebBrowser_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser_Login.Location = new System.Drawing.Point(2, 2);
            this.WebBrowser_Login.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser_Login.Name = "WebBrowser_Login";
            this.WebBrowser_Login.Size = new System.Drawing.Size(959, 630);
            this.WebBrowser_Login.TabIndex = 0;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(971, 664);
            this.Controls.Add(this.TabControl_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Main";
            this.Text = "Form_Main";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.TabControl_Main.ResumeLayout(false);
            this.TabPage_Detect.ResumeLayout(false);
            this.Panel_Detect.ResumeLayout(false);
            this.TabPage_Login.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl TabControl_Main;
        private System.Windows.Forms.TabPage TabPage_Detect;
        private System.Windows.Forms.TabPage TabPage_Login;
        private System.Windows.Forms.Panel Panel_Detect;
        private MaterialSkin.Controls.MaterialFlatButton Button_Detect_Enter;
        private MaterialSkin.Controls.MaterialProcessBar ProcessBar_Detect;
        private System.Windows.Forms.WebBrowser WebBrowser_Login;
        private MaterialSkin.Controls.MaterialSingleLineTextField Text_Detect_Number;
    }
}

