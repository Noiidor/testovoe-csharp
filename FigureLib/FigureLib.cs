using System;
using System.Linq;

namespace FigureLib
{
    public abstract class Figure
    {
        public virtual int numOfSides { get; }

        public float[] sides
        {
            get { return sides; }
            set { SetSides(value); }
        }

        public Figure(float[] sides)
        {
            SetSides(sides);
        }

        private void SetSides(float[] sides)
        {
            if (sides.Length < numOfSides)
            {
                throw new ArgumentOutOfRangeException("Array lenght cannot be below 3");
            }
            if (!sides.All(num => num > 0))
            {
                throw new ArgumentOutOfRangeException("Side lenght cannot be negative or zero");
            }
            this.sides = sides.Take(numOfSides).ToArray();
        }

        /// <summary>
        /// Represents area of <see cref="Figure"/>.
        /// </summary>
        public float area
        {
            get { return Area(); }
        }

        /// <summary>
        /// Represents sum of all sides in <see cref="Figure"/>.
        /// </summary>
        public float perimeter
        {
            get { return Perimeter(); }
        }

        private float Perimeter()
        {
            return sides.Sum();
        }

        protected abstract float Area();
    }

    // Для создания новой фигуры нужно лишь наследовать её от класса Figure
    // И переопределить numOfSides и Area

    public class Triangle : Figure
    {
        public override int numOfSides => 3;

        public Triangle(float[] sides) : base(sides)
        {}

        protected override float Area()
        {
            float p = sides.Sum() * 0.5f;
            float s = MathF.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]));
            return s;
            
        }

        /// <summary>
        /// Determines whether the <see cref="Triangle"/> is rectangular.
        /// </summary>
        /// <returns></returns>
        public bool isRight()
        {
            float hypotenuse = sides.Max();
            int hypotenuseIndex = Array.IndexOf(sides, hypotenuse);
            // Создается массив, состоящий лишь из катетов, путем удаления гипотенузы
            float[] cathets = sides.Where((value, index) => index != hypotenuseIndex).ToArray();
            
            bool pythagor = MathF.Pow(hypotenuse, 2) == (MathF.Pow(cathets[0], 2) + MathF.Pow(cathets[1], 2));
            return pythagor;
        }
        
    }

    public class Rectangle : Figure
    {
        public override int numOfSides => 2;

        public Rectangle(float[] sides) : base(sides)
        { }

        
        protected override float Area()
        {
            return sides[0] * sides[1];
        }
    }



    public class Calculations
    {
        Triangle tri = new Triangle(new float[3] { 1, 2, 3 });
        public float AreaOfCircle(float radius)
        {
            
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException("Radius cannot be negative or zero");
            }
            return MathF.Pow(radius, 2) * MathF.PI;
        }

        public float AreaOfTriangle(float sideA, float sideB, float sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                throw new ArgumentOutOfRangeException("Side lenght cannot be negative or zero");
            }
            float p = (sideA + sideB + sideC) * 0.5f;
            float s = MathF.Sqrt(p * (p - sideA) * (p - sideB) * (p - sideC));
            return s;
        }
    }

    
}
