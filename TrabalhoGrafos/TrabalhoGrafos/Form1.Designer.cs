namespace TrabalhoGrafos
{
    // Declaração parcial da classe Form1
    public partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // --- DECLARAÇÃO DOS CONTROLES FIXOS ---

        private System.Windows.Forms.Label labelQuantidade; 
        private System.Windows.Forms.NumericUpDown numUpDownQuantidade; 
        private System.Windows.Forms.Button btnConfirmar; 
        private System.Windows.Forms.Button btnAnimar;
        // NOVA VARIÁVEL: O canvas (tela) onde o grafo será desenhado
        private System.Windows.Forms.PictureBox pictureBoxGrafo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Instanciação de Controles Fixos
            this.labelQuantidade = new System.Windows.Forms.Label();
            this.numUpDownQuantidade = new System.Windows.Forms.NumericUpDown();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnAnimar = new System.Windows.Forms.Button();
            this.pictureBoxGrafo = new System.Windows.Forms.PictureBox(); // Instancia o PictureBox
            
            // Configuração do Form (Janela)
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 880); // Tamanho ajustado para caber o grafo!
            this.Text = "Configuração e Visualização do Grafo";

            // 1. Configuração de labelQuantidade
            this.labelQuantidade.AutoSize = true;
            this.labelQuantidade.Location = new System.Drawing.Point(20, 20);
            this.labelQuantidade.Name = "labelQuantidade";
            this.labelQuantidade.Size = new System.Drawing.Size(250, 20);
            this.labelQuantidade.Text = "Número de Vértices (mín. 2, máx. 10):";

            // 2. Configuração de numUpDownQuantidade
            this.numUpDownQuantidade.Location = new System.Drawing.Point(280, 20);
            this.numUpDownQuantidade.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            this.numUpDownQuantidade.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numUpDownQuantidade.Value = new decimal(new int[] { 3, 0, 0, 0 });
            this.numUpDownQuantidade.Name = "numUpDownQuantidade";

            // 3. Configuração de btnConfirmar
            this.btnConfirmar.Location = new System.Drawing.Point(405, 18);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(100, 40);
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click); 

            // 4. Configuração de btnAnimar (Inicialmente oculto)
            this.btnAnimar.Location = new System.Drawing.Point(30, 450); 
            this.btnAnimar.Name = "btnAnimar";
            this.btnAnimar.Size = new System.Drawing.Size(200, 30);
            this.btnAnimar.Text = "Gerar Grafo e Animar";
            this.btnAnimar.Visible = false; 
            this.btnAnimar.Click += new System.EventHandler(this.btnAnimar_Click); 
            
            // 5. Configuração de pictureBoxGrafo (Área de Desenho)
            this.pictureBoxGrafo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGrafo.Location = new System.Drawing.Point(20, 450); // Abaixo da área de entrada
            this.pictureBoxGrafo.Name = "pictureBoxGrafo";
            this.pictureBoxGrafo.Size = new System.Drawing.Size(560, 400); 
            this.pictureBoxGrafo.TabIndex = 10;
            this.pictureBoxGrafo.TabStop = false;
            // Associa o evento Paint ao método de desenho
            this.pictureBoxGrafo.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGrafo_Paint); 
            
            // Adiciona todos os controles fixos ao formulário
            this.Controls.Add(this.labelQuantidade);
            this.Controls.Add(this.numUpDownQuantidade);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnAnimar);
            this.Controls.Add(this.pictureBoxGrafo); // Adiciona o canvas

            this.ResumeLayout(false);
            this.PerformLayout();

            
        }

        #endregion
    }
}