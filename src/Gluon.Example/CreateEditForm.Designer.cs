using Gluon.Validation;
namespace Gluon.Example
{
    partial class CreateEditForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ValidationProvider = new Gluon.Validation.ValidationProvider();
            this.NumberValidator = new Gluon.Validation.RegexValidator();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.EmailValidator = new Gluon.Validation.RegexValidator();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 20);
            this.textBox1.TabIndex = 0;
            this.ValidationProvider.SetValidator(this.textBox1, this.NumberValidator);
            // 
            // ValidationProvider
            // 
            this.ValidationProvider.ValidationMode = Gluon.Validation.ValidationMode.Manual;
            // 
            // NumberValidator
            // 
            this.NumberValidator.ErrorMessage = "This is a numeric value.";
            this.NumberValidator.Expression = "^\\d+$";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(68, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(159, 20);
            this.textBox2.TabIndex = 1;
            this.ValidationProvider.SetValidator(this.textBox2, this.EmailValidator);
            // 
            // EmailValidator
            // 
            this.EmailValidator.ErrorMessage = "This is an e-mail value.";
            this.EmailValidator.Expression = "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$";
            this.EmailValidator.IgnoreCase = true;
            // 
            // CreateEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "CreateEditForm";
            this.Text = "CreateEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private ValidationProvider ValidationProvider;
        private System.Windows.Forms.TextBox textBox2;
        private RegexValidator NumberValidator;
        private RegexValidator EmailValidator;

    }
}