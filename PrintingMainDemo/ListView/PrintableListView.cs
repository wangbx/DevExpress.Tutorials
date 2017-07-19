using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace XtraPrintingDemos.PrintableList
{
	public class PrintableListView : System.Windows.Forms.ListView, IPrintable {
		private System.ComponentModel.Container components = null;
		private int offsetx = 0;
		private ImageList imageList = null;

		private IPrintingSystem ps;
		private IBrickGraphics graph;
		private PropertyEditorControl editorControl;

        bool IPrintable.CreatesIntersectedBricks {
            get { return true; }
        }
        bool IPrintable.HasPropertyEditor() {
			return true;
		}

		UserControl IPrintable.PropertyEditorControl { 
			get { 
				if(editorControl == null) editorControl = new PropertyEditorControl(this.View);
				return editorControl;
			}
		}

		public PrintableListView() {
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		bool IPrintable.SupportsHelp() {
			return false;
		}
		void IPrintable.ShowHelp() {
		}
		void IPrintable.AcceptChanges() {
            if(editorControl != null)
                View = editorControl.View;
		}
		void IPrintable.RejectChanges() {
            if(editorControl != null)
                editorControl.View = View;
        }
		void IBasePrintable.Initialize(IPrintingSystem ps, ILink link) {
			this.ps  = ps;
			imageList = (View == View.SmallIcon || View == View.List || View == View.Details) ? SmallImageList :
				(View == View.LargeIcon) ? LargeImageList : null;
			offsetx = (imageList == null) ? 0 : imageList.ImageSize.Height;
		}
		void IBasePrintable.Finalize(IPrintingSystem ps, ILink link) {
		}
		void IBasePrintable.CreateArea(string areaName, IBrickGraphics graph) {
			this.graph = graph;
			if( areaName.Equals("PageFooter") )
				CreatePageFooter();
			else if( areaName.Equals("DetailHeader") )
				CreateDetailHeader();
			else if( areaName.Equals("Detail") )
				CreateDetail();
		}

		private Rectangle GetCellBounds(int pi, int pj) {
			Rectangle r = Rectangle.Empty;
			for (int i = 0; i < pi; i++) 
				r.X += Columns[i].Width;
			
			r.Y += Font.Height + 4;

			for (int i = 0; i < pj; i++) 
				r.Y += Items[i].Bounds.Y;
			r.Width = Columns[pi].Width;
			r.Height = Items[pj].Bounds.Height;
			return r;
		}

		private IBrick DrawBrick(string typeName, RectangleF rect) {
			IBrick brick = ps.CreateBrick(typeName);
			return graph.DrawBrick(brick, rect);
		}

		private IBrick DrawBrick(string typeName, object[,] properties, RectangleF rect) {
			IBrick brick = ps.CreateBrick(typeName);
			brick.SetProperties(properties);
			return graph.DrawBrick(brick, rect);
		}

		private void CreatePageFooter() {
			string format = "Page {0} of {1}";
			Font brickFont = new Font("Arial", 9);
			graph.DefaultBrickStyle = new BrickStyle(BorderSide.None, 1, 
				Color.Black, Color.Transparent,	Color.Black, brickFont, 
				new BrickStringFormat(StringAlignment.Center, StringAlignment.Center));

			float brickHeight = brickFont.Height + 2;
			//ps.SetProperty("ClipPageFooter", false);
			
			RectangleF r = new RectangleF(0, 0, 0, brickHeight);

			DrawBrick("PageInfoBrick", new object[,] { {"PageInfo",PageInfo.NumberOfTotal}, 
				{"Format",format}, {"Alignment",BrickAlignment.Far}, {"AutoWidth",true} }, r);
			DrawBrick("PageInfoBrick", new object[,] { {"Alignment",BrickAlignment.Near},
				{"AutoWidth",true}, {"PageInfo",PageInfo.DateTime} }, r);
		}

		private void CreateDetailHeader() {
			if(View != View.Details) return;

			StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
			sf.LineAlignment = StringAlignment.Near;

			graph.DefaultBrickStyle = new BrickStyle(BorderSide.All, 1, Color.Black,
				SystemColors.Control, SystemColors.ControlText, this.Font, new BrickStringFormat(sf));

			Rectangle r = Rectangle.Empty;
			r.Y = 1;
			for (int i = 0; i < Columns.Count; i++) {
				r.Width = Columns[i].Width;
				r.Height = Font.Height + 4;
				TextBrick brick = (TextBrick)DrawBrick("TextBrick", r);
				brick.Text = Columns[i].Text;
				r.Offset(Columns[i].Width, 0);
			}
		}

		private void CreateDetail() {
			if(View == View.Details) CreateDetails();
			else if(View == View.LargeIcon || View == View.SmallIcon || View == View.List) 
				CreateIcons();
		}
		
		private void DrawDetailImage(ListViewItem item, Rectangle bounds) {
			int index = item.ImageIndex;
			if(index < 0) return;
			
			Rectangle r = bounds;
			r.Size = imageList.ImageSize;
			r.Offset(2, (bounds.Height - r.Height) / 2);
			IBrick brick = DrawBrick("ImageBrick", r);
			brick.SetProperties(new object[,] { {"Image", imageList.Images[index]}, {"Sides", BorderSide.None}, {"BackColor", Color.Transparent} });
		}

		private void DrawDetailText(ListViewItem item, Rectangle bounds) {
			Rectangle r = bounds;
			r.Width = bounds.Width - (imageList.ImageSize.Width + 2);
			r.Offset(bounds.Width - r.Width, 0);
			IBrick brick = DrawBrick("TextBrick", r);
			brick.SetProperties(new object[,] { {"Text", item.SubItems[0].Text}, {"Sides", BorderSide.None}, {"BackColor", Color.Transparent} });
		}

		private void CreateDetails() {
			Rectangle r = Rectangle.Empty;

			StringFormat sf = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.LineLimit);
			sf.LineAlignment = StringAlignment.Near;
			graph.DefaultBrickStyle.StringFormat = new BrickStringFormat(sf);
			graph.DefaultBrickStyle.BackColor = SystemColors.Window;
			graph.DefaultBrickStyle.BorderColor = SystemColors.Control;
			graph.DefaultBrickStyle.Sides = GridLines ? BorderSide.All : BorderSide.None;

			Point pt = Point.Empty;
			if(Items.Count > 0) {
				Rectangle itemBounds = Items[0].Bounds;
				pt = itemBounds.Location;
				pt.Y -= 3;
			}

			for(int i = 0; i < Items.Count; i++) {
				ListViewItem item = Items[i];
				graph.DefaultBrickStyle.Font = item.Font;
				graph.DefaultBrickStyle.BackColor = item.BackColor;
				graph.DefaultBrickStyle.ForeColor = item.ForeColor;

				r = item.Bounds;
				r.Offset(-pt.X, -pt.Y);
	
				for(int j = 0; j < Columns.Count; j++) {
					ColumnHeader column = Columns[j];
					r.Width = column.Width;
					
					if(j == 0 && imageList != null) {
						DrawBrick("VisualBrick", r);
						DrawDetailImage(item, r);
						DrawDetailText(item, r);
					} else {
						TextBrick brick = (TextBrick)DrawBrick("TextBrick", r);
						brick.Text = item.SubItems[j].Text;
					}
					r.Offset(r.Width, 0);
				}
			}
		}

		void CreateIcons() {
			graph.DefaultBrickStyle.BackColor = Color.Transparent;
			graph.DefaultBrickStyle.BorderColor = Color.Black;
			graph.DefaultBrickStyle.Sides = BorderSide.None;

            Size imageSize = imageList.ImageSize;
			if (offsetx != 0) {
				for (int i = 0; i < Items.Count; i++) {
					ListViewItem item = Items[i];

                    Point imageLocation = item.Bounds.Location;
                    if(View == View.LargeIcon)
                        imageLocation.X += (item.Bounds.Width - imageSize.Width) / 2;
                    else
                        imageLocation.Y += (item.Bounds.Height - imageSize.Height) / 2;

                    Rectangle imageRect = new Rectangle(imageLocation, imageSize);
                    if(item.ImageIndex < 0) 
						DrawBrick("VisualBrick", imageRect);
					else {
                        ImageBrick brick = (ImageBrick)DrawBrick("ImageBrick", imageRect);
                        brick.Image = imageList.Images[item.ImageIndex];
					}
				}
				offsetx += 3;
			}

			graph.DefaultBrickStyle.StringFormat = new BrickStringFormat(StringFormatFlags.LineLimit, StringAlignment.Near, StringAlignment.Near);
			for (int i = 0; i < Items.Count; i++) {
				ListViewItem item = Items[i];
				graph.DefaultBrickStyle.Font = item.Font;
				graph.DefaultBrickStyle.BackColor = (item.BackColor == SystemColors.Window) ? Color.Transparent : item.BackColor;
				graph.DefaultBrickStyle.ForeColor = item.ForeColor;
				
                SizeF textSize = MeasureString(item.Text);
                PointF textLocation = item.Bounds.Location;
                if(View == View.LargeIcon) {
                    textLocation.X += (item.Bounds.Width - textSize.Width) / 2f;
                    textLocation.Y = item.Bounds.Bottom - textSize.Height;
                } else {
                    textLocation.X += imageSize.Width;
                    textLocation.Y += (item.Bounds.Height - textSize.Height) / 2f;
                }

                TextBrick brick = (TextBrick)DrawBrick("TextBrick", new RectangleF(textLocation, textSize));
				brick.Text = item.Text;
			}
		}

		SizeF MeasureString(string text) {
            DevExpress.XtraPrinting.BrickGraphics brickGraphics = (BrickGraphics)graph;
            SizeF measuredSize = brickGraphics.MeasureString(text);
            measuredSize.Width += 2;	// Border size
            measuredSize.Height += 2;	// Border size
            return measuredSize;
		}
	}
}
