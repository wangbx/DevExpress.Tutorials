namespace DevExpress.NoteHint
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    internal class DragHelper
    {
        private FrameworkElement container;
        private FrameworkElement dragElement;
        private Point dragOffset;
        private bool isInDragging;
        private Window window;

        public event EventHandler StartDragging;

        public event EventHandler StopDragging;

        private void ehMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.isInDragging = true;
                this.dragOffset = e.GetPosition(this.container);
                this.dragElement.CaptureMouse();
                if (this.StartDragging != null)
                {
                    this.StartDragging(this, EventArgs.Empty);
                }
            }
        }

        private void ehMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.StopDrag();
        }

        private void ehMouseMove(object sender, MouseEventArgs e)
        {
            if (this.isInDragging)
            {
                Point position = e.GetPosition(this.container);
                double num = position.X - this.dragOffset.X;
                double num2 = position.Y - this.dragOffset.Y;
                Point point2 = new Point(this.window.Left + num, this.window.Top + num2);
                this.window.Left = point2.X;
                this.window.Top = point2.Y;
            }
        }

        public void Start(Window window, FrameworkElement container, FrameworkElement dragElement)
        {
            this.window = window;
            this.container = container;
            this.dragElement = dragElement;
            this.SubsribeEvents(dragElement);
        }

        public void Stop()
        {
            this.StopDrag();
            this.UnsubscribeEvents();
        }

        private void StopDrag()
        {
            if (this.isInDragging)
            {
                this.isInDragging = false;
                this.dragElement.ReleaseMouseCapture();
                if (this.StopDragging != null)
                {
                    this.StopDragging(this, EventArgs.Empty);
                }
            }
        }

        private void SubsribeEvents(FrameworkElement dragElement)
        {
            if (dragElement != null)
            {
                dragElement.MouseDown += new MouseButtonEventHandler(this.ehMouseDown);
                dragElement.MouseMove += new MouseEventHandler(this.ehMouseMove);
                dragElement.MouseLeftButtonUp += new MouseButtonEventHandler(this.ehMouseLeftButtonUp);
            }
        }

        private void UnsubscribeEvents()
        {
            if (this.dragElement != null)
            {
                this.dragElement.MouseDown -= new MouseButtonEventHandler(this.ehMouseDown);
                this.dragElement.MouseMove -= new MouseEventHandler(this.ehMouseMove);
                this.dragElement.MouseLeftButtonUp -= new MouseButtonEventHandler(this.ehMouseLeftButtonUp);
            }
        }
    }
}

