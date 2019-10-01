using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int WIDTH_OF_PICTURE = 300; //sirka - x
        private const int HEIGHT_OF_PICTURE = 300; //vyska - y
        private static double xmin, xmax, ymin, ymax, xstep, ystep;
        private static List<ComplexNumber> roots;
        private static Bitmap picture;
        private static Polynom polynom;
        private static Polynom derivePolynom;
        private static Color[] colorGroup;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 4)
                    SetLimits(args);
                else
                    SetLimits();

                Initialization();
                SetPolynom();

                Console.WriteLine(polynom);
                Console.WriteLine(derivePolynom);

                ComputeColorForEveryPixel();
                picture.Save("../../../out.png");
            }
            catch (Exception ex)
            {

                throw new Exception("Err:" + ex + "!");
            }
        }

        private static void ComputeColorForEveryPixel()
        {
            for (int xPosition = 0; xPosition < WIDTH_OF_PICTURE; xPosition++)
            {
                for (int yPosition = 0; yPosition < HEIGHT_OF_PICTURE; yPosition++)
                {
                    ComplexNumber complexNumber = new ComplexNumber()
                    {
                        RealPart = xmin + xPosition * xstep,
                        ImaginaryPart = ymin + yPosition * ystep
                    };
                    ChangePartsOfComplexNumber(complexNumber);

                    // find solution of equation using newton's iteration
                    int IterationsOfNewtonSolution = 0;
                    NewtonEquationFinder(ref complexNumber, ref IterationsOfNewtonSolution);

                    // find solution root number
                    int rootsNumber = 0;
                    RootSolutionFinder(complexNumber, ref rootsNumber);

                    // colorize pixel accordirng to root number
                    SetColorOfPixel(xPosition, yPosition, IterationsOfNewtonSolution, rootsNumber);
                }
            }
        }

        private static void SetColorOfPixel(int xPosition, int yPosition, int IterationsOfNewtonSolution, int rootsNumber)
        {
            const int COLOR_MAX = 255;
            var color = colorGroup[rootsNumber % colorGroup.Length];

            var red = Math.Min(Math.Max(0, color.R - IterationsOfNewtonSolution * 2), COLOR_MAX);
            var green = Math.Min(Math.Max(0, color.G - IterationsOfNewtonSolution * 2), COLOR_MAX);
            var blue = Math.Min(Math.Max(0, color.B - IterationsOfNewtonSolution * 2), COLOR_MAX);

            color = Color.FromArgb(red, green, blue);
            picture.SetPixel(xPosition, yPosition, color);
        }

        private static void RootSolutionFinder(ComplexNumber complexNumber, ref int rootsNumber)
        {
            bool rootExist = false;
            for (int i = 0; i < roots.Count; i++)
            {
                var powOfRealPart = Math.Pow(complexNumber.RealPart - roots[i].RealPart, 2);
                var powOfImaginaryPart = Math.Pow(complexNumber.ImaginaryPart - roots[i].ImaginaryPart, 2);

                if (powOfRealPart + powOfImaginaryPart <= 0.01)
                {
                    rootExist = true;
                    rootsNumber = i;
                }
            }
            if (!rootExist)
            {
                roots.Add(complexNumber);
                rootsNumber = roots.Count;
            }
        }

        private static void NewtonEquationFinder(ref ComplexNumber complexNumber, ref int IterationsOfNewtonSolution)
        {
            for (int i = 0; i < 30; i++)
            {
                var differencialPolynomAndDerivation = polynom.Eval(complexNumber).Divide(polynom.Derive().Eval(complexNumber));
                complexNumber = complexNumber.Subtract(differencialPolynomAndDerivation);

                if (Math.Pow(differencialPolynomAndDerivation.RealPart, 2) + Math.Pow(differencialPolynomAndDerivation.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                IterationsOfNewtonSolution++;
            }
        }

        private static void ChangePartsOfComplexNumber(ComplexNumber complexNumber)
        {
            if (complexNumber.RealPart == 0)
                complexNumber.RealPart = 0.0001;
            if (complexNumber.ImaginaryPart == 0)
                complexNumber.ImaginaryPart = 0.0001;
        }

        private static void SetPolynom()
        {
            polynom = new Polynom();
            polynom.CoeficientGroup.Add(new ComplexNumber() { RealPart = 1 });
            polynom.CoeficientGroup.Add(ComplexNumber.Zero);
            polynom.CoeficientGroup.Add(ComplexNumber.Zero);
            polynom.CoeficientGroup.Add(new ComplexNumber() { RealPart = 1 });
            derivePolynom = polynom.Derive();
        }

        private static void Initialization()
        {
            picture = new Bitmap(WIDTH_OF_PICTURE, HEIGHT_OF_PICTURE);

            xstep = (xmax - xmin) / WIDTH_OF_PICTURE;
            ystep = (ymax - ymin) / HEIGHT_OF_PICTURE;

            roots = new List<ComplexNumber>();

            colorGroup = new Color[]
{
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
};
        }

        private static void SetLimits()
        {
            xmin = -1.5;
            xmax = 1.5;
            ymin = -1.5;
            ymax = 1.5;
        }

        private static void SetLimits(string[] args)
        {
            Double.TryParse(args[0], out xmin);
            Double.TryParse(args[1], out xmax);
            Double.TryParse(args[2], out ymin);
            Double.TryParse(args[3], out ymax);
        }
    }
}
