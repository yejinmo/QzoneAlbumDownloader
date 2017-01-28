namespace QzoneAlbumDownloader
{
    partial class Form_QzoneLogin
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
            this.WebBrowser_Login = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // WebBrowser_Login
            // 
            this.WebBrowser_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser_Login.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser_Login.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser_Login.Name = "WebBrowser_Login";
            this.WebBrowser_Login.Size = new System.Drawing.Size(953, 669);
            this.WebBrowser_Login.TabIndex = 1;
            this.WebBrowser_Login.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_Login_DocumentCompleted);
            // 
            // Form_QzoneLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 669);
            this.Controls.Add(this.WebBrowser_Login);
            this.Name = "Form_QzoneLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请在本页面登录QQ空间";
            this.Shown += new System.EventHandler(this.Form_QzoneLogin_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowser_Login;
    }
}