using System;
using System.Drawing;   // Para desenhar formas, linhas, textos
using System.Windows.Forms; // Para criar a janela

public class GrafoForm : Form
{
    // Método que desenha na tela
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Objeto para desenhar
        Graphics g = e.Graphics;

        // Caneta (cor preta, espessura 2px) -> usada para linhas e bordas
        Pen caneta = new Pen(Color.Black, 2);

        // Cor de preenchimento dos vértices
        Brush brush = Brushes.LightBlue;

        // Fonte para escrever o número do vértice
        Font fonte = new Font("Arial", 10);

        // Posições fixas dos vértices (x, y)
        Point[] vertices = {

           //VÉRTICES GRAFO 1 

            new Point(100, 100),  // vértice 1
            new Point(250, 100),  // vértice 2
            new Point(175, 200),   // vértice 

           //VÉRTICES GRAFO 2

            new Point(300, 300),   // vértice 4
            new Point(450, 300),   // vértice 5
            new Point(375, 400),   // vértice 6

           //VÉRTICES GRAFO 3

            new Point(500, 500),   // vértice 7
            new Point(650, 500),   // vértice 8
            new Point(575, 600),   // vértice 9

           //VÉRTICES GRAFO 4

            new Point(700, 300),   // vértice 10
            new Point(850, 300),   // vértice 11
            new Point(775, 400),   // vértice 12
                     
           //VÉRTICES GRAFO 5

            new Point(950, 100),   // vértice 10
            new Point(1100, 100),   // vértice 11
            new Point(1025, 200)   // vértice 12                  
        };

        // ----------- DESENHAR ARESTAS (linhas entre vértices) -----------

       
       // LIGAÇÕES GRAFO 1 ===================================================
        // Linha entre o vértice 1 e o vértice 2
        g.DrawLine(caneta, vertices[0], vertices[1]);

        // Linha entre o vértice 2 e o vértice 3
        g.DrawLine(caneta, vertices[1], vertices[2]);

        // Linha entre o vértice 3 e o vértice 1
        g.DrawLine(caneta, vertices[2], vertices[0]);

       // LIGAÇÕES GRAFO 2 ===================================================       
        // Linha entre o vértice 4 e o vértice 5
        g.DrawLine(caneta, vertices[3], vertices[4]);

        // Linha entre o vértice 5 e o vértice 6
        g.DrawLine(caneta, vertices[4], vertices[5]);

        // Linha entre o vértice 6 e o vértice 4
        g.DrawLine(caneta, vertices[5], vertices[3]);

        // LIGAÇÕES GRAFO 3 ===================================================       
        // Linha entre o vértice 7 e o vértice 8
        g.DrawLine(caneta, vertices[6], vertices[7]);

        // Linha entre o vértice 8 e o vértice 9
        g.DrawLine(caneta, vertices[7], vertices[8]);

        // Linha entre o vértice 9 e o vértice 7
        g.DrawLine(caneta, vertices[8], vertices[6]);

       // LIGAÇÕES GRAFO 4 ===================================================       
        // Linha entre o vértice 10 e o vértice 11
        g.DrawLine(caneta, vertices[9], vertices[10]);

        // Linha entre o vértice 11 e o vértice 12
        g.DrawLine(caneta, vertices[10], vertices[11]);

        // Linha entre o vértice 12 e o vértice 10
        g.DrawLine(caneta, vertices[11], vertices[9]);

       //LIGAÇÕES GRAFO 5 ===================================================       
        // Linha entre o vértice 13 e o vértice 14
        g.DrawLine(caneta, vertices[12], vertices[13]);

        // Linha entre o vértice 14 e o vértice 15
        g.DrawLine(caneta, vertices[13], vertices[14]);

        // Linha entre o vértice 15 e o vértice 13
        g.DrawLine(caneta, vertices[14], vertices[12]);
        
        // ----------- DESENHAR VÉRTICES (bolinhas com número) -----------

        for (int i = 0; i < vertices.Length; i++)
        {
            // Desenha círculo preenchido (o vértice)
            g.FillEllipse(brush, vertices[i].X - 20, vertices[i].Y - 20, 40, 40);

            // Desenha a borda do círculo
            g.DrawEllipse(caneta, vertices[i].X - 20, vertices[i].Y - 20, 40, 40);

            // Escreve o número do vértice dentro do círculo
            g.DrawString((i + 1).ToString(), fonte, Brushes.Black, vertices[i].X - 10, vertices[i].Y - 10);
        }
    }

    // Método principal -> abre a janela
    [STAThread]
    static void Main()
    {
        Application.Run(new GrafoForm());
    }


}
