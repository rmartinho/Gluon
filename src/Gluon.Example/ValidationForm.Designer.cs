using Gluon.Validation;
namespace Gluon.Example
{
    partial class ValidationForm
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
            Gluon.Validation.RegexValidator EmailValidator;
            this.CloseButton = new System.Windows.Forms.Button();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.NumberBox = new System.Windows.Forms.TextBox();
            this.ValidationProvider = new Gluon.Validation.ValidationProvider();
            this.NumberValidator = new Gluon.Validation.NumberRangeValidator();
            this.ValidationButton = new Gluon.Validation.ValidationButton();
            EmailValidator = new Gluon.Validation.RegexValidator();
            ((System.ComponentModel.ISupportInitialize)(this.ValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // EmailValidator
            // 
            EmailValidator.Expression = "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$";
            EmailValidator.IgnoreCase = true;
            EmailValidator.Message = "This field needs a valid e-mail address.";
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(87, 213);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // EmailBox
            // 
            this.EmailBox.CausesValidation = false;
            this.EmailBox.Location = new System.Drawing.Point(68, 112);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(159, 20);
            this.EmailBox.TabIndex = 1;
            this.ValidationProvider.SetValidator(this.EmailBox, EmailValidator);
            // 
            // NumberBox
            // 
            this.NumberBox.CausesValidation = false;
            this.NumberBox.Location = new System.Drawing.Point(68, 86);
            this.NumberBox.Name = "NumberBox";
            this.NumberBox.Size = new System.Drawing.Size(159, 20);
            this.NumberBox.TabIndex = 0;
            this.ValidationProvider.SetValidator(this.NumberBox, this.NumberValidator);
            // 
            // ValidationProvider
            // 
            this.ValidationProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.ValidationProvider.ContainerControl = this;
            // 
            // NumberValidator
            // 
            this.NumberValidator.Maximum = 100;
            this.NumberValidator.Minimum = 0;
            this.NumberValidator.OutOfRangeMessage = "Please use a number between 0 and 100.";
            // 
            // ValidationButton
            // 
            this.ValidationButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ValidationButton.Location = new System.Drawing.Point(197, 213);
            this.ValidationButton.Name = "ValidationButton";
            this.ValidationButton.Size = new System.Drawing.Size(75, 23);
            this.ValidationButton.TabIndex = 4;
            this.ValidationButton.Text = "OK";
            this.ValidationButton.UseVisualStyleBackColor = true;
            this.ValidationButton.ValidationProvider = this.ValidationProvider;
            // 
            // ValidationForm
            // 
            this.AcceptButton = this.ValidationButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ValidationButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.NumberBox);
            this.Name = "ValidationForm";
            this.Text = "CreateEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.ValidationProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NumberBox;
        private ValidationProvider ValidationProvider;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Button CloseButton;
        private ValidationButton ValidationButton;
        private NumberRangeValidator NumberValidator;

    }
}