using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using FigureLib;

namespace FigureLib.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region TestCaseParameters

        // Входные данные для всех тестов одинаковы
        public static float[][] parameters =
        {
            new float[]{2, 3, 4, 10},
            new float[]{1, 1, 1, 2},
            new float[]{5, 4, 7, 10},
            new float[]{34, 23, 48, 123},
            new float[]{0.1f, 0.25f, 0.17f, 0.001f},
            new float[]{100, 100, 100, 100},
            new float[]{101, 102, 100, 1},
            new float[]{43.1f, 23.7f, 48.2f, 51.9f},
            new float[]{3, 4, 5, 7},
            new float[]{2.5f, 6, 6.5f, 8.5f}
        };

        public static IEnumerable<TestCaseData> CaseDataRectArea()
        {
            yield return new TestCaseData(parameters[0]).Returns(6);
            yield return new TestCaseData(parameters[1]).Returns(1);
            yield return new TestCaseData(parameters[2]).Returns(20);
            yield return new TestCaseData(parameters[3]).Returns(782);
            yield return new TestCaseData(parameters[4]).Returns(0.025f);
            yield return new TestCaseData(parameters[5]).Returns(10000);
            yield return new TestCaseData(parameters[6]).Returns(10302);
            yield return new TestCaseData(parameters[7]).Returns(1021.47f);
            yield return new TestCaseData(parameters[8]).Returns(12);
            yield return new TestCaseData(parameters[9]).Returns(15);
        }

        public static IEnumerable<TestCaseData> CaseDataRectPerimeter()
        {
            yield return new TestCaseData(parameters[0]).Returns(10);
            yield return new TestCaseData(parameters[1]).Returns(4);
            yield return new TestCaseData(parameters[2]).Returns(18);
            yield return new TestCaseData(parameters[3]).Returns(114);
            yield return new TestCaseData(parameters[4]).Returns(0.7f);
            yield return new TestCaseData(parameters[5]).Returns(400);
            yield return new TestCaseData(parameters[6]).Returns(406);
            yield return new TestCaseData(parameters[7]).Returns(133.6f);
            yield return new TestCaseData(parameters[8]).Returns(14);
            yield return new TestCaseData(parameters[9]).Returns(17);
        }

        // На этот моменте получаются весьма некрасивые дробные числа, но результаты верны.

        public static IEnumerable<TestCaseData> CaseDataTriArea()
        {
            yield return new TestCaseData(parameters[0]).Returns(2.9f);
            yield return new TestCaseData(parameters[1]).Returns(0.43f);
            yield return new TestCaseData(parameters[2]).Returns(9.8f);
            yield return new TestCaseData(parameters[3]).Returns(359.07f);
            yield return new TestCaseData(parameters[4]).Returns(0.01f);
            yield return new TestCaseData(parameters[5]).Returns(4330.13f);
            yield return new TestCaseData(parameters[6]).Returns(4416.3f);
            yield return new TestCaseData(parameters[7]).Returns(510.17f);
            yield return new TestCaseData(parameters[8]).Returns(6);
            yield return new TestCaseData(parameters[9]).Returns(7.5f);
        }

        public static IEnumerable<TestCaseData> CaseDataTriRight()
        {
            yield return new TestCaseData(parameters[0]).Returns(false);
            yield return new TestCaseData(parameters[1]).Returns(false);
            yield return new TestCaseData(parameters[2]).Returns(false);
            yield return new TestCaseData(parameters[3]).Returns(false);
            yield return new TestCaseData(parameters[4]).Returns(false);
            yield return new TestCaseData(parameters[5]).Returns(false);
            yield return new TestCaseData(parameters[6]).Returns(false);
            yield return new TestCaseData(parameters[7]).Returns(false);
            yield return new TestCaseData(parameters[8]).Returns(true);
            yield return new TestCaseData(parameters[9]).Returns(true);
        }

        public static IEnumerable<TestCaseData> CaseDataCircArea()
        {
            yield return new TestCaseData(parameters[0]).Returns(12.57f);
            yield return new TestCaseData(parameters[1]).Returns(3.14f);
            yield return new TestCaseData(parameters[2]).Returns(78.54f);
            yield return new TestCaseData(parameters[3]).Returns(3631.68f);
            yield return new TestCaseData(parameters[4]).Returns(0.03f);
            yield return new TestCaseData(parameters[5]).Returns(31415.93f);
            yield return new TestCaseData(parameters[6]).Returns(32047.39f);
            yield return new TestCaseData(parameters[7]).Returns(5835.85f);
            yield return new TestCaseData(parameters[8]).Returns(28.27f);
            yield return new TestCaseData(parameters[9]).Returns(19.63f);
        }

        public static IEnumerable<TestCaseData> CaseDataCircPerimeter()
        {
            yield return new TestCaseData(parameters[0]).Returns(12.57f);
            yield return new TestCaseData(parameters[1]).Returns(6.28f);
            yield return new TestCaseData(parameters[2]).Returns(31.42f);
            yield return new TestCaseData(parameters[3]).Returns(213.63f);
            yield return new TestCaseData(parameters[4]).Returns(0.63f);
            yield return new TestCaseData(parameters[5]).Returns(628.32f);
            yield return new TestCaseData(parameters[6]).Returns(634.6f);
            yield return new TestCaseData(parameters[7]).Returns(270.81f);
        }

        #endregion

        #region Tests

        // Принимается больше значений, чем требуется, чтобы протестировать срез массива до нужного количества аргументов
        [Test]
        [TestCaseSource("CaseDataRectArea")]
        public float AreaOfRectangleTest(float[] sides)
        {
            Rectangle rect = new Rectangle(sides);
            //Assert.AreEqual(rect.area, sideA * sideB);
            return rect.area;
        }

        [Test]
        [TestCaseSource("CaseDataRectPerimeter")]
        public float PerimeterOfRecrangleTest(float[] sides)
        {
            Rectangle rect = new Rectangle(sides);
            return rect.perimeter;
        }

        // Решил не встраивать округление в вычисление площади по умолчанию, чтобы пользователь сам мог округлить сколько ему нужно при помощи Round().
        [Test]
        [TestCaseSource("CaseDataTriArea")]
        public float AreaOfTriangleTest(float[] sides)
        {
            Triangle tri = new Triangle(sides);
            return MathF.Round(tri.area,2);
        }

        [Test]
        [TestCaseSource("CaseDataTriRight")]
        public bool RightTriangleTest(float[] sides)
        {
            Triangle tri = new Triangle(sides);
            return tri.isRight();
        }

        [Test]
        [TestCaseSource("CaseDataCircArea")]
        public float AreaOfCircleTest(float[] sides)
        {
            Circle circ = new Circle(sides[0]);
            return MathF.Round(circ.area, 2);
        }


        [Test]
        [TestCaseSource("CaseDataCircPerimeter")]
        public float PerimeterOfCircleTest(float[] sides)
        {
            Circle circ = new Circle(sides[0]);
            return MathF.Round(circ.perimeter, 2);
        }

        #endregion
    }
}