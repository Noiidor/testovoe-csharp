using System;
using System.Linq;

namespace FigureLib
{
    // В этом решении я использовал паттерн "Стратегия", реализованный при помощи абстрактного класса
    public abstract class Figure
    {
        public virtual int numOfSides { get; }

        // Инкапсулирование _sides для защиты от неправильного доступа
        private float[] _sides;
        public float[] sides
        {
            get { return _sides; }
            set { _sides = ValidateSides(value); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sides">Array of all sides of the <see cref="Figure"/>.</param>
        public Figure(float[] sides)
        {
            ValidateSides(sides);
        }

        //Проверяет полученный массив на соответствие количеству сторон и правильности значений
        private float[] ValidateSides(float[] sides)
        {
            sides = sides.Take(numOfSides).ToArray();

            if (sides.Length > 2)
            {
                float maxSide = sides.Max();
                int maxSideIndex = Array.IndexOf(sides, maxSide);
                float[] otherSides = sides.Where((value, index) => index != maxSideIndex).ToArray();
                if (maxSide >= otherSides.Sum())
                {
                    throw new ArgumentOutOfRangeException("One of the sides is bigger than sum of others. This figure cannot exist");
                }
            }
            if (sides.Length < numOfSides)
            {
                throw new ArgumentOutOfRangeException("Array lenght cannot be below 3");
            }
            if (!sides.All(num => num > 0))
            {
                throw new ArgumentOutOfRangeException("Side lenght cannot be negative or zero");
            }
            return sides;
        }

        /// <summary>
        /// Represents area of <see cref="Figure"/>.
        /// </summary>
        public float area
        {
            get { return Area(); }
        }

        /// <summary>
        /// Represents lenght of all sides of the <see cref="Figure"/>.
        /// </summary>
        public float perimeter
        {
            get { return Perimeter(); }
        }

        protected virtual float Perimeter()
        {
            return sides.Sum();
        }

        protected abstract float Area();
    }

    #region Figures

    // Для создания новой фигуры нужно лишь наследовать её от класса Figure
    // Переопределить numOfSides, Area и если требуется - Perimeter

    public class Triangle : Figure
    {
        public override int numOfSides => 3;

        /// <inheritdoc/>
        public Triangle(float[] sides) : base(sides)
        {}

        // Вычисляет площадь треугольника, используюя формулу Герона
        protected override float Area()
        {
            float p = sides.Sum() * 0.5f;
            float s = MathF.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]));
            return s;
            
        }

        /// <summary>
        /// Determines whether the <see cref="Triangle"/> is rectangular using Pythagorean theorem.
        /// </summary>
        /// <returns></returns>
        
        // Вычисление "прямоугольности" треугольника при помощи теоремы Пифагора.
        public bool isRight()
        {
            float hypotenuse = sides.Max();
            // Индекс первого вхождения числа, соответствующего значению гипотенузы
            int hypotenuseIndex = Array.IndexOf(sides, hypotenuse);
            // Создается массив, состоящий лишь из катетов, путем удаления гипотенузы
            float[] cathets = sides.Where((value, index) => index != hypotenuseIndex).ToArray();
            
            bool pythagor = MathF.Pow(hypotenuse, 2) == (MathF.Pow(cathets[0], 2) + MathF.Pow(cathets[1], 2));
            return pythagor;
        }
        
    }


    public class Rectangle : Figure
    {
        // У прямоугольника половина сторон равны, поэтому больше двух определять излишне
        public override int numOfSides => 2;

        /// <inheritdoc/>
        public Rectangle(float[] sides) : base(sides)
        { }

        protected override float Perimeter()
        {
            return sides.Sum() * 2;
        }

        protected override float Area()
        {
            return sides[0] * sides[1];
        }
    }


    // Окружность является небольшим исключением, так как у неё всего одна сторона и логично было бы определять её длиной окружности,
    // Но я посчитал что удобнее для пользователя будет определять окружность её радиусом,
    // поэтому в качестве длины стороны мы берет радиус и строим дальшейшие вычисления уже на основе него.
    public class Circle : Figure
    {
        public override int numOfSides => 1;

        /// <inheritdoc/>
        public Circle(float radius) : base(new float[] { radius })
        { }

        protected override float Perimeter()
        {
            return sides[0] * (MathF.PI * 2);
        }

        protected override float Area()
        {
            return MathF.Pow(sides[0], 2) * MathF.PI;
        }
    }

    #endregion

    public class Calculations
    {
        /// <summary>
        /// Calculates area of <see cref="Circle"/> of given radius.
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public float AreaOfCircle(float radius)
        {
            Circle circ = new Circle(radius);
            return circ.area;
        }

        /// <summary>
        /// Calculates area of <see cref="Rectangle"/> with given sides.
        /// </summary>
        /// <param name="sides"></param>
        /// <returns></returns>
        public float AreaOfRectangle(float[] sides)
        {
            Rectangle rect = new Rectangle(sides);
            return rect.area;
        }

        /// <summary>
        /// Calculates area of <see cref="Triangle"/> with given sides.
        /// </summary>
        /// <returns></returns>
        public float AreaOfTriangle(float[] sides)
        {
            Triangle tri = new Triangle(sides);
            return tri.area;
        }


        // А это попытка сделать вычисление площади фигуры без знания типа фигуры.
        // Задача, честно говоря, весьма неоднозначная. Если для каждой фигуры формула площади разная, то как тогда определить тип фигуры?
        // С помощью одних лишь длинн сторон это выглядит невозможным, например параллелипипед и ромб имеют практически одинаковую форму.
        // Короче говоря, я сделал определение фигуры по количетсву сторон, но способ этот не идеальный, так как многие фигуры имеют разную форму, но одинаковое кол-во сторон.
        public float AreaOfFigure(float[] sides)
        {
            switch (sides.Length)
            {
                case 1:
                    Circle circ = new Circle(sides[0]);
                    return circ.area;
                case 2:
                    Rectangle rect = new Rectangle(sides);
                    return rect.area;
                case 3:
                    Triangle tri = new Triangle(sides);
                    return tri.area;
                default:
                    throw new ArgumentOutOfRangeException("Figure with given sides does not exist");
            }
        }
    }
}