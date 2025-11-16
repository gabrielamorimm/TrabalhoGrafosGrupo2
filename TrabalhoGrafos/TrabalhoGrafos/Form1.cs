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
            new Point(100, 100),  // vértice 1
            new Point(250, 100),  // vértice 2
            new Point(175, 200)   // vértice 3
        };

        // ----------- DESENHAR ARESTAS (linhas entre vértices) -----------

        // Linha entre o vértice 1 e o vértice 2
        g.DrawLine(caneta, vertices[0], vertices[1]);

        // Linha entre o vértice 2 e o vértice 3
        g.DrawLine(caneta, vertices[1], vertices[2]);

        // Linha entre o vértice 3 e o vértice 1
        g.DrawLine(caneta, vertices[2], vertices[0]);

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
