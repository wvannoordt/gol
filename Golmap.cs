using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace gol
{
	public class Golmap
	{
		private const char T_CHAR = '#';
		private const char F_CHAR = ' ';
		private bool[,] values;
		private int rows, columns;
		public Golmap(int _rows, int _columns, byte[] _values)
		{
			int k = 0;
			values = new bool[_rows, _columns];
			if (_values.Length < _rows*_columns) throw new Exception("[Golmap] Too few values provided (" + _values.Length + ") for specified size (" + _rows + "x" + _columns + "=" + (_rows*_columns)+ ")");
			columns = _columns;
			rows = _rows;
			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _columns; j++)
				{
					values[i,j] = ((_values[k++] & 1) == 1);
				}
			}
		}
		public static Golmap FromFile(int _rows, int _columns, string filename)
		{
			return new Golmap(_rows, _columns, File.ReadAllBytes(filename));
		}
		public int Rows {get{return rows;}}
		public int Columns {get{return columns;}}
		public bool this[int i, int j]
		{
			get {return (are_valid_indices(i,j) ? values[i,j] : false);}
			set {values[i,j] = value;}
		}
		private bool are_valid_indices(int i, int j)
		{
			return (i >= 0) && (j >= 0) && (i < rows) && (j < columns);
		}
		public bool Step()
		{
			bool anytrue = false;
			bool[,] newvalues = new bool[rows, columns];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					newvalues[i,j] = get_next_state(i,j);
					anytrue = anytrue || newvalues[i,j];
				}
			}
			values = newvalues;
			return !anytrue;
		}
		private bool get_next_state(int i, int j)
		{
			int neighbors = get_neighbors(i,j);
			//Console.WriteLine(neighbors);
			if (values[i,j])
			{
				if (neighbors < 2) return false;
				else if (neighbors > 3) return false;
				else return true;
			}
			else return neighbors == 3;
		}
		private int get_neighbors(int i, int j)
		{
			int count = 0;
			for (int delta_i = -1; delta_i <= 1; delta_i++)
			{
				for (int delta_j = -1; delta_j <= 1; delta_j++)
				{
					if (this[i+delta_i,j+delta_j] && !(delta_i == 0 && delta_j == 0)) count++;
				}
			}
			return count;
		}
		public override string ToString()
		{
			string output = get_row_string(0);
			for (int i = 1; i < rows; i++)
			{
				output += ("\n" + get_row_string(i));
			}
			return output;
		}
		private string get_row_string(int i)
		{
			string output = string.Empty;
			for (int j = 0; j < columns; j++)
			{
				output += (this[i,j] ? T_CHAR : F_CHAR);
			}
			return output;
		}
	}
}
