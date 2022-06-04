using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemProgramming1
{

   class FirstSecondPass
    {
       string program_name;
       public string error_message="";
       int endAddress = 0;   //Адрес точки входа в программу
       int startAddress = 0; // Адрес начала программы
       int addressCounter = 0;
       const int maxMemmoryAdr = 16777215; //максимальное значение адреса  оперативной памяти

       int StartFlag;
       public int EndFlag;

       //Таблица исходного кода
       public string[,] SourceCodeTable;
       //ТКО
       public string[,] operation_code_table;


       //Выходная таблица
       List<List<string>> exit_table = new List<List<string>>();
       //| Метка| Адрес | Таблица символических имен
       List<List<string>> lable_table = new List<List<string>>();

       //Таблица настройки
       List<string> Tune_table = new List<string>();

       //Таблица исходного кода
       List<string> Binary_code = new List<string>();
       
       List<string> EXT_NAMES = new List<string>();

       public FirstSecondPass()
       {
           lable_table.Add(new List<string>());
           lable_table.Add(new List<string>());
           lable_table.Add(new List<string>());
           exit_table.Add(new List<string>());
           exit_table.Add(new List<string>());
           exit_table.Add(new List<string>());
           exit_table.Add(new List<string>());
       }

       public bool MemoryCheck(int addressCounter)
       {
           if (addressCounter < 0 || addressCounter > maxMemmoryAdr)
           {
               error_message = "Выход за границы доступной памяти";
               return false;
           }
           return true;
       }

       //передается массив, номер строки который надо разложить по переменным
       //проверяется допустимость введеных символов
       public bool GetRow(string[,] mas, int number, out string label, out string command, out string dir1, out string dir2)
        {
            label = mas[number, 0];
            command = mas[number, 1].ToUpper();
            dir1 = mas[number, 2];
            dir2 = mas[number, 3];

            if (Check.IsDirective(label)||Check.OnlyRegisters(label))
            {
                return false;
            }

            if (number > 0 && program_name == label.ToUpper())
                return false;

           
                if ((Check.PossibleLabelName(label)||label.Length==0) && (Check.OnlySymbolsAndNumbers(command)) && (Check.OnlySymbolsAndNumbers(dir2)||dir2.Length==0))
                {
                    if (label.Length > 0)
                    {
                        //метка должна начинаться с символа
                        if (Check.OnlySymbols(Convert.ToString(label[0])))
                            return true;
                        else
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }

                           
        }
       
       //Процедура проверяет таблицу кодов операций 
       public bool Check_operation_code_table(ref string[,] OperationCodeTable) //проверяем таблицу  операций
        {
           int rows = OperationCodeTable.GetLength(0);
           int colums = OperationCodeTable.GetLength(1);

           for (int i = 0; i < rows; i++)
           {
               if (OperationCodeTable[i, 0] == "" || OperationCodeTable[i, 1] == "" || OperationCodeTable[i, 2] == "")
               {
                   error_message = "Строка {" + (i + 1) + "Ошибка! Пустая ячейка в таблице кодов операций недопустима";
                   return false;
               }

               if (OperationCodeTable[i, 0].Length > 6 || OperationCodeTable[i, 1].Length > 2 || OperationCodeTable[i, 2].Length > 1)
               {
                   error_message = "Строка {" + (i + 1) + "Ошибка в размере строки в таблице кодов операций (Команда (от 1 до 6),МКОП (от 1 до 2), Длина(не более 1))";
                   return false;
               }

               if (OperationCodeTable[i, 2] == "1" || OperationCodeTable[i, 2] == "2" || OperationCodeTable[i, 2] == "4")
               { }
               else
               {
                   error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Нет команды такого размера:" + OperationCodeTable[i, 2] + " байт";
                   return false;
               }

                if (!Check.OnlySymbolsAndNumbers(OperationCodeTable[i, 0]))
               {
                   error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! В поле команды недопустимый символ";
                   return false;
               }


               //проверяем поле МКОП команды, что там были только 16чные цифры
               if (Check.IsAdressPossible(OperationCodeTable[i, 1]))
               {

                   if (Check.IsDirective(OperationCodeTable[i, 0]) || Check.OnlyRegisters(OperationCodeTable[i, 0]))
                   {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Поле код команды является зарезервированным словом";
                       return false;
                   }
                   //преобразуем их в число
                   int count = Converting.HexToDec(OperationCodeTable[i, 1]);
                   if (count > 63)
                   {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Поле код команды не должен превышать 3F";
                       return false;
                   }
                   else
                   {
                       //если оно в пределах байта, но записано просто как ... "F" поправляем его на "0F"
                       if (OperationCodeTable[i, 1].Length == 1)
                           OperationCodeTable[i, 1] = Converting.ToTwoChars(OperationCodeTable[i, 1]);
                   }

               }
               else
               {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Посторонние символы в поле МКОП";
                       return false;
               }


               int value = 0;
               if (Check.OnlyNumbers(OperationCodeTable[i, 2]))
               {
                   bool result = Int32.TryParse(OperationCodeTable[i, 2], out value);
                   if (result)
                   {
                       if (value <= 0 || value > 4)
                       {
                           error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Проверьте размер команды (от 1 до 4)";
                           return false;
                       }
                   }
               }
                   else
                   {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! В поле размер операции недопустимый символ";
                       return false;
                   }
               
               
               //проверяем уникальность поля названия команды
               for (int k=i+1;k<rows;k++)
               {  
                   string cmp_str1 = OperationCodeTable[i,0];
                   string cmp_str2 = OperationCodeTable[k,0];
                   if (Equals(cmp_str1, cmp_str2))
                   {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в команде";
                       return false;
                   }
               }
               //проверяем уникальность поля код операции
               for (int k=i+1;k<rows;k++)
               {
                   string cmp_str1 = Convert.ToString(Converting.HexToDec(OperationCodeTable[i, 1]));
                   string cmp_str2 = Convert.ToString(Converting.HexToDec(OperationCodeTable[k,1]));
                   if (Equals(cmp_str1, cmp_str2))
                   {
                       error_message = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в коде команды";
                       return false;
                   }
               }
           }

           return true;
        }

       //Метод проверяет каждую ячейку таблицы, имеются ли в ней записи, если в строке нет данных, то она удаляется
       public void Delete_empty_rows(DataGridView DBGrid_source_code)
       {
           for (int i = 0; i < DBGrid_source_code.Rows.Count - 1; i++)
           {
               bool empty = true;
               for (int j = 0; j < DBGrid_source_code.Rows[i].Cells.Count; j++)
                   if ((DBGrid_source_code.Rows[i].Cells[j].Value != null) && (DBGrid_source_code.Rows[i].Cells[j].Value.ToString() != ""))
                       empty = false;

               if (empty)
               {
                   DBGrid_source_code.Rows.Remove(DBGrid_source_code.Rows[i]);
               }
           }
       }

       //проверяет есть ли такая команда в массиве команд
       public int FindCodeInCodeTable(string label)
       {

           for (int i = 0; i < operation_code_table.GetLength(0); i++)
           {
               if (label == operation_code_table[i, 0])
                    return i;
           }
           return -1;
       }

       //поиск по Заголовку 
       public int FindHeader(string head)
       {
           if (exit_table[0].Count>0)
           for (int i = 0; i < exit_table[0].Count; i++)
           {
               if (head == exit_table[0][i])
               {
                   return i;
               }
           }
           return -1;
       }


       //проверяет есть ли такая метка в массиве меток
       public int FindLabelInLabelTable(string label,ref string name_address,ref string tune_address)
       {
        if (lable_table.Count>0)
               for (int i = 0; i < lable_table[0].Count; i++)
               {
                    if (label.ToUpper() == lable_table[0][i].ToUpper())
                   {
        
                       name_address = lable_table[1][i];
                       tune_address = lable_table[2][i];
                       return i;
                       
                   }
               }


           return -1;
       }

       //заменяем СИ в объектном модуле на адрес
       public int ReplaceLabel(string label,string label_adr,string currentAdr)
       {
           //имена которые надо поменять занесям в список
           //и удалим их
           List<List<string>> labels = new List<List<string>>();
           labels.Add(new List<string>());
           labels.Add(new List<string>());
           labels.Add(new List<string>());

           List<string> adrcounter = new List<string>();
 
           for (int j = 0; j < exit_table[0].Count; j++)
                adrcounter.Add(exit_table[1][j]);
           adrcounter.Add(currentAdr);



           //но в начало ТСИ добавим это СИ и адрес где она найдена
           string n1="";
           string n2="";

           //перебераем весь ТСИ
           for (int i = 0; i < lable_table[0].Count; i++)
           {
               //если имя совпало с заменяемым и адрес не назначен
               if (label == lable_table[0][i] && lable_table[1][i] == "")
               {
                   lable_table[1][i] = label_adr;

                   //добавляем все имена которые совпадают в отдельный список


                   //сохраняем CИ/адрес которые мы добавим, удалив все имена со списком
                   n1 = lable_table[0][i];
                   n2 = lable_table[1][i];

                   ///замена в выходном модуле
                   for (int j = 0; j < exit_table[0].Count; j++)
                   {
                       //ищем совпадение по адресу где надо произвести замену
                       if (lable_table[2][i] == exit_table[1][j])// добавится имя секции
                       {
                           //проверяем, что строка где мы будем парсить >0
                           if (exit_table[3][j].Length > 0)
                           {
                               //определяем тип адресации
                               string type = exit_table[3][j].Substring(0, 2);
                               int Type_of_adr = (byte)Converting.HexToDec(type) & 0x03;

                               int indexOfSubstring = exit_table[3][j].IndexOf("#" + label + "#");
                               if (indexOfSubstring > -1)
                               {
                                   switch (Type_of_adr)
                                   {
                                       case 1:
                                           {
                                               
                                               exit_table[3][j] = exit_table[3][j].Replace("#" + label + "#", label_adr);
                                               break;
                                           }
                                       case 2:
                                           {   
                                               //exit_table[3][j] = exit_table[3][j].Replace("*" + label + "*", Converting.SubHex(n2, currentAdr));
                                               exit_table[3][j] = exit_table[3][j].Replace("#" + label + "#", Converting.SubHex(n2, adrcounter[j+1]));
                                               break;
                                           }
                                   }

                                   exit_table[2][j] = Converting.ToTwoChars(Converting.DecToHex(exit_table[3][j].Length));
                               }
                           }

                       }

                   }
                   ///

               }
               else
               {
                   labels[0].Add(lable_table[0][i]);
                   labels[1].Add(lable_table[1][i]);
                   labels[2].Add(lable_table[2][i]);
               }
           }

  
           lable_table = labels;
           lable_table[0].Insert(0,n1);
           lable_table[1].Insert(0,n2);
           lable_table[2].Insert(0,"");

           return -1;
       }

       //проверка опреанда во втором проходе
       //Если в операнде метка - возращает адрес метки
       //Если в операнде регистр - возвращает номер регистра
       //Если там строка типа C"????" - возвращает ASCII код
       //Если там строка типа X"????" - возвращает строку
       //Если что-то в 10ричном формате - то вернет это же число в 16ричном формате
       //иначе возращает пустую строку
       public string CheckingOperand(string operand1, out int error, out int operandIslabel, int adrType)
       {
           //string current_csect_name="";
           //string name_type = "";
           operandIslabel = 0;
          
           string result = "";
           error = 0;
           if (operand1 != "")
           {
               //что нам делать с Операндом-меткой зависит от типа адресации
               switch (adrType)
               {
                       //прямая адресация
                   case 0:
                       {
                           int find = 0;
                           for (int i = 0; i < lable_table[0].Count; i++)
                           {
                               if (operand1 == lable_table[0][i] )
                               {
                                   find++;
                                   //if (lable_table[3][i] == "ВС")
                                   //{
                                   //    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                   //    tune_address = Support_table[0][index] + " " + lable_table[0][i];
                                   //    return result = "000000";
                                   //}
                                   //if (lable_table[3][i] == "")
                                   //{
                                   //    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                   //    tune_address = Support_table[0][index];
                                   //    return result = lable_table[1][i];
                                   //}
                                   
                               }

                           }
                           if (find == 0)
                           {
                               for (int i = 0; i < lable_table[0].Count; i++)
                               {
                                   //if (operand1 == lable_table[0][i] && csect_name == lable_table[2][i] && lable_table[3][i] == "ВИ")
                                   //{
                                   //    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                   //    tune_address = Support_table[0][index];
                                   //    return result = lable_table[1][i];
                                   //}

                               }
                           }
                           break;
                       }
           
                   //относительная адресация
                   case 1:
                       {
                          
                           if (operand1[0] == '[' && operand1[operand1.Length - 1] == ']')//убираем [] у операнда
                           {
                               int find = 0;
                               string temp = operand1;
                                      temp = temp.Substring(1, temp.Length - 2);
                               for (int i = 0; i < lable_table[0].Count; i++)
                               {
                                   if (temp == lable_table[0][i] )
                                   {
                                       find++;
                                       if (lable_table[3][i] == "ВС")
                                       {
                                           //operandIslabel = 1; //ключ ,что операндом является символьное имя
                                          // tune_address = Support_table[0][index] + " " + lable_table[0][i];
                                           error = 1;
                                           return result = "000000";
                                       }
                                       //if (lable_table[3][i] == "")
                                       //{
                                       //            operandIslabel = 1; //ключ ,что операндом является символьное имя
                                       //            return Converting.SubHex(lable_table[1][i], Support_table[0][index + 1]);
                                       //}

                                   }

                               }
                               if (find == 0)
                               {
                                   for (int i = 0; i < lable_table[0].Count; i++)
                                   {
                                       //if (temp == lable_table[0][i] && csect_name == lable_table[2][i] && lable_table[3][i] == "ВИ")
                                       //{
                                       //    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                       //    return Converting.SubHex(lable_table[1][i], Support_table[0][index + 1]);
                                       //}

                                   }
                               }
                           }
                           

                           break;
                       }
                   //смешанная адресация
                   case 2:
                       {
                           if (operand1[0] == '[' && operand1[operand1.Length - 1] == ']')//убираем [] у операнда
                           {
                               int find = 0;
                               string temp = operand1;
                               temp = temp.Substring(1, temp.Length - 2);
                               for (int i = 0; i < lable_table[0].Count; i++)
                               {
                                   if (temp == lable_table[0][i])
                                   {
                                       find++;
                                       if (lable_table[3][i] == "ВС")
                                       {
                                          // operandIslabel = 1; //ключ ,что операндом является символьное имя
                                          // tune_address = Support_table[0][index] + " " + lable_table[0][i];
                                           error = 1;
                                           return result = "000000";
                                       }
                                       //if (lable_table[3][i] == "")
                                       //{
                                       //    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                       //    return Converting.SubHex(lable_table[1][i], Support_table[0][index + 1]);
                                       //}

                                   }

                               }
                               //if (find == 0)
                               //{
                               //    for (int i = 0; i < lable_table[0].Count; i++)
                               //    {
                               //        if (temp == lable_table[0][i] && csect_name == lable_table[2][i] && lable_table[3][i] == "ВИ")
                               //        {
                               //            operandIslabel = 1; //ключ ,что операндом является символьное имя
                               //            return Converting.SubHex(lable_table[1][i], Support_table[0][index + 1]);
                               //        }

                               //    }
                               //}
                           }
                           else
                           {
                               int find = 0;
                               for (int i = 0; i < lable_table[0].Count; i++)
                               {
                                   if (operand1 == lable_table[0][i] )
                                   {
                                       find++;
                                       if (lable_table[3][i] == "ВС")
                                       {
                                           operandIslabel = 1; //ключ ,что операндом является символьное имя
                                          
                                           return result = "000000";
                                       }
                                       if (lable_table[3][i] == "")
                                       {
                                           operandIslabel = 1; //ключ ,что операндом является символьное имя
                                        
                                           return result = lable_table[1][i];
                                       }

                                   }

                               }
                               if (find == 0)
                               {
                                   for (int i = 0; i < lable_table[0].Count; i++)
                                   {
                                       if (operand1 == lable_table[0][i])
                                       {
                                           operandIslabel = 1; //ключ ,что операндом является символьное имя
                                          
                                           return result = lable_table[1][i];
                                           
                                       }

                                   }
                               }
                           }
                           break;
                       }
               }

               

     
                   //если в операнде регистр
                   int regnum = Check.RegisterNumber(operand1);
                   if (regnum > -1)
                   {
                       return result = Converting.DecToHex(regnum);
                   }
                   else
                       //если в операнде только цифры
                       if (Check.OnlyNumbers(operand1))
                       {
                           return result = Converting.DecToHex(Convert.ToInt32(operand1));
                       }
                       else
                       {
                           string sentence = "";
                           sentence = Check.String(operand1);
                           if (sentence != "")
                           {
                               return Converting.ToASCII(sentence);
                           }
                           sentence = Check.ByteString(operand1);
                           if (sentence != "")
                           {
                               return sentence;
                           }

                           //Если перепробованы все комбинации, значит ошибка
                           error = 1;
                       }

           }
           
           return result;
       }


       //**********************************************************************************************************************************************************
       ///////////////////////////////////////////////////////Первый проход////////////////////////////////////////////////////////////////////////////////////////
       //**********************************************************************************************************************************************************

       public bool Pass(DataGridView dataGrid_symbol_table, DataGridView dataGrid_exit_table, int row, DataGridView dataGrid_Tunetable)
       {
            int countEnd = 0;
            int old_addressCounter=0;
           int flag_to_replace = 0;
           
               //если директива старт найдена
               if (StartFlag == 1)
               {
                   if (!MemoryCheck(addressCounter))
                       return false;
               }

               
               //берем строку из массива и сразу же проверяем корректность данных
               //строка состоит из Label MKOP Operand1 Operand2
               string label;
               string MKOP;
               string Operand1;
               string Operand2;

               if (!GetRow(SourceCodeTable, row, out label, out MKOP, out Operand1, out Operand2))
               {
                   error_message = "Исходный текст программы --> Синтаксическая ошибка строка = " + (row + 1);
                   return false;
               }

               /////////////////////////////////////////   ПОЛЕ МЕТКИ  //////////////////////////////////////////////////////
  
                for (int k = 0; k <= operation_code_table.GetUpperBound(0)-1; k++)
                 {
                    if (Equals(label.ToUpper(), operation_code_table[k, 0].ToUpper()))
                    {
                        error_message = "Таблица кодов операций --> Строка {" + (k + 1) + "} Ошибка! Символическое имя не может совпадать с названием команды";
                        return false;
                    }
                 }

                foreach (string str in EXT_NAMES)
                {
                    if (Equals(label.ToUpper(), str.ToUpper()))
                    {
                        error_message = "Исходный текст программы --> Ошибка! Символическое имя не может совпадать с названием программы";
                        return false;
                    }
                }

                string name_address = "";
                string tune_address = "";  
                int label_row = FindLabelInLabelTable(label, ref name_address, ref tune_address);

           //если поле метки не пустое и это СИ не найдено в таблице
           //то добавляем новое СИ в ТКО
                if (label.Length > 0 && MKOP.ToUpper() != "START")
                {
                    if (label_row == -1 )
                    {
                        lable_table[0].Add(label.ToUpper());
                        lable_table[1].Add(Converting.ToSixChars(Converting.DecToHex(addressCounter)));
                        lable_table[2].Add("");
                    }
                    else
                    {
                        //если СИ найдено и ему не назначен адрес
                        if (name_address == "")
                        {
                            //назначаем адреса
                            flag_to_replace = 1;
                            old_addressCounter = addressCounter;
                            
                        }
                        //если СИ найдено и ему назначен адрес, то ошибка
                        else
                        {
                            error_message = "Исходный текст программы --> Ошибка! Повторение символьных имен недопустимо";
                            return false;
                        }
                    }
                }

               ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                if (Check.IsDirective(MKOP))
                    switch (MKOP)
                    {
                        case "START":
                            {
                                //если у нас старт не в начале массива, и найден в массиве еще раз то ошибка
                                if (row == 0 && StartFlag == 0)
                                {
                                    StartFlag = 1;
                                    //смотрим на операнд, символы соответствуют 16ричной сс
                                    if (Check.IsAdressPossible(Operand1) || Operand1 == "")
                                    {
                                        //если адрес оставить пустым, то он инициализируется нулём
                                        Operand1 = (Operand1 == "") ? "0" : Operand1;
                                        //если да то преобразуем 16ричное число в 10чное
                                        addressCounter = Converting.HexToDec(Operand1);

                                        startAddress = addressCounter;
                                        //адрес начала программы не может быть равен 0
                                        if (addressCounter != 0)
                                        {
                                            error_message = "Исходный текст программы --> Строка {1} Ошибка! Адрес начала программы должен быть = 0";
                                            return false;
                                        }
                                        //адрес начала программы не может превышать объем памяти
                                        if (addressCounter > maxMemmoryAdr)
                                        {
                                            error_message = "Исходный текст программы --> Строка {1} Ошибка! Адрес  программы выходит за диапазон памяти";
                                            return false;
                                        }

                                        if (label == "")
                                        {
                                            error_message = "Исходный текст программы --> Строка {1} Ошибка! Не задано имя программы";
                                            return false;
                                        }
                                        if (label.Length > 10)
                                        {
                                            error_message = "Исходный текст программы --> Строка {1} Ошибка! Превышена длина имени программы(>10 символов)";
                                            return false;
                                        }

                                        //Когда мы задали имя программы, нужно проверить, а совпадает ли оно с названием команды
                                        //так как таблица команд формируется раньше
                                        //до первого прохода
                                        for (int k = 0; k <= operation_code_table.GetUpperBound(0); k++)
                                        {
                                            if (Equals(label.ToUpper(), operation_code_table[k, 0].ToUpper()))
                                            {
                                                error_message = "Исходный текст программы --> Строка {" + (k + 1) + "} Ошибка! Имя программы не может совпадать с названием команды";
                                                return false;
                                            }
                                        }


                                        //теперь помещаем это в выходную таблицу
                                        AddTo.SupportTable("H", label, Converting.ToSixChars(Operand1), "", ref exit_table);
                                        //Сохраняем имя программы
                                        program_name = label;
                                        EXT_NAMES.Add(label);


                                        //выводим предупреждение если такое имеется
                                        if (Operand2.Length > 0)
                                            error_message = error_message + "Исходный текст программы --> Ошибка! Второй операнд директивы START не рассматривается\n";

                                    }
                                    else
                                    {
                                        error_message = "Исходный текст программы --> Ошибка! Неверный адрес начала программы";
                                        return false;
                                    }

                                }
                                else
                                {
                                    error_message = "Исходный текст программы --> Ошибка! Ошибка в директиве START";
                                    return false;
                                }

                            }

                            break;

                        case "WORD":
                            {
                                if (StartFlag == 1)
                                {
                                    int number;
                                    //В WORD у нас могут быть записаны только числа (в данной проге только положительные)
                                    //преобразовываем операнд в число
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number >= 0 && number <= maxMemmoryAdr)
                                        {
                                            if (addressCounter + 3 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }
                                            if (!MemoryCheck(addressCounter))
                                            {
                                                return false;
                                            }

                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "06", Converting.ToSixChars(Converting.DecToHex(number)), ref exit_table);
                                            addressCounter = addressCounter + 3;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы WORD не рассматривается\n";
                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Отрицательное число, либо превышено максимальное значение числа";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //символ вопроса, резервирует 1 слово в памяти
                                        if (Operand1.Length == 1 && Operand1 == "?")
                                        {

                                            if (addressCounter + 3 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }
                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "06", "000000", ref exit_table);
                                            addressCounter = addressCounter + 3;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы WORD не рассматривается\n";
                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Невозможно выполнить преобразование в число " + Operand1;
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    error_message = "Не найдена директива START";
                                    return false;
                                }
                            }
                            break;
                        case "BYTE":
                            {
                                if (StartFlag == 1)
                                {
                                    int number;
                                    //пытаемся преобразовать операнд в число (разрешено только положительное 0 до 255)
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number >= 0 && number <= 255)
                                        {

                                            if (addressCounter + 1 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }
                                            //BYTE = 1 байт, увеличиваем адрес
                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "02", Converting.ToTwoChars(Converting.DecToHex(number)), ref exit_table);
                                            addressCounter = addressCounter + 1;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы BYTE не рассматривается\n";
                                        }

                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Отрицательное число, либо превышеноо максимальное значение числа";
                                            return false;
                                        }
                                    }
                                    //если преобразование в число не получилось, значит разбираем строку
                                    else
                                    {
                                        //первый символ 'C' второй и последний символ это кавычки и длина строки >3
                                        string symbols = Check.String(Operand1);
                                        if (symbols != "")
                                        {

                                            if (symbols.Length > 122)
                                            {
                                                error_message = "Превышена длина строки";
                                                return false;
                                            }

                                            if (addressCounter + symbols.Length > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }


                                            int error, label1;
                                            string result1 = CheckingOperand(Operand1, out error, out label1, -1);
                                            if (error == 1)
                                            {
                                                error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";
                                                return false;
                                            }



                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(result1.Length)), result1, ref exit_table);
                                            addressCounter = addressCounter + symbols.Length;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы BYTE не рассматривается" + "//r//n";
                                            break;
                                        }

                                        //первый символ 'X' второй и последний символ это кавычки и длина строки >3
                                        symbols = "";
                                        symbols = Check.ByteString(Operand1);
                                        if (symbols != "")
                                        {
                                            int lenght = symbols.Length;
                                            //1 символ = 1 байт = 2 цифры в 16ричной системе = четное число символов
                                            if ((lenght % 2) == 0)
                                            {
                                                if (addressCounter + symbols.Length / 2 > maxMemmoryAdr)
                                                {
                                                    error_message = "Произошло переполнение";
                                                    return false;
                                                }

                                                if (symbols.Length > 122)
                                                {
                                                    error_message = "Превышена длина строки";
                                                    return false;
                                                }


                                                int error, label1;
                                                string result1 = CheckingOperand(Operand1, out error, out label1, -1);
                                                if (error == 1)
                                                {
                                                    error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";

                                                    return false;
                                                }

                                                AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(symbols.Length)), result1, ref exit_table);
                                                addressCounter = addressCounter + symbols.Length / 2;
                                                if (!MemoryCheck(addressCounter)) { return false; }

                                                if (Operand2.Length > 0)
                                                    error_message = error_message + "Исходный текст программы --> Второй операнд директивы BYTE не рассматривается" + '\r' + '\n';

                                                break;
                                            }
                                            else
                                            {
                                                error_message = "Исходный текст программы --> Невозможно преобразовать BYTE нечетное количество символов";
                                                return false;
                                            }
                                        }

                                        //если там всего один символ "?"
                                        if (Operand1.Length == 1 && Operand1 == "?")
                                        {
                                            if (addressCounter + 1 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }

                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "01", "00", ref exit_table);
                                            addressCounter = addressCounter + 1;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы BYTE не рассматривается \n";
                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Неверный формат строки " + Operand1;
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    error_message = "Исходный текст программы --> Ошибка! Не найдена директива START";
                                    return false;
                                }
                                break;
                            }
                        case "RESB":
                            {
                                if (StartFlag == 1)
                                {
                                    int number;
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number > 0)
                                        {

                                            if (addressCounter > maxMemmoryAdr)
                                            {
                                                error_message = "Переполнение памяти";
                                                return false;
                                            }
                                            else
                                            {
                                                if (addressCounter + number > maxMemmoryAdr)
                                                {
                                                    error_message = "Произошло переполнение";
                                                    return false;
                                                }
                                                AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.DecToHex(number), "", ref exit_table);
                                                addressCounter = addressCounter + number;//WORD = 3 байта, увеличиваем адрес
                                                if (!MemoryCheck(addressCounter)) { return false; }

                                                if (Operand2.Length > 0)
                                                    error_message = error_message + "Исходный текст программы --> Второй операнд директивы RESB не рассматривается \n";
                                            }

                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Количество байт равно нулю или меньше нуля";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        error_message = "Исходный текст программы --> Невозможно выполнить преобразование в число " + Operand1;
                                        return false;
                                    }
                                }
                                else
                                {
                                    error_message = "Исходный текст программы --> Ошибка! Не найдена директива START";
                                    return false;
                                }
                            }
                            break;
                        case "RESW":
                            {
                                if (StartFlag == 1)
                                {
                                    int number;
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number > 0)
                                        {

                                            if (addressCounter + number * 3 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }
                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.DecToHex(number * 3), "", ref exit_table);
                                            //WORD = 3 байта, увеличиваем адрес
                                            addressCounter = addressCounter + number * 3;
                                            if (!MemoryCheck(addressCounter)) { return false; }
                                            if (addressCounter < 0 || addressCounter > maxMemmoryAdr)
                                            {
                                                error_message = "Исходный текст программы --> Выход за границы доступной памяти";
                                                return false;
                                            }


                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы RESW не рассматривается \n";


                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Количество слов равно нулю или меньше нуля";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        error_message = "Исходный текст программы --> Ошибка! Невозможно выполнить преобразование в число " + Operand1;
                                        return false;
                                    }
                                }
                                else
                                {
                                    error_message = "Исходный текст программы --> Ошибка! Не найдена директива START";
                                    return false;
                                }
                            }
                            break;

                        case "END":
                            {
                            /*if (Tune_table.Count > 0)
                            {
                                for (int i = 0; i < Tune_table.Count; i++)
                                {
                                    AddTo.SupportTable("M", Tune_table[i], "", "", ref exit_table);
                                }
                            }*/
                            countEnd++;

                                //error_message = "count = " + countEnd;
                                if(countEnd > 0 && countEnd < 2)
                                {
                                    if (StartFlag == 1 && EndFlag == 0)
                                    {
                                        
                                        EndFlag = 1;
                                        if (Operand1.Length == 0)
                                        {
                                        if (Tune_table.Count > 0)
                                        {
                                            for (int i = 0; i < Tune_table.Count; i++)
                                            {
                                                AddTo.SupportTable("M", Tune_table[i], "", "", ref exit_table);
                                            }
                                        }
                                        int num;
                                        if (!Check.EmptyAddress(lable_table, out num))
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Найдено неопределенное внешнее имя " + lable_table[0][num];
                                            return false;
                                        }
                                        endAddress = startAddress;
                                            AddTo.SupportTable("E", Converting.ToSixChars(Converting.DecToHex(endAddress)), "", "", ref exit_table);

                                            int head = FindHeader("H");
                                            if (head > -1)
                                            {
                                                exit_table[3][head] = Converting.ToSixChars(Converting.DecToHex(addressCounter - startAddress));
                                            }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд директивы END не рассматривается \n";
                                            break;
                                        }
                                        else
                                        {
                                            if (Check.IsAdressPossible(Operand1))
                                            {
                                                //если да то преобразуем 16ричное число в 10чное
                                                endAddress = Converting.HexToDec(Operand1);
                                                if (endAddress >= startAddress && endAddress <= addressCounter)
                                                {
                                                    int head = FindHeader("H");
                                                    if (head > -1)
                                                    {
                                                        exit_table[3][head] = Converting.ToSixChars(Converting.DecToHex(addressCounter - startAddress));
                                                    }


                                                    //замену найденного СИ, который был найден в поле метки, а в ТСИ у него нет адреса
                                                    // проводим после определения МКОП, и инициализации нового значения счетчика команд
                                                    if (flag_to_replace == 1)
                                                    {
                                                        flag_to_replace = 0;
                                                        ReplaceLabel(label, Converting.ToSixChars(Converting.DecToHex(old_addressCounter)), Converting.ToSixChars(Converting.DecToHex(addressCounter)));
                                                    }

                                                    //Покажем ТСИ после перемещения
                                                    dataGrid_symbol_table.Rows.Clear();
                                                    for (int j = 0; j < lable_table[1].Count; j++)
                                                    {
                                                        dataGrid_symbol_table.Rows.Add();
                                                        dataGrid_symbol_table.Rows[j].Cells[0].Value = lable_table[0][j];
                                                        dataGrid_symbol_table.Rows[j].Cells[1].Value = lable_table[1][j];
                                                        dataGrid_symbol_table.Rows[j].Cells[2].Value = lable_table[2][j];

                                                    }


                                                //проверка  на неопределенные адреса
                                                    int num;
                                                    if (!Check.EmptyAddress(lable_table, out num))
                                                    {
                                                        error_message = "Исходный текст программы --> Ошибка! Найдено неопределенное внешнее имя " + lable_table[0][num];
                                                        return false;
                                                    }


                                                if (Tune_table.Count > 0)
                                                {
                                                    for (int i = 0; i < Tune_table.Count; i++)
                                                    {
                                                        AddTo.SupportTable("M", Tune_table[i], "", "", ref exit_table);
                                                    }
                                                }




                                                
                                                    if (Operand2.Length > 0)
                                                        error_message = error_message + "Исходный текст программы --> Второй операнд директивы END не рассматривается \n";

                                                    AddTo.SupportTable("E", Converting.ToSixChars(Converting.DecToHex(endAddress)), "", "", ref exit_table);

                                                    break;
                                                }
                                                else
                                                {
                                                    error_message = "Исходный текст программы --> Ошибка! Адрес точки выхода неверен";
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                error_message = "Исходный текст программы --> Ошибка! Неверный адрес входа в программу";
                                                return false;
                                            }
                                        }

                                    }
                                }
                                
                                else
                                {  
                                     error_message = "Исходный текст программы --> Ошибка в директиве END";
                                     return false;                                
                                } 
                            }
                            break;

                    }
                  
            
                //значит в строке команда, обрабатываем тут
                else
                {
                    if (StartFlag == 1)
                    {
                        //    //Если в строке МКОП что-то написано
                        if (MKOP.Length > 0)
                        {
                            //Смотрим есть такой МКОП в таблице
                            int num = FindCodeInCodeTable(MKOP);
                            if (num > -1)
                            {
                                //если он есть, то смотрим на длину команды
                                //ДЛИНА КОМАНДЫ = 1
                                //например NOP, операндов нет, а если и есть то не смотрим на них
                                if (operation_code_table[num, 2] == "1")
                                {
                                    if (addressCounter + 1 > maxMemmoryAdr)
                                    {
                                        error_message = "Произошло переполнение";
                                        return false;
                                    }
                                    int AddressationType = Converting.HexToDec(operation_code_table[num, 1]) * 4;


                                    int error, label1;
                                    string result1 = CheckingOperand(Operand1, out error, out label1, -1);
                                    if (error == 1)
                                    {
                                        error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";
                                        return false;
                                    }

                                    string str = Converting.ToTwoChars(Converting.DecToHex(AddressationType));
                                    AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(str.Length)), str, ref exit_table);
                                    addressCounter = addressCounter + 1;
                                    if (!MemoryCheck(addressCounter)) { return false; }

                                    if (Operand1.Length > 0 || Operand2.Length > 0)
                                        error_message = error_message + "Исходный текст программы --> Операнды не рассматриваются в команде " + operation_code_table[num, 0] + "\r\n";
                                }
                                //            else
                                //ДЛИНА КОМАНДЫ = 2
                                //ADD r1,r2   операнды это регистры, либо число //INT 200
                                if (operation_code_table[num, 2] == "2")
                                {
                                    int number;
                                    //сначала пытаемся преобразовать первый операнд в число
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number >= 0 && number <= 255)
                                        {
                                            //так как операнд является числом, то это непосредственная адресация
                                            //просто  сдвигаем на два разряда влево
                                            int AddressationType = Converting.HexToDec(operation_code_table[num, 1]) * 4;
                                            if (addressCounter + 2 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }


                                            int error, label1;
                                            string result1 = CheckingOperand(Operand1, out error, out label1, -1);
                                            if (error == 1)
                                            {
                                                error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";
                                                return false;
                                            }

                                            String RecordLength = Converting.ToTwoChars(Converting.DecToHex(Convert.ToInt32(operation_code_table[num, 2]) + result1.Length));

                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), RecordLength, Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + Converting.ToTwoChars(Converting.DecToHex(number)), ref exit_table);

                                            addressCounter = addressCounter + 2;
                                            if (!MemoryCheck(addressCounter)) { return false; }

                                            if (Operand2.Length > 0)
                                                error_message = error_message + "Исходный текст программы --> Второй операнд команды" + operation_code_table[num, 0] + " не рассматривается \n";
                                        }

                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Отрицательное число, либо превышено максимальное значение числа";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //если первый и второй операнд - регистры
                                        if (Check.OnlyRegisters(Operand1) && Check.OnlyRegisters(Operand2))
                                        {
                                            //так как оба операнда регистры то это регистровая(регистровой==непосредственной) адресация
                                            //просто  сдвигаем на два разряда влево
                                            int AddressationType = Converting.HexToDec(operation_code_table[num, 1]) * 4;
                                            if (addressCounter + 2 > maxMemmoryAdr)
                                            {
                                                error_message = "Произошло переполнение";
                                                return false;
                                            }


                                            int error, label1;
                                            string result1 = CheckingOperand(Operand1, out error, out label1, -1);
                                            if (error == 1)
                                            {
                                                error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";
                                                return false;
                                            }
                                            string result2 = CheckingOperand(Operand2, out error, out label1, -1);
                                            if (error == 1)
                                            {
                                                error_message = error_message + "Исходный текст программы --> Ошибка в операнде,код отсутствует в ТСИ " + (row + 1) + " строка";
                                                return false;
                                            }
                                            String RecordLength = Converting.ToTwoChars(Converting.DecToHex(2 + result1.Length + result2.Length));
                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), RecordLength, Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + result1 + result2, ref exit_table);

                                            addressCounter = addressCounter + 2;
                                            if (!MemoryCheck(addressCounter)) { return false; }
                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка в команде " + operation_code_table[num, 0];
                                            return false;
                                        }

                                    }

                                }

                                else
                                    //////////////////////////////////////////////////ДЛИНА КОМАНДЫ = 4/////////////////////////////////////////////////////////////////////////////////////
                                    if (operation_code_table[num, 2] == "4")
                                    {
                                        if (addressCounter + 4 > maxMemmoryAdr)
                                        {
                                            error_message = "Произошло переполнение";
                                            return false;
                                        }

                                        int AddressationType;
                                        int type;
                                        // проверяем что операнд есть в  команде из 4 байт
                                       
                                        if (Operand1.Length > 0)
                                        {
                                            //длина операнда при относительной адресации должна быть >2 т.к. это скобки
                                           
                                            if (Operand1[0] == '[' && Operand1[Operand1.Length - 1] == ']')
                                            {

                                                Operand1 = Operand1.Substring(1, Operand1.Length - 2);
                                        if (Operand1.Length > 0)
                                            if (Check.PossibleLabelName(Operand1) && Check.OnlySymbols(Convert.ToString(Operand1[0]))) { }
                                            else { error_message = error_message + "Исходный текст программы --> Ошибка в символическом имени"; return false; }
                                        else { error_message = error_message + "Исходный текст программы --> Ошибка в символическом имени"; return false; }
                                                AddressationType = Converting.HexToDec(operation_code_table[num, 1]) * 4 + 2;
                                                type=2;
                                            }

                                            else
                                            {
                                                //проверяем операнд при прямой адресации
                                                if (Check.PossibleLabelName(Operand1) && Check.OnlySymbols(Convert.ToString(Operand1[0])))
                                                //Добавление в таблицу настройки если встретилась команда с прямой адресацией
                                                { Tune_table.Add(Converting.ToSixChars(Converting.DecToHex(addressCounter))); } 
                                                else { error_message = error_message + "Исходный текст программы --> Ошибка в символическом имени"; return false; }
                                                AddressationType = Converting.HexToDec(operation_code_table[num, 1]) * 4 + 1;
                                                    type=1;
                                            }

                                        }
                                        else
                                        {
                                            error_message = "Исходный текст программы --> Ошибка! Не найден операнд в строке:" + (row + 1);
                                            return false;
                                        }


                                        for (int k = 0; k <= operation_code_table.GetUpperBound(0) - 1; k++)
                                        {
                                            if (Equals(Operand1.ToUpper(), operation_code_table[k, 0].ToUpper()))
                                            {
                                                error_message = "Исходный текст программы --> Строка {" + (k + 1) + "} Ошибка! Символическое имя не может совпадать с названием команды";
                                                return false;
                                            }
                                        }

                                        foreach (string str in EXT_NAMES)
                                        {
                                            if (Equals(Operand1.ToUpper(), str.ToUpper()))
                                            {
                                                error_message = "Исходный текст программы --> Ошибка! Символическое имя не может совпадать с названием программы";
                                                return false;
                                            }
                                        }


                                        if (Check.IsDirective(Operand1) || Check.OnlyRegisters(Operand1))
                                        { 
                                            error_message = "Исходный текст программы --> Ошибка! Символическое имя не может совпадать с системным именем";
                                            return false;
                                        }

                                        //Операнд проверен, ищем СИ в ТСИ
                                        string operand_adr = "";
                                        string tune_adr = "";
                                        int finded_row = FindLabelInLabelTable(Operand1, ref operand_adr, ref tune_adr);

                                        //если такое СИ  операнда найдено
                                        if (finded_row > -1)
                                        {
                                            //у него задан адрес
                                            if (operand_adr != "")
                                            {

                                                if (type == 1)
                                                {
                                                    //меняем СИ на прямой адрес
                                                    string temp = Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + operand_adr;
                                                    //  Tune_table.Add(operand_adr);//*********************************************************************************************************************************************************
                                                    AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(temp.Length)), temp, ref exit_table);
                                                }
                                                if (type == 2)
                                                {
                                                    int temp_addressCounter=addressCounter;
                                                    temp_addressCounter = temp_addressCounter + 4;
                                                    if (!MemoryCheck(temp_addressCounter)) { return false; }

                                                    string operandd = Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + Converting.SubHex(lable_table[1][finded_row], Converting.ToSixChars(Converting.DecToHex(temp_addressCounter)));


                                                    AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(operandd.Length)), operandd, ref exit_table);
                                                }
                                            }
                                            //у него не задан адрес
                                            else
                                            {
                                                lable_table[0].Insert(finded_row, Operand1);
                                                lable_table[1].Insert(finded_row, "");
                                                lable_table[2].Insert(finded_row, Converting.ToSixChars(Converting.DecToHex(addressCounter)));
                                                AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "", Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + "#" + Operand1 + "#", ref exit_table);
                                            }
                                        }
                                        else
                                        {
                                            //если такое СИ  операнда не найдено, то добавляем его в ТСИ
                                            lable_table[0].Add(Operand1.ToUpper());
                                            lable_table[1].Add("");
                                            lable_table[2].Add(Converting.ToSixChars(Converting.DecToHex(addressCounter)));
                                            AddTo.SupportTable("T", Converting.ToSixChars(Converting.DecToHex(addressCounter)), "", Converting.ToTwoChars(Converting.DecToHex(AddressationType)) + "#" + Operand1 + "#", ref exit_table);
                                        }


                                        addressCounter = addressCounter + 4;
                                        if (!MemoryCheck(addressCounter)) { return false; }

                                        if (Operand2.Length > 0)
                                            error_message = error_message + "Исходный текст программы --> Второй операнд команды" + operation_code_table[num, 0] + " не рассматривается\n";
                                    }
                                    else
                                    {
                                        error_message = "Исходный текст программы --> Ошибка! Размер команды больше установленного";
                                        return false;
                                    }

                            }
                            else
                            {
                                error_message = "Исходный текст программы --> Ошибка! МКОП отсутствует в ТКО";
                                return false;
                            }
                        }
                        else
                        {
                            error_message = "Исходный текст программы --> Ошибка в МКОП";
                            return false;
                        }
                    }
                
                   else
                   {
                       error_message = "Исходный текст программы --> Ошибка! Не найдена директива START";
                       return false;
                   }
                }
         

            //замену найденного СИ, который был найден в поле метки, а в ТСИ у него нет адреса
            // проводим после определения МКОП, и инициализации нового значения счетчика команд
            if (flag_to_replace == 1)
                {
                     ReplaceLabel(label, Converting.ToSixChars(Converting.DecToHex(old_addressCounter)), Converting.ToSixChars(Converting.DecToHex(addressCounter)));
                }

            dataGrid_Tunetable.Rows.Clear();
           dataGrid_exit_table.Rows.Clear();

           if (Tune_table.Count > 0)
           {
               for (int i = 0; i < Tune_table.Count; i++)
               {
                   dataGrid_Tunetable.Rows.Add();
                   dataGrid_Tunetable.Rows[i].Cells[0].Value = Tune_table[i];
               }
           }

            //Помещаем сформированную Выходную таблицу в датагрид
            if (exit_table[0].Count>0)
           for (int i = 0; i < exit_table[0].Count; i++)
           {
               dataGrid_exit_table.Rows.Add();
               dataGrid_exit_table.Rows[i].Cells[0].Value = exit_table[0][i];
               dataGrid_exit_table.Rows[i].Cells[1].Value = exit_table[1][i];
               dataGrid_exit_table.Rows[i].Cells[2].Value = exit_table[2][i];
               dataGrid_exit_table.Rows[i].Cells[3].Value = exit_table[3][i];
           }

           dataGrid_symbol_table.Rows.Clear();
           //Помещаем сформированную Таблицу вспомогательных имен в датагрид
               for (int j = 0; j < lable_table[1].Count; j++)
               {
                   dataGrid_symbol_table.Rows.Add();
                   dataGrid_symbol_table.Rows[j].Cells[0].Value = lable_table[0][j];
                   dataGrid_symbol_table.Rows[j].Cells[1].Value = lable_table[1][j];
                   dataGrid_symbol_table.Rows[j].Cells[2].Value = lable_table[2][j];                  
               }

               return true;
       }
    }
}
