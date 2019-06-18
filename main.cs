using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace gol
{
  public class Program
  {
  	public static void Main(string[] args)
  	{
  		Console.Clear();
      //byte[] stuff = new byte[] {0, 1, 0, 0, 1 , 0, 0, 1, 0};
      //byte[] stuff = new byte[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
      //Golmap p = new Golmap(3, 3, stuff);
      Golmap p = Golmap.FromFile(62, 235, "./file");
      Console.WriteLine(p);
      bool done = false;
      while (!done)
      {
        //string line = Console.ReadLine();
        string line = "";
        bool allgone = p.Step();
        Console.Clear();
        Console.WriteLine(p);
        done = (line.Length != 0) || allgone;
        Thread.Sleep(40);
      }
      Console.Clear();
  	}
  }
}
