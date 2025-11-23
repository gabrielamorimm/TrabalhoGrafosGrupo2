using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrabalhoGrafos
{
    public partial class Form1 : Form
    {
        // Variáveis de Estado (para o grafo)
        private List<TextBox> nomeVertexTextBoxes = new List<TextBox>();
        private TextBox txtLigacoes;
        private Label labelLigacoes;
        
        // Variáveis que armazenarão os dados do grafo para desenho
        private List<string> verticesNomes;
        private List<(string, string)> arestas; 

        public Form1()
        {
            InitializeComponent();
            
            // Inicialização dos controles dinâmicos (para que existam antes de serem manipulados)
            this.txtLigacoes = new TextBox();
            this.labelLigacoes = new Label();
            
            this.txtLigacoes.Multiline = true;
            this.txtLigacoes.Size = new Size(300, 100);
            this.labelLigacoes.Text = "Ligações (Ex: V1-V2, V1-V3):";
            this.labelLigacoes.AutoSize = true;

            this.Controls.Add(this.txtLigacoes);
            this.Controls.Add(this.labelLigacoes);

            // Oculta os campos de ligação no início
            this.txtLigacoes.Visible = false;
            this.labelLigacoes.Visible = false;
        }

        /// <summary>
        /// Manipulador do botão "Confirmar Nomes". Cria campos de entrada dinamicamente.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Limpa controles antigos (para evitar duplicações se o usuário clicar mais de uma vez)
            foreach (var control in nomeVertexTextBoxes)
            {
                this.Controls.Remove(control);
            }
            nomeVertexTextBoxes.Clear();
            
            int numVertices = (int)this.numUpDownQuantidade.Value;
            int yPos = 60; 

            // Loop para Criar os campos de nome
            for (int i = 0; i < numVertices; i++)
            {
                Label lbl = new Label();
                lbl.Text = $"Nome do Vértice {i + 1}:";
                lbl.Location = new Point(20, yPos);
                this.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Name = $"txtNomeVertice_{i}"; 
                txt.Location = new Point(150, yPos);
                this.Controls.Add(txt);
                
                nomeVertexTextBoxes.Add(txt);

                yPos += 30;
            }

            // Posiciona e mostra a área de Ligações
            this.labelLigacoes.Location = new Point(20, yPos + 10);
            this.txtLigacoes.Location = new Point(20, yPos + 35);
            this.txtLigacoes.Visible = true;
            this.labelLigacoes.Visible = true;
            
            // Mostra o Botão de Animar
            this.btnAnimar.Location = new Point(20, yPos + 140); // Reposiciona o botão
            this.btnAnimar.Visible = true;
        }

        /// <summary>
        /// Manipulador do botão "Gerar Grafo e Animar". Coleta os dados e inicia o desenho.
        /// </summary>
        private void btnAnimar_Click(object sender, EventArgs e)
        {
            // 1. Coleta os Nomes dos Vértices
            this.verticesNomes = new List<string>();
            foreach (TextBox txt in nomeVertexTextBoxes)
            {
                this.verticesNomes.Add(txt.Text.Trim()); 
            }

            // 2. Coleta e Processa as Ligações
            string ligacoesTexto = this.txtLigacoes.Text.Replace(" ", "");
            this.arestas = new List<(string, string)>();
            
            string[] pares = ligacoesTexto.Split(',');
            foreach (string par in pares)
            {
                string[] no = par.Split('-');
                if (no.Length == 2)
                {
                    this.arestas.Add((no[0].Trim(), no[1].Trim()));
                }
            }
            
            // Força o PictureBox a chamar o método Paint para desenhar o grafo
            this.pictureBoxGrafo.Invalidate(); 
        }

        /// <summary>
        /// Onde a mágica acontece: Desenha os vértices e arestas no PictureBox.
        /// </summary>
        private void pictureBoxGrafo_Paint(object sender, PaintEventArgs e)
        {
            if (this.verticesNomes == null || this.verticesNomes.Count == 0)
            {
                return;
            }

            Graphics g = e.Graphics;
            Pen penAresta = new Pen(Color.Black, 2);
            SolidBrush brushVertice = new SolidBrush(Color.Red);
            SolidBrush brushTexto = new SolidBrush(Color.Black);
            Font fontTexto = new Font("Arial", 10);
            
            int raio = 15; 
            
            // Cálculo do Layout Circular
            int centroX = this.pictureBoxGrafo.Width / 2;
            int centroY = this.pictureBoxGrafo.Height / 2;
            int raioLayout = Math.Min(centroX, centroY) - 50; 
            
            Dictionary<string, Point> posicoes = new Dictionary<string, Point>();
            
            // 1. Posiciona os Vértices em Círculo
            for (int i = 0; i < verticesNomes.Count; i++)
            {
                double angulo = 2 * Math.PI * i / verticesNomes.Count;
                int x = (int)(centroX + raioLayout * Math.Cos(angulo));
                int y = (int)(centroY + raioLayout * Math.Sin(angulo));
                posicoes.Add(verticesNomes[i], new Point(x, y));
            }
            
            // 2. Desenha as Arestas
            foreach (var aresta in this.arestas)
            {
                if (posicoes.ContainsKey(aresta.Item1) && posicoes.ContainsKey(aresta.Item2))
                {
                    Point p1 = posicoes[aresta.Item1];
                    Point p2 = posicoes[aresta.Item2];
                    g.DrawLine(penAresta, p1, p2);
                }
            }
            
            // 3. Desenha os Vértices (Círculos e Nomes)
            foreach (var kvp in posicoes)
            {
                Point centro = kvp.Value;
                string nome = kvp.Key;
                
                // Desenha o círculo
                g.FillEllipse(brushVertice, centro.X - raio, centro.Y - raio, raio * 2, raio * 2);
                g.DrawEllipse(penAresta, centro.X - raio, centro.Y - raio, raio * 2, raio * 2);

                // Desenha o nome
                SizeF tamanhoTexto = g.MeasureString(nome, fontTexto);
                g.DrawString(nome, fontTexto, brushTexto, centro.X - tamanhoTexto.Width / 2, centro.Y - tamanhoTexto.Height / 2);
            }
            
            penAresta.Dispose();
            brushVertice.Dispose();
            brushTexto.Dispose();
        }
    }
}