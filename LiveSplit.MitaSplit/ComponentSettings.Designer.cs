using System.Drawing;
using System.Windows.Forms;

namespace LiveSplit.MitaSplit
{
    partial class ComponentSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnableAutoSplitCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.GameEndSecondTriggerCheckBox = new System.Windows.Forms.CheckBox();
            this.ILAutoStartCheckbox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EnableAutoResetCheckbox = new System.Windows.Forms.CheckBox();
            this.EnableAutoStartCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableAutoSplitCheckbox
            // 
            this.EnableAutoSplitCheckbox.AutoSize = true;
            this.EnableAutoSplitCheckbox.Checked = true;
            this.EnableAutoSplitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableAutoSplitCheckbox.Location = new System.Drawing.Point(6, 19);
            this.EnableAutoSplitCheckbox.Name = "EnableAutoSplitCheckbox";
            this.EnableAutoSplitCheckbox.Size = new System.Drawing.Size(104, 17);
            this.EnableAutoSplitCheckbox.TabIndex = 6;
            this.EnableAutoSplitCheckbox.Text = "Enable AutoSplit";
            this.EnableAutoSplitCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InfoLabel);
            this.groupBox1.Controls.Add(this.GameEndSecondTriggerCheckBox);
            this.groupBox1.Controls.Add(this.EnableAutoSplitCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 335);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AutoSplit";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.BackColor = System.Drawing.SystemColors.Control;
            this.InfoLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.InfoLabel.Location = new System.Drawing.Point(4, 296);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(185, 13);
            this.InfoLabel.TabIndex = 8;
            this.InfoLabel.Text = "NORMAL GAME ENDING RELATED";
            // 
            // GameEndSecondTriggerCheckBox
            // 
            this.GameEndSecondTriggerCheckBox.AutoSize = true;
            this.GameEndSecondTriggerCheckBox.Location = new System.Drawing.Point(6, 312);
            this.GameEndSecondTriggerCheckBox.Name = "GameEndSecondTriggerCheckBox";
            this.GameEndSecondTriggerCheckBox.Size = new System.Drawing.Size(180, 17);
            this.GameEndSecondTriggerCheckBox.TabIndex = 7;
            this.GameEndSecondTriggerCheckBox.Text = "End run on 2nd trigger activation";
            this.GameEndSecondTriggerCheckBox.UseVisualStyleBackColor = true;
            // 
            // ILAutoStartCheckbox
            // 
            this.ILAutoStartCheckbox.AutoSize = true;
            this.ILAutoStartCheckbox.Location = new System.Drawing.Point(7, 42);
            this.ILAutoStartCheckbox.Name = "ILAutoStartCheckbox";
            this.ILAutoStartCheckbox.Size = new System.Drawing.Size(115, 17);
            this.ILAutoStartCheckbox.TabIndex = 8;
            this.ILAutoStartCheckbox.Text = "AutoStart for levels";
            this.ILAutoStartCheckbox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 411);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ILAutoStartCheckbox);
            this.groupBox2.Controls.Add(this.EnableAutoResetCheckbox);
            this.groupBox2.Controls.Add(this.EnableAutoStartCheckbox);
            this.groupBox2.Location = new System.Drawing.Point(3, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 65);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AutoStart";
            // 
            // EnableAutoResetCheckbox
            // 
            this.EnableAutoResetCheckbox.AutoSize = true;
            this.EnableAutoResetCheckbox.Checked = true;
            this.EnableAutoResetCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableAutoResetCheckbox.Location = new System.Drawing.Point(127, 19);
            this.EnableAutoResetCheckbox.Name = "EnableAutoResetCheckbox";
            this.EnableAutoResetCheckbox.Size = new System.Drawing.Size(76, 17);
            this.EnableAutoResetCheckbox.TabIndex = 1;
            this.EnableAutoResetCheckbox.Text = "AutoReset";
            this.EnableAutoResetCheckbox.UseVisualStyleBackColor = true;
            // 
            // EnableAutoStartCheckbox
            // 
            this.EnableAutoStartCheckbox.AutoSize = true;
            this.EnableAutoStartCheckbox.Checked = true;
            this.EnableAutoStartCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableAutoStartCheckbox.Location = new System.Drawing.Point(7, 20);
            this.EnableAutoStartCheckbox.Name = "EnableAutoStartCheckbox";
            this.EnableAutoStartCheckbox.Size = new System.Drawing.Size(70, 17);
            this.EnableAutoStartCheckbox.TabIndex = 0;
            this.EnableAutoStartCheckbox.Text = "AutoStart";
            this.EnableAutoStartCheckbox.UseVisualStyleBackColor = true;
            // 
            // ComponentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ComponentSettings";
            this.Size = new System.Drawing.Size(222, 418);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox EnableAutoSplitCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox EnableAutoResetCheckbox;
        private System.Windows.Forms.CheckBox EnableAutoStartCheckbox;
        private CheckBox ILAutoStartCheckbox;
        private CheckBox GameEndSecondTriggerCheckBox;
        private Label InfoLabel;
    }
}
