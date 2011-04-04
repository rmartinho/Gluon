using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gluon.Validation
{
    [ToolboxItemFilter("System.Windows.Forms")]
    [ToolboxBitmap(typeof(ErrorProvider))]
    [ProvideProperty("Validator", typeof(Control))]
    //[ProvideProperty("Error", typeof(Control))]
    //[ProvideProperty("IconAlignment", typeof(Control))]
    //[ProvideProperty("IconPadding", typeof(Control))]
    public class ValidationProvider : Component, IExtenderProvider
    {
        bool IExtenderProvider.CanExtend(object extendee)
        {
            return extendee is Control;
        }

        [DefaultValue(ValidationMode.Automatic)]
        [Category("Behavior")]
        [Description("When to do validation: either automatically or only when manually invoked.")]
        public ValidationMode ValidationMode
        {
            get { return validationMode; }
            set
            {
                if (value == ValidationMode.Automatic)
                {
                    foreach (var control in validators.Keys)
                    {
                        AttachValidation(control);
                    }
                }
                else if (value == ValidationMode.Manual)
                {
                    foreach (var control in validators.Keys)
                    {
                        DetachValidation(control);
                    }
                }
                else
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ValidationMode));
                }
                validationMode = value;
            }
        }
        private ValidationMode validationMode;

        [Category("Behavior")]
        [Description("The validator for this control.")]
        public IControlValidator GetValidator(Control control)
        {
            IControlValidator validator;
            if (!validators.TryGetValue(control, out validator))
            {
                return AlwaysTrueValidator.Instance;
            }
            return validator;
        }
        public void SetValidator(Control control, IControlValidator validator)
        {
            if (validator == null)
            {
                if (validators.Remove(control))
                {
                    DetachValidation(control);
                }
            }
            else
            {
                validators[control] = validator;
                AttachValidation(control);
            }
        }
        private readonly IDictionary<Control, IControlValidator> validators = new Dictionary<Control, IControlValidator>();

        private void AttachValidation(Control control)
        {
            control.Validating += Validate;
        }
        private void DetachValidation(Control control)
        {
            control.Validating -= Validate;
        }
        private void Validate(object sender, CancelEventArgs e)
        {
            var control = (Control)sender;
            var result = validators[control].Validate(control);
            if (!result.IsValid)
            {
                MessageBox.Show(result.ErrorMessage);
                //e.Cancel = true;
            }
        }

        public void ValidateAll()
        {
            foreach (var item in validators)
            {
                var control = item.Key;
                var validator = item.Value;
                validator.Validate(control);
            }
        }
    }
    
    public enum ValidationMode { Automatic, Manual }
}
