using System;
using System.Text;
using System.IO;

/// <summary>
/// Test harness for ASCII85 class
/// </summary>
/// <remarks>
/// Jeff Atwood
/// http://www.codinghorror.com/blog/archives/000410.html
/// </remarks>
class Class1
{

	[STAThread]
	static void Main(string[] args)
	{
		TestString();
		//TestFile();
		//TestError();
	}

	/// <summary>
	/// Tests bad data scenarios that should result in an exception
	/// </summary>
	static void TestError()
	{
		Ascii85 a = new Ascii85();
		string encoded = "";
		//encoded = "<~blocz~>";
		//encoded = "<~bloc|~>";
		//encoded = "<~block";
		//encoded = "<~blocku~>";
		a.Decode(encoded);

	}

	/// <summary>
	/// Tests binary file encoding and decoding
	/// </summary>
	static void TestFile()
	{
		Ascii85 a = new Ascii85();
		a.EnforceMarks = false;
		a.LineLength = 0;

		// read the file
		FileStream fs = new FileStream(@"c:\test.gif", FileMode.Open);			
		byte[] ba = new byte[fs.Length];
		fs.Read(ba, 0, (int)fs.Length);
		fs.Close();

		// encode it
		string encoded;
		encoded = a.Encode(ba);
		Console.WriteLine("file encoded in string of length " + encoded.Length);

		// decode it
		byte[] decoded = a.Decode(encoded);

		// write the file
		FileStream fs2 = new FileStream(@"c:\test_out.gif", FileMode.OpenOrCreate);			
		fs2.Write(decoded, 0, decoded.Length);
		fs2.Close();			
	}

	/// <summary>
	/// Tests text encoding and decoding, 
	/// derived from Wikipedia entry on ASCII85
	/// http://en.wikipedia.org/wiki/Ascii85
	/// </summary>
	static void TestString()
	{
		Ascii85 a = new Ascii85();

		string s = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";
		byte[] ba = Encoding.ASCII.GetBytes(s);
		string encoded = a.Encode(ba);
		Console.WriteLine();
		Console.WriteLine(encoded);
		Console.WriteLine();
		Console.WriteLine("Encoded in " + encoded.Length + " chars");
		Console.WriteLine();

		byte[] decoded = a.Decode(encoded);
		Console.WriteLine(Encoding.ASCII.GetString(decoded));			
		Console.WriteLine();
		Console.WriteLine("Decoded " + decoded.Length + " chars");
		Console.WriteLine();

    byte[] decoded2 = a.Decode(encoded);
    Console.WriteLine(Encoding.ASCII.GetString(decoded2));

    Console.WriteLine("Equal results from multiple Decode calls?\t" + Encoding.ASCII.GetString(decoded) == Encoding.ASCII.GetString(decoded2));

    Console.ReadKey();
  }
}