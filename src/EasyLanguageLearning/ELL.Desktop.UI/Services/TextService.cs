using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services
{
    public class TextService : ITextService
    {
        private string text = "Hello world DI!!!";
        public string GetText()
        {
            return text;
        }
    }
}
