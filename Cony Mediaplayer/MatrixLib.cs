// Copyright (C) 2002 by Nic Wilson  http://www.nicsoft.com.au
// Written by Nic Wilson nicw@bigpond.net.au
// All rights reserved
//*******************************************
// .NET realisation written by Mikhail Cholokhov
// mikhail.cholokhov@t-online.de
/*
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;


// www.pierotofy.it   Thejuster
//Attenzione In caso che il compilatore continua a dare fastidiosi problemi per questa classe
//eliminatela.

namespace MatrixLib
{
	public  enum		ScrollDirection {Left = 0, Right, Up, Down};

	public class MatrixControl : System.Windows.Forms.Control
	{
		

		private static  int CHARWIDTH		=	19;
		private static  int CHARHEIGHT		=	27;
		private static  int XSPACING		=	3;
		private static  int YSPACING		=	3;
		private static  int CHARWIDTHSMALL	=	14;
		private static  int CHARHEIGHTSMALL =	20;
		private static  int XSPACINGSMALL	=	2;
		private static  int YSPACINGSMALL	=	2;
		private static  int CHARWIDTHTINY	=	10;
		private static  int CHARHEIGHTINY	=	14;
		private static  int XSPACINGTINY	=	1;
		private static  int YSPACINGTINY	=	1;

		public	enum		MatrixSize		{Small = 0, Large, Tiny};

		private char		m_cPadChar;
		private string		m_StrText;
		private bool		
			m_bModified
			, m_bImmediateUpdate
			, m_bTimer
			, m_bAutoPad;	
		private Color		m_OffColor
			,m_crOnColor
			, m_crBackColor;
		private Timer		m_Timer;
		private Bitmap		m_ImageMatrix;
		private int			
			m_CharWidth
			,m_CharHeight
			,m_XSpacing
			,m_YSpacing;
		private int			
			m_MaxYChars
			,m_MaxXChars;
		private MatrixControl.MatrixSize m_Size = MatrixSize.Small;
		private	ScrollDirection	m_Direction		= ScrollDirection.Left;
		private Color[]		SEGM_COLORS;


		private System.Resources.ResourceManager rm;
		/// <summary>
		/// Costruttore di Default
		/// 
		/// </summary>
		public	MatrixControl()
		{
			m_Direction = ScrollDirection.Left;
			m_bTimer	= false;
			m_bAutoPad	= false;
			m_Timer	= new System.Windows.Forms.Timer();
			m_Timer.Tick += new EventHandler(this.OnTimer);
			rm  = new System.Resources.ResourceManager("MatrixLib.Resource1", System.Reflection.Assembly.GetAssembly(this.GetType()));
			
			SEGM_COLORS = new Color[]{
										 Color.FromArgb(63, 181, 255),	// BLU ON
										 Color.FromArgb(23, 64,  103),	// BLU OFF
										 Color.FromArgb(0,  0,   0),	// Nero
		
			};
			SetMatrixSize(m_Size);
			SetDisplayColors(Color.FromArgb(0, 0,50), Color.FromArgb(63, 181, 255),Color.FromArgb(23, 64,  103));
			
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			this.SetStyle(ControlStyles.UserPaint, true);
		}
		public	MatrixControl(MatrixSize size)
		{
			m_Direction = ScrollDirection.Left;
			m_bTimer	= false;
			m_bAutoPad	= false;
			m_Timer	= new System.Windows.Forms.Timer();
			m_Timer.Tick += new EventHandler(this.OnTimer);
			rm  = new System.Resources.ResourceManager("MatrixLib.Resource1", System.Reflection.Assembly.GetAssembly(this.GetType()));
			
			SEGM_COLORS = new Color[]{
										 Color.FromArgb(63, 181, 255),	// BLUE ON
										 Color.FromArgb(23, 64,  103),	// BLUE OFF
										 Color.FromArgb(0,  0,   0),	// BLACK
		
			};
			m_Size = size;
			SetMatrixSize(size);
			SetDisplayColors(Color.FromArgb(0, 0,50), Color.FromArgb(63, 181, 255),Color.FromArgb(23, 64,  103));
			//Prevenzione dell'effetto Flik
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			this.SetStyle(ControlStyles.UserPaint, true);
		}
		public	Color BkColor
		{
			set 
			{
				m_crBackColor = Color.FromArgb(value.R,value.G,value.B);
			}
		}
		public	Color OnColor
		{
			set 
			{
				m_crOnColor = Color.FromArgb(value.R,value.G,value.B);
			}
		}
		public	Color OffColor
		{
			set 
			{
				m_OffColor = Color.FromArgb(value.R,value.G,value.B);
			}
		}
		public  ScrollDirection Direction
		{
			get {return m_Direction;}
			set {m_Direction = value;}
		}
		public	Rectangle GetCharBmpOffset(char ch )
		{
			Debug.WriteLine( new StackFrame(true).GetMethod().Name);
			int 
				_left
				,_right;

	
			if ((int)ch >= 127 || (int)ch <= 31)
				ch = ' ';
			int val = (int)ch;
			_left = (val - 32);
			if (_left < 32)
				_left *= (m_CharWidth + m_XSpacing);
			else if (_left < 64)
				_left = (_left - 32) * (m_CharWidth + m_XSpacing);
			else
				_left = (_left - 64) * (m_CharWidth + m_XSpacing);
			_right	=	((val - 32)/ 32) * (m_CharHeight + m_YSpacing);

			return new Rectangle(_left,_right,m_CharWidth,m_CharHeight);
		}
		public void GetCharBmpOffset(char ch, ref Rectangle rc)
		{
			int 
				_left
				,_right;

			if ((int)ch >= 127 || (int)ch <= 31)
				ch = ' ';
			int val = (int)ch;
			_left = (val - 32);
			if (_left < 32)
				_left *= (m_CharWidth + m_XSpacing);
			else if (_left < 64)
				_left = (_left - 32) * (m_CharWidth + m_XSpacing);
			else
				_left = (_left - 64) * (m_CharWidth + m_XSpacing);
			_right	=	((val - 32)/ 32) * (m_CharHeight + m_YSpacing);

			rc = new Rectangle(_left,_right,m_CharWidth,m_CharHeight);

		}
		public  string ScreenText
		{
			set
			{
				if (m_StrText != value)
				{
					m_StrText = value;
				
					m_bModified = true;
					if ((m_MaxXChars * m_MaxYChars) > m_StrText.Length)
					{
						string last = new String(' ',(m_MaxXChars*m_MaxYChars)- m_StrText.Length);
						m_StrText += last;
					}
					if (m_MaxXChars == 0)
						SetXCharsPerLine(0);
					if (m_MaxYChars == 0)
						SetNumberOfLines(0);
					if (m_bImmediateUpdate)
						this.Refresh();
					else
						Invalidate();
				}

			}
		}
		public	void SetText(string text)
		{
			if (m_StrText != text)
			{
				m_StrText = text;
				
				m_bModified = true;
				if ((m_MaxXChars*m_MaxYChars) > m_StrText.Length)
				{
					string last = new String(' ',(m_MaxXChars*m_MaxYChars)- m_StrText.Length);
					m_StrText += last;
				}
				if (m_MaxXChars == 0)
					SetXCharsPerLine(0);
				if (m_MaxYChars == 0)
					SetNumberOfLines(0);
				if (m_bImmediateUpdate)
					this.Refresh();
				else
					Invalidate();
			}
		}

		private	void SetMatrixSize(MatrixSize size)
		{
			try
			{
				if (size == MatrixSize.Small)
				{
					m_CharWidth  = CHARWIDTHSMALL;
					m_CharHeight = CHARHEIGHTSMALL;
					m_XSpacing   = XSPACINGSMALL;
					m_YSpacing   = YSPACINGSMALL;
					m_ImageMatrix = (Bitmap)rm.GetObject("MATRIXSMALL");
                  
				}
				else if (size == MatrixSize.Tiny)
				{
					m_CharWidth  = CHARWIDTHTINY;
					m_CharHeight = CHARHEIGHTINY;
					m_XSpacing   = XSPACINGTINY;
					m_YSpacing   = YSPACINGTINY;
					m_ImageMatrix = (Bitmap)rm.GetObject("MATRIXTINY");
				}
				else
				{
					m_CharWidth  = CHARWIDTH;
					m_CharHeight = CHARHEIGHT;
					m_XSpacing   = XSPACING;
					m_YSpacing   = YSPACING;
					m_ImageMatrix = (Bitmap)rm.GetObject("MATRIXLARGE");
				}
			}
			catch
			{
				MessageBox.Show("can not load resource picture");
			}
			Invalidate();
		}

		private	void SetBitmapResource(Bitmap _bitmap)
		{
			m_ImageMatrix = _bitmap;
		}

		private	void OnTimer(object sender, EventArgs e ) 
		{ 
			Debug.WriteLine( new StackFrame(true).GetMethod().Name);
			switch (m_MaxYChars)
			{
				case 1:
				{
					if (m_bAutoPad == true)
					{
						if (m_StrText.Length < m_MaxXChars)
						{
							int diff =  m_MaxXChars - m_StrText.Length;
							for (;diff > 0; diff--)
								m_StrText += m_cPadChar;
						}
					}
					if (m_Direction  == ScrollDirection.Left)
						m_StrText = m_StrText.Substring(1) + m_StrText[0]; //Move the first character to the end.
					else
					{
						int len = m_StrText.Length;
						string R = m_StrText [len-1].ToString();
						string L = m_StrText.Substring(0,len -1);
						m_StrText = R+L;
					}
					break;
				}
				default:
				{
					if (m_bAutoPad == true)
					{
						if (m_StrText.Length < (m_MaxXChars * m_MaxYChars))
						{
							int diff = m_MaxXChars * m_MaxYChars - m_StrText.Length;
							for (;diff > 0; diff--)
								m_StrText += m_cPadChar;
						}
					}
					switch (m_Direction)
					{
						case ScrollDirection.Down:
						{
							string tmp = m_StrText.Substring(m_StrText.Length - m_MaxXChars);
							m_StrText = tmp + m_StrText.Substring(0,m_StrText.Length - m_MaxXChars);
							break;
						}
						case ScrollDirection.Up:
						{
							string tmp = m_StrText.Substring(0,m_MaxXChars);
							m_StrText = m_StrText.Substring(m_MaxXChars) + tmp;
							break;
						}
						case ScrollDirection.Left:
						{
							int loop = m_StrText.Length / m_MaxXChars;
							if (loop == 0 || m_StrText.Length < m_MaxXChars)
								loop = 1;
							int offset = 0;
							for (;loop > 0; loop--)
							{
								m_StrText = m_StrText.Insert((offset + m_MaxXChars), m_StrText[offset].ToString());
								m_StrText = m_StrText.Remove(offset, 1);
								offset += m_MaxXChars;
							}
							break;
						}
						case ScrollDirection.Right:
						{
							int loop = m_StrText.Length / m_MaxXChars;
							if (loop == 0 || m_StrText.Length < m_MaxXChars)
								return;
							int offset = 0;
							for (;loop > 0; loop--)
							{
								m_StrText = m_StrText.Insert(offset, m_StrText[offset+(m_MaxXChars-1)].ToString());
								try
								{
									m_StrText = m_StrText.Remove(offset+(m_MaxXChars),1);
								}
								catch (System.Exception ex)
								{
									Debug.Fail(ex.Message);
								}
								offset += m_MaxXChars;
							}
							break;
						}
							
					}

				}
				break;
			}
			Invalidate(false);
		} 
		public	void DoScroll(int speed, ScrollDirection dir)
		{
			if (m_bTimer == false)
			{
				m_bTimer			= true;
				m_Direction			=  dir;
				m_Timer.Interval	= speed;
				m_Timer.Start();
			}
		}
		public	void StopScroll()
		{
			if (m_bTimer == true)
			{
				m_Timer.Stop();
				m_bTimer = false;
			}
		}
        public void about()
        {

            MessageBox.Show("Matrix Contro By Nic Wilson\nSelected by MakingItalia");
        }

		protected override void OnPaint(PaintEventArgs pea) 
		{
			Rectangle m_rect		= this.DisplayRectangle;
			//Create buffer image
			Bitmap _buffImage		= new Bitmap(m_rect.Width,m_rect.Height);
			Graphics _buff			= Graphics.FromImage(_buffImage);
			int x = 0, y = 0;
			int strlen = m_StrText.Length;
			if (strlen == 0)
				return;
			//Create array of color maps
			System.Drawing.Imaging.ColorMap []_ColorMap = new System.Drawing.Imaging.ColorMap[3];
			_ColorMap[0] = new System.Drawing.Imaging.ColorMap();
			_ColorMap[1] = new System.Drawing.Imaging.ColorMap();
			_ColorMap[2] = new System.Drawing.Imaging.ColorMap();
			_ColorMap[0].OldColor	= SEGM_COLORS[0]; 
			_ColorMap[0].NewColor	= m_crOnColor;
			_ColorMap[1].OldColor	= SEGM_COLORS[1]; 
			_ColorMap[1].NewColor	= m_OffColor;
			_ColorMap[2].OldColor	= SEGM_COLORS[2]; 
			_ColorMap[2].NewColor	= m_crBackColor;
			
			//Obtain image attributes
			System.Drawing.Imaging.ImageAttributes bmpAttr = new System.Drawing.Imaging.ImageAttributes();
			try
			{
				bmpAttr.SetRemapTable(_ColorMap);	
			}
			catch(System.Exception ex)
			{
				Debug.Fail(ex.Message);
			}
			int charcount = 0;
			int linecount = 1;
			SolidBrush hbBkBrush = new SolidBrush(m_crBackColor);
			//Fill control rectangle 
			_buff.FillRectangle(hbBkBrush,m_rect);

			//Initialize two rectangeles
			Rectangle clipDstn	= Rectangle.Empty;
			Rectangle clipSrc	= Rectangle.Empty;
			
			//Now we will start main processing.
			for (int ix = 0; ix < strlen; ix++)
			{
				//This method calculates clip region for current char.
				GetCharBmpOffset((char)m_StrText[ix], ref clipSrc);

				//Initializes target clip.
				clipDstn = new Rectangle(x,y,clipSrc.Width,clipSrc.Height);

				//Draw current symbol in buffer 
				_buff.DrawImage(m_ImageMatrix,clipDstn,clipSrc.X,clipSrc.Y,clipSrc.Width ,clipSrc.Height,GraphicsUnit.Pixel,bmpAttr);
			
				x += m_CharWidth + m_XSpacing;
				charcount++;
				if ((charcount == m_MaxXChars) && m_MaxYChars == 1)
				{
					break;
				}
				else if ((charcount == m_MaxXChars) && m_MaxYChars > 1)
				{
					if (linecount == m_MaxYChars)
					{
						break;
					}
					x = charcount = 0;
					y += m_CharHeight + m_YSpacing;
					linecount++;
				}
				
			}
			//And finally draw our image on control surface.
			pea.Graphics.DrawImage(_buffImage,0,0);	
			//Next lines are necessary...
			hbBkBrush.Dispose();
			hbBkBrush= null;
			bmpAttr.Dispose();
			bmpAttr = null;
			_buff.Dispose();_buff = null;
			
			_buffImage.Dispose(); _buffImage = null;

		}
		public	void SetCustomCharSizes(int width, int height, int xspace, int yspace)
		{
			m_CharWidth  = width;
			m_CharHeight = height;
			m_XSpacing = xspace;
			m_YSpacing = yspace;
		}

		public	void SetXCharsPerLine(int max)
		{
			if (max == 0)
				m_MaxXChars = m_StrText.Length;
			else
				m_MaxXChars = max;
		}
		public	void SetNumberOfLines(int max)
		{
			if (max == 0)
				if (m_MaxXChars != 0)
					m_MaxYChars = (int) m_StrText.Length / m_MaxXChars;
				else
					m_MaxYChars = 1;
			m_MaxYChars = max;
		}
		private void AdjustToClientRectangle()
		{
			int		_symbolPerLine	= this.Size.Width / this.m_CharWidth;
			int 	_lines			= this.Size.Height / this.m_CharHeight;
			int		_Width			= (_symbolPerLine * this.m_CharWidth) + m_XSpacing * _symbolPerLine;
			int		_Height			= (_lines * this.m_CharHeight) + m_YSpacing * _lines;
			m_StrText = new String(' ',_lines * _symbolPerLine);
			SetNumberOfLines(_lines);
			SetXCharsPerLine(_symbolPerLine);
			this.Size = new System.Drawing.Size(_Width,_Height);
		}
		public	void SetDisplayColors(Color bk, Color on, Color off)
		{
			SetBkColor(bk);
			SetColor(off, on);
		}
		public	void SetBkColor(Color bk)
		{
			m_crBackColor = bk;
			Invalidate();
		}
		public	void SetColor(Color off,  Color on)
		{
			m_OffColor = off;
			m_crOnColor = on;
			Invalidate();
		}

		public	void SetAutoPadding(bool pad, char ch)
		{
			m_bAutoPad = pad;
			m_cPadChar = ch;
		}
		private bool IsModified = false;
		protected override void OnSizeChanged(System.EventArgs e)
		{
			if (!IsModified)//To prevent endless loop, we set up this variable
			{
				IsModified = true;
				AdjustToClientRectangle();
				base.OnSizeChanged(e);
			}
		}

		[System.Runtime.InteropServices.DllImportAttribute(
			 "gdi32.dll")]
		private static extern bool BitBlt(
			IntPtr hdcDest, // handle to destination DC
			int nXDest, // x-coord of dest upper-left corner
			int nYDest, // y-coord of dest upper-left corner
			int nWidth, // width of destination rectangle
			int nHeight, // height of destination rectangle
			IntPtr hdcSrc,  // handle to source DC
			int nXSrc, // x-coord of source upper-left corner
			int nYSrc, // y-coord of source upper-left corner
			System.Int32 dwRop // raster operation code
			);
	}

}
*/