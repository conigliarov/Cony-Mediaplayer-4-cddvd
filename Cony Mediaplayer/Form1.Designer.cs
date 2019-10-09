using System.Drawing;

namespace WindowsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.matrixControl1 = new MatrixLib.MatrixControl();
            this.song = new MatrixLib.MatrixControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // matrixControl1
            // 
            this.matrixControl1.Direction = MatrixLib.ScrollDirection.Left;
            this.matrixControl1.Location = new System.Drawing.Point(291, 62);
            this.matrixControl1.Name = "matrixControl1";
            this.matrixControl1.Size = new System.Drawing.Size(832, 88);
            this.matrixControl1.TabIndex = 0;
            this.matrixControl1.Text = "matrixControl1";
            // 
            // song
            // 
            this.song.Direction = MatrixLib.ScrollDirection.Left;
            this.song.Location = new System.Drawing.Point(291, 106);
            this.song.Name = "song";
            this.song.Size = new System.Drawing.Size(832, 22);
            this.song.TabIndex = 2;
            this.song.Text = "Traccia";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::ConyMediaplayer.Properties.Resources.player;
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Location = new System.Drawing.Point(22, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 91);
            this.panel1.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel8.Location = new System.Drawing.Point(199, 13);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(22, 34);
            this.panel8.TabIndex = 10;
            this.panel8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel8_MouseClick);
            // 
            // panel7
            // 
            this.panel7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel7.Location = new System.Drawing.Point(105, 13);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(22, 34);
            this.panel7.TabIndex = 9;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel6.Location = new System.Drawing.Point(15, 14);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(34, 36);
            this.panel6.TabIndex = 8;
            this.panel6.Click += new System.EventHandler(this.panel6_Paint);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::ConyMediaplayer.Properties.Resources.x;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(621, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(79, 28);
            this.panel2.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel5.Location = new System.Drawing.Point(2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 24);
            this.panel5.TabIndex = 7;
            this.panel5.Click += new System.EventHandler(this.panel5_Paint);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel4.Location = new System.Drawing.Point(28, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 24);
            this.panel4.TabIndex = 6;
            this.panel4.Click += new System.EventHandler(this.panel4_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel3.Location = new System.Drawing.Point(55, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 24);
            this.panel3.TabIndex = 5;
            this.panel3.Click += new System.EventHandler(this.panel3_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cony Mediaplayer - Sergio Cony";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(719, 335);
            this.Icon = ConyMediaplayer.Properties.Resources.Icon;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.song);
            this.Controls.Add(this.matrixControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cony Mediaplayer";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MDwn);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MatrixLib.MatrixControl matrixControl1;
        private MatrixLib.MatrixControl song;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;



    }
}

