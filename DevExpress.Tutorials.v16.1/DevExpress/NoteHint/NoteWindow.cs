using DevExpress.Utils.Drawing.Helpers;
using DevExpress.Utils.Text;
using DevExpress.Utils.Text.Internal;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DevExpress.NoteHint
{
    public class NoteWindow : Window, IComponentConnector
    {
        private delegate void RefreshDelegate();

        private const string StrFadeInAnimationName = "uxFadeInAnimation";

        private Storyboard moveAnimation;

        private Rect startSnapRect;

        private DragHelper dragHelper;

        private System.Windows.Point snapPoint;

        private System.Windows.Point startScreenPoint;

        private bool useAnimationInLocation;

        internal Grid root;

        internal NoteHintContainer hintContainer;

        private bool _contentLoaded;

        public bool ShowHtmlCloseButton
        {
            get;
            set;
        }

        public object HintContent
        {
            get
            {
                return this.hintContainer.Content;
            }
            set
            {
                this.hintContainer.Content = value;
            }
        }

        public double CornerRadius
        {
            get
            {
                return this.hintContainer.CornerRadius;
            }
            set
            {
                this.hintContainer.CornerRadius = value;
            }
        }

        public NoteHintPosition HintPosition
        {
            get
            {
                return this.hintContainer.HintPosition;
            }
            set
            {
                this.hintContainer.HintPosition = value;
            }
        }

        public double ArrowScale
        {
            get
            {
                return this.hintContainer.ArrowScale;
            }
            set
            {
                this.hintContainer.ArrowScale = value;
            }
        }

        public double ArrowOffsetRatio
        {
            get
            {
                return this.hintContainer.ArrowOffsetRatio;
            }
            set
            {
                this.hintContainer.ArrowOffsetRatio = value;
            }
        }

        public System.Windows.Point HintOffset
        {
            get
            {
                return this.hintContainer.HintOffset;
            }
            set
            {
                this.hintContainer.HintOffset = value;
            }
        }

        public NoteHintStyle NoteHintStyle
        {
            get
            {
                return this.hintContainer.HintStyle;
            }
            set
            {
                this.hintContainer.HintStyle = value;
            }
        }

        public System.Windows.Media.Brush Fill
        {
            get
            {
                return this.hintContainer.Background;
            }
            set
            {
                this.hintContainer.Background = value;
            }
        }

        public System.Windows.Media.Brush Stroke
        {
            get
            {
                return this.hintContainer.Stroke;
            }
            set
            {
                this.hintContainer.Stroke = value;
            }
        }

        public double StrokeThickness
        {
            get
            {
                return this.hintContainer.StrokeThickness;
            }
            set
            {
                this.hintContainer.StrokeThickness = value;
            }
        }

        public NoteWindow()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.ehLoaded);
            base.DragEnter += new System.Windows.DragEventHandler(this.ehDragEnter);
            base.SizeChanged += new SizeChangedEventHandler(this.ehSizeChanged);
            this.dragHelper = new DragHelper();
            this.dragHelper.Start(this, this.hintContainer, this.hintContainer);
            this.dragHelper.StopDragging += new EventHandler(this.ehStopDragging);
            this.hintContainer.ShapeChanged += new EventHandler(this.ehHintContainerShapeChanged);
        }

        private void StartFadeInAnimation()
        {
            Storyboard storyboard = base.FindResource("uxFadeInAnimation") as Storyboard;
            if (storyboard != null)
            {
                Storyboard.SetTarget(storyboard, this);
                storyboard.Completed += new EventHandler(this.ehFadeInAnimationCompleted);
                storyboard.Begin();
            }
        }

        private void StopFadeInAnimation()
        {
            Storyboard storyboard = base.FindResource("uxFadeInAnimation") as Storyboard;
            if (storyboard != null)
            {
                storyboard.Completed -= new EventHandler(this.ehFadeInAnimationCompleted);
                storyboard.Stop();
                base.Opacity = 1.0;
            }
        }

        private Rect GetWindowBounds()
        {
            System.Windows.Point point = new System.Windows.Point(base.Left, base.Top);
            return new Rect(point.X, point.Y, base.ActualWidth, base.ActualHeight);
        }

        private Rect ScreenBounds()
        {
            System.Windows.Point location = base.PointToScreen(new System.Windows.Point(0.0, 0.0));
            return new Rect(location, base.RenderSize);
        }

        private Rect TransformRect(Rect rect, Matrix transform)
        {
            System.Windows.Point topLeft = rect.TopLeft;
            System.Windows.Point bottomRight = rect.BottomRight;
            System.Windows.Point location = transform.Transform(topLeft);
            System.Windows.Point point = transform.Transform(bottomRight);
            double width = point.X - location.X;
            double height = point.Y - location.Y;
            return new Rect(location, new System.Windows.Size(width, height));
        }

        private System.Windows.Point DeviceToWPF(System.Windows.Point point)
        {
            System.Windows.Point result;
            using (HwndSource hwndSource = new HwndSource(default(HwndSourceParameters)))
            {
                result = hwndSource.CompositionTarget.TransformFromDevice.Transform(point);
            }
            return result;
        }

        private Rect DeviceToWPF(Rect rect)
        {
            Rect result;
            using (HwndSource hwndSource = new HwndSource(default(HwndSourceParameters)))
            {
                result = this.DeviceToWPF(ref rect, hwndSource);
            }
            return result;
        }

        private Rect DeviceToWPF(ref Rect rect, HwndSource source)
        {
            Matrix transformFromDevice = source.CompositionTarget.TransformFromDevice;
            return this.TransformRect(rect, transformFromDevice);
        }

        private System.Windows.Point WPFToDevice(System.Windows.Point point)
        {
            System.Windows.Point result;
            using (HwndSource hwndSource = new HwndSource(default(HwndSourceParameters)))
            {
                result = this.WPFToDevice(ref point, hwndSource);
            }
            return result;
        }

        private System.Windows.Point WPFToDevice(ref System.Windows.Point point, HwndSource source)
        {
            return source.CompositionTarget.TransformToDevice.Transform(point);
        }

        private Screen GetStartScreen()
        {
            System.Windows.Point point = this.WPFToDevice(this.startScreenPoint);
            return Screen.FromPoint(new System.Drawing.Point((int)point.X, (int)point.Y));
        }

        private Rect GetStartScreenBounds()
        {
            Screen startScreen = this.GetStartScreen();
            if (startScreen == null)
            {
                return Rect.Empty;
            }
            return this.DeviceToWPF(new Rect((double)startScreen.WorkingArea.X, (double)startScreen.WorkingArea.Y, (double)startScreen.WorkingArea.Width, (double)startScreen.WorkingArea.Height));
        }

        private System.Windows.Point CalcTargetLocation(System.Windows.Point location, bool centerOverTarget)
        {
            System.Windows.Point result = location;
            Rect windowBounds = this.GetWindowBounds();
            if (centerOverTarget)
            {
                result = new System.Windows.Point(location.X - windowBounds.Width / 2.0, location.Y - windowBounds.Height / 2.0);
            }
            return result;
        }

        private bool HasScreenIntersection()
        {
            if (this.snapPoint.X == 0.0 && this.snapPoint.Y == 0.0)
            {
                return false;
            }
            Rect hintBounds = this.GetHintBounds(this.snapPoint, this.HintPosition);
            Screen startScreen = this.GetStartScreen();
            return startScreen != null && !this.DeviceToWPF(new Rect((double)startScreen.WorkingArea.X, (double)startScreen.WorkingArea.Y, (double)startScreen.WorkingArea.Width, (double)startScreen.WorkingArea.Height)).Contains(hintBounds);
        }

        private void StopMoveAnimation()
        {
            if (this.moveAnimation == null)
            {
                return;
            }
            this.moveAnimation.Completed -= new EventHandler(this.ehStoryboardCompleted);
            this.moveAnimation.Stop();
            this.moveAnimation = null;
        }

        private void AnimatioHintMotion(System.Windows.Point location)
        {
            if (this.moveAnimation != null)
            {
                this.StopMoveAnimation();
            }
            QuarticEase quarticEase = new QuarticEase();
            quarticEase.EasingMode = EasingMode.EaseOut;
            this.moveAnimation = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation(base.Left, location.X, new Duration(TimeSpan.FromMilliseconds(500.0)));
            doubleAnimation.EasingFunction = quarticEase;
            Storyboard.SetTarget(doubleAnimation, this);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Left", new object[0]));
            this.moveAnimation.Children.Add(doubleAnimation);
            DoubleAnimation doubleAnimation2 = new DoubleAnimation(base.Top, location.Y, new Duration(TimeSpan.FromMilliseconds(500.0)));
            doubleAnimation2.EasingFunction = quarticEase;
            Storyboard.SetTarget(doubleAnimation2, this);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Top", new object[0]));
            this.moveAnimation.Children.Add(doubleAnimation2);
            this.moveAnimation.Completed += new EventHandler(this.ehStoryboardCompleted);
            this.moveAnimation.Begin();
        }

        public void RefreshContainer()
        {
            this.hintContainer.Refresh();
        }

        private void LocateHint()
        {
            this.LocateHint(false);
        }

        private void LocateHint(bool firstRun)
        {
            if (!this.startSnapRect.IsEmpty && this.HasScreenIntersection())
            {
                this.UpdateSnapPoint(this.startSnapRect, false, ref this.snapPoint);
                this.startSnapRect = Rect.Empty;
                base.Dispatcher.BeginInvoke(new NoteWindow.RefreshDelegate(this.RefreshContainer), null);
            }
            System.Windows.Point location = this.snapPoint;
            System.Windows.Point location2 = this.CalcTargetLocation(location, this.HintPosition == NoteHintPosition.Centered);
            this.SetLocation(location2, true);
        }

        private bool CanMoveHorz(Line line, System.Windows.Point point, double moveDistance)
        {
            double num = point.X + moveDistance;
            return line.X1 <= num && line.X2 >= num;
        }

        private bool CanMoveVert(Line line, System.Windows.Point point, double moveDistance)
        {
            double num = point.Y + moveDistance;
            return line.Y1 <= num && line.Y2 >= num;
        }

        private System.Windows.Point GetHintTargetPoint(NoteHintPosition hintPosition)
        {
            Rect rect = this.ScreenBounds();
            System.Windows.Point result = new System.Windows.Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0);
            System.Windows.Point targetLocation = NoteHintContainer.GetTargetLocation(rect.Size, this.HintOffset, new System.Windows.Size(30.0, 30.0), this.ArrowOffsetRatio, hintPosition);
            if (hintPosition != NoteHintPosition.Centered)
            {
                result.X = rect.X + targetLocation.X;
                result.Y = rect.Y + targetLocation.Y;
            }
            return result;
        }

        private Rect GetHintBounds(System.Windows.Point location, NoteHintPosition hintPosition)
        {
            Rect windowBounds = this.GetWindowBounds();
            System.Windows.Point point = location;
            if (this.HintPosition == NoteHintPosition.Centered)
            {
                point = new System.Windows.Point(location.X - windowBounds.Width / 2.0, location.Y - windowBounds.Height / 2.0);
            }
            else
            {
                System.Windows.Point targetLocation = NoteHintContainer.GetTargetLocation(windowBounds.Size, this.HintOffset, new System.Windows.Size(30.0, 30.0), this.ArrowOffsetRatio, hintPosition);
                point = new System.Windows.Point(location.X - targetLocation.X, location.Y - targetLocation.Y);
            }
            return new Rect(point.X, point.Y, windowBounds.Width, windowBounds.Height);
        }

        private bool ValidateLeftIntersection(Rect screenBounds, Rect hintBounds, System.Windows.Point targetPoint, Line line, out double offset)
        {
            offset = 0.0;
            if (hintBounds.X < screenBounds.X)
            {
                offset = screenBounds.X - hintBounds.X;
                if (!this.CanMoveHorz(line, targetPoint, offset))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateRightIntersection(Rect screenBounds, Rect hintBounds, System.Windows.Point targetPoint, Line line, out double offset)
        {
            offset = 0.0;
            if (hintBounds.Right > screenBounds.Right)
            {
                offset = screenBounds.Right - hintBounds.Right;
                if (!this.CanMoveHorz(line, targetPoint, offset))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateUpIntersection(Rect screenBounds, Rect hintBounds, System.Windows.Point targetPoint, Line line, out double offset)
        {
            offset = 0.0;
            if (hintBounds.Y < screenBounds.Y)
            {
                offset = screenBounds.Y - hintBounds.Y;
                if (!this.CanMoveVert(line, targetPoint, offset))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateDownIntersection(Rect screenBounds, Rect hintBounds, System.Windows.Point targetPoint, Line line, out double offset)
        {
            offset = 0.0;
            if (hintBounds.Bottom < screenBounds.Bottom)
            {
                offset = screenBounds.Bottom - hintBounds.Bottom;
                if (!this.CanMoveVert(line, targetPoint, offset))
                {
                    return false;
                }
            }
            return true;
        }

        private System.Windows.Point CorrectScreenIntersectionsAlongLine(Line line, System.Windows.Point location, NoteHintPosition hintPosition, out bool canCorrect)
        {
            canCorrect = true;
            if (this.GetStartScreen() == null)
            {
                return location;
            }
            Rect hintBounds = this.GetHintBounds(location, hintPosition);
            Rect startScreenBounds = this.GetStartScreenBounds();
            if (startScreenBounds.Contains(hintBounds))
            {
                return location;
            }
            canCorrect = false;
            if (hintPosition == NoteHintPosition.Up || hintPosition == NoteHintPosition.Down)
            {
                double num;
                if (!this.ValidateLeftIntersection(startScreenBounds, hintBounds, location, line, out num))
                {
                    return location;
                }
                hintBounds.X += num;
                location.X += num;
                if (!this.ValidateRightIntersection(startScreenBounds, hintBounds, location, line, out num))
                {
                    return location;
                }
                hintBounds.X += num;
                location.X += num;
            }
            else if (hintPosition == NoteHintPosition.Left || hintPosition == NoteHintPosition.Right)
            {
                double num;
                if (!this.ValidateUpIntersection(startScreenBounds, hintBounds, location, line, out num))
                {
                    return location;
                }
                hintBounds.Y += num;
                location.Y += num;
                if (!this.ValidateDownIntersection(startScreenBounds, hintBounds, location, line, out num))
                {
                    return location;
                }
                hintBounds.Y += num;
                location.Y += num;
            }
            canCorrect = startScreenBounds.Contains(hintBounds);
            return location;
        }

        private System.Windows.Point CorrectScreenIntersections(System.Windows.Point location, Rect bounds)
        {
            if (this.GetStartScreen() == null)
            {
                return location;
            }
            System.Windows.Point result = location;
            Rect startScreenBounds = this.GetStartScreenBounds();
            if (result.X < startScreenBounds.X)
            {
                result.X = startScreenBounds.X;
            }
            if (result.X + bounds.Width > startScreenBounds.Right)
            {
                result.X -= result.X + bounds.Width - startScreenBounds.Right;
            }
            if (result.Y < startScreenBounds.Y)
            {
                result.Y = startScreenBounds.Y;
            }
            if (result.Y + bounds.Height > startScreenBounds.Bottom)
            {
                result.Y -= result.Y + bounds.Height - startScreenBounds.Bottom;
            }
            return result;
        }

        private System.Windows.Point GetSnapPoint(Rect snapRect, out NoteHintPosition hintPosition)
        {
            hintPosition = NoteHintPosition.Centered;
            Dictionary<Line, NoteHintPosition> dictionary = new Dictionary<Line, NoteHintPosition>();
            dictionary.Add(new Line(snapRect.Left, snapRect.Top, snapRect.Right, snapRect.Top), NoteHintPosition.Up);
            dictionary.Add(new Line(snapRect.Right, snapRect.Top, snapRect.Right, snapRect.Bottom), NoteHintPosition.Right);
            dictionary.Add(new Line(snapRect.Left, snapRect.Bottom, snapRect.Right, snapRect.Bottom), NoteHintPosition.Down);
            dictionary.Add(new Line(snapRect.Left, snapRect.Top, snapRect.Left, snapRect.Bottom), NoteHintPosition.Left);
            double num = -1.0;
            System.Windows.Point result = new System.Windows.Point(0.0, 0.0);
            foreach (Line current in dictionary.Keys)
            {
                NoteHintPosition hintPosition2 = dictionary[current];
                System.Windows.Point hintTargetPoint = this.GetHintTargetPoint(hintPosition2);
                System.Windows.Point point;
                double num2 = current.GetDistance(hintTargetPoint, out point);
                if (!current.ContainsPoint(point))
                {
                    num2 = current.GetDistanceToCenterPoint(hintTargetPoint, out point);
                }
                bool flag;
                point = this.CorrectScreenIntersectionsAlongLine(current, point, hintPosition2, out flag);
                if (flag)
                {
                    num2 = Line.GetLength(hintTargetPoint.X, hintTargetPoint.Y, point.X, point.Y);
                    if (num == -1.0 || num2 < num)
                    {
                        hintPosition = dictionary[current];
                        num = num2;
                        result = point;
                    }
                }
            }
            return result;
        }

        private void UpdateSnapPoint(Rect snapRect, bool staysAtPos, ref System.Windows.Point snapPoint)
        {
            if (staysAtPos)
            {
                snapPoint = snapRect.Location;
                snapPoint.Offset(this.hintContainer.ArrowTargetLocation.X, this.hintContainer.ArrowTargetLocation.Y);
                return;
            }
            NoteHintPosition hintPosition;
            snapPoint = this.GetSnapPoint(snapRect, out hintPosition);
            this.HintPosition = hintPosition;
        }

        private void SetLocation(System.Windows.Point location, bool correctIntersections)
        {
            System.Windows.Point location2 = location;
            if (this.HintPosition != NoteHintPosition.Centered)
            {
                location2 = new System.Windows.Point(location.X - this.hintContainer.ArrowTargetLocation.X, location.Y - this.hintContainer.ArrowTargetLocation.Y);
            }
            if (correctIntersections)
            {
                Rect windowBounds = this.GetWindowBounds();
                location2 = this.CorrectScreenIntersections(location2, windowBounds);
            }
            if (this.useAnimationInLocation)
            {
                this.AnimatioHintMotion(location2);
                return;
            }
            base.Left = location2.X;
            base.Top = location2.Y;
        }

        private void Snap(Rect snapRect, bool staysAtPos)
        {
            if (this.HintPosition == NoteHintPosition.Centered || this.HintPosition == NoteHintPosition.None)
            {
                return;
            }
            System.Windows.Point location = snapRect.Location;
            this.UpdateSnapPoint(snapRect, staysAtPos, ref location);
            this.useAnimationInLocation = true;
            this.snapPoint = location;
            this.hintContainer.Refresh();
        }

        private System.Windows.Point GetFirstSnapPoint(Rect snapRect)
        {
            switch (this.HintPosition)
            {
                case NoteHintPosition.Left:
                    return new System.Windows.Point(snapRect.Left, snapRect.Top + snapRect.Height / 2.0);
                case NoteHintPosition.Right:
                    return new System.Windows.Point(snapRect.Right, snapRect.Top + snapRect.Height / 2.0);
                case NoteHintPosition.Up:
                    return new System.Windows.Point(snapRect.Left + snapRect.Width / 2.0, snapRect.Top);
                case NoteHintPosition.Down:
                    return new System.Windows.Point(snapRect.Left + snapRect.Width / 2.0, snapRect.Bottom);
                case NoteHintPosition.Centered:
                    return new System.Windows.Point(snapRect.Left + snapRect.Width / 2.0, snapRect.Top + snapRect.Height / 2.0);
            }
            return snapRect.Location;
        }

        private void ehSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.SizeChanged -= new SizeChangedEventHandler(this.ehSizeChanged);
            this.LocateHint(true);
        }

        private void ehLoaded(object sender, RoutedEventArgs e)
        {
            base.Loaded -= new RoutedEventHandler(this.ehLoaded);
            this.LocateHint();
            this.StartFadeInAnimation();
        }

        private void ehDragEnter(object sender, System.Windows.DragEventArgs e)
        {
            this.StopMoveAnimation();
        }

        private void ehStopDragging(object sender, EventArgs e)
        {
            this.Snap(this.startSnapRect, false);
        }

        private void ehFadeInAnimationCompleted(object sender, EventArgs e)
        {
            this.StopFadeInAnimation();
        }

        private void ehHintContainerShapeChanged(object sender, EventArgs e)
        {
            this.LocateHint();
        }

        private void ehStoryboardCompleted(object sender, EventArgs e)
        {
            if (this.moveAnimation == null)
            {
                return;
            }
            this.moveAnimation.Completed -= new EventHandler(this.ehStoryboardCompleted);
            this.moveAnimation.Stop();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.DragEnter -= new System.Windows.DragEventHandler(this.ehDragEnter);
            if (this.dragHelper != null)
            {
                this.dragHelper.Stop();
            }
            base.OnClosed(e);
        }

        public void Show(Rectangle snapRect)
        {
            this.Show(new Rect((double)snapRect.X, (double)snapRect.Y, (double)snapRect.Width, (double)snapRect.Height));
        }

        public void Show(Rect snapRect)
        {
            this.startSnapRect = snapRect;
            this.snapPoint = this.GetFirstSnapPoint(snapRect);
            this.startScreenPoint = this.snapPoint;
            base.Show();
        }

        public void SetHtmlContent(string text, IStringImageProvider imageProvider)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.AddRange(this.Parse(text, imageProvider));
            stackPanel.Children.Add(textBlock);
            if (this.ShowHtmlCloseButton)
            {
                System.Windows.Controls.Button button = new System.Windows.Controls.Button
                {
                    Content = "Hide",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Width = 50.0
                };
                button.Click += delegate (object s, RoutedEventArgs e)
                {
                    base.Hide();
                };
                stackPanel.Children.Add(button);
            }
            this.HintContent = stackPanel;
        }

        private List<Inline> Parse(string text, IStringImageProvider imageProvider)
        {
            List<Inline> list = new List<Inline>();
            List<StringBlock> list2 = StringParser.Parse(12f, text, true);
            int num = 0;
            foreach (StringBlock current in list2)
            {
                if (current.LineNumber != num)
                {
                    list.Add(new LineBreak());
                }
                num = current.LineNumber;
                Inline inline = null;
                switch (current.Type)
                {
                    case StringBlockType.Text:
                        inline = this.CreateTextBlock(current);
                        break;
                    case StringBlockType.Image:
                        inline = this.CreateImageBlock(current, imageProvider);
                        break;
                    case StringBlockType.Link:
                        inline = this.CreateLink(current);
                        break;
                }
                if (inline != null)
                {
                    list.Add(inline);
                }
            }
            return list;
        }

        private Inline CreateImageBlock(StringBlock block, IStringImageProvider imageProvider)
        {
            if (imageProvider == null)
            {
                return null;
            }
            Bitmap bitmap = (Bitmap)imageProvider.GetImage(block.ImageName);
            if (bitmap == null)
            {
                return null;
            }
            IntPtr hbitmap = bitmap.GetHbitmap();
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Stretch = Stretch.None;
            image.SnapsToDevicePixels = true;
            image.Source = bitmapSource;
            image.Width = bitmapSource.Width + 1.0;
            image.Height = bitmapSource.Height + 1.0;
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            InlineUIContainer result = new InlineUIContainer(image);
            NativeMethods.DeleteObject(hbitmap);
            return result;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private Inline CreateLink(StringBlock block)
        {
            Hyperlink hyperlink = new Hyperlink(new Run(block.Text))
            {
                NavigateUri = new Uri(block.Link)
            };
            Inline result = hyperlink;
            hyperlink.RequestNavigate += delegate (object s, RequestNavigateEventArgs e)
            {
                this.OpenLink(block.Link);
            };
            return result;
        }

        private void OpenLink(string link)
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(link);
                process.Start();
            }
        }

        private Inline CreateTextBlock(StringBlock block)
        {
            if (block.Text == "\r")
            {
                return new Run();
            }
            Run run = new Run(block.Text);
            Inline inline = run;
            inline.FontSize = (double)block.FontSettings.Size;
            System.Drawing.Color color = block.FontSettings.Color;
            if (color != System.Drawing.Color.Empty)
            {
                inline.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            if ((block.FontSettings.Style & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold)
            {
                inline = new Bold(inline);
            }
            if ((block.FontSettings.Style & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic)
            {
                inline = new Italic(inline);
            }
            if ((block.FontSettings.Style & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
            {
                inline = new Underline(inline);
            }
            if ((block.FontSettings.Style & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Strikeout)
            {
                inline = new Underline(inline);
            }
            return inline;
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
            {
                return;
            }
            this._contentLoaded = true;
            Uri resourceLocator = new Uri("/DevExpress.Tutorials.v16.1;component/description/hint/notewindow.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler)
        {
            return Delegate.CreateDelegate(delegateType, this, handler);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.root = (Grid)target;
                    return;
                case 2:
                    this.hintContainer = (NoteHintContainer)target;
                    return;
                default:
                    this._contentLoaded = true;
                    return;
            }
        }
    }
}