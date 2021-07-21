namespace AnkCow
{
    class DataNumber
    {
        //Число игрока.
        public string NumberGame { get; set; }
        //Количество коров.
        public int CountCow { get; set; }
        //Количество быков.
        public int CountAnk { get; set; }

        public DataNumber()
        {
            NumberGame = null;
            CountCow = 0;
            CountAnk = 0;
        }
        //Расчет быков и коров.
        public void CowAndAnk(int number)
        {
            CountCow = 0;
            CountAnk = 0;
            string tempNumber = number.ToString();

            for (int i = 0; i < NumberGame.Length; i++)
            {
                    if (NumberGame[i] == tempNumber[i])
                    {
                        CountAnk++;
                        continue;
                    }
                for (int j = 0; j < tempNumber.Length; j++)
                {
                    if (NumberGame[i] != tempNumber[i] && NumberGame[i]==tempNumber[j])
                    {
                        CountCow++;
                        break;
                    }

                }
            }
        }
       
       



    }
}
