using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace BigApp.Classes
{
    public class Docs
    {
        void printer() 
        {
            var word = new Word.Application();

            var document = word.Documents.Open(Environment.CurrentDirectory + @"\" + "Template.docx");

            var table = document.Tables[1];
        }

    }
}
