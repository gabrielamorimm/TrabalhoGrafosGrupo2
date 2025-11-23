using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TrabalhoGrafos
{
    public partial class Form1 : Form
    {
        private Grafo grafoAtual;
        
        // --- Variáveis de Animação ---
        // CORREÇÃO AQUI: Especificamos explicitamente que é o Timer do Windows Forms
        private System.Windows.Forms.Timer timerAnimacao;
        
        private int verticesVisiveis = 0;
        private int arestasVisiveis = 0;
        private bool animandoVertices = true; 
        
        // Variáveis visuais
        private List<Aresta> arestasDestacadas = new List<Aresta>();
        private Color corDestaque = Color.Blue;

        public Form1()
        {
            InitializeComponent();
            grafoAtual = new Grafo();

            // Configuração do Timer de Animação
            // CORREÇÃO AQUI TAMBÉM
            timerAnimacao = new System.Windows.Forms.Timer();
            timerAnimacao.Interval = 100; // Velocidade (quanto menor, mais rápido)
            timerAnimacao.Tick += TimerAnimacao_Tick;
        }

        // --- LÓGICA DA ANIMAÇÃO ---
        private void IniciarAnimacao()
        {
            verticesVisiveis = 0;
            arestasVisiveis = 0;
            animandoVertices = true;
            arestasDestacadas.Clear();
            timerAnimacao.Start();
        }

        private void TimerAnimacao_Tick(object sender, EventArgs e)
        {
            // Fase 1: Animar Vértices
            if (animandoVertices)
            {
                if (verticesVisiveis < grafoAtual.Vertices.Count)
                {
                    verticesVisiveis++;
                }
                else
                {
                    animandoVertices = false;
                }
            }
            // Fase 2: Animar Arestas
            else
            {
                if (arestasVisiveis < grafoAtual.Arestas.Count)
                {
                    arestasVisiveis++;
                }
                else
                {
                    timerAnimacao.Stop();
                }
            }
            pictureBoxGrafo.Invalidate();
        }

        private void PararAnimacaoEExibirTudo()
        {
            timerAnimacao.Stop();
            verticesVisiveis = grafoAtual.Vertices.Count;
            arestasVisiveis = grafoAtual.Arestas.Count;
            pictureBoxGrafo.Invalidate();
        }

        // --- 1. GERAR ALEATÓRIO ---
        private void btnGerarAleatorio_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumVertices.Text, out int n) || n < 2)
            {
                MessageBox.Show("Por favor, digite um número válido de vértices (mínimo 2).");
                return;
            }

            bool dirigido = chkDirigido.Checked;
            grafoAtual.GerarAleatorio(n, 20, dirigido);
            
            AtualizarCombos();
            txtLog.Text = $"Gerando Grafo Aleatório com {n} vértices...";
            IniciarAnimacao();
        }

        // --- 2. GERAR MANUAL ---
        private void btnGerarManual_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumVertices.Text, out int n) || n < 2)
            {
                MessageBox.Show("Defina a quantidade de vértices primeiro.");
                return;
            }

            grafoAtual.Vertices.Clear();
            grafoAtual.Arestas.Clear();
            grafoAtual.EhDirigido = chkDirigido.Checked;

            for (int i = 1; i <= n; i++) grafoAtual.Vertices.Add($"V{i}");

            string entrada = txtArestasManual.Text;
            if (string.IsNullOrWhiteSpace(entrada))
            {
                MessageBox.Show("Digite as ligações (ex: 1-2:5, 2-3).");
                return;
            }

            string[] pares = entrada.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var item in pares)
            {
                string[] dadosPeso = item.Split(':');
                string conexao = dadosPeso[0];
                int peso = 1;

                if (dadosPeso.Length > 1) int.TryParse(dadosPeso[1], out peso);

                string[] nos = conexao.Split('-');
                if (nos.Length == 2)
                {
                    string o = nos[0].Trim().ToUpper().StartsWith("V") ? nos[0].Trim() : "V" + nos[0].Trim();
                    string d = nos[1].Trim().ToUpper().StartsWith("V") ? nos[1].Trim() : "V" + nos[1].Trim();

                    if (grafoAtual.Vertices.Contains(o) && grafoAtual.Vertices.Contains(d))
                    {
                        grafoAtual.Arestas.Add(new Aresta(o, d, peso)); 
                    }
                }
            }

            AtualizarCombos();
            txtLog.Text = "Construindo Grafo Manual...";
            IniciarAnimacao();
        }

        // --- ALGORITMOS ---
        private void btnAGM_Click(object sender, EventArgs e)
        {
            if (grafoAtual.Vertices.Count == 0) return;
            PararAnimacaoEExibirTudo();

            var agm = grafoAtual.ObterAGM();
            arestasDestacadas = agm;
            corDestaque = Color.Green;
            int pesoTotal = agm.Sum(a => a.Peso);
            txtLog.AppendText($"\r\nAGM Calculada. Peso Total: {pesoTotal}");
            pictureBoxGrafo.Invalidate();
        }

        private void btnCaminho_Click(object sender, EventArgs e)
        {
            if (cmbOrigem.SelectedItem == null || cmbDestino.SelectedItem == null) return;
            PararAnimacaoEExibirTudo();

            string inicio = cmbOrigem.SelectedItem.ToString();
            string fim = cmbDestino.SelectedItem.ToString();

            var caminho = grafoAtual.Dijkstra(inicio, fim);
            
            if (caminho.Count > 0 || inicio == fim)
            {
                arestasDestacadas = caminho;
                corDestaque = Color.Red;
                int custo = caminho.Sum(a => a.Peso);
                txtLog.AppendText($"\r\nCaminho {inicio}->{fim}: Custo {custo}");
            }
            else
            {
                txtLog.AppendText($"\r\nSem caminho entre {inicio} e {fim}.");
                arestasDestacadas.Clear();
            }
            pictureBoxGrafo.Invalidate();
        }

        private void btnDFS_Click(object sender, EventArgs e)
        {
             if (cmbOrigem.SelectedItem == null) return;
             PararAnimacaoEExibirTudo();

             string inicio = cmbOrigem.SelectedItem.ToString();
             HashSet<string> visitados = new HashSet<string>();
             List<string> ordem = new List<string>();
             grafoAtual.DFS(inicio, visitados, ordem);
             txtLog.AppendText($"\r\nDFS/Fecho de {inicio}: " + string.Join(" -> ", ordem));
             arestasDestacadas.Clear();
             pictureBoxGrafo.Invalidate();
        }

        private void btnBFS_Click(object sender, EventArgs e)
        {
            if (cmbOrigem.SelectedItem == null) return;
            PararAnimacaoEExibirTudo();

            string inicio = cmbOrigem.SelectedItem.ToString();
            var visitados = grafoAtual.BFS(inicio);
            txtLog.AppendText($"\r\nBFS de {inicio}: " + string.Join(" -> ", visitados));
        }

        private void AtualizarCombos()
        {
            cmbOrigem.Items.Clear();
            cmbDestino.Items.Clear();
            foreach(var v in grafoAtual.Vertices)
            {
                cmbOrigem.Items.Add(v);
                cmbDestino.Items.Add(v);
            }
            if (cmbOrigem.Items.Count > 0) cmbOrigem.SelectedIndex = 0;
            if (cmbDestino.Items.Count > 1) cmbDestino.SelectedIndex = 1;
        }

        // --- DESENHO ---
        private void pictureBoxGrafo_Paint(object sender, PaintEventArgs e)
        {
            if (grafoAtual == null || grafoAtual.Vertices.Count == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen penNormal = new Pen(Color.LightGray, 2);
            Pen penDestaque = new Pen(corDestaque, 4);

            if (grafoAtual.EhDirigido)
            {
                AdjustableArrowCap seta = new AdjustableArrowCap(6, 6, true); 
                penNormal.CustomEndCap = seta;
                penDestaque.CustomEndCap = seta;
            }
            else
            {
                penNormal.EndCap = LineCap.Flat;
                penDestaque.EndCap = LineCap.Flat;
            }

            Brush brushVertice = new SolidBrush(Color.SteelBlue);
            Brush brushTexto = Brushes.White;
            Font font = new Font("Arial", 10, FontStyle.Bold);

            int raio = 20;
            int centroX = pictureBoxGrafo.Width / 2;
            int centroY = pictureBoxGrafo.Height / 2;
            int raioGrafo = Math.Min(centroX, centroY) - 50;

            Dictionary<string, Point> posicoes = new Dictionary<string, Point>();
            for (int i = 0; i < grafoAtual.Vertices.Count; i++)
            {
                double angulo = 2 * Math.PI * i / grafoAtual.Vertices.Count;
                int x = (int)(centroX + raioGrafo * Math.Cos(angulo));
                int y = (int)(centroY + raioGrafo * Math.Sin(angulo));
                posicoes.Add(grafoAtual.Vertices[i], new Point(x, y));
            }

            // Arestas
            if (!animandoVertices || (animandoVertices == false)) 
            {
                int limiteArestas = Math.Min(arestasVisiveis, grafoAtual.Arestas.Count);
                for (int i = 0; i < limiteArestas; i++)
                {
                    var aresta = grafoAtual.Arestas[i];
                    if (!posicoes.ContainsKey(aresta.Origem) || !posicoes.ContainsKey(aresta.Destino)) continue;

                    Point p1 = posicoes[aresta.Origem];
                    Point p2Original = posicoes[aresta.Destino];
                    Point p2Ajustado = p2Original;

                    if (grafoAtual.EhDirigido && p1 != p2Original)
                    {
                        float dx = p2Original.X - p1.X;
                        float dy = p2Original.Y - p1.Y;
                        float distancia = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distancia > raio)
                        {
                            float t = (distancia - raio) / distancia;
                            p2Ajustado = new Point((int)(p1.X + dx * t), (int)(p1.Y + dy * t));
                        }
                    }

                    bool destaque = arestasDestacadas.Exists(a => a == aresta || (!grafoAtual.EhDirigido && a.Origem == aresta.Destino && a.Destino == aresta.Origem));
                    g.DrawLine(destaque ? penDestaque : penNormal, p1, p2Ajustado);

                    int midX = (p1.X + p2Original.X) / 2;
                    int midY = (p1.Y + p2Original.Y) / 2;
                    g.FillRectangle(Brushes.White, midX - 10, midY - 7, 20, 14);
                    g.DrawString(aresta.Peso.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, midX - 8, midY - 6);
                }
            }

            // Vértices
            int limiteVertices = Math.Min(verticesVisiveis, grafoAtual.Vertices.Count);
            for (int i = 0; i < limiteVertices; i++)
            {
                string v = grafoAtual.Vertices[i];
                Point p = posicoes[v];
                g.FillEllipse(brushVertice, p.X - raio, p.Y - raio, raio * 2, raio * 2);
                g.DrawEllipse(Pens.Black, p.X - raio, p.Y - raio, raio * 2, raio * 2);
                SizeF size = g.MeasureString(v, font);
                g.DrawString(v, font, brushTexto, p.X - size.Width / 2, p.Y - size.Height / 2);
            }
        }
    }
}