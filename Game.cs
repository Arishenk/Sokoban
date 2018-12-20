using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Sokoban
{
    public static class Game
    {
        public static int Scores;
        public static bool IsOver;
        public static Keys KeyPressed;
        public static ICreature[,] field;
        public static int CountScores;
        public static int Level = 1;
        public static int CountLevel = 3;
        public static int fieldWidth => field.GetLength(0);
        public static int fieldHeight => field.GetLength(1);

        private static void GetLevelAndScores(string login)
        {
            string databaseName = @"../../sokoban.db";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'sokoban';", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    string Login = record["login"].ToString();
                    if (Login.Equals(login))
                    {
                        Level = int.Parse(reader["level"].ToString());
                    }
                }
            }
            connection.Close();
        }

        public static void CreateMap(string login)
        {
            GetLevelAndScores(login);
            string map = System.IO.File.ReadAllText("../../maps/" + Level + ".txt");
            switch (Level)
            {
                case 1:
                    CountScores = 2;
                    field = CreatureMapCreator.CreateMap(map);
                    break;
                case 2:
                    CountScores = 2;
                    field = CreatureMapCreator.CreateMap(map);
                    break;
                case 3:
                    CountScores = 3;
                    field = CreatureMapCreator.CreateMap(map);
                    break;
                default:
                    break;
            }
        }
    }
}
