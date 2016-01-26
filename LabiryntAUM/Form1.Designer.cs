namespace LabiryntAUM
{
    partial class Form1
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
            this.drawingArea = new System.Windows.Forms.PictureBox();
            this.btDraw = new System.Windows.Forms.Button();
            this.btn_przejdz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // drawingArea
            // 
            this.drawingArea.Location = new System.Drawing.Point(118, 12);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(620, 614);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.TabStop = false;
            // 
            // btDraw
            // 
            this.btDraw.Location = new System.Drawing.Point(12, 12);
            this.btDraw.Name = "btDraw";
            this.btDraw.Size = new System.Drawing.Size(100, 23);
            this.btDraw.TabIndex = 1;
            this.btDraw.Text = "Rysuj labirynt";
            this.btDraw.UseVisualStyleBackColor = true;
            this.btDraw.Click += new System.EventHandler(this.btDraw_Click);
            // 
            // btn_przejdz
            // 
            this.btn_przejdz.Location = new System.Drawing.Point(13, 42);
            this.btn_przejdz.Name = "btn_przejdz";
            this.btn_przejdz.Size = new System.Drawing.Size(99, 23);
            this.btn_przejdz.TabIndex = 2;
            this.btn_przejdz.Text = "Przejdź labirynt";
            this.btn_przejdz.UseVisualStyleBackColor = true;
            this.btn_przejdz.Click += new System.EventHandler(this.btn_przejdz_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 638);
            this.Controls.Add(this.btn_przejdz);
            this.Controls.Add(this.btDraw);
            this.Controls.Add(this.drawingArea);
            this.Name = "Form1";
            this.Text = "Labirynt";
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox drawingArea;
        private System.Windows.Forms.Button btDraw;
        private System.Windows.Forms.Button btn_przejdz;
    }
}

