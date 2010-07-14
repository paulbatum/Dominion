namespace Dominion
{
    partial class GameForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbHand = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTurn = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBuyOf = new System.Windows.Forms.Label();
            this.lblActions = new System.Windows.Forms.Label();
            this.lblBuys = new System.Windows.Forms.Label();
            this.btnEndTurn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBuyStep = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbTurn.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnBuyStep);
            this.splitContainer1.Panel1.Controls.Add(this.btnEndTurn);
            this.splitContainer1.Panel1.Controls.Add(this.gbTurn);
            this.splitContainer1.Panel1.Controls.Add(this.lbHand);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(604, 320);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbHand
            // 
            this.lbHand.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbHand.FormattingEnabled = true;
            this.lbHand.Location = new System.Drawing.Point(10, 23);
            this.lbHand.Name = "lbHand";
            this.lbHand.Size = new System.Drawing.Size(190, 147);
            this.lbHand.TabIndex = 1;
            this.lbHand.DoubleClick += new System.EventHandler(this.lbHand_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Hand:";
            // 
            // gbTurn
            // 
            this.gbTurn.Controls.Add(this.lblBuys);
            this.gbTurn.Controls.Add(this.lblActions);
            this.gbTurn.Controls.Add(this.lblBuyOf);
            this.gbTurn.Controls.Add(this.label4);
            this.gbTurn.Controls.Add(this.label3);
            this.gbTurn.Controls.Add(this.label2);
            this.gbTurn.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTurn.Location = new System.Drawing.Point(10, 170);
            this.gbTurn.Name = "gbTurn";
            this.gbTurn.Size = new System.Drawing.Size(190, 100);
            this.gbTurn.TabIndex = 2;
            this.gbTurn.TabStop = false;
            this.gbTurn.Text = "This turn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Buy of:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Buys:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Actions remaining:";
            // 
            // lblBuyOf
            // 
            this.lblBuyOf.AutoSize = true;
            this.lblBuyOf.Location = new System.Drawing.Point(126, 20);
            this.lblBuyOf.Name = "lblBuyOf";
            this.lblBuyOf.Size = new System.Drawing.Size(13, 13);
            this.lblBuyOf.TabIndex = 3;
            this.lblBuyOf.Text = "0";
            // 
            // lblActions
            // 
            this.lblActions.AutoSize = true;
            this.lblActions.Location = new System.Drawing.Point(126, 42);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(13, 13);
            this.lblActions.TabIndex = 4;
            this.lblActions.Text = "0";
            // 
            // lblBuys
            // 
            this.lblBuys.AutoSize = true;
            this.lblBuys.Location = new System.Drawing.Point(126, 66);
            this.lblBuys.Name = "lblBuys";
            this.lblBuys.Size = new System.Drawing.Size(13, 13);
            this.lblBuys.TabIndex = 5;
            this.lblBuys.Text = "0";
            // 
            // btnEndTurn
            // 
            this.btnEndTurn.Location = new System.Drawing.Point(125, 276);
            this.btnEndTurn.Name = "btnEndTurn";
            this.btnEndTurn.Size = new System.Drawing.Size(75, 23);
            this.btnEndTurn.TabIndex = 3;
            this.btnEndTurn.Text = "End Turn";
            this.btnEndTurn.UseVisualStyleBackColor = true;
            this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(390, 320);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnBuyStep
            // 
            this.btnBuyStep.Location = new System.Drawing.Point(10, 276);
            this.btnBuyStep.Name = "btnBuyStep";
            this.btnBuyStep.Size = new System.Drawing.Size(75, 23);
            this.btnBuyStep.TabIndex = 4;
            this.btnBuyStep.Text = "Do Buys";
            this.btnBuyStep.UseVisualStyleBackColor = true;
            this.btnBuyStep.Click += new System.EventHandler(this.btnBuyStep_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 320);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GameForm";
            this.Text = "Dominion";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gbTurn.ResumeLayout(false);
            this.gbTurn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbHand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTurn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBuys;
        private System.Windows.Forms.Label lblActions;
        private System.Windows.Forms.Label lblBuyOf;
        private System.Windows.Forms.Button btnEndTurn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnBuyStep;

    }
}

