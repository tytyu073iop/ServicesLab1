using Microsoft.Data.Sqlite;

public class DBManager
{
    public SqliteConnection connection;

    public List<Roster> GetAllRosters()
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT playerid, jersey, fname, sname, position, birthday, weight, height, birthcity, birthstate FROM roster; ";

        using var reader = command.ExecuteReader();

        return ReaderToRosters(reader);
        }

        private static void CheckRosterWithThrowing(Roster player)
        {

            // Rule 2: If position is Defender and height > 185, weight must be >= 80
            if (player.Position == "RW" && player.Height > 185 && player.Weight < 80)
            {
                throw new Exception($"Rule 2 violated: {player.Fname} {player.Sname} is underweight for a tall defender.");
            }

            // Rule 4: If name starts with 'А', position is Forward, height < 175, weight must be < 70
            if (player.Fname.StartsWith("А") && player.Position == "LW" && player.Height < 175 && player.Weight >= 70)
            {
                throw new Exception($"Rule 4 violated: {player.Fname} {player.Sname} is too heavy for a short forward.");
            }
        }

        public void AddRoster(Roster roster)
        {
            CheckRosterWithThrowing(roster);

            using var command = connection.CreateCommand();
            command.CommandText = """
    INSERT INTO roster (
        playerid, jersey, fname, sname, position, birthday,
        Weight, Height, Birthcity, Birthstate
    ) VALUES (
        @Playerid, @Jersey, @Fname, @Sname, @Postition, @Birthday,
        @Weight, @Height, @Birthcity, @Birthstate
    )
""";

            command.Parameters.AddWithValue("@Playerid", roster.Playerid);
            command.Parameters.AddWithValue("@Jersey", roster.Jersey);
            command.Parameters.AddWithValue("@Fname", roster.Fname);
            command.Parameters.AddWithValue("@Sname", roster.Sname);
            command.Parameters.AddWithValue("@Postition", roster.Position);
            command.Parameters.AddWithValue("@Birthday", roster.Birthday);
            command.Parameters.AddWithValue("@Weight", roster.Weight);
            command.Parameters.AddWithValue("@Height", roster.Height);
            command.Parameters.AddWithValue("@Birthcity", roster.Birthcity);
            command.Parameters.AddWithValue("@Birthstate", roster.Birthstate);

            command.ExecuteNonQuery();


        }

        public DBManager(String dataSource)
        {
        connection = new SqliteConnection($"Data Source={dataSource};Mode=ReadWrite");

        connection.Open();
        }

        ~DBManager()
        {
            connection.Close();
        }

        private static List<Roster> ReaderToRosters(SqliteDataReader sdr)
        {
            var arr = new List<Roster>();
            while (sdr.Read())
            {
                arr.Add(new Roster(
    Playerid: sdr[0].ToString(),
    Jersey: Convert.ToInt32(sdr[1]),
    Fname: sdr[2].ToString(),
    Sname: sdr[3].ToString(),
    Position: sdr[4].ToString(),
    Birthday: sdr[5].ToString(),
    Weight: Convert.ToInt32(sdr[6]),
    Height: Convert.ToInt32(sdr[7]),
    Birthcity: sdr[8].ToString(),
    Birthstate: sdr[9].ToString()
));
            }
            return arr;
        }
    }
