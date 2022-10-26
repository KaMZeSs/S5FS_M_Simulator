using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class Emulator
    {
        S5FS s5fs;

        List<String> commands;
        
        public Emulator(S5FS s5fs)
        {
            this.s5fs = s5fs;
            commands = new()
            {
                new("man"), // Справка
                new("ls"), // Список файлов в папке
                new("cat"), // Прочитать файл
                new("cd"), // Перейти в другой каталог
                new("mkdir"), // Создать папку
                new("cp"), // Скопировать
                new("mv"), // Переместить
                new("rm"), // Удалить
                new("ln"), // Создать ссылку на файл
                new("chmod"), // Изменение прав
                new("chown"), // Смена владельца файла
                new("chgrp"), // Смена группы файла
                new("df"), // Инфа о диске - свободное место и тд
                new("du"), // Скок места занимает df -d - глубина рекурсии
                new("clear"),
                new("useradd"),
                new("userdel"),
                new("usermod"),
                new("groupadd"),
                new("groupdel"),
                new("changegroup")
            };
        }

        public void Work()
        {

        }
    }
}
