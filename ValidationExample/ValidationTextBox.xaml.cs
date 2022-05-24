using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValidationExample
{
    /// <summary>
    /// Interaction logic for ValidationTextBox.xaml
    /// </summary>
    public partial class ValidationTextBox : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(ValidationTextBox), new PropertyMetadata(default(string)));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        public static readonly DependencyProperty TextDataProperty =
            DependencyProperty.Register(nameof(TextData), typeof(string), typeof(ValidationTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextDataChanged));

        public string TextData
        {
            get { return (string)GetValue(TextDataProperty); }
            set { SetValue(TextDataProperty, value); }
        }

        public ValidationTextBox()
        {
            InitializeComponent();
        }

        private static void OnTextDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var userControl = (d as ValidationTextBox);
            BindingExpression? bindingExpression = userControl?.GetBindingExpression(TextDataProperty);
            if (bindingExpression is null)
            {
                return;
            }

            userControl?.OnTextDataChanged(bindingExpression.HasError, Validation.GetErrors(userControl).FirstOrDefault());
        }

        private void OnTextDataChanged(bool hasError, ValidationError? validationError)
        {
            BindingExpression bindingExpression = internalTextBox.GetBindingExpression(TextBox.TextProperty);
            if (hasError)
            {
                validationError = new ValidationError(new DummyValidationRule(), bindingExpression, validationError?.ErrorContent, validationError?.Exception);
                Validation.MarkInvalid(bindingExpression, validationError);
            }
            else
            {
                Validation.ClearInvalid(bindingExpression);
            }
        }

        private class DummyValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo) => throw new NotSupportedException();
        }
    }
}
