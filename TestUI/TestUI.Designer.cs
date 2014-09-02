namespace TestUI
{
    partial class TestUI
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
            if(disposing && (components != null))
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
            this.bStartServer = new System.Windows.Forms.Button();
            this.bStartBot = new System.Windows.Forms.Button();
            this.tBotCommand = new System.Windows.Forms.TextBox();
            this.bStopBot = new System.Windows.Forms.Button();
            this.tBotSay = new System.Windows.Forms.TextBox();
            this.tBotAction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tBotJoin = new System.Windows.Forms.TextBox();
            this.bWeapon = new System.Windows.Forms.Button();
            this.tWeapon = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bStartServer
            // 
            this.bStartServer.Location = new System.Drawing.Point(12, 12);
            this.bStartServer.Name = "bStartServer";
            this.bStartServer.Size = new System.Drawing.Size(98, 23);
            this.bStartServer.TabIndex = 0;
            this.bStartServer.Text = "Start Server";
            this.bStartServer.UseVisualStyleBackColor = true;
            this.bStartServer.Click += new System.EventHandler(this.bStartServer_Click);
            // 
            // bStartBot
            // 
            this.bStartBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStartBot.Location = new System.Drawing.Point(12, 214);
            this.bStartBot.Name = "bStartBot";
            this.bStartBot.Size = new System.Drawing.Size(75, 23);
            this.bStartBot.TabIndex = 1;
            this.bStartBot.Text = "StartBot";
            this.bStartBot.UseVisualStyleBackColor = true;
            this.bStartBot.Click += new System.EventHandler(this.bStartBot_Click);
            // 
            // tBotCommand
            // 
            this.tBotCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBotCommand.Location = new System.Drawing.Point(93, 371);
            this.tBotCommand.Name = "tBotCommand";
            this.tBotCommand.Size = new System.Drawing.Size(357, 20);
            this.tBotCommand.TabIndex = 3;
            this.tBotCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBotCommand_KeyDown);
            // 
            // bStopBot
            // 
            this.bStopBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStopBot.Location = new System.Drawing.Point(93, 214);
            this.bStopBot.Name = "bStopBot";
            this.bStopBot.Size = new System.Drawing.Size(75, 23);
            this.bStopBot.TabIndex = 4;
            this.bStopBot.Text = "EndBot";
            this.bStopBot.UseVisualStyleBackColor = true;
            this.bStopBot.Click += new System.EventHandler(this.bStopBot_Click);
            // 
            // tBotSay
            // 
            this.tBotSay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBotSay.Location = new System.Drawing.Point(93, 305);
            this.tBotSay.Name = "tBotSay";
            this.tBotSay.Size = new System.Drawing.Size(357, 20);
            this.tBotSay.TabIndex = 6;
            this.tBotSay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBotSay_KeyDown);
            // 
            // tBotAction
            // 
            this.tBotAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBotAction.Location = new System.Drawing.Point(93, 331);
            this.tBotAction.Name = "tBotAction";
            this.tBotAction.Size = new System.Drawing.Size(357, 20);
            this.tBotAction.TabIndex = 7;
            this.tBotAction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBotAction_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "say";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "action";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "cmd";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "join";
            // 
            // tBotJoin
            // 
            this.tBotJoin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBotJoin.Location = new System.Drawing.Point(93, 279);
            this.tBotJoin.Name = "tBotJoin";
            this.tBotJoin.Size = new System.Drawing.Size(357, 20);
            this.tBotJoin.TabIndex = 11;
            this.tBotJoin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBotJoin_KeyDown);
            // 
            // bWeapon
            // 
            this.bWeapon.Location = new System.Drawing.Point(12, 83);
            this.bWeapon.Name = "bWeapon";
            this.bWeapon.Size = new System.Drawing.Size(108, 23);
            this.bWeapon.TabIndex = 13;
            this.bWeapon.Text = "Random Weapon";
            this.bWeapon.UseVisualStyleBackColor = true;
            this.bWeapon.Click += new System.EventHandler(this.bWeapon_Click);
            // 
            // tWeapon
            // 
            this.tWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tWeapon.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tWeapon.Location = new System.Drawing.Point(126, 83);
            this.tWeapon.Name = "tWeapon";
            this.tWeapon.Size = new System.Drawing.Size(324, 111);
            this.tWeapon.TabIndex = 14;
            this.tWeapon.Text = "";
            // 
            // TestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 424);
            this.Controls.Add(this.tWeapon);
            this.Controls.Add(this.bWeapon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tBotJoin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBotAction);
            this.Controls.Add(this.tBotSay);
            this.Controls.Add(this.bStopBot);
            this.Controls.Add(this.tBotCommand);
            this.Controls.Add(this.bStartBot);
            this.Controls.Add(this.bStartServer);
            this.Name = "TestUI";
            this.Text = "TestUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button bStartServer;
        private System.Windows.Forms.Button bStartBot;
        private System.Windows.Forms.TextBox tBotCommand;
        private System.Windows.Forms.Button bStopBot;
        private System.Windows.Forms.TextBox tBotSay;
        private System.Windows.Forms.TextBox tBotAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBotJoin;
        private System.Windows.Forms.Button bWeapon;
        private System.Windows.Forms.RichTextBox tWeapon;
    }
}
