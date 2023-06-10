using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkLib;

namespace FractalCurves
{
    public partial class MainWindow : Window
    {
        Turtle pen;
        private bool isDrawingCancelled = false;

        public MainWindow()
        {
            InitializeComponent();
            pen = new Turtle(canvas);
            pen.Visible = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isDrawingCancelled = true;
            double length = (double)lineLength.Value;
            int complexity = (int)iterationSlider.Value;

            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;

            double kochHeight = length * Math.Sqrt(3) / 2;
            double levyCurveHeight = length / Math.Sqrt(2);
            double dragonCurveHeight = length * Math.Sin(Math.PI / 4);

            double startX = (canvasWidth - length) / 2;
            double startY;

            pen.Clear();
            pen.BrushWidth = lineThickness.Value;
            isDrawingCancelled = false;

            if (curveSelect.SelectedIndex == 0)
            {
                startY = (canvasHeight - kochHeight) / 2 + 50;
                pen.WarpTo(startX, startY);
                KochCurve(pen, length, complexity, canvasWidth, canvasHeight);
            }
            else if (curveSelect.SelectedIndex == 1)
            {
                startY = (canvasHeight - kochHeight) / 2 + 50;
                pen.WarpTo(startX, startY);
                KochSnowflake(pen, length, complexity, canvasWidth, canvasHeight);
            }
            else if (curveSelect.SelectedIndex == 2)
            {
                startY = (canvasHeight - levyCurveHeight) / 2;
                pen.WarpTo(startX, startY + 100);
                LevyCurve(pen, length, complexity, canvasWidth, canvasHeight);
            }
            else if (curveSelect.SelectedIndex == 3)
            {
                startY = (canvasHeight - dragonCurveHeight) / 2;
                pen.WarpTo(startX, startY + 100);
                DragonCurve(pen, length, complexity, canvasWidth, canvasHeight);
            }
            else if (curveSelect.SelectedIndex == 4)
            {
                startY = (canvasHeight - length) / 2;
                pen.WarpTo(startX, startY + 100);
                MinkowskiCurve(pen, length, complexity, canvasWidth, canvasHeight);
            }     
        }
        private void curveSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isDrawingCancelled)
                return;
            if (curveSelect.SelectedIndex == 0)
            {
                iterationSlider.Maximum = 6;
                lineLength.Maximum = 300;
            }
            else if (curveSelect.SelectedIndex == 1)
            {
                iterationSlider.Maximum = 6;
                lineLength.Maximum = 300;
            }
            else if (curveSelect.SelectedIndex == 2)
            {
                iterationSlider.Maximum = 14;
                lineLength.Maximum = 200;
            }
            else if (curveSelect.SelectedIndex == 3)
            {
                iterationSlider.Maximum = 30;
                lineLength.Maximum = 200;
            }
            else if (curveSelect.SelectedIndex == 4)
            {
                iterationSlider.Maximum = 5;
                lineLength.Maximum = 300;
            }
        }
        private void colorSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isDrawingCancelled)
                return;
            if (colorSelect.SelectedIndex == 0)
            {
                pen.LineBrush = Brushes.Black;
            }
            else if(colorSelect.SelectedIndex == 1)
            {
                pen.LineBrush = Brushes.Blue;
            }
            else if(colorSelect.SelectedIndex == 2)
            {
                pen.LineBrush = Brushes.Red;
            }
            else if(colorSelect.SelectedIndex == 3) 
            {
                pen.LineBrush = Brushes.Purple;
            }
            else if(colorSelect.SelectedIndex == 4)
            {
                pen.LineBrush = Brushes.Green;
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            isDrawingCancelled = true;
            pen.Clear();
        }
        void KochCurve(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            if (_complexity == 0)
            {
                _pen.Forward(_length);
            }
            else
            {
                KochCurve(_pen, _length / 3, _complexity - 1, _canvasWidth, _canvasHeight);  
                _pen.Left(60);
                KochCurve(_pen, _length / 3, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(120);
                KochCurve(_pen, _length / 3, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(60);
                KochCurve(_pen, _length / 3, _complexity - 1, _canvasWidth, _canvasHeight);
            }
        }
        void KochSnowflake(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            for (int i = 0; i < 3; i++)
            {
                KochCurve(_pen, _length, _complexity, _canvasWidth, _canvasHeight);
                _pen.Right(120);
            }
        }
        void LevyCurve(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            if (_complexity == 0)
            {
                _pen.Forward(_length);
            }
            else
            {
                _pen.Left(45);
                LevyCurve(_pen, _length / Math.Sqrt(2), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(90);
                LevyCurve(_pen, _length / Math.Sqrt(2), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(45);
            }
        }
        void DragonCurve(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            if (_complexity == 0 | _length < 2.4)
            {
                _pen.Forward(_length);
            }
            else
            {
                _pen.Left(25);
                DragonCurve(_pen, _length * Math.Cos(Math.PI / 180 * 25), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(90);                                       
                DragonCurveReverse(_pen, _length * Math.Sin(Math.PI / 180 * 25), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(65);
            }
        }
        void DragonCurveReverse(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            if (_complexity == 0 | _length < 2.4)
            {
                _pen.Forward(_length);
            }
            else
            {
                _pen.Right(65);
                DragonCurve(_pen, _length * Math.Sin(Math.PI / 180 * 25), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(90);
                DragonCurveReverse(_pen, _length * Math.Cos(Math.PI / 180 * 25), _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(25);
            }
        }
        void MinkowskiCurve(Turtle _pen, double _length, int _complexity, double _canvasWidth, double _canvasHeight)
        {
            if (isDrawingCancelled)
                return;
            if (_complexity == 0)
            {
                _pen.Forward(_length);
            }
            else
            {
                double segmentLength = _length / 4;
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Left(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
                _pen.Right(90);
                MinkowskiCurve(_pen, segmentLength, _complexity - 1, _canvasWidth, _canvasHeight);
            }
        }
    }
}
