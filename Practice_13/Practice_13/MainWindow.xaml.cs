using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibArray;
using Microsoft.Win32;

namespace Practice_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MyArray _myArray = new MyArray(3, 3);

        private void FillArray_Click(object sender, RoutedEventArgs e)
        {
            _myArray.Fill();
            sizeRow.Text = $"Строк {dataGrid.Items.Count}";
            sizeColumn.Text = $"Столбцов {dataGrid.Columns.Count}";
            dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void SaveArray_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Заполните матрицу перед сохранением!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Title = "Экспорт массива";

            if (saveFileDialog.ShowDialog() == true)
            {
                _myArray.Export(saveFileDialog.FileName);
            }
            dataGrid.ItemsSource = null;
            dataGridResult.ItemsSource = null;
        }

        private void OpenArray_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Импорт массива";

            if (openFileDialog.ShowDialog() == true)
            {
                _myArray.Import(openFileDialog.FileName);
            }
            dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            int tmp = 0;
            int Nmax = 0, Nmin = 0;
            int min = _myArray[0, 0];
            int max = _myArray[0, 0];
            for (int i = 0; i < _myArray.RowLength; i++)
            {
                for (int j = 0; j < _myArray.ColumnLength; j++)
                {
                    if (_myArray[i, j] > max)
                    {
                        max = _myArray[i, j];
                        Nmax = i;
                    }
                    if (_myArray[i, j] < min)
                    {
                        min = _myArray[i, j];
                        Nmin = i;
                    }
                }
            }
            for (int j = 0; j < _myArray.ColumnLength; j++)
            {
                tmp = _myArray[Nmax, j];
                _myArray[Nmax, j] = _myArray[Nmin, j];
                _myArray[Nmin, j] = tmp;
            }
            dataGridResult.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Золотарев М. А.\n Группа: ИСП-34\n Вариант №13", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CurrentCellIndex_Click(object sender, MouseEventArgs e)
        {
            selectedCell.Text = $"[{dataGrid.Items.IndexOf(dataGrid.CurrentItem) + 1};" +
                $"{dataGrid.CurrentColumn.DisplayIndex + 1}]";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
        }

        private void ClearRight_Click(object sender, RoutedEventArgs e)
        {
            dataGridResult.ItemsSource = null;
        }

    }
}
