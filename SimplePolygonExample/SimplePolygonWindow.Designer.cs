namespace SimplePolygonExample
{
    partial class SimplePolygonWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.polyBefore = new SimplePolygonExample.PolygonRenderer();
            this.polyAfter = new SimplePolygonExample.PolygonRenderer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "BEFORE TRIANGULATION";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(695, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "AFTER TRIANGULATION";
            // 
            // polyBefore
            // 
            this.polyBefore.Location = new System.Drawing.Point(12, 70);
            this.polyBefore.Name = "polyBefore";
            this.polyBefore.RenderMode = SimplePolygonExample.PolygonRenderer.Mode.Wireframe;
            this.polyBefore.Size = new System.Drawing.Size(432, 424);
            this.polyBefore.TabIndex = 2;
            // 
            // polyAfter
            // 
            this.polyAfter.Location = new System.Drawing.Point(608, 70);
            this.polyAfter.Name = "polyAfter";
            this.polyAfter.RenderMode = SimplePolygonExample.PolygonRenderer.Mode.Wireframe;
            this.polyAfter.Size = new System.Drawing.Size(432, 424);
            this.polyAfter.TabIndex = 3;
            // 
            // SimplePolygonWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 602);
            this.Controls.Add(this.polyAfter);
            this.Controls.Add(this.polyBefore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SimplePolygonWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Polygon Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private PolygonRenderer polyBefore;
        private PolygonRenderer polyAfter;
    }
}

