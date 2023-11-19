using Microsoft.Data.Sqlite;
using System.Drawing;
using System.Text;

internal class Program
{
    private static string ConnectionString = "Data Source=FAV.sqlite;";

    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        using var connection = new SqliteConnection(ConnectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Conection is successful\n");
            CreateTable(connection);

            //CreateOBJ(connection,"banan","fruit","yellow","120");
            //CreateOBJ(connection, "carrot", "vegetable", "orange", "70");
            Console.WriteLine("Відображення всієї інформації з таблиці овочів і фруктів.");
            SelAll(connection);
            Console.WriteLine();
            Console.WriteLine("Відображення усіх назв овочів і фруктів.");
            SelNames(connection);
            Console.WriteLine();
            Console.WriteLine(" Відображення усіх кольорів.");
            SelColors(connection);
            Console.WriteLine();
            Console.WriteLine(" Показати максимальну калорійність.");
            SelMax(connection);
            Console.WriteLine();
            Console.WriteLine("Показати мінімальну калорійність.");
            SelMin(connection);
            Console.WriteLine();
            Console.WriteLine("Показати середню калорійність.");
            SelAvg(connection);
            Console.WriteLine();
            Console.WriteLine("Показати кількість овочів.");
            SelFr(connection);
            Console.WriteLine();
            Console.WriteLine("Показати кількість фруктів.");
            SelVg(connection);
            Console.WriteLine();
            Console.WriteLine("Показати кількість овочів і фруктів заданого кольору.(red)");
            SelByColor(connection,"red");
            Console.WriteLine();
            Console.WriteLine("Показати кількість овочів і фруктів кожного кольору.");
            SelAllCol(connection);
            Console.WriteLine();
            Console.WriteLine("Показати овочі та фрукти з калорійністю нижче вказаної.");
            SelCalBelow(connection, 80);
            Console.WriteLine();
            Console.WriteLine("Показати овочі та фрукти з калорійністю вище вказаної.");
            SelCalAbove(connection, 90);
            Console.WriteLine();
            Console.WriteLine("Показати овочі та фрукти з калорійністю у вказаному діапазоні.\r");
            SelCalBetween(connection, 70, 100);
            Console.WriteLine();
            Console.WriteLine("Показати усі овочі та фрукти жовтого або червоного кольору.");
            SelRedOrYellow(connection);


            Console.WriteLine("\nPress \"e\" and \"enter\" to exit DB");
            while (true)
            {
                if (Console.ReadLine() == "e")
                {
                    connection.Close();
                    Console.WriteLine("\nConection is closed");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Conection is unsuccessful, error message - ${ex.Message}");
        }

        return;
    }

    private static void CreateTable(SqliteConnection connection)
    {
        string sql = "create table if not exists FruitsAndVegetables (name TEXT,type TEXT,color TEXT,calory INTEGER)";
        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private static void CreateOBJ(SqliteConnection connection, string name,string type,string color,string calory)
    {
        using var transaction = connection.BeginTransaction();
        var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO FruitsAndVegetables (name, type, color, calory) VALUES ($name, $type, $color, $calory)";
        insertCommand.Parameters.AddWithValue("$name", name);
        insertCommand.Parameters.AddWithValue("$type", type);
        insertCommand.Parameters.AddWithValue("$color", color);
        insertCommand.Parameters.AddWithValue("$calory", calory);
        insertCommand.ExecuteNonQuery();
        transaction.Commit(); // Commit the transaction after all users are inserted
    }
    private static void SelAll(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }

        transaction.Commit();
    }

    private static void SelNames(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]}");
        }

        transaction.Commit();
    }
    private static void SelColors(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["color"]}");
        }

        transaction.Commit();
    }

    private static void SelMax(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select max(calory) from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["max(calory)"]}");
        }

        transaction.Commit();
    }

    private static void SelMin(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select min(calory) from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["min(calory)"]}");
        }

        transaction.Commit();
    }
    private static void SelAvg(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select avg(calory) from FruitsAndVegetables";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["avg(calory)"]}");
        }

        transaction.Commit();
    }
    private static void SelFr(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select count(name)\r\nfrom FruitsAndVegetables as al\r\nwhere al.type = \"fruit\";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["count(name)"]}");
        }

        transaction.Commit();
    }
    private static void SelVg(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select count(name)\r\nfrom FruitsAndVegetables as al\r\nwhere al.type = \"vegetable\";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["count(name)"]}");
        }

        transaction.Commit();
    }
    private static void SelByColor(SqliteConnection connection,string color)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select count(name) from FruitsAndVegetables as al where al.color = $col ;";
        Command.Parameters.AddWithValue("$col", color);
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["count(name)"]}");
        }

        transaction.Commit();
    }

    private static void SelAllCol(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        List<string> list = new List<string>();
        list.Add("red");
        list.Add("green");
        list.Add("orange");
        list.Add("yellow");
        foreach (string s in list)
        {
            var Command = connection.CreateCommand();
            Command.CommandText = "select $col,count(name) from FruitsAndVegetables as al where al.color = $col ;";
            Command.Parameters.AddWithValue("$col", s);
            Command.ExecuteNonQuery();
            using var reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["$col"]} {reader["count(name)"]}");
            }
        }

        transaction.Commit();
    }
    private static void SelCalBelow(SqliteConnection connection,int bel)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select *\r\nfrom FruitsAndVegetables as al\r\nwhere al.calory < \"" + bel +"\";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }

        transaction.Commit();
    }
    
    private static void SelCalAbove(SqliteConnection connection, int ab)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select *\r\nfrom FruitsAndVegetables as al\r\nwhere al.calory > \"" + ab + "\";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }

        transaction.Commit();
    }
    private static void SelCalBetween(SqliteConnection connection, int bel,int ab)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables as al where al.calory > " + bel + " AND al.calory < " + ab + ";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }

        transaction.Commit();
    }
    private static void SelRedOrYellow(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables as al where al.color = \"yellow\" or al.color = \"red\";";
        Command.ExecuteNonQuery();
        using var reader = Command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }

        transaction.Commit();
    }



}