using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace AnkCow
{ 

    class DataSource: INotifyPropertyChanged
    {
        //Флаг начала игры.
        private bool flagStart = false;
        //Флаг конца игры при досрочном выходе.
        private bool flagEnd = false;
        //Строка статуса игры.
        private string statusGame;
        //Команда ввода числа.
        private readonly Command inputCommand;
        //Команда начала игры.
        private readonly Command startCommand;
        //Команда выхода из игры.
        private readonly Command exitCommand;
        //Команда информации.
        private readonly Command aboutus;
        //Поле класса игровой логики.
        private DataNumber DataNumber;
        //Текущее введенное игроком число.
        private string currentNumber;
        //Загаданное компьютером число.
        private readonly int secretNumber;

        //Статичное свойство для обращения к методам элементов главного окна.
        private static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;
        //Коллекция для привязки к ListBox.
        private readonly ICollection<DataNumber> number = new ObservableCollection<DataNumber>();
        public IEnumerable<DataNumber> ToListbox => number;

        public DataSource()
        {
            statusGame = "ожидание";
            currentNumber = "0000";
            Random rnd = new Random();
            secretNumber = rnd.Next(1000, 9999);
            DataNumber = new DataNumber();
            inputCommand = new DelegateCommand(GameAction, () => FlagStart != false);
            startCommand = new DelegateCommand(StartGame,()=>FlagStart!=true);
            exitCommand = new DelegateCommand(ExitGame,()=>flagEnd==false);
            aboutus = new DelegateCommand(About_Us, () => true);
            PropertyChanged += (sender, e) =>
            {
                  inputCommand.RaiseCanExecuteChanged();
                  startCommand.RaiseCanExecuteChanged();                
            };         
        }
        //Команды.
        public ICommand InputCommand => inputCommand;
        public ICommand StartCommand => startCommand;
        public ICommand ExitCommand => exitCommand;
        public ICommand AboutUs => aboutus;

        public bool FlagStart
        {
            get => flagStart;
            set
            {
                if (!flagStart.Equals(value))
                {
                    flagStart = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FlagStart)));
                }

            }
        }
        public string CurrentNumber
        {
            get =>currentNumber;
            set
            {
                if (!CheckNumber(value))
                {
                    MessageBox.Show("Только числа!");
                    currentNumber = "";
                }
                else
                {
                    currentNumber = value;
                }
            }
        }

        public string StatusGame
        {
            get => statusGame;
            set
            {
                if (!statusGame.Equals(value))
                {
                    statusGame = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(StatusGame)));
                }
            }

        }

        protected void StartGame()
        {
            FlagStart = true;
            MainWindow.InputBox.SelectAll();
            StatusGame="идет игра";  
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        //Метод игровой логики.
        public void GameAction()
        {
            //Проверка на пустую строку.
            if (CurrentNumber == "")
            {
                return;
            }
            //Проверка на формат числа.
            if (CurrentNumber.Length < 4)
            {
                MessageBox.Show("Только четырехзначные числа!");
                return;
            }
            //Выделение строки в окне ввода чисел.
                MainWindow.InputBox.SelectAll();           
                DataNumber.NumberGame = CurrentNumber;
                DataNumber.CowAndAnk(secretNumber);
                number.Add(DataNumber);
                EndGame();

        }
        //Проверка ввода чисел.
        public bool CheckNumber(string obj)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if (!Char.IsDigit(obj[i]))
                {
                    return false;
                }
            }

            return true;
        }
        //Выход из игры в случае выигрыша.
        public void EndGame()
        {
            if (DataNumber.CountAnk == 4)
            {
                StatusGame = "игра окончена";
                MessageBox.Show($"Вы выиграли!\n" +
                    $"Загаданное число: {secretNumber}" +
                    $"\nПока!");
                MainWindow.Close();
            }
          
        }
        //Досрочный выход из игры.
        public void ExitGame()
        {

            flagEnd = true;
            MessageBox.Show("Поражение! Пока!");
            MainWindow.Close();
        }
        public void About_Us()
        {
            MessageBox.Show("Разработано Lebedev_SoFT_&_SoLuTioN");
        }


    }



}
