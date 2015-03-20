using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;

//the file should not contain the front % and the back %
class interpreter{
	public static void Main(){
		StreamReader objReader = new StreamReader("data/op010.tap");
		ArrayList list = new ArrayList(); 
		string Line = "";
		while(Line != null){
			Line = objReader.ReadLine();
			if(Line != null && !Line.Equals(""))
				list.Add(Line);
		}
		objReader.Close();
		string index = "";
		string op = "";
		int str_Length = 0;
		foreach (string i in list){
			index = i.Substring(0,5);
			str_Length = i.Length;
			op = i.Substring(5); //the index 6 contain all the operation
			if(op != null){
				Console.WriteLine(index);
				int x_number = op.Split('x').Length - 1; 
				int y_number = op.Split('y').Length - 1;
				int g_number = op.Split('g').Length - 1;
				int m_number = op.Split('m').Length - 1;
				int s_number = op.Split('s').Length - 1;
				int t_number = op.Split('t').Length - 1;
				int z_number = op.Split('z').Length - 1;

				var list_G_num = new List<int>();
				var list_X_num = new List<int>();
				var list_Y_num = new List<int>();
				var list_M_num = new List<int>();
				var list_S_num = new List<int>();
				var list_T_num = new List<int>();
				var list_Z_num = new List<int>();
				SortedDictionary<string, int> all_index = new SortedDictionary<string, int>();

				Console.Out.NewLine = "";
				Console.WriteLine("\n");
				int cter = 0;
				if(g_number != -1){
					//record the index of each 'G'
					Console.WriteLine("G index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'G'){
							list_G_num.Add(j);
							all_index["G" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				
				cter = 0;
				if(x_number != -1){
					//record the index of each 'X'
					Console.WriteLine("X index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'X'){
							list_X_num.Add(j);
							all_index["X" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				cter = 0;
				if(y_number != -1){
					//record the index of each 'Y'
					Console.WriteLine("Y index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'Y'){
							list_Y_num.Add(j);
							all_index["Y" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				cter = 0;
				if(m_number != -1){
					//record the index of each 'M'
					Console.WriteLine("M index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'M'){
							list_M_num.Add(j);
							all_index["M" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				cter = 0;
				if(s_number != -1){
					//record the index of each 'S'
					Console.WriteLine("S index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'S'){
							list_S_num.Add(j);
							all_index["S" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				cter = 0;
				if(t_number != -1){
					//record the index of each 'T'
					Console.WriteLine("T index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'T'){
							list_T_num.Add(j);
							all_index["T" + cter] = j;
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				cter = 0;
				if(z_number != -1){
					//record the index of each 'Z'
					Console.WriteLine("Z index:");
					for(int j = 0; j < op.Length; j++){
						if(op[j] == 'Z'){
							list_Z_num.Add(j);
							all_index["Z" + cter] = j; 
							Console.WriteLine(" " + j);
							cter++;
						}
					}
					Console.WriteLine("\n");
				}
				//sort the index
				var sorted_allindex = from entry in all_index orderby entry.Value ascending select entry;
				int temp = -1;
				int begin = 0;
				int sorted_allindex_count = 0;

				foreach(KeyValuePair<string, int> u in sorted_allindex){
					sorted_allindex_count++;
				}
				//Console.WriteLine(sorted_allindex_count + " " + op.Length + "\n");

				string content = "";
				char letter_name;
				foreach(KeyValuePair<string, int> u in sorted_allindex){
					//Console.WriteLine(u.Key + " " + u.Value + "\n");
					if(begin == 0 && sorted_allindex_count > 1){
						begin = 1;
						temp = u.Value;
						//Console.WriteLine(temp + "\n");
					}else if(begin == 0 && sorted_allindex_count.Equals(1)){
						content = "";
						for(int p = temp + 2; p < op.Length; p++){
							content += op[p];
						}
						letter_name = check_op(op[temp + 1]);
						operate_Gcode(letter_name, content);
						//Console.WriteLine(letter_name + " " + content + "\n");
					}else{
						content = "";
						for(int p = temp + 1; p < u.Value; p++){
							content += op[p];
						}
						letter_name = check_op(op[temp]);
						operate_Gcode(letter_name, content);
						//Console.WriteLine(letter_name + " " + content + "\n");
						temp = u.Value;
						sorted_allindex_count--;
					}
					if(begin == 1 && sorted_allindex_count.Equals(1)){
						content = "";
						for(int p = temp + 1; p < op.Length; p++){ 
							content += op[p]; 
						}
						letter_name = check_op(op[temp]);
						operate_Gcode(letter_name, content);
						//Console.WriteLine(letter_name + " " + content + "\n");
					}
				}
			}
		}
	}
	static char check_op(char str){
		if(str == 'G')
			return 'G';
		if(str == 'X')
			return 'X';
		if(str == 'Y')
			return 'Y';
		if(str == 'Z')
			return 'Z';
		if(str == 'M')
			return 'M';
		if(str == 'S')
			return 'S';
		if(str == 'T')
			return 'T';
		else
			return 'M';
	}
	static void operate_Gcode(char letter_name, string value){
		if(letter_name == 'G'){
			G_op(value);
		}else if(letter_name == 'X'){
			X_op(value);
		}else if(letter_name == 'Y'){
			Y_op(value);
		}else if(letter_name == 'Z'){
			Z_op(value);
		}else if(letter_name == 'M'){
			M_op(value);
		}else if(letter_name == 'S'){
			S_op(value);
		}else if(letter_name == 'T'){
			T_op(value);
		}else{
			Console.WriteLine(" ");
		}

	}
	static void G_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//G operation code 
	}
	static void X_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//X operation code 
	}
	static void Y_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//Y operation code 
	}
	static void Z_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//Z operation code 
	}
	static void M_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//M operation code 
	}
	static void S_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//S operation code 
	}
	static void T_op(string str){
		Console.WriteLine("Get the value " + str + "\n");
		//T operation code 
	}
}
