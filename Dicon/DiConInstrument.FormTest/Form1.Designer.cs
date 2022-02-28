
namespace DiconInstrument.FormTest
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GpibDevicesTreeView = new System.Windows.Forms.TreeView();
            this.BtnScanRx = new System.Windows.Forms.Button();
            this.BtnScanNormal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GpibDevicesTreeView
            // 
            this.GpibDevicesTreeView.Location = new System.Drawing.Point(3, 1);
            this.GpibDevicesTreeView.Name = "GpibDevicesTreeView";
            this.GpibDevicesTreeView.Size = new System.Drawing.Size(277, 449);
            this.GpibDevicesTreeView.TabIndex = 0;
            // 
            // BtnScanRx
            // 
            this.BtnScanRx.Location = new System.Drawing.Point(296, 12);
            this.BtnScanRx.Name = "BtnScanRx";
            this.BtnScanRx.Size = new System.Drawing.Size(114, 23);
            this.BtnScanRx.TabIndex = 1;
            this.BtnScanRx.Text = "ScanRxMethod";
            this.BtnScanRx.UseVisualStyleBackColor = true;
            this.BtnScanRx.Click += new System.EventHandler(this.Scan_Click);
            // 
            // BtnScanNormal
            // 
            this.BtnScanNormal.Location = new System.Drawing.Point(296, 42);
            this.BtnScanNormal.Name = "BtnScanNormal";
            this.BtnScanNormal.Size = new System.Drawing.Size(114, 23);
            this.BtnScanNormal.TabIndex = 2;
            this.BtnScanNormal.Text = "ScanNormalMethod";
            this.BtnScanNormal.UseVisualStyleBackColor = true;
            this.BtnScanNormal.Click += new System.EventHandler(this.BtnScanNormal_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnScanNormal);
            this.Controls.Add(this.BtnScanRx);
            this.Controls.Add(this.GpibDevicesTreeView);
            this.Name = "MainForm";
            this.Text = "GpibScanApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView GpibDevicesTreeView;
        private System.Windows.Forms.Button BtnScanRx;
        private System.Windows.Forms.Button BtnScanNormal;
    }
}

