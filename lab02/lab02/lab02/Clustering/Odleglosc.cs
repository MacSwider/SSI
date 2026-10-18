namespace lab02.Clustering
{
    // miary z pdf-a
    public enum RodzajOdleglosci
    {
        Euklidesowa,
        Manhattan,
        Czebyszewa,
        Minkowskiego,
        TylkoX1 // raport 2: |x1a - x1b|, x2 ignorujemy
    }

    public static class Odleglosc
    {
        public static double Policz(double[] a, double[] b, RodzajOdleglosci rodzaj, double mink = 3)
        {
            if (rodzaj == RodzajOdleglosci.TylkoX1)
                return Math.Abs(a[0] - b[0]);

            if (rodzaj == RodzajOdleglosci.Euklidesowa)
            {
                // sqrt( suma (ai-bi)^2 )
                double suma = 0;
                for (int i = 0; i < a.Length; i++)
                    suma += (a[i] - b[i]) * (a[i] - b[i]);
                return Math.Sqrt(suma);
            }

            if (rodzaj == RodzajOdleglosci.Manhattan)
            {
                // suma |ai-bi|
                double suma = 0;
                for (int i = 0; i < a.Length; i++)
                    suma += Math.Abs(a[i] - b[i]);
                return suma;
            }

            if (rodzaj == RodzajOdleglosci.Czebyszewa)
            {
                // max |ai-bi|
                double max = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    double d = Math.Abs(a[i] - b[i]);
                    if (d > max) max = d;
                }
                return max;
            }

            // Minkowski: (suma |ai-bi|^mink ) ^ (1/mink)
            double s = 0;
            for (int i = 0; i < a.Length; i++)
                s += Math.Pow(Math.Abs(a[i] - b[i]), mink);
            return Math.Pow(s, 1.0 / mink);
        }
    }
}
