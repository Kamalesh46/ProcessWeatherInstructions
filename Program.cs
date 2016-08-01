namespace ProcessWeatherInstructions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        /// <summary>
        /// Dictionary which holds instructions for HOT&COLD weather conditions
        /// </summary>
        Dictionary<int, string[]> instructions = new Dictionary<int, string[]>();

        /// <summary>
        /// Dictionary which hold the instrcution ID and its priority
        /// </summary>
        Dictionary<int, int> instructionPriority = new Dictionary<int, int>();
        
        /// <summary>
        /// Weather Type 1-HOT, 2-COLD
        /// </summary>
        int weatherType = 0;

        /// <summary>
        /// Populates Instructions for both HOT & COLD weater into Dictionary, Key as instruction ID 1to8 & HOT&COLD weather recommendation as value 
        /// </summary>
        public void PopulateInstructions()
        {
            instructions.Add(1, new string[2] { "sandals", "boots" });
            instructions.Add(2, new string[2] { "sun visor", "hat" });
            instructions.Add(3, new string[2] { "fail", "socks" });
            instructions.Add(4, new string[2] { "t-shirt", "shirt" });
            instructions.Add(5, new string[2] { "fail", "jacket" });
            instructions.Add(6, new string[2] { "shorts", "pants" });
            instructions.Add(7, new string[2] { "leaving house", "leaving house" });
            instructions.Add(8, new string[2] { "Removing PJs", "Removing PJs" });
        }


        /*                                                       ID    Priority
         * Put on footwear 	    “sandals” 		 “boots”          1 ==> 3
         * Put on headwear		“sun visor” 	 “hat” 	          2 ==> 3
         * Put on socks 		fail 			 “socks”          3 ==> 2
         * Put on shirt 		“t-shirt” 		 “shirt”          4 ==> 2
         * Put on jacket		fail 			 “jacket”         5 ==> 3
         * Put on pants		    “shorts” 		 “pants”	      6 ==> 2
         * Leave house 		    “leaving house”  “leaving house”  7 ==> 4
         * Take off pajamas 	“Removing PJs”   “Removing PJs”   8 ==> 1 
         */
        /// <summary>
        /// Populate InstrucionID and its priority
        /// </summary>
        public void PopulateInstructionPriority()
        {
            instructionPriority.Add(1, 3);
            instructionPriority.Add(2, 3);
            instructionPriority.Add(3, 2);
            instructionPriority.Add(4, 2);
            instructionPriority.Add(5, 3);
            instructionPriority.Add(6, 2);
            instructionPriority.Add(7, 4);
            instructionPriority.Add(8, 1);
        }

        /// <summary>
        /// Process Instructions
        /// </summary>
        /// <param name="ins">array of instructions ids</param>
        public void ProcessInstructions(int[] ins)
        {
            int len = ins.Length;
            int[] temp = new int[len];
            bool failed = false;
            string result = string.Empty;

            // Getting priorities of the instructions to a temp array, 
            // if instructions id not present in instructions them priority of it is -1
            for (int i=0; i < len; i++)
            {
                if(instructionPriority.ContainsKey(ins[i]))
                {
                    temp[i] = instructionPriority[ins[i]];
                }
                else
                {
                    temp[i] = -1;
                    failed = true;
                }
            }

            if (!failed) 
            {
                // if first instructions is not Removing PJ's or last instruction is Leaving house
                if (temp[0] != 1) 
                {
                    Console.WriteLine("fail");
                    return;
                    
                }

                if(temp[len-1]!=4)
                {
                    failed = true;
                }

                // If less priority ins happen before high priority instruction
                if (!failed)
                {
                    for (int i = 1; i < len; i++)
                    {
                        if (temp[i] < temp[i - 1])
                        {
                            failed = true;
                        }
                    }
                }
            }

            // Generating result string from the temp && ins arrays
            for (int i=0; i < len; i++)
            {
                if (temp[i] == -1)
                {
                    break;
                }
                else
                {
                    string[] output = instructions[ins[i]];
                    
                    // For HOT weather Put on jacket, Put on socks will be invalid
                    if (weatherType == 1 && output[weatherType - 1].Equals("fail", StringComparison.OrdinalIgnoreCase))
                    {
                        failed = true;
                        continue;
                    }
                    
                    if (!result.Contains(output[weatherType - 1]))
                    {
                        result += output[weatherType - 1] + " ,";
                    }
                    
                    
                }

                // if instructions to wear socks but no boots
                // if instructions to Removing PJ's but no pants or shorts
                // if cold weather instructions to wear shirt but no jacket
                if ((result.Contains("socks") && !result.Contains("boots")) 
                    || (result.Contains("Removing PJ's") && (!result.Contains("pants") || !result.Contains("shorts"))) 
                    || (result.Contains("shirt") && !result.Contains("jacket")))
                {
                    failed = true;
                }
            }

            if (failed)
            {
                result += "fail";
            }

            result = result.TrimEnd(new char[] { ',', ' ' });
            Console.WriteLine("Output : " + result);
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">main arguments</param>
        public static void Main(string[] args)
        {
            Program obj = new Program();
            obj.PopulateInstructions();
            obj.PopulateInstructionPriority();

            while(true)
            {
                Console.Write("Input : ");
                string input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting, Please press any key");
                    break;
                }
                
                if (input.StartsWith("HOT", StringComparison.OrdinalIgnoreCase))
                {
                    obj.weatherType = 1;
                    input= input.ToLower().Replace("hot", string.Empty).Trim(' ');
                    input = input.Replace(",", string.Empty).Trim(' ');
                    int[] ins = input.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                    obj.ProcessInstructions(ins);
                }
                else if (input.StartsWith("COLD", StringComparison.OrdinalIgnoreCase))
                {
                    obj.weatherType = 2;
                    input = input.ToLower().Replace("cold", string.Empty).Trim(' ');
                    input = input.Replace(",", string.Empty).Trim(' ');
                    int[] ins = input.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                    obj.ProcessInstructions(ins);
                }
                else
                {
                    Console.WriteLine("fail");
                }

                Console.WriteLine();
            }
            
            Console.ReadLine();
        }
    }
}
