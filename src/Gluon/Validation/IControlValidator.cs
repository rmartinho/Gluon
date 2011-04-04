using System.Windows.Forms;

namespace Gluon.Validation
{
    public interface IControlValidator
    {
        ValidationResult Validate(Control control);
    }
}