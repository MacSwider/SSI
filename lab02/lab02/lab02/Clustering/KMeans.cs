namespace lab02.Clustering
{
    // wynik po jakiejś liczbie iteracji – żeby dało się narysować i wypisać do raportu
    public class WynikKMeans
    {
        public double[][] V = null!; // srodki grup
        public int[] U = null!;      // U[s] = nr grupy probki s
        public int iteracje;
    }

    public class KMeans
    {
        Random rnd = new Random(123); // stały seed, żeby wyniki się powtarzały przy robieniu raportu

        // algorytm z pdf-a (pseudokod)
        public WynikKMeans Uruchom(double[][] probki, int m, int iters, RodzajOdleglosci miara, double mink)
        {
            int M = probki.Length;   // liczba probek
            int n = probki[0].Length; // atrybuty (u nas 2: x1, x2)

            // 1. wylosuj m różnych probek jako środki V
            double[][] V = new double[m][];
            List<int> zajete = new List<int>();
            for (int j = 0; j < m; j++)
            {
                int idx;
                do
                {
                    idx = rnd.Next(M);
                } while (zajete.Contains(idx));
                zajete.Add(idx);

                V[j] = new double[n];
                for (int i = 0; i < n; i++)
                    V[j][i] = probki[idx][i];
            }

            int[] U = new int[M]; // przynaleznosc probek do grup

            // 2. petla iteracji
            for (int it = 0; it < iters; it++)
            {
                // 2.1 dla kazdej probki znajdz najblizszy srodek
                for (int s = 0; s < M; s++)
                {
                    double najlepsza = double.MaxValue;
                    int najlepszaGrupa = 0;

                    for (int j = 0; j < m; j++)
                    {
                        double d = Odleglosc.Policz(probki[s], V[j], miara, mink);
                        if (d < najlepsza)
                        {
                            najlepsza = d;
                            najlepszaGrupa = j;
                        }
                    }

                    U[s] = najlepszaGrupa;
                }

                // 2.2 dla kazdej grupy policz nowy srodek (srednia)
                for (int j = 0; j < m; j++)
                {
                    // zbieramy probki z tej grupy
                    List<double[]> Xgr = new List<double[]>();
                    for (int s = 0; s < M; s++)
                    {
                        if (U[s] == j)
                            Xgr.Add(probki[s]);
                    }

                    // pusta grupa -> nic nie zmieniamy
                    if (Xgr.Count == 0)
                        continue;

                    for (int i = 0; i < n; i++)
                    {
                        double suma = 0;
                        for (int k = 0; k < Xgr.Count; k++)
                            suma += Xgr[k][i];
                        V[j][i] = suma / Xgr.Count;
                    }
                }
            }

            return new WynikKMeans { V = V, U = U, iteracje = iters };
        }
    }
}
