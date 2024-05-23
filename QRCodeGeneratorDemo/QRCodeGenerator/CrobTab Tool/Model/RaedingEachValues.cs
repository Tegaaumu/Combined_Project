namespace QRCodeGenerator.CrobTab_Tool.Model
{
    public static class RaedingEachValues
    {
        public static bool Rex { set; get; }

        private static Dictionary<int, string> foundItemsList = new Dictionary<int, string>();

        public static Dictionary<int, string> FoundItemsList
        {
            get { return foundItemsList; } // Return the value of the field
            set { foundItemsList = value; } // Set the value of the field
        }

        public static void CheckEachValue(string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                //Rex = values[i].Contains();
                string[] itemsToCheck = { "/", "*", "-", "," };
                string input = values[i]; // replace with your actual input

                bool containsAny = itemsToCheck.Any(item => input.Contains(item, StringComparison.OrdinalIgnoreCase));

                if (containsAny)
                {
                    foreach (var item in itemsToCheck)
                    {
                        if (input.Contains(item, StringComparison.OrdinalIgnoreCase))
                        {
                            if (FoundItemsList.ContainsKey(i))
                            {
                                foundItemsList.Clear();
                            }
                            FoundItemsList.Add(i, item);
                        }
                        //if (input.Contains(item, StringComparison.OrdinalIgnoreCase))
                        //{
                        //    foundItemsList.Add(new KeyValuePair<int, string>(i, item));
                        //}
                    }
                }
                else
                {
                    Console.WriteLine("Input does not contain any of the specified items.");
                }

            }
        }    
    }

    }