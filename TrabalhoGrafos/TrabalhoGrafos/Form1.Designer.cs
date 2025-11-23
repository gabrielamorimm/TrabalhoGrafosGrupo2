namespace TrabalhoGrafos
{
    public partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Controles UI
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBoxGrafo;
        
        // Grupo A
        private System.Windows.Forms.GroupBox grpGeracao;
        private System.Windows.Forms.Button btnGerarAleatorio;
        private System.Windows.Forms.Button btnGerarManual;
        private System.Windows.Forms.CheckBox chkDirigido;
        private System.Windows.Forms.TextBox txtNumVertices;
        private System.Windows.Forms.Label lblNumV;
        private System.Windows.Forms.Label lblManual;
        private System.Windows.Forms.TextBox txtArestasManual;

        // Grupo B
        private System.Windows.Forms.GroupBox grpAlgoritmos;
        private System.Windows.Forms.Button btnAGM;
        private System.Windows.Forms.Button btnCaminho;
        private System.Windows.Forms.Button btnBFS;
        private System.Windows.Forms.Button btnDFS;
        private System.Windows.Forms.ComboBox cmbOrigem;
        private System.Windows.Forms.ComboBox cmbDestino;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.Label lblPara;
        
        private System.Windows.Forms.TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 720);
            this.Text = "Trabalho de Grafos - Algoritmos";
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            // --- PictureBox ---
            this.pictureBoxGrafo = new System.Windows.Forms.PictureBox();
            this.pictureBoxGrafo.Location = new System.Drawing.Point(300, 60);
            this.pictureBoxGrafo.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxGrafo.BackColor = System.Drawing.Color.White;
            this.pictureBoxGrafo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGrafo.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGrafo_Paint);
            this.Controls.Add(this.pictureBoxGrafo);

            // --- Título ---
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTitulo.Text = "Visualizador de Grafos";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTitulo.AutoSize = true;
            this.Controls.Add(this.lblTitulo);

            // --- Grupo Criação ---
            this.grpGeracao = new System.Windows.Forms.GroupBox();
            this.grpGeracao.Text = "1. Criação do Grafo";
            this.grpGeracao.Location = new System.Drawing.Point(10, 60);
            this.grpGeracao.Size = new System.Drawing.Size(270, 280);
            
            this.lblNumV = new System.Windows.Forms.Label() { Text = "Qtd. Vértices:", Location = new System.Drawing.Point(15, 33), AutoSize = true };
            
            this.txtNumVertices = new System.Windows.Forms.TextBox(); 
            this.txtNumVertices.Location = new System.Drawing.Point(100, 30);
            this.txtNumVertices.Size = new System.Drawing.Size(50, 25);
            this.txtNumVertices.Text = "5";
            this.txtNumVertices.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.chkDirigido = new System.Windows.Forms.CheckBox() { Text = "Grafo Dirigido?", Location = new System.Drawing.Point(15, 65), AutoSize = true };
            
            this.btnGerarAleatorio = new System.Windows.Forms.Button();
            this.btnGerarAleatorio.Text = "🎲 Gerar Aleatório";
            this.btnGerarAleatorio.Location = new System.Drawing.Point(15, 100);
            this.btnGerarAleatorio.Size = new System.Drawing.Size(240, 30);
            this.btnGerarAleatorio.BackColor = System.Drawing.Color.LightBlue;
            this.btnGerarAleatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarAleatorio.Click += new System.EventHandler(this.btnGerarAleatorio_Click);

            System.Windows.Forms.Label lblOu = new System.Windows.Forms.Label() { Text = "--- OU CRIE MANUALMENTE ---", Location = new System.Drawing.Point(40, 145), AutoSize = true, ForeColor = System.Drawing.Color.Gray };

            // MUDANÇA AQUI: Texto explicativo atualizado
            this.lblManual = new System.Windows.Forms.Label() { Text = "Ligações (Ex: 1-2, 2-3:5):", Location = new System.Drawing.Point(15, 170), AutoSize = true };
            
            this.txtArestasManual = new System.Windows.Forms.TextBox();
            this.txtArestasManual.Location = new System.Drawing.Point(15, 190);
            this.txtArestasManual.Size = new System.Drawing.Size(240, 25);
            // MUDANÇA AQUI: Placeholder atualizado
            this.txtArestasManual.PlaceholderText = "Ex: 1-2, 2-3:10, 3-1:5"; 

            this.btnGerarManual = new System.Windows.Forms.Button();
            this.btnGerarManual.Text = "✏️ Criar Manual";
            this.btnGerarManual.Location = new System.Drawing.Point(15, 225);
            this.btnGerarManual.Size = new System.Drawing.Size(240, 30);
            this.btnGerarManual.BackColor = System.Drawing.Color.LightGray;
            this.btnGerarManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarManual.Click += new System.EventHandler(this.btnGerarManual_Click);

            this.grpGeracao.Controls.Add(lblNumV);
            this.grpGeracao.Controls.Add(txtNumVertices);
            this.grpGeracao.Controls.Add(chkDirigido);
            this.grpGeracao.Controls.Add(btnGerarAleatorio);
            this.grpGeracao.Controls.Add(lblOu);
            this.grpGeracao.Controls.Add(lblManual);
            this.grpGeracao.Controls.Add(txtArestasManual);
            this.grpGeracao.Controls.Add(btnGerarManual);
            this.Controls.Add(this.grpGeracao);

            // --- Grupo Algoritmos ---
            this.grpAlgoritmos = new System.Windows.Forms.GroupBox();
            this.grpAlgoritmos.Text = "2. Análises";
            this.grpAlgoritmos.Location = new System.Drawing.Point(10, 350);
            this.grpAlgoritmos.Size = new System.Drawing.Size(270, 250);

            this.lblDe = new System.Windows.Forms.Label() { Text = "De:", Location = new System.Drawing.Point(15, 30), AutoSize = true };
            this.cmbOrigem = new System.Windows.Forms.ComboBox() { Location = new System.Drawing.Point(45, 27), Width = 70 };
            this.lblPara = new System.Windows.Forms.Label() { Text = "Para:", Location = new System.Drawing.Point(130, 30), AutoSize = true };
            this.cmbDestino = new System.Windows.Forms.ComboBox() { Location = new System.Drawing.Point(170, 27), Width = 70 };

            this.btnAGM = new System.Windows.Forms.Button() { Text = "Exibir AGM", Location = new System.Drawing.Point(15, 70), Size = new System.Drawing.Size(240, 30), BackColor = System.Drawing.Color.LightGreen, FlatStyle = System.Windows.Forms.FlatStyle.Flat };
            this.btnAGM.Click += new System.EventHandler(this.btnAGM_Click);

            this.btnCaminho = new System.Windows.Forms.Button() { Text = "Caminho Mínimo", Location = new System.Drawing.Point(15, 110), Size = new System.Drawing.Size(240, 30), BackColor = System.Drawing.Color.LightSalmon, FlatStyle = System.Windows.Forms.FlatStyle.Flat };
            this.btnCaminho.Click += new System.EventHandler(this.btnCaminho_Click);

            this.btnDFS = new System.Windows.Forms.Button() { Text = "Fecho Transitivo (DFS)", Location = new System.Drawing.Point(15, 150), Size = new System.Drawing.Size(240, 30), BackColor = System.Drawing.Color.LightGoldenrodYellow, FlatStyle = System.Windows.Forms.FlatStyle.Flat };
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);

            this.btnBFS = new System.Windows.Forms.Button() { Text = "Busca em Largura (BFS)", Location = new System.Drawing.Point(15, 190), Size = new System.Drawing.Size(240, 30) };
            this.btnBFS.Click += new System.EventHandler(this.btnBFS_Click);

            this.grpAlgoritmos.Controls.Add(lblDe);
            this.grpAlgoritmos.Controls.Add(cmbOrigem);
            this.grpAlgoritmos.Controls.Add(lblPara);
            this.grpAlgoritmos.Controls.Add(cmbDestino);
            this.grpAlgoritmos.Controls.Add(btnAGM);
            this.grpAlgoritmos.Controls.Add(btnCaminho);
            this.grpAlgoritmos.Controls.Add(btnDFS);
            this.grpAlgoritmos.Controls.Add(btnBFS);
            this.Controls.Add(this.grpAlgoritmos);

            // --- Log ---
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtLog.Location = new System.Drawing.Point(10, 610);
            this.txtLog.Size = new System.Drawing.Size(270, 100);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Text = "Log de execução...";
            this.Controls.Add(this.txtLog);
        }
    }
}