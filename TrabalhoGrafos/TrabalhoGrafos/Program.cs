using System;
using System.Windows.Forms;

namespace TrabalhoGrafos
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Inicia a aplicação com a classe do formulário Form1
            Application.Run(new Form1()); 
        }
    }
}