using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationExample
{
    public partial class MainViewModel : ObservableValidator
    {
        [ObservableProperty]
        [Required]
        [MinLength(2)]
        [MaxLength(4)]
        private string _firstName;
    }
}
