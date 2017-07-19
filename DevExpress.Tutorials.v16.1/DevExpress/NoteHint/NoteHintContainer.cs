namespace DevExpress.NoteHint
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class NoteHintContainer : ContentControl
    {
        public static readonly DependencyProperty ArrowOffsetRatioProperty = DependencyProperty.Register("ArrowOffsetRatio", typeof(double), typeof(NoteHintContainer), new PropertyMetadata(0.2));
        public static readonly DependencyProperty ArrowScaleProperty = DependencyProperty.Register("ArrowScale", typeof(double), typeof(NoteHintContainer), new PropertyMetadata(0.5));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(double), typeof(NoteHintContainer), new PropertyMetadata(7.0));
        private Point hintArrowTargetLocation;
        private Point hintContentLocation;
        public static readonly DependencyProperty HintOffsetProperty = DependencyProperty.Register("HintOffset", typeof(Point), typeof(NoteHintContainer), new PropertyMetadata(new Point(5.0, 5.0)));
        private Path hintPath;
        public static readonly DependencyProperty HintPositionProperty = DependencyProperty.Register("HintPosition", typeof(NoteHintPosition), typeof(NoteHintContainer), new PropertyMetadata(NoteHintPosition.Up));
        private ContentPresenter hintPresenter;
        private Geometry hintShapeGeometry;
        public static readonly DependencyProperty NoteHintStyleProperty = DependencyProperty.Register("NoteHintStyle", typeof(NoteHintStyle), typeof(NoteHintContainer), new PropertyMetadata(NoteHintStyle.WithArrow));
        private FrameworkElement rootElement;
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(NoteHintContainer), null);
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(NoteHintContainer), new PropertyMetadata(1.0));

        public event ContentChangedHandler ContentChanged;

        public event EventHandler ShapeChanged;

        public NoteHintContainer()
        {
            base.DefaultStyleKey = typeof(NoteHintContainer);
            this.AssignColors();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            double num = (this.hintPath != null) ? this.hintPath.StrokeThickness : 0.0;
            if (this.hintPresenter != null)
            {
                Rect finalRect = new Rect(this.hintContentLocation.X, this.hintContentLocation.Y, this.hintPresenter.DesiredSize.Width, this.hintPresenter.DesiredSize.Height);
                this.hintPresenter.Arrange(finalRect);
            }
            if (this.hintPath != null)
            {
                Rect rect2 = new Rect(num / 2.0, num / 2.0, this.hintPath.DesiredSize.Width, this.hintPath.DesiredSize.Height);
                this.hintPath.Arrange(rect2);
            }
            return size;
        }

        private void AssignColors()
        {
            base.Background = GetFillBrush();
            this.Stroke = GetOutlineBrush();
        }

        private bool DoubleIsValid(double value)
        {
            return ((!double.IsNaN(value) && !double.IsInfinity(value)) && !(value == 0.0));
        }

        private Geometry GetArrowGeometry(Size rectangleSize, out Size arrowSize)
        {
            double horzOffset = 0.0;
            double vertOffset = 0.0;
            if (((this.HintStyle == NoteHintStyle.Simple) || (this.HintPosition == NoteHintPosition.None)) || (this.HintPosition == NoteHintPosition.Centered))
            {
                arrowSize = new Size(0.0, 0.0);
                return null;
            }
            switch (this.HintPosition)
            {
                case NoteHintPosition.Left:
                    horzOffset = rectangleSize.Width;
                    vertOffset = rectangleSize.Height * this.ArrowOffsetRatio;
                    break;

                case NoteHintPosition.Right:
                    horzOffset = 0.0;
                    vertOffset = rectangleSize.Height * this.ArrowOffsetRatio;
                    break;

                case NoteHintPosition.Up:
                    horzOffset = rectangleSize.Width * this.ArrowOffsetRatio;
                    vertOffset = rectangleSize.Height;
                    break;

                case NoteHintPosition.Down:
                    horzOffset = rectangleSize.Width * this.ArrowOffsetRatio;
                    vertOffset = 0.0;
                    break;
            }
            double lineThickness = (this.hintPath != null) ? this.hintPath.StrokeThickness : 0.0;
            return this.GetArrowGeometry(horzOffset, vertOffset, this.ArrowScale, this.HintPosition, lineThickness, out arrowSize);
        }

        private Geometry GetArrowGeometry(double horzOffset, double vertOffset, double scale, NoteHintPosition hintPosition, double lineThickness, out Size bounds)
        {
            Geometry baseArrowGeomentry = this.GetBaseArrowGeomentry();
            bounds = baseArrowGeomentry.Bounds.Size;
            TransformGroup group = new TransformGroup();
            switch (hintPosition)
            {
                case NoteHintPosition.Left:
                    group.Children.Add(new RotateTransform(-90.0, bounds.Width / 2.0, bounds.Height));
                    group.Children.Add(new TranslateTransform(bounds.Width / 2.0, -bounds.Height / 2.0));
                    group.Children.Add(new TranslateTransform(-2.0 * lineThickness, 0.0));
                    break;

                case NoteHintPosition.Right:
                    group.Children.Add(new RotateTransform(90.0, bounds.Width / 2.0, bounds.Height));
                    group.Children.Add(new TranslateTransform(-bounds.Width / 2.0, -bounds.Height / 2.0));
                    break;

                case NoteHintPosition.Up:
                    group.Children.Add(new TranslateTransform(0.0, -2.0 * lineThickness));
                    break;

                case NoteHintPosition.Down:
                    group.Children.Add(new RotateTransform(180.0, bounds.Width / 2.0, bounds.Height));
                    group.Children.Add(new TranslateTransform(0.0, -bounds.Height));
                    break;
            }
            group.Children.Add(new ScaleTransform(scale, scale));
            group.Children.Add(new TranslateTransform(horzOffset, vertOffset));
            baseArrowGeomentry.Transform = group;
            bounds = baseArrowGeomentry.Bounds.Size;
            return baseArrowGeomentry;
        }

        private Geometry GetBaseArrowGeomentry()
        {
            string text = "M 40.001144 0.01965503 19.97359 39.994846 0.00113897 -5.784127e-4 z";
            PathFigureCollectionConverter converter = new PathFigureCollectionConverter();
            PathFigureCollection figures = converter.ConvertFromString(text) as PathFigureCollection;
            return new PathGeometry { 
                Figures = figures,
                FillRule = FillRule.Nonzero
            };
        }

        private Point GetContentLocation(Size arrowSize)
        {
            double num = (this.hintPath != null) ? (this.hintPath.StrokeThickness / 2.0) : 0.0;
            switch (this.HintPosition)
            {
                case NoteHintPosition.Left:
                case NoteHintPosition.Up:
                    return new Point(this.CornerRadius + num, this.CornerRadius + num);

                case NoteHintPosition.Right:
                    return new Point((this.CornerRadius + arrowSize.Width) - num, this.CornerRadius + num);

                case NoteHintPosition.Down:
                    return new Point(this.CornerRadius + num, (this.CornerRadius + arrowSize.Height) - num);
            }
            return new Point(this.CornerRadius + num, this.CornerRadius + num);
        }

        private Size GetContentSize()
        {
            if (this.hintPresenter == null)
            {
                return new Size(100.0, 100.0);
            }
            Size desiredSize = this.hintPresenter.DesiredSize;
            if (!this.SizeIsValid(desiredSize))
            {
                desiredSize = new Size(this.hintPresenter.ActualWidth, this.hintPresenter.ActualHeight);
            }
            if (!this.SizeIsValid(desiredSize))
            {
                desiredSize = new Size(100.0, 100.0);
            }
            return desiredSize;
        }

        private static LinearGradientBrush GetFillBrush()
        {
            return new LinearGradientBrush(new GradientStopCollection { 
                new GradientStop(Color.FromRgb(0xff, 0xfe, 0xf7), 0.0),
                new GradientStop(Color.FromRgb(0xff, 0xfd, 0xf2), 0.05),
                new GradientStop(Color.FromRgb(250, 0xfb, 0xf8), 0.5),
                new GradientStop(Color.FromRgb(0xf7, 0xf6, 0xeb), 0.95),
                new GradientStop(Color.FromRgb(0xe5, 0xe3, 0xd7), 1.0)
            }, new Point(0.5, 0.0), new Point(0.5, 1.0));
        }

        private Size GetHintSize(NoteHintPosition hintPosition, Size rectangleSize, Size arrowSize)
        {
            Size size = rectangleSize;
            switch (hintPosition)
            {
                case NoteHintPosition.Left:
                case NoteHintPosition.Right:
                    return new Size(rectangleSize.Width + arrowSize.Width, rectangleSize.Height);

                case NoteHintPosition.Up:
                case NoteHintPosition.Down:
                    return new Size(rectangleSize.Width, rectangleSize.Height + arrowSize.Height);
            }
            return size;
        }

        private static LinearGradientBrush GetOutlineBrush()
        {
            return new LinearGradientBrush(new GradientStopCollection { 
                new GradientStop(Color.FromRgb(0xc2, 0xc1, 0xb3), 0.0),
                new GradientStop(Color.FromRgb(0x66, 0x65, 0x58), 1.0)
            }, new Point(0.5, 0.0), new Point(0.5, 1.0));
        }

        private RectangleGeometry GetRectangleGeometry(Size arrowSize, Size contentSize)
        {
            double horzOffset = 0.0;
            double vertOffset = 0.0;
            double num3 = (this.hintPath != null) ? this.hintPath.StrokeThickness : 0.0;
            if (this.HintStyle == NoteHintStyle.WithArrow)
            {
                switch (this.HintPosition)
                {
                    case NoteHintPosition.Right:
                        horzOffset += arrowSize.Width - num3;
                        break;

                    case NoteHintPosition.Down:
                        vertOffset += arrowSize.Height - num3;
                        break;
                }
            }
            return this.GetRectangleGeometry(horzOffset, vertOffset, contentSize, this.CornerRadius);
        }

        private RectangleGeometry GetRectangleGeometry(double horzOffset, double vertOffset, Size size, double cornerRadius)
        {
            RectangleGeometry geometry = new RectangleGeometry(new Rect(0.0, 0.0, size.Width, size.Height), cornerRadius, cornerRadius);
            TranslateTransform transform = new TranslateTransform(horzOffset, vertOffset);
            geometry.Transform = transform;
            return geometry;
        }

        private Size GetRectangleSize(Size contentSize, double cornerRadius)
        {
            return new Size(contentSize.Width + (2.0 * cornerRadius), contentSize.Height + (2.0 * cornerRadius));
        }

        private CombinedGeometry GetShapeGeometry(out Point contentLocation, out Point targetLocation)
        {
            Size size3;
            Size contentSize = this.GetContentSize();
            Size rectangleSize = this.GetRectangleSize(contentSize, this.CornerRadius);
            Geometry arrowGeometry = this.GetArrowGeometry(rectangleSize, out size3);
            Geometry rectangleGeometry = this.GetRectangleGeometry(size3, rectangleSize);
            contentLocation = this.GetContentLocation(size3);
            CombinedGeometry geometry3 = new CombinedGeometry {
                GeometryCombineMode = GeometryCombineMode.Union,
                Geometry1 = rectangleGeometry,
                Geometry2 = arrowGeometry
            };
            targetLocation = this.GetTargetLocation(geometry3.Bounds, size3);
            return geometry3;
        }

        private Point GetTargetLocation(Rect hintBounds, Size arrowSize)
        {
            return GetTargetLocation(hintBounds.Size, this.HintOffset, arrowSize, this.ArrowOffsetRatio, this.HintPosition);
        }

        public static Point GetTargetLocation(Size hintSize, Point hintOffset, Size arrowSize, double arrowOffsetRatio, NoteHintPosition hintPosition)
        {
            switch (hintPosition)
            {
                case NoteHintPosition.Left:
                    return new Point(hintSize.Width + hintOffset.X, (hintSize.Height * arrowOffsetRatio) + (arrowSize.Height / 2.0));

                case NoteHintPosition.Right:
                    return new Point(-hintOffset.X, (hintSize.Height * arrowOffsetRatio) + (arrowSize.Height / 2.0));

                case NoteHintPosition.Up:
                    return new Point((hintSize.Width * arrowOffsetRatio) + (arrowSize.Width / 2.0), hintSize.Height + hintOffset.Y);

                case NoteHintPosition.Down:
                    return new Point((hintSize.Width * arrowOffsetRatio) + (arrowSize.Width / 2.0), -hintOffset.Y);
            }
            return new Point(hintSize.Width * arrowOffsetRatio, hintSize.Height);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            if (this.hintPresenter == null)
            {
                return size;
            }
            this.hintPresenter.Measure(constraint);
            this.ValidateShapeGeomentry();
            if (this.hintPath != null)
            {
                this.hintPath.Measure(constraint);
            }
            Rect bounds = this.hintShapeGeometry.Bounds;
            double num = (this.hintPath != null) ? this.hintPath.StrokeThickness : 0.0;
            return new Size(bounds.Width + num, bounds.Height + num);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.hintPath = base.GetTemplateChild("path") as Path;
            this.rootElement = base.GetTemplateChild("root") as FrameworkElement;
            this.hintPresenter = base.GetTemplateChild("presenter") as ContentPresenter;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (this.ContentChanged != null)
            {
                this.ContentChanged(oldContent, newContent);
            }
        }

        protected virtual void OnShapeChanged()
        {
            if (this.ShapeChanged != null)
            {
                this.ShapeChanged(this, EventArgs.Empty);
            }
        }

        public void Refresh()
        {
            this.hintShapeGeometry = null;
            base.InvalidateMeasure();
            base.InvalidateArrange();
            base.InvalidateVisual();
        }

        private bool SizeIsValid(Size size)
        {
            return (this.DoubleIsValid(size.Width) && this.DoubleIsValid(size.Height));
        }

        private void ValidateShapeGeomentry()
        {
            if (this.hintShapeGeometry == null)
            {
                this.hintShapeGeometry = this.GetShapeGeometry(out this.hintContentLocation, out this.hintArrowTargetLocation);
                if (this.hintPath != null)
                {
                    this.hintPath.Data = this.hintShapeGeometry;
                }
                this.OnShapeChanged();
            }
        }

        public double ArrowOffsetRatio
        {
            get
            {
                return (double) base.GetValue(ArrowOffsetRatioProperty);
            }
            set
            {
                base.SetValue(ArrowOffsetRatioProperty, value);
            }
        }

        public double ArrowScale
        {
            get
            {
                return (double) base.GetValue(ArrowScaleProperty);
            }
            set
            {
                base.SetValue(ArrowScaleProperty, value);
            }
        }

        public Point ArrowTargetLocation
        {
            get
            {
                return this.hintArrowTargetLocation;
            }
        }

        public double CornerRadius
        {
            get
            {
                return (double) base.GetValue(CornerRadiusProperty);
            }
            set
            {
                base.SetValue(CornerRadiusProperty, value);
            }
        }

        public Point HintOffset
        {
            get
            {
                return (Point) base.GetValue(HintOffsetProperty);
            }
            set
            {
                base.SetValue(HintOffsetProperty, value);
            }
        }

        public NoteHintPosition HintPosition
        {
            get
            {
                return (NoteHintPosition) base.GetValue(HintPositionProperty);
            }
            set
            {
                base.SetValue(HintPositionProperty, value);
            }
        }

        public NoteHintStyle HintStyle
        {
            get
            {
                return (NoteHintStyle) base.GetValue(NoteHintStyleProperty);
            }
            set
            {
                base.SetValue(NoteHintStyleProperty, value);
            }
        }

        public Brush Stroke
        {
            get
            {
                return (Brush) base.GetValue(StrokeProperty);
            }
            set
            {
                base.SetValue(StrokeProperty, value);
            }
        }

        public double StrokeThickness
        {
            get
            {
                return (double) base.GetValue(StrokeThicknessProperty);
            }
            set
            {
                base.SetValue(StrokeThicknessProperty, value);
            }
        }
    }
}

