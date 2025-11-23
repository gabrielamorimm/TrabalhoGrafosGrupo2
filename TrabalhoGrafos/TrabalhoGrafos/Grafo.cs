using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrabalhoGrafos
{
    public class Aresta
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Peso { get; set; }

        public Aresta(string origem, string destino, int peso)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
        }
    }

    public class Grafo
    {
        public List<string> Vertices { get; set; } = new List<string>();
        public List<Aresta> Arestas { get; set; } = new List<Aresta>();
        public bool EhDirigido { get; set; }

        // a) Gerar Grafo Aleatório
        public void GerarAleatorio(int numVertices, int maxPeso, bool dirigido)
        {
            Vertices.Clear();
            Arestas.Clear();
            EhDirigido = dirigido;
            Random rnd = new Random();

            // Cria vértices
            for (int i = 1; i <= numVertices; i++) Vertices.Add($"V{i}");

            // Cria arestas aleatórias (densidade média)
            foreach (var v1 in Vertices)
            {
                foreach (var v2 in Vertices)
                {
                    if (v1 != v2 && rnd.Next(0, 100) < 30) // 30% de chance de conexão
                    {
                        // Evita duplicar arestas em não dirigidos
                        if (!EhDirigido && Arestas.Exists(a => (a.Origem == v2 && a.Destino == v1))) continue;
                        
                        int peso = rnd.Next(1, maxPeso);
                        Arestas.Add(new Aresta(v1, v2, peso));
                    }
                }
            }
        }

        // b) Árvore Geradora Mínima (Algoritmo de Kruskal Simples)
        public List<Aresta> ObterAGM()
        {
            // Para AGM, consideramos o subjacente (não dirigido)
            List<Aresta> agm = new List<Aresta>();
            var arestasOrdenadas = Arestas.OrderBy(a => a.Peso).ToList();
            Dictionary<string, string> pai = new Dictionary<string, string>();

            foreach (var v in Vertices) pai[v] = v;

            string Find(string i)
            {
                if (pai[i] == i) return i;
                return Find(pai[i]);
            }

            void Union(string i, string j)
            {
                string raizI = Find(i);
                string raizJ = Find(j);
                if (raizI != raizJ) pai[raizI] = raizJ;
            }

            foreach (var a in arestasOrdenadas)
            {
                if (Find(a.Origem) != Find(a.Destino))
                {
                    agm.Add(a);
                    Union(a.Origem, a.Destino);
                }
            }
            return agm;
        }

        // c) Caminho Mínimo (Dijkstra)
        public List<Aresta> Dijkstra(string inicio, string fim)
        {
            var distancias = new Dictionary<string, int>();
            var anteriores = new Dictionary<string, Aresta>();
            var naoVisitados = new List<string>(Vertices);

            foreach (var v in Vertices) distancias[v] = int.MaxValue;
            distancias[inicio] = 0;

            while (naoVisitados.Count > 0)
            {
                naoVisitados.Sort((x, y) => distancias[x].CompareTo(distancias[y]));
                var atual = naoVisitados[0];
                naoVisitados.RemoveAt(0);

                if (atual == fim) break;
                if (distancias[atual] == int.MaxValue) break;

                // Vizinhos
                var arestasDoVertice = Arestas.Where(a => a.Origem == atual || (!EhDirigido && a.Destino == atual)).ToList();

                foreach (var aresta in arestasDoVertice)
                {
                    string vizinho = (aresta.Origem == atual) ? aresta.Destino : aresta.Origem;
                    if (naoVisitados.Contains(vizinho))
                    {
                        int alt = distancias[atual] + aresta.Peso;
                        if (alt < distancias[vizinho])
                        {
                            distancias[vizinho] = alt;
                            anteriores[vizinho] = aresta;
                        }
                    }
                }
            }

            // Reconstruir caminho
            var caminho = new List<Aresta>();
            string curr = fim;
            while (anteriores.ContainsKey(curr))
            {
                caminho.Add(anteriores[curr]);
                var a = anteriores[curr];
                curr = (a.Destino == curr) ? a.Origem : a.Destino;
            }
            return caminho;
        }

        // d.5) Busca em Largura (BFS) & Fecho Transitivo
        public List<string> BFS(string inicio)
        {
            var visitados = new List<string>();
            var fila = new Queue<string>();

            visitados.Add(inicio);
            fila.Enqueue(inicio);

            while (fila.Count > 0)
            {
                var atual = fila.Dequeue();
                // Pega vizinhos respeitando a direção
                var vizinhos = Arestas.Where(a => a.Origem == atual).Select(a => a.Destino).ToList();
                
                if (!EhDirigido) // Se não for dirigido, pega o inverso também
                    vizinhos.AddRange(Arestas.Where(a => a.Destino == atual).Select(a => a.Origem));

                foreach (var v in vizinhos)
                {
                    if (!visitados.Contains(v))
                    {
                        visitados.Add(v);
                        fila.Enqueue(v);
                    }
                }
            }
            return visitados;
        }

        // d.4) Busca em Profundidade (DFS)
        public void DFS(string atual, HashSet<string> visitados, List<string> ordemVisita)
        {
            visitados.Add(atual);
            ordemVisita.Add(atual);

            var vizinhos = Arestas.Where(a => a.Origem == atual).Select(a => a.Destino).ToList();
             if (!EhDirigido)
                    vizinhos.AddRange(Arestas.Where(a => a.Destino == atual).Select(a => a.Origem));

            foreach (var v in vizinhos)
            {
                if (!visitados.Contains(v))
                {
                    DFS(v, visitados, ordemVisita);
                }
            }
        }
    }
}